![Logo](../../../Logo.png)
# Elect.Consul
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview
- Support auto Register and Deregister Service to the Consul Service.
- Auto generate Name of Service, ID and Tags support for Fabio Load Balancing.

## Installation

### Consul Service
- [Download Consul](https://consul.io)
    + If MacOS, can install via brew
    ```cmd
    brew install consul
    ```
- Create a setting file named 'consul-config.json'
    ```json
    {
      "primary_datacenter": "dc1",
      "data_dir": "<directory folder path>",
      "server": true,
      "bootstrap": true,
      "ui": true,
      "ports": {
        "http": 8500
      },
      "acl" : {
        "enabled": true,
        "default_policy": "deny",
        "enable_token_persistence": true,
        "tokens": {
          "default": "<your generation unique key>"
        }
      }
    }
    ```
    + Please adjust the `data_dir` (Folder to save Consul Data) and `acl.tokens.default` (Access Token).
        * The `acl.tokens.default` use for protect both UI and Agent services.
        * When you access the Consul Service, need key-in the token to view the registered services.

- Run Consul Agent with config
    ```cmd
    consul agent -config-file="<Your Path>/consul-config.json"
    ```

- Consul by default
    + The UI and Agent run on http://localhost:8500

### Fabio Service (Optional)
- [Download Fabio](https://github.com/fabiolb/fabio/releases)
    + If MacOS, can install via brew
    ```cmd
    brew install fabio
    ```
- Run Fabio
    ```cmd
    fabio -registry.consul.addr="<your consul endpoint>" -registry.consul.token="<your consul token>"
    ```

- Consul by default
    + Fabio proxy run on http://localhost:9999 and the UI will run on http://localhost:9998
    + Fabio use Consul run on http://localhost:8500

### Elect Consul Nuget
- Package Manager
    ```
    PM> Install-Package Elect.Consul
    ```

- .NET CLI
    ```
    dotnet add package Elect.Consul
    ```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Consul/).

## Usage

- Add Service
    + You can config endpoint via [`ElectConsulOptions`](Models/ElectConsulOptions.cs).
    + By defaut already bind to the default endpoint, port from Consul Service.
    ```c#
    services.AddElectHealthCheck();
    services.AddElectConsul();
    ```
- Use Service
    ```c#
    app.UseElectHealthCheck();
    ```
- Note
    + You must add the [Elect.HealthCheck](https://www.nuget.org/packages/Elect.HealthCheck/) service before use Elect.Consul service.
    + The HealCheck needed for healthy check to make Fabio Service work well.
        > Fabio only load balancing for pass health check services.
        
## License
Elect.Consul is licensed under the [MIT License](../../../LICENSE).