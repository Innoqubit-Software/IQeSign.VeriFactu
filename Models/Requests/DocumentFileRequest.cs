using IQeSign.VeriFactu.Enums;

namespace IQeSign.VeriFactu.Models.Requests;

/// <summary>
/// Datos del emisor de la factura.
/// </summary>
public sealed class IssuerInfo
{
    /// <summary>
    /// Nombre o razón social del emisor.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// CIF/NIF del emisor.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string CifNif { get; set; } = string.Empty;
}

/// <summary>
/// Información de la factura rectificada.
/// Solo se debe rellenar cuando <c>Type</c> sea R1, R2, R3, R4 o R5.
/// Ver <see cref="InvoiceType"/>.
/// </summary>
public sealed class RectifiedInfo
{
    /// <summary>
    /// Tipo de rectificación.
    /// <para><b>Requerido si se incluye el nodo Rectified.</b></para>
    /// <para>Valores permitidos: <see cref="RectificationType.PorSustitucion"/> ("S"), <see cref="RectificationType.PorDiferencias"/> ("I").</para>
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// CIF/NIF de la factura rectificada.
    /// <para>Opcional.</para>
    /// </summary>
    public string? CifNif { get; set; }

    /// <summary>
    /// Serie de la factura rectificada.
    /// <para>Opcional.</para>
    /// </summary>
    public string? Serial { get; set; }

    /// <summary>
    /// Número de la factura rectificada.
    /// <para>Opcional.</para>
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Fecha de la factura rectificada en formato yyyy-MM-dd.
    /// <para>Opcional.</para>
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    /// Importe base de la rectificación.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? BaseAmount { get; set; }

    /// <summary>
    /// Importe del impuesto de la rectificación.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? VatAmount { get; set; }

    /// <summary>
    /// Importe del recargo de equivalencia de la rectificación.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? VatEcAmount { get; set; }

    /// <summary>
    /// Lista de facturas relacionadas con la rectificación.
    /// <para>Opcional.</para>
    /// </summary>
    public List<RectifiedInvoiceRef>? Invoices { get; set; }
}

/// <summary>
/// Referencia a una factura relacionada en una rectificación.
/// </summary>
public sealed class RectifiedInvoiceRef
{
    /// <summary>CIF/NIF de la factura referenciada. <para>Opcional.</para></summary>
    public string? CifNif { get; set; }

    /// <summary>Serie de la factura referenciada. <para>Opcional.</para></summary>
    public string? Serial { get; set; }

    /// <summary>Número de la factura referenciada. <para>Opcional.</para></summary>
    public string? Number { get; set; }

    /// <summary>Fecha de la factura referenciada en formato yyyy-MM-dd. <para>Opcional.</para></summary>
    public string? Date { get; set; }
}

/// <summary>
/// Detalle de IVA/impuesto de la factura.
/// </summary>
public sealed class VatDetailItem
{
    /// <summary>
    /// Tipo de impuesto aplicado.
    /// <para><b>Requerido.</b></para>
    /// <para>Valores permitidos: ver <see cref="VatType"/> (Lista L1): "01" (IVA), "02" (IPSI), "03" (IGIC), "05" (Otros).</para>
    /// </summary>
    public string Vat { get; set; } = string.Empty;

    /// <summary>
    /// Clave del régimen especial o trascendencia.
    /// <para><b>Requerido.</b></para>
    /// <para>Si <c>Vat</c> es distinto de "03" (IGIC): ver <see cref="VatKey"/> (Lista L8A).</para>
    /// <para>Si <c>Vat</c> es "03" (IGIC): ver <see cref="VatKeyIgic"/> (Lista L8B).</para>
    /// </summary>
    public string VatKey { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de operación (sujeta/no sujeta/exenta).
    /// <para><b>Requerido.</b></para>
    /// <para>Para operaciones sujetas y no sujetas: ver <see cref="VatOperationType"/> (Lista L9): "S1", "S2", "N1", "N2".</para>
    /// <para>Para operaciones exentas: ver <see cref="VatExemptionType"/> (Lista L10): "E1" a "E6".</para>
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Porcentaje de IVA/impuesto aplicado.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? VatPercent { get; set; }

    /// <summary>
    /// Importe de la cuota del IVA/impuesto.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? VatAmount { get; set; }

    /// <summary>
    /// Porcentaje de recargo de equivalencia.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? VatEcPercent { get; set; }

    /// <summary>
    /// Importe del recargo de equivalencia.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? VatEcAmount { get; set; }

    /// <summary>
    /// Importe de la base imponible.
    /// <para>Opcional.</para>
    /// </summary>
    public decimal? BaseAmount { get; set; }
}

/// <summary>
/// Información del tercero cuando la factura es emitida por un tercero.
/// Solo se debe rellenar cuando el emisor sea un tercero.
/// </summary>
public sealed class ThirdPartyInfo
{
    /// <summary>
    /// Nombre o razón social del tercero.
    /// <para><b>Requerido si se incluye el nodo ThirdParty.</b></para>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// CIF/NIF del tercero.
    /// <para><b>Requerido si se incluye el nodo ThirdParty.</b></para>
    /// </summary>
    public string CifNif { get; set; } = string.Empty;

    /// <summary>
    /// Código de país del tercero (ISO 3166-1 alfa-2, ej. "ES", "FR").
    /// <para>Opcional.</para>
    /// </summary>
    public string? Country { get; set; }
}

/// <summary>
/// Metadatos estadísticos opcionales para identificar la plataforma o usuario origen.
/// </summary>
public sealed class DocumentMetadata
{
    /// <summary>Versión de la plataforma origen. <para>Opcional.</para></summary>
    public string? Version { get; set; }

    /// <summary>Identificador del usuario origen. <para>Opcional.</para></summary>
    public string? User { get; set; }

    /// <summary>Email del usuario origen. <para>Opcional.</para></summary>
    public string? Email { get; set; }

    /// <summary>Nombre de la empresa origen. <para>Opcional.</para></summary>
    public string? Company { get; set; }

    /// <summary>Identificador del tenant origen. <para>Opcional.</para></summary>
    public string? Tenant { get; set; }

    /// <summary>Descripción libre. <para>Opcional.</para></summary>
    public string? Description { get; set; }

    /// <summary>Nombre de la plataforma origen (ej. "BusinessCentral", "Custom"). <para>Opcional.</para></summary>
    public string? Platform { get; set; }
}

/// <summary>
/// Estructura de datos de la factura VeriFactu.
/// Contiene todos los campos necesarios para generar y firmar el XML de la factura.
/// </summary>
public sealed class VeriFactuDocumentFile
{
    /// <summary>
    /// Datos del emisor de la factura.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public IssuerInfo Issuer { get; set; } = new();

    /// <summary>
    /// Versión del esquema VeriFactu.
    /// <para><b>Requerido.</b></para>
    /// <para>Valor permitido: <see cref="SchemaVersion.V1_0"/> ("1.0") (Lista L15).</para>
    /// </summary>
    public string Version { get; set; } = SchemaVersion.V1_0;

    /// <summary>
    /// CIF/NIF del destinatario/receptor de la factura.
    /// <para>Opcional para facturas simplificadas (F2, R5).</para>
    /// </summary>
    public string? CifNif { get; set; }

    /// <summary>
    /// Nombre o razón social del destinatario/receptor.
    /// <para>Opcional para facturas simplificadas (F2, R5).</para>
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Tipo de identificación del destinatario.
    /// <para>Opcional. Necesario cuando el destinatario no tiene NIF español.</para>
    /// <para>Valores permitidos: ver <see cref="IdentificationType"/> (Lista L7).</para>
    /// </summary>
    public string? IdentificationType { get; set; }

    /// <summary>
    /// Serie de la factura.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string Serial { get; set; } = string.Empty;

    /// <summary>
    /// Número de la factura.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de la factura en formato yyyy-MM-dd.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// Código de país del receptor en formato ISO 3166-1 alfa-2 (ej. "ES", "FR").
    /// <para>Opcional.</para>
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Fecha de la operación en formato yyyy-MM-dd.
    /// <para>Opcional. Solo se debe rellenar si es distinta a la fecha de la factura (<c>Date</c>).</para>
    /// </summary>
    public string? OperationDate { get; set; }

    /// <summary>
    /// Descripción de la operación facturada.
    /// <para>Opcional.</para>
    /// </summary>
    public string? OperationDescription { get; set; }

    /// <summary>
    /// Indica si la factura es simplificada.
    /// <para>Opcional.</para>
    /// <para>Solo puede ser <c>true</c> si <c>Type</c> es F1, F3, R1, R2, R3 o R4.</para>
    /// <para>Para F2 o R5 la factura siempre es simplificada (este campo se ignora).</para>
    /// </summary>
    public bool? Simplified { get; set; }

    /// <summary>
    /// Tipo de factura.
    /// <para><b>Requerido.</b></para>
    /// <para>Valores permitidos: ver <see cref="InvoiceType"/> (Lista L2): "F1", "F2", "F3", "R1", "R2", "R3", "R4", "R5".</para>
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Información de la factura rectificada.
    /// <para>Opcional. Solo se debe rellenar si <c>Type</c> es R1, R2, R3, R4 o R5.</para>
    /// </summary>
    public RectifiedInfo? Rectified { get; set; }

    /// <summary>
    /// Desglose de IVA/impuestos de la factura.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public List<VatDetailItem> VatDetail { get; set; } = new();

    /// <summary>
    /// Indica que la factura ha sido emitida por el destinatario.
    /// <para>Opcional. Solo se debe rellenar cuando el emisor sea el destinatario.</para>
    /// </summary>
    public bool? IssuedByRecipient { get; set; }

    /// <summary>
    /// Información del tercero emisor de la factura.
    /// <para>Opcional. Solo se debe rellenar cuando el emisor sea un tercero.</para>
    /// </summary>
    public ThirdPartyInfo? ThirdParty { get; set; }

    /// <summary>
    /// Importe base imponible total de la factura.
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public decimal BaseAmount { get; set; }

    /// <summary>
    /// Importe total de la factura (base + impuestos).
    /// <para><b>Requerido.</b></para>
    /// </summary>
    public decimal TotalAmount { get; set; }
}
