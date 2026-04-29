namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de tipo de factura (Lista L2 VeriFactu).
/// Usado en <c>VeriFactuDocumentFile.Type</c>.
/// </summary>
public static class InvoiceType
{
    /// <summary>Factura (art. 6, 7.2 y 7.3 del RD 1619/2012).</summary>
    public const string Factura = "F1";

    /// <summary>Factura Simplificada y Facturas sin identificación del destinatario art. 6.1.d) RD 1619/2012.</summary>
    public const string FacturaSimplificada = "F2";

    /// <summary>Factura emitida en sustitución de facturas simplificadas facturadas y declaradas.</summary>
    public const string FacturaSustitucion = "F3";

    /// <summary>Factura Rectificativa (Error fundado en derecho y Art. 80 Uno Dos y Seis LIVA).</summary>
    public const string RectificativaError = "R1";

    /// <summary>Factura Rectificativa (Art. 80.3).</summary>
    public const string Rectificativa80_3 = "R2";

    /// <summary>Factura Rectificativa (Art. 80.4).</summary>
    public const string Rectificativa80_4 = "R3";

    /// <summary>Factura Rectificativa (Resto).</summary>
    public const string RectificativaResto = "R4";

    /// <summary>Factura Rectificativa en facturas simplificadas.</summary>
    public const string RectificativaSimplificada = "R5";
}
