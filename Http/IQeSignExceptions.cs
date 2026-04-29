namespace IQeSign.VeriFactu.Http;

/// <summary>
/// Excepción base para errores de la API IQ eSign.
/// </summary>
public abstract class IQeSignException : Exception
{
    /// <param name="message">Mensaje de error.</param>
    protected IQeSignException(string message) : base(message) { }

    /// <param name="message">Mensaje de error.</param>
    /// <param name="inner">Excepción interna.</param>
    protected IQeSignException(string message, Exception inner) : base(message, inner) { }
}

/// <summary>
/// Excepción lanzada cuando falla la autenticación con la API IQ eSign.
/// Compruebe que <c>CredentialGuid</c> es correcto y que la cuenta está activa en IQ Portal.
/// </summary>
public sealed class IQeSignAuthException : IQeSignException
{
    /// <param name="message">Mensaje de error.</param>
    public IQeSignAuthException(string message) : base(message) { }
}

/// <summary>
/// Excepción lanzada cuando la API IQ eSign devuelve un error HTTP no esperado (4xx/5xx).
/// </summary>
public sealed class IQeSignApiException : IQeSignException
{
    /// <summary>Código de estado HTTP devuelto por la API.</summary>
    public int HttpStatusCode { get; }

    /// <param name="message">Mensaje de error.</param>
    /// <param name="httpStatusCode">Código HTTP devuelto por la API.</param>
    public IQeSignApiException(string message, int httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}
