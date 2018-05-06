![Logo](../../../Logo.png)
# Elect.DI
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Managing Dependency Injection in .NET Core.

I have seen some situations where a web project has a `Startup.cs` that is hundreds/thousands lines with `services.AddTransient` `services.AddScoped` `services.AddSingleton` calls for every single interface and implementation.

With Elect.DI, you can register dependency injection by Attributes - lifetime define in the Implementaion class and just 1 line of code in `Startup.cs` to enable `Scanner` and auto register follow attribute setup.

## Installation
- Package Manager
```
PM> Install-Package Elect.DI
```
- .NET CLI
```
dotnet add package Elect.DI
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.DI/).

## Usage

- `Startup.cs`: add `Scanner` - Auto register dependency injection.
```csharp
 public void ConfigureServices(IServiceCollection services)
{
    // Auto Register Dependency Injection
    services.AddElectDI();

    // Optional - Print out Registered Service with Information
    services.PrintServiceAddedToConsole();
}
```

- Define lifetime in implementation
  + ScopedDependency: Register as Scoped Lifetime.
  + TransientDependency: Register as Transient Lifetime.
  + SingletonDependency: Register as Singleton Lifetime.
  + Register for Interface via parameter `ServiceType`, if not set parameter value then the Implementation **injection it self**.
 
```csharp
public interface ISampleService{

}

[ScopedDependency(ServiceType = typeof(ISampleService))]
public class SampleService : ISampleService{
}
```

## [View Sample](../../../samples/DI/Elect.Sample.DI/README.md)

## License
Elect.DI is licensed under the [MIT License](../../../LICENSE).