namespace IQeSign.VeriFactu.Models.Responses;

/// <summary>
/// Código VeriFactu de una lista de referencia (L1, L2, L3, L7, L8A, L8B, L9, L10, L15, etc.).
/// </summary>
public sealed class VeriFactuCodeItem
{
    /// <summary>Tipo/nombre de la lista de referencia (ej. "L1", "L2", "L7").</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>Código de la lista (ej. "01", "F1", "S").</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Descripción legible del código.</summary>
    public string Description { get; set; } = string.Empty;
}

/// <summary>Respuesta del endpoint GET VeriFactu/Codes.</summary>
public sealed class GetCodesResponse : ApiResponse<List<VeriFactuCodeItem>> { }
