# IQeSign.VeriFactu

[![NuGet](https://img.shields.io/nuget/v/IQeSign.VeriFactu.svg)](https://www.nuget.org/packages/IQeSign.VeriFactu)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-net8.0%20%7C%20netstandard2.1-purple)](https://dotnet.microsoft.com)

Cliente .NET para la **API IQ eSign VeriFactu** de [InnoQubit Software](https://www.innoqubit.com). Permite integrar desde cualquier aplicación .NET la presentación de facturas electrónicas a la plataforma **VeriFactu** de la AEAT, gestionando certificados digitales, firma de peticiones y control de flujo automático.

---

## Sobre InnoQubit

[**InnoQubit Business Software**](https://www.innoqubit.com) es una empresa tecnológica en crecimiento con sede en Castellón (España), especializada en la **digitalización y automatización de procesos empresariales** para sistemas ERP.

Su producto insignia **IQ eSign** agrupa soluciones de facturación electrónica y firma digital que se integran con cualquier ERP (Microsoft Dynamics 365 Business Central, Navision y software a medida):

| Solución | Descripción |
|---|---|
| **IQ eSign VeriFactu** | Presentación de facturas al sistema VeriFactu de la AEAT |
| **IQ eSign TicketBAI** | Facturación electrónica para el País Vasco |
| **IQ eSign Facturae** | Generación y envío de facturas en formato Facturae |
| **IQ eSign ePDF** | Generación de PDFs firmados digitalmente |

Todas las soluciones se gestionan desde **IQ Portal**, el panel centralizado de InnoQubit donde se administran credenciales, certificados y consumo del servicio.

---

## ¿Qué es VeriFactu?

**VeriFactu** es el sistema de registro de facturación aprobado por la AEAT en el Real Decreto 1007/2023. Obliga a determinadas empresas y autónomos a enviar cada factura emitida a la Agencia Tributaria en tiempo real, garantizando su integridad mediante encadenamiento de huellas digitales y firma electrónica.

Este paquete encapsula toda la complejidad de la integración:

- Autenticación JWT automática con refresco de token
- Firma de peticiones con certificado digital (.pfx) almacenado en IQ Portal
- Encadenamiento de documentos (FlowControl) gestionado por la plataforma
- Soporte completo de los tipos de factura (F1-F3, R1-R5), regímenes de IVA e IGIC y causas de exención

---

## Instalación

```bash
dotnet add package IQeSign.VeriFactu
```

Para obtener una cuenta y el `CredentialGuid` necesarios para usar este paquete, contacta con el equipo comercial de InnoQubit en [comercial@innoqubit.com](mailto:comercial@innoqubit.com).

---

## Inicio rápido

### 1. Registro en el contenedor DI

```csharp
// Program.cs
builder.Services.AddIQeSignVeriFactu(options =>
{
    options.CredentialGuid = builder.Configuration["IQeSign:CredentialGuid"]!;
    options.Environment = IQeSignEnvironment.Production; // o Staging para pruebas
    options.TimeoutSeconds = 30;
});
```

O bien usando una sección de `appsettings.json`:

```json
{
  "IQeSign": {
    "CredentialGuid": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "Environment": "Production"
  }
}
```

```csharp
builder.Services.AddIQeSignVeriFactu(
    builder.Configuration.GetSection(IQeSignOptions.SectionName));
```

### 2. Enviar una factura

```csharp
public class FacturacionService(IVeriFactuService veriFactu)
{
    public async Task<string> EnviarFacturaAsync(CancellationToken ct = default)
    {
        var response = await veriFactu.AddDocumentAsync(new AddDocumentRequest
        {
            CertificateId = "id-del-certificado-en-iqportal",
            CertificatePass = "contraseña-del-pfx",
            File = new VeriFactuDocumentFile
            {
                Version = SchemaVersion.V1_0,
                Type = InvoiceType.Factura, // "F1"
                Serial = "FAC",
                Number = "2024-001",
                Date = "2024-01-15",
                OperationDescription = "Servicios de consultoría",
                Issuer = new IssuerInfo
                {
                    Name = "Mi Empresa S.L.",
                    CifNif = "B12345678"
                },
                Name = "Cliente S.A.",
                CifNif = "A98765432",
                BaseAmount = 1000.00m,
                TotalAmount = 1210.00m,
                VatDetail =
                [
                    new VatDetailItem
                    {
                        Vat = VatType.Iva, // "01"
                        VatKey = VatKey.RegimenGeneral, // "01"
                        Type = VatOperationType.SujetaNoExentaSinInversion, // "S1"
                        VatPercent = 21m,
                        VatAmount = 210.00m,
                        BaseAmount = 1000.00m
                    }
                ]
            },
            Metadata = new DocumentMetadata { Platform = "MiApp" }
        }, ct);

        if (!response.IsSuccess)
            throw new Exception($"Error VeriFactu [{response.ErrorCode}]: {response.ErrorMessage}");

        return response.Result!.Id!;
    }
}
```

### 3. Gestión de certificados

```csharp
public class CertificadoService(ICertificateService certs)
{
    // Subir un certificado .pfx
    public async Task<string> SubirCertificadoAsync(byte[] pfxBytes, string nombre)
    {
        var resp = await certs.AddAsync(new AddCertificateRequest
        {
            Name = nombre,
            File = Convert.ToBase64String(pfxBytes)
        });
        return resp.Result!.Id;
    }

    // Listar todos los certificados disponibles
    public async Task<List<CertificateInfo>> ListarAsync()
    {
        var resp = await certs.ListAsync();
        return resp.Result ?? [];
    }
}
```

---

## Servicios disponibles

### `IVeriFactuService`

| Método | Endpoint | Descripción |
|---|---|---|
| `GetCodesAsync()` | `GET /api/v2/VeriFactu/Codes` | Obtiene las listas de referencia L1-L15 |
| `GetUsageAsync()` | `GET /api/v2/VeriFactu/Usage` | Consulta el consumo del plan contratado |
| `AddDocumentAsync(request)` | `POST /api/v2/VeriFactu/Document` | Envía una nueva factura a VeriFactu |
| `GetDocumentByIdAsync(id)` | `GET /api/v2/VeriFactu/Document/{id}` | Consulta el estado de un documento |
| `UpdateDocumentAsync(id, request)` | `PUT /api/v2/VeriFactu/Document/{id}` | Actualiza y reenvía un documento |
| `CancelDocumentAsync(id, request)` | `PUT /api/v2/VeriFactu/Document/{id}/Cancel` | Cancela un documento en VeriFactu |
| `ListDocumentsAsync(filtros?)` | `GET /api/v2/VeriFactu/Document/List` | Lista documentos con filtro de fechas opcional |
| `CheckDocumentsAsync()` | `GET /api/v2/VeriFactu/Document/Check` | Procesa documentos pendientes por FlowControl |

### `ICertificateService`

| Método | Endpoint | Descripción |
|---|---|---|
| `AddAsync(request)` | `POST /api/v2/Certificate` | Sube un certificado .pfx en Base64 |
| `GetByIdAsync(id)` | `GET /api/v2/Certificate/{id}` | Consulta un certificado por ID |
| `DownloadAsync(id)` | `GET /api/v2/Certificate/{id}/Download` | Descarga el .pfx en Base64 |
| `ListAsync()` | `GET /api/v2/Certificate/List` | Lista todos los certificados |
| `DeleteAsync(id)` | `DELETE /api/v2/Certificate/{id}` | Elimina un certificado |

---

## Listas de referencia VeriFactu

Todos los valores de los campos codificados están disponibles como constantes `string` en las clases de la carpeta `Enums/`:

```csharp
// Tipos de impuesto (L1)
VatType.Iva // "01"
VatType.Igic // "03"

// Tipos de factura (L2)
InvoiceType.Factura // "F1"
InvoiceType.FacturaSimplificada // "F2"
InvoiceType.RectificativaError // "R1"

// Tipos de operación IVA (L9)
VatOperationType.SujetaNoExentaSinInversion // "S1"
VatOperationType.NoSujetaArticulo7 // "N1"

// Causas de exención (L10)
VatExemptionType.Articulo20 // "E1"

// Versión del esquema (L15)
SchemaVersion.V1_0 // "1.0"
```

Para IGIC usar `VatKeyIgic` en lugar de `VatKey`. Para el resto de listas ver `IdentificationType` (L7), `RectificationType` (L3), `VatExemptionType` (L10).

---

## Entornos

| Entorno | URL | Uso |
|---|---|---|
| `IQeSignEnvironment.Production` | `https://iqesignapi.azurewebsites.net` | Producción real |
| `IQeSignEnvironment.Staging` | `https://iqesignapistaging.azurewebsites.net` | Pruebas e integración |

---

## Control de errores

Todos los métodos devuelven un objeto que hereda de `ApiResponse` con las propiedades `IsSuccess`, `ErrorCode` y `ErrorMessage`.

```csharp
var response = await veriFactu.CancelDocumentAsync(id, request);

if (!response.IsSuccess)
{
    // ErrorCode "1" → problema con el plan/cliente
    // ErrorCode "2" → problema con el certificado
    // ErrorCode "3" → límite de documentos excedido o error de firma
    // ErrorCode "9" → error no controlado (ver ErrorMessage)
    // ErrorCode "17" → error devuelto por la plataforma VeriFactu
    Console.WriteLine($"[{response.ErrorCode}] {response.ErrorMessage}");
}
```

Las excepciones se lanzan únicamente ante fallos de comunicación:

| Excepción | Cuándo se lanza |
|---|---|
| `IQeSignAuthException` | El `CredentialGuid` es inválido o la cuenta está inactiva |
| `IQeSignApiException` | La API devuelve un código HTTP 4xx/5xx inesperado |

---

## Requisitos

- .NET 8.0 o .NET Standard 2.1 (compatible con .NET 6+, .NET 7+)
- Cuenta activa en [IQ Portal](https://www.innoqubit.com) con la solución IQ eSign VeriFactu contratada
- Certificado digital .pfx válido para firma de facturas (FNMT, ACA, etc.)

---

## Documentación adicional

- [Documentación técnica de la API IQ eSign VeriFactu (PDF)](https://www.innoqubit.com/wp-content/uploads/VeriFactu_MemoriaTecnica.pdf)
- [IQ Portal — gestión de credenciales y certificados](https://www.innoqubit.com)
- [Swagger de la API (producción)](https://iqesignapi.azurewebsites.net/swagger/ui/index)

---

## Licencia

MIT © [InnoQubit Software](https://www.innoqubit.com)
