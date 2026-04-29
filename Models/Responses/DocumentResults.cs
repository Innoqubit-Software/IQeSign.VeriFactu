using IQeSign.VeriFactu.Enums;

namespace IQeSign.VeriFactu.Models.Responses;

/// <summary>
/// Información del control de flujo automático de VeriFactu.
/// La plataforma puede indicar cuándo y cuántos documentos enviar en el siguiente envío.
/// </summary>
public sealed class FlowControlResult
{
    /// <summary>
    /// Fecha y hora en la que se debe realizar el siguiente envío (formato: yyyy-MM-dd HH:mm:ss).
    /// Puede ser <c>null</c> si no hay restricción de tiempo.
    /// </summary>
    public string? NextPresentationAt { get; set; }

    /// <summary>
    /// Número de documentos a presentar en el siguiente envío.
    /// </summary>
    public int QuantityDocumentsToSend { get; set; }

    /// <summary>
    /// Cantidad acumulada de documentos pendientes de presentar.
    /// </summary>
    public int QuantityDocumentsStored { get; set; }
}

/// <summary>
/// Referencia a un documento procesado (identificador).
/// </summary>
public sealed class ProcessedDocumentRef
{
    /// <summary>Identificador del documento procesado en IQ Portal.</summary>
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Resultado de enviar, actualizar o cancelar un documento VeriFactu.
/// </summary>
public sealed class DocumentOperationResult
{
    /// <summary>
    /// Identificador del documento creado en IQ Portal.
    /// Solo presente en la respuesta de POST (crear documento).
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Información del control de flujo automático (si aplica).
    /// La plataforma VeriFactu puede indicar restricciones para el siguiente envío.
    /// </summary>
    public FlowControlResult? FlowControl { get; set; }

    /// <summary>
    /// Lista de documentos procesados automáticamente por el control de flujo.
    /// </summary>
    public List<ProcessedDocumentRef> DocumentsProcessed { get; set; } = new();
}

/// <summary>
/// Aviso asociado a una versión de documento enviado.
/// </summary>
public sealed class DocumentWarning
{
    /// <summary>Código del aviso devuelto por la plataforma VeriFactu.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Descripción del aviso.</summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Versión de un documento enviado a la plataforma VeriFactu.
/// </summary>
public sealed class DocumentVersionResult
{
    /// <summary>Identificador de la versión del documento enviado.</summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Acción realizada sobre el documento.
    /// Valores: <see cref="DocumentAction.Send"/>, <see cref="DocumentAction.Modify"/>, <see cref="DocumentAction.Cancel"/>.
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>Avisos devueltos por la plataforma VeriFactu para esta versión.</summary>
    public List<DocumentWarning> Warnings { get; set; } = new();
}

/// <summary>
/// Información completa de un documento VeriFactu.
/// </summary>
public sealed class DocumentResult
{
    /// <summary>Serie de la factura.</summary>
    public string Serial { get; set; } = string.Empty;

    /// <summary>Número de la factura.</summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Estado del documento.
    /// Valores: ver <see cref="DocumentStatus"/>: "1" (aceptada), "2" (aceptada con errores),
    /// "3" (rechazada), "4" (cancelada), "5" (pendiente envío), "6" (pendiente cancelación).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Fecha de la factura en formato yyyy-MM-dd.</summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>Huella VeriFactu (hash de encadenamiento del documento).</summary>
    public string? Mark { get; set; }

    /// <summary>URL de comprobación de la presentación de la factura en la sede electrónica de la AEAT.</summary>
    public string? Url { get; set; }

    /// <summary>Código QR que apunta a la URL de comprobación.</summary>
    public string? Qr { get; set; }

    /// <summary>Historial de versiones del documento enviadas a la plataforma.</summary>
    public List<DocumentVersionResult> Documents { get; set; } = new();
}

/// <summary>
/// Resumen de un documento en el listado.
/// </summary>
public sealed class DocumentSummary
{
    /// <summary>Serie de la factura.</summary>
    public string Serie { get; set; } = string.Empty;

    /// <summary>Número de la factura.</summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Estado del documento.
    /// Valores: ver <see cref="DocumentStatus"/>: "1" (aceptada), "2" (aceptada con errores),
    /// "3" (rechazada), "4" (cancelada), "5" (pendiente envío), "6" (pendiente cancelación).
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>Fecha de la factura en formato yyyy-MM-dd.</summary>
    public string DocumentDate { get; set; } = string.Empty;

    /// <summary>Huella VeriFactu (hash de encadenamiento del documento).</summary>
    public string? Mark { get; set; }

    /// <summary>URL de comprobación de la presentación de la factura.</summary>
    public string? Url { get; set; }
}

/// <summary>
/// Resultado del check de documentos procesados automáticamente por FlowControl.
/// </summary>
public sealed class CheckDocumentResult
{
    /// <summary>Lista de documentos procesados por el control de flujo automático.</summary>
    public List<ProcessedDocumentRef> DocumentsProcessed { get; set; } = new();

    /// <summary>Estado actual del control de flujo.</summary>
    public FlowControlResult? FlowControl { get; set; }
}

/// <summary>Respuesta del endpoint POST VeriFactu/Document.</summary>
public sealed class AddDocumentResponse : ApiResponse<DocumentOperationResult> { }

/// <summary>Respuesta del endpoint PUT VeriFactu/Document/{id}.</summary>
public sealed class UpdateDocumentResponse : ApiResponse<DocumentOperationResult> { }

/// <summary>Respuesta del endpoint PUT VeriFactu/Document/{id}/Cancel.</summary>
public sealed class CancelDocumentResponse : ApiResponse<DocumentOperationResult> { }

/// <summary>Respuesta del endpoint GET VeriFactu/Document/{id}.</summary>
public sealed class GetDocumentResponse : ApiResponse<DocumentResult> { }

/// <summary>Respuesta del endpoint GET VeriFactu/Document/List.</summary>
public sealed class ListDocumentsResponse : ApiResponse<List<DocumentSummary>> { }

/// <summary>Respuesta del endpoint GET VeriFactu/Document/Check.</summary>
public sealed class CheckDocumentResponse : ApiResponse<CheckDocumentResult> { }
