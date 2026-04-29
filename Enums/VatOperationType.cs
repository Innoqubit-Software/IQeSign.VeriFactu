namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de tipo de operación IVA (Lista L9 VeriFactu).
/// Usado en <c>VatDetailItem.Type</c> para operaciones sujetas y no sujetas.
/// </summary>
public static class VatOperationType
{
    /// <summary>Operación Sujeta y No exenta - Sin inversión del sujeto pasivo.</summary>
    public const string SujetaNoExentaSinInversion = "S1";

    /// <summary>Operación Sujeta y No exenta - Con Inversión del sujeto pasivo.</summary>
    public const string SujetaNoExentaConInversion = "S2";

    /// <summary>Operación No Sujeta artículo 7, 14, otros.</summary>
    public const string NoSujetaArticulo7 = "N1";

    /// <summary>Operación No Sujeta por Reglas de localización.</summary>
    public const string NoSujetaLocalizacion = "N2";
}
