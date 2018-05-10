![Logo](../../../Logo.png)
# Elect.Notification.Esms
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Client for [eSMS.vn](http://eSMS.vn).

- Support Send SMS.
- Receive balance information.

## Installation
- Package Manager
```
PM> Install-Package Elect.Notification.Esms
```
- .NET CLI
```
dotnet add package Elect.Notification.Esms
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Notification.Esms/).

## Usage

- Add service: `services.AddElectNotificationEsms();`.
- You can config client information (eg: ApiKey, ApiSecret) for the service by parameter [`ElectEsmsOptions`](Models/ElectEsmsOptions.cs).

- Send SMS via `SendAsync` method.
- Receive Balance via `GetBalanceAsync` method.

## License
Elect.Notification.Esms is licensed under the [MIT License](../../../LICENSE).