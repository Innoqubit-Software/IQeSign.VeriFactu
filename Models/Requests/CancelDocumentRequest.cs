namespace IQeSign.VeriFactu.Models.Requests;

/// <summary>
/// Solicitud para cancelar un documento VeriFactu en la plataforma.
/// La API firmará la cancelación con el certificado indicado.
/// Endpoint: PUT /api/v2/VeriFactu/Document/{id}/Cancel
/// </summary>
public sealed class CancelDocumentRequest
{
    /// <summary>
    /// Identificador del certificado (.pfx) subido previamente a IQ Portal.
    /// Se usa para firmar la petición de cancelación enviada a VeriFactu.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string CertificateId { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del certificado .pfx.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string CertificatePass { get; set; } = string.Empty;
}
