using IQeSign.VeriFactu.Models.Requests;
using IQeSign.VeriFactu.Models.Responses;

namespace IQeSign.VeriFactu.Services;

/// <summary>
/// Servicio para gestionar certificados digitales (.pfx) en IQ Portal.
/// Los certificados son compartidos entre las soluciones IQ eSign Facturae, ePDF, TicketBAI y VeriFactu.
/// </summary>
public interface ICertificateService
{
    /// <summary>
    /// Añade un certificado .pfx a IQ Portal.
    /// El identificador devuelto se usará al enviar documentos VeriFactu.
    /// </summary>
    /// <param name="request">Datos del certificado en Base64 y nombre descriptivo.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <returns>Respuesta con el identificador asignado al certificado.</returns>
    Task<AddCertificateResponse> AddAsync(AddCertificateRequest request, CancellationToken ct = default);

    /// <summary>
    /// Elimina un certificado de IQ Portal por su identificador.
    /// </summary>
    /// <param name="id">Identificador del certificado a eliminar.</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<DeleteCertificateResponse> DeleteAsync(string id, CancellationToken ct = default);

    /// <summary>
    /// Obtiene la información de un certificado por su identificador.
    /// </summary>
    /// <param name="id">Identificador del certificado.</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<GetCertificateResponse> GetByIdAsync(string id, CancellationToken ct = default);

    /// <summary>
    /// Descarga el contenido del certificado (.pfx en Base64) por su identificador.
    /// </summary>
    /// <param name="id">Identificador del certificado.</param>
    /// <param name="ct">Token de cancelación.</param>
    Task<DownloadCertificateResponse> DownloadAsync(string id, CancellationToken ct = default);

    /// <summary>
    /// Obtiene el listado completo de certificados almacenados en IQ Portal.
    /// </summary>
    /// <param name="ct">Token de cancelación.</param>
    Task<ListCertificatesResponse> ListAsync(CancellationToken ct = default);
}
