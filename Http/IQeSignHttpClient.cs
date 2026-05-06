using IQeSign.VeriFactu.Configuration;
using IQeSign.VeriFactu.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace IQeSign.VeriFactu.Http;

/// <summary>
/// Cliente HTTP interno que encapsula la comunicación con la API IQ eSign.
/// Gestiona la autenticación JWT de forma automática, incluyendo el refresco del token.
/// </summary>
internal sealed class IQeSignHttpClient
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
	};

	private readonly IHttpClientFactory _httpClientFactory;
	private readonly IQeSignOptions _options;
	private readonly ILogger<IQeSignHttpClient> _logger;

	// Token cache con control de concurrencia
	private string? _cachedToken;
	private DateTime _tokenExpiresAt = DateTime.MinValue;
	private readonly SemaphoreSlim _tokenSemaphore = new(1, 1);

	public IQeSignHttpClient(
		IHttpClientFactory httpClientFactory,
		IOptions<IQeSignOptions> options,
		ILogger<IQeSignHttpClient> logger)
	{
		_httpClientFactory = httpClientFactory;
		_options = options.Value;
		_logger = logger;
	}

	// -------------------------------------------------------------------------
	// Métodos públicos de petición HTTP
	// -------------------------------------------------------------------------

	/// <summary>Realiza una petición GET autenticada.</summary>
	public Task<TResponse> GetAsync<TResponse>(string relativeUrl, CancellationToken ct = default)
		where TResponse : ApiResponse
		=> SendAsync<TResponse>(HttpMethod.Get, relativeUrl, body: null, ct);

	/// <summary>Realiza una petición GET autenticada con parámetros de query string.</summary>
	public Task<TResponse> GetAsync<TResponse>(string relativeUrl, IDictionary<string, string?> queryParams, CancellationToken ct = default)
		where TResponse : ApiResponse
	{
		var url = BuildUrl(relativeUrl, queryParams);
		return SendAsync<TResponse>(HttpMethod.Get, url, body: null, ct);
	}

	/// <summary>Realiza una petición POST autenticada con cuerpo JSON.</summary>
	public Task<TResponse> PostAsync<TResponse>(string relativeUrl, object body, CancellationToken ct = default)
		where TResponse : ApiResponse
		=> SendAsync<TResponse>(HttpMethod.Post, relativeUrl, body, ct);

	/// <summary>Realiza una petición PUT autenticada con cuerpo JSON.</summary>
	public Task<TResponse> PutAsync<TResponse>(string relativeUrl, object body, CancellationToken ct = default)
		where TResponse : ApiResponse
		=> SendAsync<TResponse>(HttpMethod.Put, relativeUrl, body, ct);

	/// <summary>Realiza una petición DELETE autenticada.</summary>
	public Task<TResponse> DeleteAsync<TResponse>(string relativeUrl, CancellationToken ct = default)
		where TResponse : ApiResponse
		=> SendAsync<TResponse>(HttpMethod.Delete, relativeUrl, body: null, ct);

	// -------------------------------------------------------------------------
	// Implementación interna
	// -------------------------------------------------------------------------

	private async Task<TResponse> SendAsync<TResponse>(
		HttpMethod method,
		string relativeUrl,
		object? body,
		CancellationToken ct)
		where TResponse : ApiResponse
	{
		var token = await GetOrRefreshTokenAsync(ct).ConfigureAwait(false);
		var client = CreateHttpClient();

		using var request = BuildRequest(method, relativeUrl, body, token);

		_logger.LogDebug("IQeSign API → {Method} {Url}", method, relativeUrl);

		HttpResponseMessage response;
		try
		{
			response = await client.SendAsync(request, ct).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error de comunicación con la API IQ eSign ({Method} {Url})", method, relativeUrl);
			throw;
		}

		return await DeserializeResponseAsync<TResponse>(response, method, relativeUrl, ct).ConfigureAwait(false);
	}

	private async Task<string> GetOrRefreshTokenAsync(CancellationToken ct)
	{
		// Fast path: token vigente
		if (_cachedToken is not null && DateTime.UtcNow < _tokenExpiresAt)
			return _cachedToken;

		await _tokenSemaphore.WaitAsync(ct).ConfigureAwait(false);
		try
		{
			// Double-check tras adquirir el semáforo
			if (_cachedToken is not null && DateTime.UtcNow < _tokenExpiresAt)
				return _cachedToken;

			_cachedToken = await FetchTokenAsync(ct).ConfigureAwait(false);
			_tokenExpiresAt = DateTime.UtcNow.AddHours(23); // Margen de 1h antes de los 24h reales
			return _cachedToken;
		}
		finally
		{
			_tokenSemaphore.Release();
		}
	}

	private async Task<string> FetchTokenAsync(CancellationToken ct)
	{
		_logger.LogDebug("Solicitando token JWT a la API IQ eSign...");

		var client = CreateHttpClient();
		var loginBody = new { CredentialGuid = _options.CredentialGuid };

		using var loginRequest = BuildRequest(HttpMethod.Post, "/api/v2/login", loginBody, bearerToken: null);
		var loginResponse = await client.SendAsync(loginRequest, ct).ConfigureAwait(false);

		var loginResult = await DeserializeResponseAsync<LoginResponse>(
			loginResponse, HttpMethod.Post, "/api/v2/login", ct).ConfigureAwait(false);

		if (!loginResult.IsSuccess || loginResult.Result?.Token is null)
			throw new IQeSignAuthException(
				$"Error al autenticar en IQ eSign API. Código: {loginResult.ErrorCode}. Mensaje: {loginResult.ErrorMessage}");

		_logger.LogDebug("Token JWT obtenido correctamente.");
		return loginResult.Result.Token;
	}

	private static HttpRequestMessage BuildRequest(HttpMethod method, string relativeUrl, object? body, string? bearerToken)
	{
		var request = new HttpRequestMessage(method, relativeUrl);

		if (bearerToken is not null)
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

		if (body is not null)
		{
			var json = JsonSerializer.Serialize(body, JsonOptions);
			request.Content = new StringContent(json, Encoding.UTF8, "application/json");
		}

		return request;
	}

	private static async Task<TResponse> DeserializeResponseAsync<TResponse>(
		HttpResponseMessage response,
		HttpMethod method,
		string url,
		CancellationToken ct)
		where TResponse : ApiResponse
	{
		// ReadAsStringAsync(CancellationToken) no está disponible en netstandard2.1
		var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

		if (!response.IsSuccessStatusCode)
		{
			throw new IQeSignApiException(
				$"La API IQ eSign devolvió HTTP {(int)response.StatusCode} en {method} {url}. Respuesta: {content}",
				(int)response.StatusCode);
		}

		var result = JsonSerializer.Deserialize<TResponse>(content, JsonOptions);
		return result ?? throw new IQeSignApiException(
			$"La respuesta de la API IQ eSign no pudo deserializarse ({method} {url}).", (int)response.StatusCode);
	}

	private HttpClient CreateHttpClient()
	{
		var client = _httpClientFactory.CreateClient(_options.GetHttpClientName());
		return client;
	}

	private static string BuildUrl(string baseUrl, IDictionary<string, string?> queryParams)
	{
		var nonNull = queryParams.Where(kv => kv.Value is not null).ToList();
		if (nonNull.Count == 0) return baseUrl;

		var query = string.Join("&", nonNull.Select(kv =>
			$"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));

		return $"{baseUrl}?{query}";
	}
}
