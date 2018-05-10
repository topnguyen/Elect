![Logo](../../../Logo.png)
# Elect.Location.Google
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Utilities to use Google Api.

Featured Methods:
- Fastest Trip: Help you calcuate fastest route by A->Z or A->A (Round Trip).
- Distance Duration Matrix: receive a matrix of distance and duration for coordinates.

## Installation
- Package Manager
```
PM> Install-Package Elect.Location.Google
```
- .NET CLI
```
dotnet add package Elect.Location.Google
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Location.Google/).

## Usage

- Add service: `services.AddElectLocationGoogle();`.
- You can config client information (eg: Google Api Key) for the service by parameter [`ElectLocationGoogleOptions`](Models/ElectLocationGoogleOptions.cs).
- To use the Service permanent - no limited, you should add Google Api Key in configuration for the service.

## License
Elect.Location.Google is licensed under the [MIT License](../../../LICENSE).