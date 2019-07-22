![Logo](../../../Logo.png)
# Elect.Jaeger
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview
 - Health check for the service a live or not.
 - Support SQL Server healthy check between Service <-> SQL Server.
 - Extend `Microsoft.AspNetCore.Diagnostics.HealthChecks` and allow you to add more `Builder` after this service finish config.
    + Please see the setup callback in [`ElectHealthCheckOptions`](Models/ElectHealthCheckOptions.cs)
    
## Installation
 - Package Manager
    ```
    PM> Install-Package Elect.HealthCheck
    ```
 - .NET CLI
    ```
    dotnet add package Elect.HealthCheck
    ```

See more information in [![Nuget](https://buildstats.info/nuget/Elect.HealthCheck)](https://www.nuget.org/packages/Elect.HealthCheck/).

## Usage
- Add Service
    + You can config endpoints by [`ElectHealthCheckOptions`](Models/ElectHealthCheckOptions.cs), default is `/health`.
    + Support Check Healthy between Service <-> SQL Server. Just need to config the DB Connection in `DbConnectionString` Property of the [`ElectHealthCheckOptions`](Models/ElectHealthCheckOptions.cs).
    ```c#
    services.AddElectHealthCheck();
    ```
    
- Use Service
    ```c#
    app.UseElectHealthCheck();
    ```

## License
Elect.Jaeger is licensed under the [MIT License](../../../LICENSE).