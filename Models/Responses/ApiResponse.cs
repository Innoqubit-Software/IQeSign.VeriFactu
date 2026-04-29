namespace IQeSign.VeriFactu.Models.Responses;

/// <summary>
/// Respuesta base de la API IQ eSign. Todas las respuestas incluyen estos campos.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Código de error de la operación.
    /// <list type="bullet">
    ///   <item><description>"0" — Sin errores.</description></item>
    ///   <item><description>"1" — Error referido a la solución o cliente (ej. cliente sin solución contratada).</description></item>
    ///   <item><description>"2" — Error referido a la gestión de certificados.</description></item>
    ///   <item><description>"3" — Error referido a documentos o firma (ej. excedido el número de documentos).</description></item>
    ///   <item><description>"9" — Error no controlado con información detallada en <see cref="ErrorMessage"/>.</description></item>
    ///   <item><description>"17" — Error referido a acciones realizadas en la API de VeriFactu.</description></item>
    /// </list>
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del error. Vacío cuando <see cref="ErrorCode"/> es "0".
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Indica si la operación fue exitosa (<see cref="ErrorCode"/> == "0").
    /// </summary>
    public bool IsSuccess => ErrorCode == "0";
}

/// <summary>
/// Respuesta de la API con resultado tipado.
/// </summary>
/// <typeparam name="TResult">Tipo del objeto resultado.</typeparam>
public class ApiResponse<TResult> : ApiResponse
{
    /// <summary>
    /// Resultado de la operación. Puede ser <c>null</c> si la operación falló.
    /// </summary>
    public TResult? Result { get; set; }
}
