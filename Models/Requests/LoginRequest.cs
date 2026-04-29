namespace IQeSign.VeriFactu.Models.Requests;

/// <summary>
/// Solicitud de autenticación en la API IQ eSign.
/// Devuelve un token JWT con validez de 24 horas.
/// </summary>
public sealed class LoginRequest
{
    /// <summary>
    /// Identificador de acceso a IQ Portal.
    /// Se obtiene desde el panel de administración de IQ Portal.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string CredentialGuid { get; set; } = string.Empty;
}
