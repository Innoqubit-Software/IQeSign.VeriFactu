namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de causa de exención de IVA (Lista L10 VeriFactu).
/// Usado en <c>VatDetailItem.Type</c> para operaciones exentas.
/// </summary>
public static class VatExemptionType
{
    /// <summary>Exenta por el artículo 20.</summary>
    public const string Articulo20 = "E1";

    /// <summary>Exenta por el artículo 21.</summary>
    public const string Articulo21 = "E2";

    /// <summary>Exenta por el artículo 22.</summary>
    public const string Articulo22 = "E3";

    /// <summary>Exenta por los artículos 23 y 24.</summary>
    public const string Articulos23Y24 = "E4";

    /// <summary>Exenta por el artículo 25.</summary>
    public const string Articulo25 = "E5";

    /// <summary>Exenta por otros artículos.</summary>
    public const string Otros = "E6";
}
