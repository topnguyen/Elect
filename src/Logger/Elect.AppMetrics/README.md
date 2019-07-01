![Logo](../../../Logo.png)
# Elect.AppMetrics
> Author [**Top Nguyen**](http://topnguyen.com)

## Overview
 - Support enable and config to the AppMetrics Service automatically.

## Installation

### Install InfluxDB or Prometheus (Optional)
 - InfluxDB
    + Download InfluxDB from https://www.influxdata.com/get-influxdb/
    + Run InfluxDB by `influxd.exe`
        ```
        influxd.exe
        ```
 - Prometheus
    + Download Prometheus from https://prometheus.io/download/ or can use `brew install prometheus`
    + Config [yml config file](https://trello-attachments.s3.amazonaws.com/5aa9f07ca83e022bcd047d69/5d0ba7268178d077c2436f1f/3a373d4439528043dbbd97ed99e6b188/prometheus.yml) for Prometheus
    + Run Cli
        ```
        prometheus --config.file="<your path>/prometheus.yml"
        ```
        + Default Prometheus run on localhost:9090

### Elect AppMetrics Nuget
 - Package Manager
    ```
    PM> Install-Package Elect.AppMetrics
    ```

 - .NET CLI
    ```
    dotnet add package Elect.AppMetrics
    ```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.AppMetrics/).

## Usage

 - In `Program.cs` add AppMetrics
    ```c#
    webHostBuilder.UseElectAppMetrics("ElectAppMetrics");
    ```
    + You can config endpoints, influxdb / prometheus via [`ElectAppMetricsOptions`](Models/ElectAppMetricsOptions.cs).


## License
Elect.AppMetrics is licensed under the [MIT License](../../../LICENSE).