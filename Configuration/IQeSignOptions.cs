using System.ComponentModel.DataAnnotations;

namespace IQeSign.VeriFactu.Configuration;

/// <summary>
/// Opciones de configuración para el cliente IQ eSign VeriFactu.
/// </summary>
public sealed class IQeSignOptions
{
    /// <summary>
    /// Nombre de la sección de configuración en appsettings.json.
    /// </summary>
    public const string SectionName = "IQeSign";

    /// <summary>
    /// Identificador de acceso a IQ Portal (CredentialGuid).
    /// Se obtiene desde el portal en https://iqesignapi.azurewebsites.net.
    /// Requerido.
    /// </summary>
    [Required]
    public string CredentialGuid { get; set; } = string.Empty;

    /// <summary>
    /// Entorno de ejecución de la API.
    /// Por defecto: <see cref="IQeSignEnvironment.Production"/>.
    /// </summary>
    public IQeSignEnvironment Environment { get; set; } = IQeSignEnvironment.Production;

    /// <summary>
    /// Tiempo de espera (timeout) para las peticiones HTTP, en segundos.
    /// Por defecto: 30 segundos.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Devuelve la URL base correspondiente al entorno configurado.
    /// </summary>
    internal string GetBaseUrl() => Environment switch
    {
        IQeSignEnvironment.Production => "https://iqesignapi.azurewebsites.net",
        IQeSignEnvironment.Staging    => "https://iqesignapistaging.azurewebsites.net",
        _ => throw new InvalidOperationException($"Entorno desconocido: {Environment}")
    };

    /// <summary>
    /// Nombre del cliente HTTP registrado con IHttpClientFactory.
    /// </summary>
    internal string GetHttpClientName() => Environment switch
    {
        IQeSignEnvironment.Production => HttpClientNames.Production,
        IQeSignEnvironment.Staging    => HttpClientNames.Staging,
        _ => throw new InvalidOperationException($"Entorno desconocido: {Environment}")
    };
}

/// <summary>
/// Nombres de los clientes HTTP registrados.
/// </summary>
internal static class HttpClientNames
{
    internal const string Production = "IQeSign.Production";
    internal const string Staging    = "IQeSign.Staging";
}
