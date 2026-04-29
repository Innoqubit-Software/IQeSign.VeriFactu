namespace IQeSign.VeriFactu.Models.Requests;

/// <summary>
/// Solicitud para actualizar y reenviar un documento VeriFactu existente.
/// La API generará el XML actualizado y lo firmará con el certificado indicado.
/// Endpoint: PUT /api/v2/VeriFactu/Document/{id}
/// </summary>
public sealed class UpdateDocumentRequest
{
    /// <summary>
    /// Datos actualizados de la factura a enviar a la plataforma VeriFactu.
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
