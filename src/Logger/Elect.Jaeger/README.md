![Logo](../../../Logo.png)
# Elect.Jaeger
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview
- Support add trace to the Jaeger Service automatically.

## Installation

### Jaeger Service
- [Download Jaeger](https://www.jaegertracing.io/download/)
- Run the jaeger service
    ```cmd
    jaeger-all-in-one
    ```
- Jaeger by default
    + The query UI run on http://localhost:16686
    + Sampler port is 5778
    + Reporter port is 6831

### Elect Jaeger Nuget
- Package Manager
    ```
    PM> Install-Package Elect.Jaeger
    ```

- .NET CLI
    ```
    dotnet add package Elect.Jaeger
    ```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Jaeger/).

## Usage

- Add Service
    + You can config endpoint, sampler, reporter port by [`ElectJaegerOptions`](Models/ElectJaegerOptions.cs).
    + By default already bind to the default endpoint, port from Jaeger Service.
    ```c#
    services.AddElectJaeger();
    ```
    
## License
Elect.Jaeger is licensed under the [MIT License](../../../LICENSE).