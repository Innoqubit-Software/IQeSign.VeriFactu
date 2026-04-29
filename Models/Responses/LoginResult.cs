namespace IQeSign.VeriFactu.Models.Responses;

/// <summary>
/// Resultado de la autenticación en la API IQ eSign.
/// </summary>
public sealed class LoginResult
{
    /// <summary>
    /// Token JWT para usar en el header de autenticación (Bearer).
    /// Tiene una validez de 24 horas.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}

/// <summary>Respuesta completa del endpoint de login.</summary>
public sealed class LoginResponse : ApiResponse<LoginResult> { }
