namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de tipo de identificación del destinatario (Lista L7 VeriFactu).
/// Usado en <c>VeriFactuDocumentFile.IdentificationType</c>.
/// </summary>
public static class IdentificationType
{
    /// <summary>NIF-IVA.</summary>
    public const string NifIva = "02";

    /// <summary>Pasaporte.</summary>
    public const string Pasaporte = "03";

    /// <summary>Documento oficial de identificación expedido por el país o territorio de residencia.</summary>
    public const string DocumentoOficial = "04";

    /// <summary>Certificado de residencia.</summary>
    public const string CertificadoResidencia = "05";

    /// <summary>Otro documento probatorio.</summary>
    public const string OtroDocumento = "06";

    /// <summary>No censado.</summary>
    public const string NoCensado = "07";
}
