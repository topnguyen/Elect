![Logo](../../../Logo.png)
# Elect.Logger.Models
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

- Support write log to Json files with parameters
  + {`<DateTimeFormat>`} - current UTC datetime.
    * "Logs/{yyyy-MM-dd}.json" - 1 file / day
    * "Logs/{yyyy-MM-dd HH}.json" - 1 file / hour
    * "Logs/{yyyy-MM-dd HH_mm}.json" - 1 file / minute.
    * "Logs/{yyyy-MM}/{yyyy-MM-dd}.json" - 1 month is 1 folder, each folder content 1 json log file / day.

  + {Type} - Support types: Debug, Info, Warning, Error, Fatal.
    * "Logs/{Type}/{yyyy-MM-dd}.json" - 1 file for each type / day.
        
- Support batch logs by message queue.

## Installation
- Package Manager
```
PM> Install-Package Elect.Logger
```
- .NET CLI
```
dotnet add package Elect.Logger
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Logger/).

## Usage

- Add Service
  + You can config `FilePath` (default is `Logs\{yyyy-MM-dd}.json`) by parameter [`ElectLogOptions`](Logging/Models/ElectLogOptions.cs).
```c#
services.AddElectLog();
```

- Log
    + Inject `IElectLog` to your class.
    + Call Capture methods
```c#
    _electLog.BeforeLog = (log) => {
        // Modify log info or do some logic before Elect write log
        return log;
    }
    _electLog.Capture(log);
``` 

## [View Sample](../../../samples/Logger/Elect.Sample.Logger/README.md)

## License
Elect.Logger.Models is licensed under the [MIT License](../../../LICENSE).