namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Estados posibles de un documento VeriFactu.
/// Devuelto en el campo <c>Status</c> de los métodos de consulta de documentos.
/// </summary>
public static class DocumentStatus
{
    /// <summary>Factura aceptada por la plataforma VeriFactu.</summary>
    public const string Aceptada = "1";

    /// <summary>Factura aceptada con errores por la plataforma VeriFactu.</summary>
    public const string AceptadaConErrores = "2";

    /// <summary>Factura rechazada por la plataforma VeriFactu.</summary>
    public const string Rechazada = "3";

    /// <summary>Factura cancelada.</summary>
    public const string Cancelada = "4";

    /// <summary>Factura pendiente de envío.</summary>
    public const string PendienteEnvio = "5";

    /// <summary>Factura pendiente de cancelación.</summary>
    public const string PendienteCancelacion = "6";
}

/// <summary>
/// Acciones posibles sobre un documento VeriFactu enviado.
/// Devuelto en el campo <c>Action</c> de <c>DocumentVersionResult</c>.
/// </summary>
public static class DocumentAction
{
    /// <summary>El documento fue enviado a la plataforma VeriFactu.</summary>
    public const string Send = "Send";

    /// <summary>El documento fue modificado/actualizado.</summary>
    public const string Modify = "Modify";

    /// <summary>El documento fue cancelado.</summary>
    public const string Cancel = "Cancel";
}
