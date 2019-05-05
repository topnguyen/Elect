![Logo](Logo.png)
# Elect
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Number of mini libraries to make faster develop .NET Core system.

- .Net Core Utilities methods.
- Save time to project setup and focus on Business Logic.
- Up to date: Elect always up to date the last .NET Core stable version. All Elect modules publish in [Nuget Package](https://www.nuget.org/packages?q=TopNguyen) so easy for you update the library.
- Almost library work well as cross platform, you can feel free to code on Windows or Mac.

## Installation

All Elect modules publish in [nuget.org](https://www.nuget.org/packages?q=TopNguyen).

You can add **My Nuget Server** to have fastest packages update 
 - No waste time for nuget index package
 - Full nuget.org mirror packages
 - Add `<add key="Top Nguyen" value="http://nuget.topnguyen.com/v3/index.json"/>` to NuGet.Config
    + If you not have NuGet.Config, just create the file named "NuGet.Config" in your root project folder with below content 
        ```xml
        <?xml version="1.0" encoding="utf-8"?>
        <configuration>
            <packageSources>
                <add key="Top Nguyen" value="http://nuget.topnguyen.com/v3/index.json"/>
            </packageSources>
        </configuration>
         ```
## Usage

Elect have difference modules/libraries, each lib handle a 3rd library or focus utility for a field.

- [Elect.Core](src/Elect.Core/README.md): Utitlies and Core of Ecosystem.
- Dependency Injection
    + [Elect.DI](src/DI/Elect.DI/README.md): Register service and implementation by Attributes, support scan assemblies in difference folders to register.
- Data
    + [Elect.Data.EF](src/Data/Elect.Data.EF/README.md): Entity Framework by Unit of Work and Repository wrap pattern with Transaction support.
    + [Elect.Data.IO](src/Data/Elect.Data.IO/README.md)
      + Physical: Support handle file, folder, image (resize, compress, dominant color).
- Mapper
    + [Elect.Mapper.AutoMapper](src/Mapper/Elect.Mapper.AutoMapper/README.md): Extend of [AutoMapper](https://github.com/AutoMapper/AutoMapper), support auto register Mapper Profile by scan assemblies.
- Web
    + [Elect.Web](src/Web/Elect.Web/README.md): ASP Net Core utitlies, Beautiful paged collection API support.
    + [Elect.Web.Middlewares](src/Web/Elect.Web.Middlewares/README.md): Useful middlewares for ASP NET Core project such as: measure processing time, minimy HTML, CSS, JS on response.
    + [Elect.Web.Swagger](src/Web/Elect.Web.Swagger/README.md): Extend of [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore), easier to setup and support more friendly UI, security API Document URI.
    + [Elect.Web.DataTable](src/Web/Elect.Web.DataTable/README.md): Server generate [jQuery DataTable](https://datatables.net/) support paging server, column filter, column visible and so on for both legacy and modern version.
    + [Elect.Web.HttpDetection](src/Web/Elect.Web.HttpDetection/README.md): Trade Device information (address/location via IP) via HttpRequest User-Agent.
    + [Elect.Web.Ajaxify](src/Web/Elect.Web.Ajaxify/README.md): Make your regular website (multiple page) to single page by ajax approach.
- Background Job
    + [Elect.Job.Hangfire](src/Job/Elect.Job.Hangfire/README.md): Extend of [Hangfire](https://github.com/HangfireIO/Hangfire), easier to setup and secure Hangfire Dashboard.
- Notification
    + [Elect.Notification.OneSignal](src/Notification/Elect.Notification.OneSignal/README.md): Client for [OneSignal.com](http://OneSignal.com).
    + [Elect.Notification.Esms](src/Notification/Elect.Notification.Esms/README.md): Client for [eSMS.vn](http://eSMS.vn).
- Logger
    + [Elect.Logger](src/Logger/Elect.Logger/README.md): Event logger for ASP NET Core project, storage into a Json Files - easy to query and statistic.
    + [Elect.Logger.Dashboard](src/Logger/Elect.Logger/README.md) (use `app.UseElectLog()`): Visual `Elect.Logger` in Dashboard with security check support.

- Localization
    + ***[On Plan]** Elect.Localization.Json: Localization with resource in separate Json files.*
    + ***[On Plan]** Elect.Localization.Dashboard: Support manage Elect.Localization.Json via a Dashboard with security check support.*

## Samples

Please access "samples" folder in this repo to explore example for each library.

## License
Elect is licensed under the [MIT License](LICENSE).

## Thank for Jetbrains

<img src="jetbrains-variant-4.png" width="150" alt="JetBrains Logo" />

I have been using ReSharper, Rider for a good number of years and find it absolutely amazing that they assist open source to have free license.

So a big **thank you** to [JetBrains](https://www.jetbrains.com/?from=Elect)!