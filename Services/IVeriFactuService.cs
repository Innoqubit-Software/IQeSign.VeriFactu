using IQeSign.VeriFactu.Models.Requests;
using IQeSign.VeriFactu.Models.Responses;

namespace IQeSign.VeriFactu.Services;

/// <summary>
/// Servicio para gestionar documentos VeriFactu (facturas electrónicas) a través de la API IQ eSign.
/// </summary>
public interface IVeriFactuService
{
    /// <summary>
    /// Obtiene los códigos de referencia VeriFactu (listas L1, L2, L3, L7, L8A, L8B, L9, L10, L15).
    /// Útil para poblar listas desplegables o validar valores antes de enviar documentos.
    /// </summary>
    /// <param name="ct">Token de cancelación.</param>
    Task<GetCodesResponse> GetCodesAsync(CancellationToken ct = default);

    /// <summary>
    /// Obtiene el resumen de uso de la solución IQ eSign VeriFactu:
    /// número de certificados, documentos procesados, plan contratado y uso por mes.
    /// </summary>
    /// <param name="ct">Token de cancelación.</param>
    Task<GetUsageResponse> GetUsageAsync(CancellationToken ct = default);

    /// <summary>
    /// Envía un nuevo documento (factura) a la plataforma VeriFactu.
    /// La API genera el XML, lo firma con el certificado indicado y lo presenta a VeriFactu.
    /// </summary>
    /// <param name="request">Datos de la factura y del certificado de firma.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Respuesta con el identificador del documento y el control de flujo.</returns>
    Task<AddDocumentResponse> AddDocumentAsync(AddDocumentRequest request, CancellationToken ct = default);

    /// <summary>
    /// Obtiene el estado y las versiones enviadas de un documento VeriFactu por su identificador.
    /// </summary>
    /// <param name="id">Identificador del documento en IQ Portal.</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<GetDocumentResponse> GetDocumentByIdAsync(string id, CancellationToken ct = default);

    /// <summary>
    /// Actualiza y reenvía un documento VeriFactu existente.
    /// La API genera el XML actualizado, lo firma y lo presenta a VeriFactu.
    /// </summary>
    /// <param name="id">Identificador del documento a actualizar.</param>
    /// <param name="request">Datos actualizados de la factura y del certificado de firma.</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<UpdateDocumentResponse> UpdateDocumentAsync(string id, UpdateDocumentRequest request, CancellationToken ct = default);

    /// <summary>
    /// Cancela un documento VeriFactu en la plataforma.
    /// La API genera el XML de cancelación, lo firma y lo presenta a VeriFactu.
    /// </summary>
    /// <param name="id">Identificador del documento a cancelar.</param>
    /// <param name="request">Identificador y contraseña del certificado de firma.</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<CancelDocumentResponse> CancelDocumentAsync(string id, CancelDocumentRequest request, CancellationToken ct = default);

    /// <summary>
    /// Obtiene el listado de documentos VeriFactu, con filtro opcional por rango de fechas.
    /// </summary>
    /// <param name="request">Filtros opcionales de fecha (initDate, finishDate en formato yyyy-MM-dd).</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<ListDocumentsResponse> ListDocumentsAsync(GetDocumentListRequest? request = null, CancellationToken ct = default);

    /// <summary>
    /// Comprueba y procesa los documentos pendientes de presentación automática
    /// gestionados por el sistema de control de flujo (FlowControl) de VeriFactu.
    /// </summary>
    /// <param name="ct">Token de cancelación.</param>
    Task<CheckDocumentResponse> CheckDocumentsAsync(CancellationToken ct = default);
}
