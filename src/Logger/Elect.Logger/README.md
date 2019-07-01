![Logo](../../../Logo.png)
# Elect.Logger
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview
 - Support write log to Json files with parameters
    + {`<DateTimeFormat>`} - current Local Machine datetime.
        * "Logs/{yyyy-MM-dd}.json" - 1 file / day
        * "Logs/{yyyy-MM-dd HH}.json" - 1 file / hour
        * "Logs/{yyyy-MM-dd HH_mm}.json" - 1 file / minute.
        * "Logs/{yyyy-MM}/{yyyy-MM-dd}.json" - 1 month is 1 folder, each folder content 1 json log file / day.

    + {Type} - Support types: Debug, Info, Warning, Error, Fatal.
        * "Logs/{Type}/{yyyy-MM-dd}.json" - 1 file for each type / day.
        
 - Support batch logs by message queue.

 - Support filters
    + Credit card number will filter and repalce to `"####-CC-CENSORED-####"`.

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
    + Limit log information (remove "runtime", "environmentModel", "sdk", "httpContext") by property `IsLogFullInfo` (bool), default is false.
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
    
    try
    {
       // Your logic code
    }
    catch(Exception e)
    {
       // Use setting for json file path
       _electLog.Capture(e); // You can capture an Exception, LogModel, String message
        
       // Override setting for json file path
       // Still apply the file name rule on Overview section
       _electLog.Capture(e, jsonFilePath: "Custom Path"); 
    }
    ``` 

 - **Access Log Dashboard**
    + Use Middleware: `app.UseElectLog()`.
    + By default, Log Dashboard URL is "/developers/logs".
    + Support filter log detail by query strings 
        * "skip" (int): default 0
        * "take" (int): default 1000
        * "type" (string)
        * "exception_place" (string)
        * "message" (string)
    + Limit display information (remove "runtime", "environmentModel", "sdk", "httpContext") by query string "full_info" (bool), default is false.
    + Delete the log file by query string "delete_file" (bool), default is false.
    + You can config access key to control access permission by the [`ElectLogOptions`](Logging/Models/ElectLogOptions.cs).

## [View Sample Log Web App](../../../samples/Web/Elect.Sample.Web/README.md)

## [View Sample Log Console App](../../../samples/Logger/Elect.Sample.Logger/README.md)

## Sample Output
```json
{
  "metadata": [
    {
      "createdTime": "2018-06-21T17:07:27.567827+07:00",
      "lastUpdatedTime": "2018-06-21T17:07:27.567827+07:00"
    }
  ],
  "logs": [
    {
      "id": "69307a85-7034-4e4c-b583-5bd8261ef6c6",
      "createdTime": "2018-06-21T17:07:22.710494+07:00",
      "type": 3,
      "message": "\"message credit cart is ####-CC-CENSORED-####\"",
      "exceptions": null,
      "exceptionPlace": null,
      "runtime": {
        "appName": "Elect.Sample.Web",
        "appVersion": "1.0.0.0",
        "referencePackages": [
          {
            "name": "Elect.Core",
            "version": "1.0.0.0"
          },
          {
            "name": "Elect.Logger",
            "version": "1.0.0.0"
          },
          {
            "name": "Elect.Logger.Models",
            "version": "1.0.0.0"
          },
          {
            "name": "Elect.Sample.Web",
            "version": "1.0.0.0"
          },
          {
            "name": "Elect.Sample.Web.Views",
            "version": "1.0.0.0"
          },
          {
            "name": "Elect.Web",
            "version": "1.0.0.0"
          },
          {
            "name": "Microsoft.AspNetCore",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Antiforgery",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Authentication",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Authentication.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Authentication.Cookies",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Authentication.Core",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Authorization",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Authorization.Policy",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Connections.Abstractions",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Cors",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Cryptography.Internal",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.DataProtection",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.DataProtection.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Diagnostics.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.HostFiltering",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Hosting",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Hosting.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Hosting.Server.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Html.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Http",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Http.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Http.Extensions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Http.Features",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Identity.UI",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Identity.UI.Views",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.JsonPatch",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.ApiExplorer",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.Core",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.Cors",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.DataAnnotations",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.Formatters.Json",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.Razor",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.Razor.Extensions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.RazorPages",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.TagHelpers",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Mvc.ViewFeatures",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Razor",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Razor.Language",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Razor.Runtime",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Routing",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Routing.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.AspNetCore.Server.IISIntegration",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Server.Kestrel",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Server.Kestrel.Core",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.SpaServices",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.SpaServices.Extensions",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.AspNetCore.StaticFiles",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.CodeAnalysis",
            "version": "2.8.0.0"
          },
          {
            "name": "Microsoft.CodeAnalysis.CSharp",
            "version": "2.8.0.0"
          },
          {
            "name": "Microsoft.CodeAnalysis.Razor",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Caching.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Caching.Memory",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration.Binder",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration.CommandLine",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration.EnvironmentVariables",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration.FileExtensions",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Configuration.Json",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.DependencyInjection",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.DependencyInjection.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.DependencyModel",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.FileProviders.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.FileProviders.Composite",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.FileProviders.Physical",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.FileSystemGlobbing",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Hosting.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Localization.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Logging",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Logging.Abstractions",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Logging.Configuration",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Logging.Console",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Logging.Debug",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.ObjectPool",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Options",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.Options.ConfigurationExtensions",
            "version": "2.1.0.0"
          },
          {
            "name": "Microsoft.Extensions.Primitives",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Extensions.WebEncoders",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Net.Http.Headers",
            "version": "2.1.1.0"
          },
          {
            "name": "Microsoft.Win32.Registry",
            "version": "4.1.1.0"
          },
          {
            "name": "netstandard",
            "version": "2.0.0.0"
          },
          {
            "name": "Newtonsoft.Json",
            "version": "11.0.0.0"
          },
          {
            "name": "System.AppContext",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Buffers",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Collections",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Collections.Concurrent",
            "version": "4.0.14.0"
          },
          {
            "name": "System.Collections.Immutable",
            "version": "1.2.3.0"
          },
          {
            "name": "System.Collections.NonGeneric",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Collections.Specialized",
            "version": "4.1.1.0"
          },
          {
            "name": "System.ComponentModel",
            "version": "4.0.3.0"
          },
          {
            "name": "System.ComponentModel.Annotations",
            "version": "4.2.1.0"
          },
          {
            "name": "System.ComponentModel.Primitives",
            "version": "4.2.1.0"
          },
          {
            "name": "System.ComponentModel.TypeConverter",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Console",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Data.Common",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Diagnostics.Debug",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Diagnostics.DiagnosticSource",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Diagnostics.Process",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Diagnostics.TraceSource",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Diagnostics.Tracing",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Drawing.Primitives",
            "version": "4.2.0.0"
          },
          {
            "name": "System.IO",
            "version": "4.2.1.0"
          },
          {
            "name": "System.IO.FileSystem",
            "version": "4.1.1.0"
          },
          {
            "name": "System.IO.FileSystem.Primitives",
            "version": "4.1.1.0"
          },
          {
            "name": "System.IO.FileSystem.Watcher",
            "version": "4.1.1.0"
          },
          {
            "name": "System.IO.Pipelines",
            "version": "4.0.0.0"
          },
          {
            "name": "System.Linq",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Linq.Expressions",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Memory",
            "version": "4.1.0.0"
          },
          {
            "name": "System.Net.Primitives",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Net.Sockets",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Numerics.Vectors",
            "version": "4.1.4.0"
          },
          {
            "name": "System.ObjectModel",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Private.CoreLib",
            "version": "4.0.0.0"
          },
          {
            "name": "System.Private.Uri",
            "version": "4.0.5.0"
          },
          {
            "name": "System.Private.Xml",
            "version": "4.0.1.0"
          },
          {
            "name": "System.Private.Xml.Linq",
            "version": "4.0.1.0"
          },
          {
            "name": "System.Reflection",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Reflection.Emit.ILGeneration",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Reflection.Emit.Lightweight",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Reflection.Primitives",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Resources.ResourceManager",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Runtime",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Runtime.CompilerServices.Unsafe",
            "version": "4.0.4.0"
          },
          {
            "name": "System.Runtime.Extensions",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Runtime.InteropServices",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Runtime.InteropServices.RuntimeInformation",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Runtime.Numerics",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Runtime.Serialization.Formatters",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Runtime.Serialization.Primitives",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Security.Claims",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Security.Cryptography.Algorithms",
            "version": "4.3.1.0"
          },
          {
            "name": "System.Security.Cryptography.Encoding",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Security.Cryptography.Primitives",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Security.Cryptography.X509Certificates",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Security.Principal",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Text.Encoding.Extensions",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Text.Encodings.Web",
            "version": "4.0.3.0"
          },
          {
            "name": "System.Text.RegularExpressions",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Threading",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Threading.Tasks",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Threading.Tasks.Extensions",
            "version": "4.3.0.0"
          },
          {
            "name": "System.Threading.Thread",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Threading.ThreadPool",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Threading.Timer",
            "version": "4.1.1.0"
          },
          {
            "name": "System.Xml.ReaderWriter",
            "version": "4.2.1.0"
          },
          {
            "name": "System.Xml.XDocument",
            "version": "4.1.1.0"
          }
        ],
        "processStartTime": "2018-06-21T17:06:57.917983+07:00",
        "processArchitecture": "X64",
        "machineName": "TopNguyen",
        "machineUserName": "Top"
      },
      "environmentModel": {
        "osName": "OSX",
        "osVersion": "Unix 17.6.0.0",
        "osDescription": "Darwin 17.6.0 Darwin Kernel Version 17.6.0: Tue May  8 15:22:16 PDT 2018; root:xnu-4570.61.1~1/RELEASE_X86_64",
        "osArchitecture": "X64",
        "osTimeZoneId": "Asia/Saigon",
        "osTimeZone": "07:00:00",
        "osTimeZoneDisplay": "GMT+07:00",
        "osBootTime": "2017-12-24T16:44:50.09394+07:00",
        "frameworkName": ".NET Core",
        "frameworkVersion": "2.1.0",
        "frameworkDescription": ".NET Core 4.6.26515.07"
      },
      "sdk": {
        "name": "Elect.Logger",
        "version": "1.0.0.0"
      },
      "httpContext": null,
      "additionalData": {}
    }
  ]
}
```

## License
Elect.Logger is licensed under the [MIT License](../../../LICENSE).