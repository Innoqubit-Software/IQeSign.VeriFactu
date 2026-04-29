namespace IQeSign.VeriFactu.Models.Requests;

/// <summary>
/// Solicitud para añadir y enviar un nuevo documento VeriFactu a la plataforma.
/// La API generará el XML correspondiente y lo firmará con el certificado indicado.
/// Endpoint: POST /api/v2/VeriFactu/Document
/// </summary>
public sealed class AddDocumentRequest
{
    /// <summary>
    /// Datos de la factura a enviar a la plataforma VeriFactu.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public VeriFactuDocumentFile File { get; set; } = new();

    /// <summary>
    /// Identificador del certificado (.pfx) subido previamente a IQ Portal.
    /// Se usa para firmar la petición enviada a VeriFactu.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string CertificateId { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del certificado .pfx.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string CertificatePass { get; set; } = string.Empty;

    /// <summary>
    /// Metadatos estadísticos opcionales de la plataforma o integración origen.
    /// <para>Opcional.</para>
    /// </summary>
    public DocumentMetadata? Metadata { get; set; }
}
