![Logo](../../../Logo.png)
# Elect.Web.DataTable
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Jquery DataTable generate by Asp Net Core Server.

- Support global and column filter.
- Support sort by column.
- Support column visible configuration.
- Support Localization.

## Installation
- Package Manager
```
PM> Install-Package Elect.Web.DataTable
```
- .NET CLI
```
dotnet add package Elect.Web.DataTable
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Web.DataTable/).

## Usage

1. Config master UI and Layout for DataTable.
  - Copy the [DataTable Assets](Assets/Sample.cshtml) to your project.
  - Feel free to change the config adapt with your UI.

2. Config DataTable options for specific Model
  - Each DataTable will have a Model map with Class in C#.
  - Use [DataTable](Attributes/DataTableAttribute.cs) Attribute to config column in Model Property.
  - You can mask the property not generate to column in DataTable by [DataTableIgnore](Attributes/DataTableIgnoreAttribute.cs) Attribute
  - You can config the row id follow data of a property instead of default increase row id by [DataTableRowId](Attributes/DataTableRowIdAttribute.cs) Attribute.

## [View Sample](../../../samples/Web/Elect.Sample.Web.DataTable/README.md)

## License
Elect.Web.DataTable is licensed under the [MIT License](../../../LICENSE).