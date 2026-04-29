using IQeSign.VeriFactu.Http;
using IQeSign.VeriFactu.Models.Requests;
using IQeSign.VeriFactu.Models.Responses;

namespace IQeSign.VeriFactu.Services;

/// <inheritdoc cref="IVeriFactuService"/>
internal sealed class VeriFactuService : IVeriFactuService
{
    private readonly IQeSignHttpClient _client;

    public VeriFactuService(IQeSignHttpClient client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public Task<GetCodesResponse> GetCodesAsync(CancellationToken ct = default)
        => _client.GetAsync<GetCodesResponse>("/api/v2/VeriFactu/Codes", ct);

    /// <inheritdoc/>
    public Task<GetUsageResponse> GetUsageAsync(CancellationToken ct = default)
        => _client.GetAsync<GetUsageResponse>("/api/v2/VeriFactu/Usage", ct);

    /// <inheritdoc/>
    public Task<AddDocumentResponse> AddDocumentAsync(AddDocumentRequest request, CancellationToken ct = default)
        => _client.PostAsync<AddDocumentResponse>("/api/v2/VeriFactu/Document", request, ct);

    /// <inheritdoc/>
    public Task<GetDocumentResponse> GetDocumentByIdAsync(string id, CancellationToken ct = default)
        => _client.GetAsync<GetDocumentResponse>($"/api/v2/VeriFactu/Document/{Uri.EscapeDataString(id)}", ct);

    /// <inheritdoc/>
    public Task<UpdateDocumentResponse> UpdateDocumentAsync(string id, UpdateDocumentRequest request, CancellationToken ct = default)
        => _client.PutAsync<UpdateDocumentResponse>($"/api/v2/VeriFactu/Document/{Uri.EscapeDataString(id)}", request, ct);

    /// <inheritdoc/>
    public Task<CancelDocumentResponse> CancelDocumentAsync(string id, CancelDocumentRequest request, CancellationToken ct = default)
        => _client.PutAsync<CancelDocumentResponse>($"/api/v2/VeriFactu/Document/{Uri.EscapeDataString(id)}/Cancel", request, ct);

    /// <inheritdoc/>
    public Task<ListDocumentsResponse> ListDocumentsAsync(GetDocumentListRequest? request = null, CancellationToken ct = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["initDate"]    = request?.InitDate,
            ["finishDate"]  = request?.FinishDate
        };

        return _client.GetAsync<ListDocumentsResponse>("/api/v2/VeriFactu/Document/List", queryParams, ct);
    }

    /// <inheritdoc/>
    public Task<CheckDocumentResponse> CheckDocumentsAsync(CancellationToken ct = default)
        => _client.GetAsync<CheckDocumentResponse>("/api/v2/VeriFactu/Document/Check", ct);
}
