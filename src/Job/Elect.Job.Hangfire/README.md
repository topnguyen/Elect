![Logo](../../../Logo.png)
# Elect.Job.Hangfire
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Easy way to add, config and secure Hangfire.

## Installation
- Package Manager
```
PM> Install-Package Elect.Job.Hangfire
```
- .NET CLI
```
dotnet add package Elect.Job.Hangfire
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Job.Hangfire/).

## Usage

- Add Service
  + You can config dashboard, security by parameter [`ElectHangfireOptions`](Models/ElectHangfireOptions.cs)
```c#
services.AddElectHangfire();
```

- Advance Configuration
  + If you want to add more config for `IGlobalConfiguration` after Elect.Web.Swagger config it. Just add your config in `ExtendOptions` Property of [`ExtendOptions`](Models/ElectHangfireOptions.cs).

- Use Middleware
```c#
app.UseElectHangfire();
```

## License
Elect.Job.Hangfire is licensed under the [MIT License](../../../LICENSE).