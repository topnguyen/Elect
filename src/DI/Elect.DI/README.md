![Logo](../../../Logo.png)
# Elect.DI
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview

Managing Dependency Injection in .NET Core.

I have seen some situations where a web project has a `Startup.cs` that is hundreds/thousands lines with `services.AddTransient` `services.AddScoped` `services.AddSingleton` calls for every single interface and implementation.

With Elect.DI, you can register dependency injection by Attributes - lifetime define in the Implementaion class and just 1 line of code in `Startup.cs` to enable `Scanner` and auto register follow attribute setup.

Elect.DI support to auto register Service and Implementation by
- Projects reference.
- Scan assemblies in difference folders.

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
```c#
public void ConfigureServices(IServiceCollection services)
{
    // Auto Register Dependency Injection
    services.AddElectDI();
    
//  services.AddElectDI(_ =>
//  {
//      _.ListAssemblyName = new List<string> // default is 1 assembly name: application name - Elect.Sample.DI
//      {
//          "ExampleAssembly" // will scan ExampleAssembly.dll and ExampleAssembly.*.dll
//      };
//      _.ListAssemblyFolderPath = new List<string> // default is  1 folder: application base folder path
//      {
//          "C:\\ExampleAssemblyFolderPath" 
//      };
//  });

    // Optional - Print out Registered Service with Information
    services.PrintServiceAddedToConsole();
}
```

- Define lifetime in implementation
  + ScopedDependency: Register as Scoped Lifetime.
  + TransientDependency: Register as Transient Lifetime.
  + SingletonDependency: Register as Singleton Lifetime.
  + Register for Interface via parameter `ServiceType`, if not set parameter value then the Implementation **injection it self**.
 
```c#
public interface ISampleService{

}

[ScopedDependency(ServiceType = typeof(ISampleService))]
public class SampleService : ISampleService{
}
```

## [View Sample](../../../samples/DI/Elect.Sample.DI/README.md)

## License
Elect.DI is licensed under the [MIT License](../../../LICENSE).