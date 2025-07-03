![Logo](Logo.png)
# Elect
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview

Number of mini libraries to make faster develop .NET Core system.

- .Net Core Utilities methods.
- Save time to project setup and focus on Business Logic.
- Up to date: Elect always up to date the last .NET Core stable version. All Elect modules publish in [Nuget Package](https://www.nuget.org/packages?q=TopNguyen) so easy for you update the library.
- Almost library work well as cross platform, you can feel free to code on Windows or Mac.

## Installation

All Elect modules publish in [nuget.org](https://www.nuget.org/packages?q=TopNguyen).

## Usage

Elect have difference modules/libraries, each lib handle a 3rd library or focus utility for a field.

- ### [Elect.Core](https://github.com/topnguyen/Elect/tree/master/src/Elect.Core/README.md): Utilities and Core of Ecosystem.

- ### Dependency Injection
    + [Elect.DI](https://github.com/topnguyen/Elect/tree/master/src/DI/Elect.DI/README.md): Register service and implementation by Attributes, support scan assemblies in difference folders to register.

- ### Data
    + [Elect.Data.EF](https://github.com/topnguyen/Elect/tree/master/src/Data/Elect.Data.EF/README.md): Entity Framework by Unit of Work and Repository wrap pattern with Transaction support.

    + [Elect.Data.IO](https://github.com/topnguyen/Elect/tree/master/src/Data/Elect.Data.IO/README.md) 
        * Physical: Support handle file, folder, image (resize, compress, dominant color).

- ### Web
    + [Elect.Web](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web/README.md): ASP Net Core utitlies, Beautiful paged collection API support.
  
    + [Elect.Web.Middlewares](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Middlewares/README.md): Useful middlewares for ASP NET Core project such as: measure processing time, minimize HTML, CSS, JS on response.

    + [Elect.Web.DataTable](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.DataTable/README.md): Server generate [jQuery DataTable](https://datatables.net/) support paging server, column filter, column visible and so on for both legacy and modern version.

    + [Elect.Web.HttpDetection](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.HttpDetection/README.md): Trade Device information (address/location via IP) via HttpRequest User-Agent.

    + [Elect.Web.Ajaxify](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Ajaxify/README.md): Make your regular website (multiple page) to single page by ajax approach.

- ### Background Job
    + [Elect.Job.Hangfire](https://github.com/topnguyen/Elect/tree/master/src/Job/Elect.Job.Hangfire/README.md): Extend of [Hangfire](https://github.com/HangfireIO/Hangfire), easier to setup and secure Hangfire Dashboard.

## Samples
Please access "samples" folder in this repo to explore example for each library.

## License
Elect is licensed under the [MIT License](https://github.com/topnguyen/Elect/blob/master/LICENSE).
