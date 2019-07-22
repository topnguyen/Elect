![Logo](../../../Logo.png)
# Elect.Face.Kairos
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview
 - Client by .NET Core for [Kairos](https://kairos.com) Face Recognition Service.

## Installation
 - Package Manager
    ```
    PM> Install-Package Elect.Face.Kairos
    ```

 - .NET CLI
    ```
    dotnet add package Elect.Face.Kairos
    ```

See more information in [![Nuget](https://buildstats.info/nuget/Elect.Face.Kairos)](https://www.nuget.org/packages/Elect.Face.Kairos/).

## Usage
 - Add Service
    + You can config `ApiId`, `ApiKey` and `DefaultGallery` via [`ElectKairosOptions`](Models/ElectKairosOptions.cs).
    ```c#
    services.AddElectKairos(_ =>
    {
        _.AppId = "<Your Kairos App Id>";
        _.AppKey = "<Your Kairos App Key>";
        _.DefaultGallery = "<Your Default Gallery Name (Optional)>";
    });
    ```
    
## License
Elect.Face.Kairos is licensed under the [MIT License](../../../LICENSE).