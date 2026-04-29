namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de tipo de impuesto (Lista L1 VeriFactu).
/// Usado en <c>VatDetail.Vat</c>.
/// </summary>
public static class VatType
{
    /// <summary>Impuesto sobre el Valor Añadido (IVA).</summary>
    public const string Iva = "01";

    /// <summary>Impuesto sobre la Producción, los Servicios y la Importación (IPSI) de Ceuta y Melilla.</summary>
    public const string Ipsi = "02";

    /// <summary>Impuesto General Indirecto Canario (IGIC).</summary>
    public const string Igic = "03";

    /// <summary>Otros impuestos.</summary>
    public const string Otros = "05";
}
