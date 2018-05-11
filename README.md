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

All Elect modules publish in [Nuget Package](https://www.nuget.org/packages?q=TopNguyen).

## Usage

Elect have difference modules/libraries, each lib handle a 3rd library or focus utility for a field.

- [Elect.Core](src/Elect.Core/README.md): Utitlies and Core of Ecosystem.
- Dependency Injection
    + [Elect.DI](src/DI/Elect.DI/README.md): Register service and implementation by Attributes, support scan assemblies in difference folders to register.
- Data
    + [Elect.Data.EF](src/Data/Elect.Data.EF/README.md): Entity Framework by Unit of Work and Repository wrap pattern with Transaction support.
    + [Elect.Data.IO](src/Data/Elect.Data.IO/README.md)
      + Physical: Support handle file, folder, image (resize, compress, dominant color).
      + ***[On Plan]** S3: Utitlies to manage file in [AWS S3](https://aws.amazon.com/s3/)*.
- Mapper
    + [Elect.Mapper.AutoMapper](src/Mapper/Elect.Mapper.AutoMapper/README.md): Extend of [AutoMapper](https://github.com/AutoMapper/AutoMapper), support auto register Mapper Profile by scan assemblies.
- Web
    + [Elect.Web](src/Web/Elect.Web/README.md): ASP Net Core utitlies, Beautiful paged collection API support.
    + [Elect.Web.Middlewares](src/Web/Elect.Web.Middlewares/README.md): Useful middlewares for ASP NET Core project such as: measure processing time, minimy HTML, CSS, JS on response.
    + [Elect.Web.Swagger](src/Web/Elect.Web.Swagger/README.md): Extend of [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore), easier to setup and support more friendly UI, security API Document URI.
    + [Elect.Web.DataTable](src/Web/Elect.Web.DataTable/README.md): Server generate [jQuery DataTable](https://datatables.net/) support paging server, column filter, column visible and so on for both legacy and modern version.
    + [Elect.Web.HttpDetection](src/Web/Elect.Web.HttpDetection/README.md): Trade Device information (address/location via IP) via HttpRequest User-Agent.
- Background Job
    + [Elect.Job.Hangfire](src/Job/Elect.Job.Hangfire/README.md): Extend of [Hangfire](https://github.com/HangfireIO/Hangfire), easier to setup and secure Hangfire Dashboard.
- Notification
    + [Elect.Notification.OneSignal](src/Notification/Elect.Notification.OneSignal/README.md): Client for [OneSignal.com](http://OneSignal.com).
    + [Elect.Notification.Esms](src/Notification/Elect.Notification.Esms/README.md): Client for [eSMS.vn](http://eSMS.vn).
- Logger
    + ***[On Plan]** Elect.Logger.Sqlite: Event logger for ASP NET Core project, storage into a Sqlite file - easy to query and statistic.*
    + ***[On Plan]** Elect.Logger.Dashboard: Visual `Elect.Logger.Sqlite` in Dashboard with securiry check support.*

- Localization
    + ***[On Plan]** Elect.Localization.Json: Localization with resource in separate Json files.*
    + ***[On Plan]** Elect.Localization.Dashboard: Support manage Elect.Localization.Json via a Dashboard with securiry check support.*
## License
Elect.Core is licensed under the [MIT License](LICENSE).