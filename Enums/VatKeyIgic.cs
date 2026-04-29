namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de clave de régimen especial para IGIC (Lista L8B VeriFactu).
/// Usado en <c>VatDetailItem.VatKey</c> cuando <c>VatDetailItem.Vat</c> es <see cref="VatType.Igic"/> ("03").
/// </summary>
public static class VatKeyIgic
{
    /// <summary>Operación de régimen general.</summary>
    public const string RegimenGeneral = "01";

    /// <summary>Exportación.</summary>
    public const string Exportacion = "02";

    /// <summary>Operaciones a las que se aplique el régimen especial de bienes usados, objetos de arte, antigüedades y objetos de colección.</summary>
    public const string BienesUsados = "03";

    /// <summary>Régimen especial del oro de inversión.</summary>
    public const string OroInversion = "04";

    /// <summary>Régimen especial de las agencias de viajes.</summary>
    public const string AgenciasViajes = "05";

    /// <summary>Régimen especial grupo de entidades en IGIC (Nivel Avanzado).</summary>
    public const string GrupoEntidades = "06";

    /// <summary>Régimen especial del criterio de caja.</summary>
    public const string CriterioCaja = "07";

    /// <summary>Operaciones sujetas al IPSI/IVA.</summary>
    public const string IpsiIva = "08";

    /// <summary>Facturación de prestaciones de servicios de agencias de viaje que actúan como mediadoras en nombre y por cuenta ajena (D.A.4ª RD1619/2012).</summary>
    public const string AgenciasMediadorasNombreAjeno = "09";

    /// <summary>Cobros por cuenta de terceros de honorarios profesionales o de derechos derivados de la propiedad industrial.</summary>
    public const string CobrosTerceros = "10";

    /// <summary>Operaciones de arrendamiento de local de negocio.</summary>
    public const string ArrendamientoLocal = "11";

    /// <summary>Factura con IGIC pendiente de devengo en certificaciones de obra cuyo destinatario sea una Administración Pública.</summary>
    public const string IgicPendienteAdministracionPublica = "14";

    /// <summary>Factura con IGIC pendiente de devengo en operaciones de tracto sucesivo.</summary>
    public const string IgicPendienteTractoSucesivo = "15";

    /// <summary>Régimen especial de comerciante minorista.</summary>
    public const string Comerciante = "17";

    /// <summary>Régimen especial del pequeño empresario o profesional.</summary>
    public const string PequenoEmpresario = "18";

    /// <summary>Operaciones interiores exentas por aplicación artículo 25 Ley 19/1994.</summary>
    public const string ExentasArticulo25 = "19";
}
