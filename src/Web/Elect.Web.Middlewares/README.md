![Logo](../../../Logo.png)
# Elect.Web.Middlewares
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Elect.Web.Middlewares contain many utility middlewares to help you monitor your application.

- CorsMiddleware: Configuration cross-origin requestp policy.
  
- HttpContextMiddleware: Help you can access `HttpContext` of the Http Request in any layer.

- MeasureProcessingTimeMiddleware: Auto Start `Stopwatch` when receive a Http Request and End when finish Http Response.

- MinResponseMiddleware: Auto minify HTML, CSS and JS content when response.

- RequestRewindMiddleware: Enable Rewind for Http Request, useful if you want to read Request Body content.

- ServerInfoMiddleware: Add Server Information, useful to Client have  author contact and server information. You can fake this information to improve your system security.

## Installation
- Package Manager
```
PM> Install-Package Elect.Web.Middlewares
```
- .NET CLI
```
dotnet add package Elect.Web.Middlewares
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Web.Middlewares/).

## Usage

- Almost Middleware have Extensions for `IApplicationBuilder` and `IServiceCollection`.

- CorsMiddleware
    + Add Service: `services.AddElectCors()` - You can configuration by parameter [`ElectCorsOptions`](CorsMiddleware/Models/ElectCorsOptions.cs).
    + Use Middleware: `app.UseElectCors()`.
    + Use: This middleware auto verify `HttpRequest` and append Headers for `HttpResponse` about Cors Information.

- HttpContextMiddleware
    + Add Service: `services.AddElectHttpContext()`.(CorsMiddleware/Models/ElectCorsOptions.cs)
    + Use Middleware: `app.UseElectHttpContext()`.
    + Use: You can access `HttpContext` of current `HttpRequest` by static object **`HttpContext.Current`** - using namespace `Elect.Web.Middlewares.HttpContextMiddleware`.

- MeasureProcessingTimeMiddleware
    + Use Middleware: `app.UseElectMeasureProcessingTime()`.
    + Use: This middleware auto append Headers for `HttpResponse` key `X-Processing-Time-Milliseconds` about total milliseconds for handle the request.

- MinResponseMiddleware
    + Add Service: `services.AddElectMinResponse()`.
    + Use Middleware: `app.UseElectMinResponse()`.
    + Use: This middleware auto minify HTML, CSS and JS content when response.

- RequestRewindMiddleware
    + Use Middleware: `app.UseElectRequestRewind()`.
    + Use: This middleware auto enable Request Rewind every `HttpRequest`, you can read raw HttpRequest Body.
```csharp
using (var ms = new MemoryStream(2048))
{
    await Request.Body.CopyToAsync(ms);
    var contentBytes =  ms.ToArray();
}
```

- ServerInfoMiddleware
    + Add Service: `services.AddElectServerInfo()` - You can configuration by parameter [`ElectCorsOptions`](ServerInfoMiddleware/Models/ElectServerInfoOptions.cs).
    + Use Middleware: `app.UseElectServerInfo()`.
    + Use: This middleware auto append Headers for `HttpResponse` about Server Info.

## [View Sample](../../../samples/Web/Elect.Sample.Web.Middlewares/README.md)

## License
Elect.Web.Middlewares is licensed under the [MIT License](../../../LICENSE).