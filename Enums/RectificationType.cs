namespace IQeSign.VeriFactu.Enums;

/// <summary>
/// Códigos de tipo de rectificación (Lista L3 VeriFactu).
/// Usado en <c>RectifiedInfo.Type</c>.
/// Solo aplicable cuando <c>VeriFactuDocumentFile.Type</c> es R1, R2, R3, R4 o R5.
/// </summary>
public static class RectificationType
{
    /// <summary>Rectificación por sustitución.</summary>
    public const string PorSustitucion = "S";

    /// <summary>Rectificación por diferencias.</summary>
    public const string PorDiferencias = "I";
}
