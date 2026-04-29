namespace IQeSign.VeriFactu.Models.Responses;

/// <summary>
/// Resultado de añadir un certificado. Contiene el identificador asignado.
/// </summary>
public sealed class CertificateIdResult
{
    /// <summary>Identificador único del certificado en IQ Portal.</summary>
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Información de un certificado (sin el archivo).
/// </summary>
public sealed class CertificateInfo
{
    /// <summary>Identificador único del certificado en IQ Portal.</summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>Nombre descriptivo del certificado.</summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Resultado de descarga de un certificado .pfx.
/// </summary>
public sealed class CertificateFileResult
{
    /// <summary>Contenido del archivo .pfx en formato Base64.</summary>
    public string File { get; set; } = string.Empty;
}

/// <summary>Respuesta del endpoint POST Certificate.</summary>
public sealed class AddCertificateResponse : ApiResponse<CertificateIdResult> { }

/// <summary>Respuesta del endpoint GET Certificate/{id}.</summary>
public sealed class GetCertificateResponse : ApiResponse<CertificateInfo> { }

/// <summary>Respuesta del endpoint GET Certificate/{id}/Download.</summary>
public sealed class DownloadCertificateResponse : ApiResponse<CertificateFileResult> { }

/// <summary>Respuesta del endpoint GET Certificate/List.</summary>
public sealed class ListCertificatesResponse : ApiResponse<List<CertificateInfo>> { }

/// <summary>Respuesta del endpoint DELETE Certificate/{id}.</summary>
public sealed class DeleteCertificateResponse : ApiResponse { }
