![Logo](../../../Logo.png)
# Elect.Web.HttpDetection
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Get Device information from `User-Agent` in header of the `HttpRequest`.

Device Info contain: Location base on IP Address, Device OS version and so on.

## Installation
- Package Manager
```
PM> Install-Package Elect.Web.HttpDetection
```
- .NET CLI
```
dotnet add package Elect.Web.HttpDetection
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Web.HttpDetection/).

## Usage

HttpDetection work as `HttpRequest` extensions.

```csharp
var deviceInfo = HttpRequest.GetDeviceInformation();
```

If you want to use HttpDetection in Service Layer, please use "HttpContextMiddleware" from [Elect.Web.Middlewares](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Middlewares)to get HttpContext of the current request then call the extensions method. 

## License
Elect.Web.HttpDetection is licensed under the [MIT License](../../../LICENSE).