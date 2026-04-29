using IQeSign.VeriFactu.Http;
using IQeSign.VeriFactu.Models.Requests;
using IQeSign.VeriFactu.Models.Responses;

namespace IQeSign.VeriFactu.Services;

/// <inheritdoc cref="ICertificateService"/>
internal sealed class CertificateService : ICertificateService
{
    private readonly IQeSignHttpClient _client;

    public CertificateService(IQeSignHttpClient client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public Task<AddCertificateResponse> AddAsync(AddCertificateRequest request, CancellationToken ct = default)
        => _client.PostAsync<AddCertificateResponse>("/api/v2/Certificate", request, ct);

    /// <inheritdoc/>
    public Task<DeleteCertificateResponse> DeleteAsync(string id, CancellationToken ct = default)
        => _client.DeleteAsync<DeleteCertificateResponse>($"/api/v2/Certificate/{Uri.EscapeDataString(id)}", ct);

    /// <inheritdoc/>
    public Task<GetCertificateResponse> GetByIdAsync(string id, CancellationToken ct = default)
        => _client.GetAsync<GetCertificateResponse>($"/api/v2/Certificate/{Uri.EscapeDataString(id)}", ct);

    /// <inheritdoc/>
    public Task<DownloadCertificateResponse> DownloadAsync(string id, CancellationToken ct = default)
        => _client.GetAsync<DownloadCertificateResponse>($"/api/v2/Certificate/{Uri.EscapeDataString(id)}/Download", ct);

    /// <inheritdoc/>
    public Task<ListCertificatesResponse> ListAsync(CancellationToken ct = default)
        => _client.GetAsync<ListCertificatesResponse>("/api/v2/Certificate/List", ct);
}
