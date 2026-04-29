namespace IQeSign.VeriFactu.Configuration;

/// <summary>
/// Entornos disponibles para la API IQ eSign VeriFactu.
/// </summary>
public enum IQeSignEnvironment
{
    /// <summary>
    /// Entorno de producción: https://iqesignapi.azurewebsites.net
    /// </summary>
    Production,

    /// <summary>
    /// Entorno de staging (pruebas): https://iqesignapistaging.azurewebsites.net
    /// </summary>
    Staging
}
