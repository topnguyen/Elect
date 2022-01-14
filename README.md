![Logo](Logo.png)
# Elect
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview

Number of mini libraries to make faster develop .NET Core system.

- .Net Core Utilities methods.
- Save time to project setup and focus on Business Logic.
- Up to date: Elect always up to date the last .NET Core stable version. All Elect modules publish in [Nuget Package](https://www.nuget.org/packages?q=TopNguyen) so easy for you update the library.
- Almost library work well as cross platform, you can feel free to code on Windows or Mac.

![Elect Library Diagram](LibraryDiagram.png)

## Installation

All Elect modules publish in [nuget.org](https://www.nuget.org/packages?q=TopNguyen).

You can add **My Nuget Server** to have fastest packages update
 - No waste time for nuget index package
 - Add **`<add key="Top Nguyen" value="https://nuget.topnguyen.com/v3/index.json"/>`** to NuGet.Config
    + If you not have NuGet.Config, just create the file named "NuGet.Config" in your root project folder with below content 
        ```xml
        <?xml version="1.0" encoding="utf-8"?>
        <configuration>
            <packageSources>
                <add key="Top Nguyen" value="https://nuget.topnguyen.com/v3/index.json"/>
            </packageSources>
        </configuration>
         ```
## Usage

Elect have difference modules/libraries, each lib handle a 3rd library or focus utility for a field.

- ### [Elect.Core](https://github.com/topnguyen/Elect/tree/master/src/Elect.Core/README.md): Utilities and Core of Ecosystem.
    > [![Nuget](https://buildstats.info/nuget/Elect.Core)](https://www.nuget.org/packages/Elect.Core/)

- ### AI
    + [Elect.Face.Kairos](https://github.com/topnguyen/Elect/tree/master/src/AI/Elect.Face.Kairos/README.md): Client by .NET Core for [Kairos](https://kairos.com) Face Recognition Service.
        > [![Nuget](https://buildstats.info/nuget/Elect.Face.Kairos)](https://www.nuget.org/packages/Elect.Face.Kairos/)

- ### Dependency Injection
    + [Elect.DI](https://github.com/topnguyen/Elect/tree/master/src/DI/Elect.DI/README.md): Register service and implementation by Attributes, support scan assemblies in difference folders to register.
        > [![Nuget](https://buildstats.info/nuget/Elect.DI)](https://www.nuget.org/packages/Elect.DI/)

- ### Data
    + [Elect.Data.EF](https://github.com/topnguyen/Elect/tree/master/src/Data/Elect.Data.EF/README.md): Entity Framework by Unit of Work and Repository wrap pattern with Transaction support.
        > [![Nuget](https://buildstats.info/nuget/Elect.Data.EF)](https://www.nuget.org/packages/Elect.Data.EF/)
        
    + [Elect.Data.IO](https://github.com/topnguyen/Elect/tree/master/src/Data/Elect.Data.IO/README.md) 
        * Physical: Support handle file, folder, image (resize, compress, dominant color).
        > [![Nuget](https://buildstats.info/nuget/Elect.Data.IO)](https://www.nuget.org/packages/Elect.Data.IO/)

- ### Mapper
    + [Elect.Mapper.AutoMapper](https://github.com/topnguyen/Elect/tree/master/src/Mapper/Elect.Mapper.AutoMapper/README.md): Extend of [AutoMapper](https://github.com/AutoMapper/AutoMapper), support auto register Mapper Profile by scan assemblies.
        > [![Nuget](https://buildstats.info/nuget/Elect.Mapper.AutoMapper)](https://www.nuget.org/packages/Elect.Mapper.AutoMapper/)

- ### Web
    + [Elect.Web](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web/README.md): ASP Net Core utitlies, Beautiful paged collection API support.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web)](https://www.nuget.org/packages/Elect.Web/)
            
    + [Elect.Web.Middlewares](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Middlewares/README.md): Useful middlewares for ASP NET Core project such as: measure processing time, minimize HTML, CSS, JS on response.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web.Middlewares)](https://www.nuget.org/packages/Elect.Web.Middlewares/)

    + [Elect.Web.Swagger](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Swagger/README.md): Extend of [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore), easier to setup and support more friendly UI, security API Document URI.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web.Swagger)](https://www.nuget.org/packages/Elect.Web.Swagger/)
        
    + [Elect.Web.DataTable](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.DataTable/README.md): Server generate [jQuery DataTable](https://datatables.net/) support paging server, column filter, column visible and so on for both legacy and modern version.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web.DataTable)](https://www.nuget.org/packages/Elect.Web.DataTable/)
        
    + [Elect.Web.HttpDetection](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.HttpDetection/README.md): Trade Device information (address/location via IP) via HttpRequest User-Agent.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web.HttpDetection)](https://www.nuget.org/packages/Elect.Web.HttpDetection/)
        
    + [Elect.Web.Ajaxify](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Ajaxify/README.md): Make your regular website (multiple page) to single page by ajax approach.
    
    + [Elect.Web.HealthCheck](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.HealthCheck/README.md): Health check for the service a live or not.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web.HealthCheck)](https://www.nuget.org/packages/Elect.Web.HealthCheck/)

    + [Elect.Web.Consul](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Consul/README.md): Support auto Register and Deregister Service to the Consul Service with Fabio Load Balancing.
        > [![Nuget](https://buildstats.info/nuget/Elect.Web.Consul)](https://www.nuget.org/packages/Elect.Web.Consul/)
        
- ### Background Job
    + [Elect.Job.Hangfire](https://github.com/topnguyen/Elect/tree/master/src/Job/Elect.Job.Hangfire/README.md): Extend of [Hangfire](https://github.com/HangfireIO/Hangfire), easier to setup and secure Hangfire Dashboard.
        > [![Nuget](https://buildstats.info/nuget/Elect.Job.Hangfire)](https://www.nuget.org/packages/Elect.Job.Hangfire/)
        
- ### Notification
    + [Elect.Notification.OneSignal](https://github.com/topnguyen/Elect/tree/master/src/Notification/Elect.Notification.OneSignal/README.md): Client for [OneSignal.com](http://OneSignal.com).
        > [![Nuget](https://buildstats.info/nuget/Elect.Notification.OneSignal)](https://www.nuget.org/packages/Elect.Notification.OneSignal/)
        
    + [Elect.Notification.Esms](https://github.com/topnguyen/Elect/tree/master/src/Notification/Elect.Notification.Esms/README.md): Client for [eSMS.vn](http://eSMS.vn).
        > [![Nuget](https://buildstats.info/nuget/Elect.Notification.Esms)](https://www.nuget.org/packages/Elect.Notification.Esms/)

- ### Logger
    + [Elect.Jaeger](https://github.com/topnguyen/Elect/tree/master/src/Logger/Elect.Jaeger/README.md): Support add trace to the Jaeger Service automatically.
        > [![Nuget](https://buildstats.info/nuget/Elect.Jaeger)](https://www.nuget.org/packages/Elect.Jaeger/)
        
    + [Elect.AppMetrics](https://github.com/topnguyen/Elect/tree/master/src/Logger/Elect.AppMetrics/README.md): Support enable and config to the AppMetrics Service automatically.
        > [![Nuget](https://buildstats.info/nuget/Elect.AppMetrics)](https://www.nuget.org/packages/Elect.AppMetrics/)

- ### Localization
    + ***[On Plan]** Elect.Localization.Json: Localization with resource in separate Json files.*
    + ***[On Plan]** Elect.Localization.Dashboard: Support manage Elect.Localization.Json via a Dashboard with security check support.*

## Samples

Please access "samples" folder in this repo to explore example for each library.

## License
Elect is licensed under the [MIT License](https://github.com/topnguyen/Elect/blob/master/LICENSE).
