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
  + Group Name will use the `Controller` Name by default. If you want to change it, use  [`ApiDocGroup`](Attributes/ApiDocGroupAttribute.cs) to config the group name.
  + You can config multiple Group Name for `Action`. So the `Action` will appear in multiple groups.

- Additional Parameters for API Document
    + You can add additional parameter by [`ApiParameter`](Attributes/ApiParameterAttribute.cs)
    + View more in [swagger document](https://swagger.io/docs/specification/describing-parameters/).

- Access API Document
    + By defaul, API Document URL is "/developers".

- Advance Configuration
  + If you want to add more config for `SwaggerGenOptions` after Elect.Web.Swagger config it. Just add your config in `ExtendOptions` Property of [`ElectSwaggerOptions`](Models/ElectSwaggerOptions.cs).

## [View Sample](../../../samples/Web/Elect.Sample.Web.Swagger/README.md)

## License
Elect.Web.Swagger is licensed under the [MIT License](../../../LICENSE).