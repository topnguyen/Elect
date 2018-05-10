![Logo](../../../Logo.png)
# Elect.Web.Swagger
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

## Installation
- Package Manager
```
PM> Install-Package Elect.Web.Swagger
```
- .NET CLI
```
dotnet add package Elect.Web.Swagger
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Web.Swagger/).

## Usage

- Enable generate XML for your project: Edit your `.csproj` add the code below, feel free to change
```xml
<!-- {namespace} is your root system namespace, eg: Elect.Web -->

<PropertyGroup>
    <DocumentationFile>{namespace}.xml</DocumentationFile>
</PropertyGroup>

<ItemGroup>
    <!-- Copy to Ouput -->
   <Content Include="{namespace}.xml">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
</ItemGroup>
```

- Add Service
  + Use can config by parameter [`ElectSwaggerOptions`](Models/ElectSwaggerOptions.cs)
```csharp
services.AddElectSwagger();
```

- Use Middleware
```csharp
app.UseElectSwagger();
```

- Config Display in API Document
  + You need specific config for Controller/Action to show in API Document by [`ShowInApiDoc`](Attributes/ShowInApiDocAttribute.cs) Attribute, if not set, not show in API Document by default.

  + Use Attribute [`ShowInApiDoc`](Attributes/ShowInApiDocAttribute.cs) in `Controller` or `Action` to Enable `Action` show in API Document.

  + Use Attribute [`HideInApiDoc`](Attributes/HideInApiDocAttribute.cs) in `Controller` or `Action` to Override [`ShowInApiDoc`](Attributes/ShowInApiDocAttribute.cs) attribute => make `Action` not show in API Document.

- Title for API Document
  + In your API `Action` need add comment `///` to display title for API in Document

- Group Configuration
  + Group Name will use the `Controller` Name by default. 

- Additional Fields for API Document
    + 

## License
Elect.Web.Swagger is licensed under the [MIT License](../../../LICENSE).