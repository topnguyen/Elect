![Logo](../../Logo.png)
# Elect.Core
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview
- .Net Core Utilities methods.
- Core of all Elect modules, standalize ecosystem.

## Installation
- Package Manager
```
PM> Install-Package Elect.Core
```
- .NET CLI
```
dotnet add package Elect.Core
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Core/).

## Usage
- Almost Utilies have "Helper" and "Extensions", "Extensions" will call method insite "Helper" so feel free to choice one of them to use.

- Utitlies
  + Action
    + [Action Helper](ActionUtils/ActionHelper.cs)
    + [Action Extensions](ActionUtils/ActionExtensions.cs)
  + Assembly
    + [Assembly Helper](AssemblyUtils/AssemblyHelper.cs)
    + [Assembly Extensions](AssemblyUtils/AssemblyExtensions.cs)
    + [Assembly Loader](AssemblyUtils/AssemblyLoader.cs): Load assembly from folder.
  + Attributes
    + [CanBeNull](Attributes/CanBeNullAttribute.cs): Indicates that the value of the marked element could be `null` sometimes, so the check for `null` is necessary before its usage.
    + [NotNull](Attributes/CanBeNullAttribute.cs): Indicates that the value of the marked element could never be `null`.
  + Check - Validator
    + [Check Helper](CheckUtils/CheckHelper.cs): Check parameter value and throw exception if invalid data.
  + Configuration: Extend of `IConfiguration`
    + [Configuration Helper](ConfigUtils/IConfigurationHelper.cs): Support load configuration depend on Machine/Window User Name or Environment.
    + [Configuration Extensions](ConfigUtils/IConfigurationExtensions.cs)
  + Constants
    + [Configuration File Name](Constants/ConfigurationFileName.cs): Content all best practise configuration file name such as: "appsettings.json", "connectionconfig.json" and so on. Use this constants to prevent "hard string" and mistake relate to it in your code.
     + [Configuration Section Name](Constants/ConfigurationSectionName.cs): Content all best practise section name in configuration file, such as: "ConnectionStrings" and so on. Use this constants to prevent "hard string" and mistake relate to it in your code.
     + [Formatting](Constants/Formatting.cs): Content all best practise format such as: "JsonSerializerSettings", DateTime format and so on. Use this to prevent difference format among your modules.
  + DateTime
    + [DateTime Helper](DateTimeUtils/DateTimeHelper.cs)
    + [DateTime Extensions](DateTimeUtils/DateTimeExtensions.cs)
  + Dictionary
    + [Dictionary Helper](DictionaryUtils/DictionaryHelper.cs)
    + [Dictionary Extensions](DictionaryUtils/DirectoryExtensions.cs)
  + Environment
    + [Environment Helper](EnvUtils/EnvHelper.cs): Get and check methods for current/run time environment.
  + Guid
    + [Guid Helper](GuidUtils/GuidHelper.cs)  
  + JsonContractResolver: Extend of Newtonsoft.Json
    + [Without Virtual Contract Resolver](JsonContractResolver/WithoutVirtualContractResolver.cs): Remove all `virtual` properties insite object when serialize it.
  + JsonConverters: Extend of Newtonsoft.Json
    + [Object to Array](JsonConverters/ObjectToArrayConverter.cs): Convert object/property (complex type) to array. Eg: result like "["string", 1, true]".
  + Linq
    + [Linq Extensions](LinqUtils/LinqExtensions.cs): Extend linq such as: `DistinctBy` and `RemoveWhere`. 
  + Object
    + [Object Helper](ObjUtils/ObjHelper.cs)
    + [Object Extensions](ObjUtils/ObjExtensions.cs)
  + Security
    + [Security Helper](SecurityUtils/SecurityHelper.cs): Support generate salt, hash, encrypt/descrypt.
  + String
    + [String Helper](StringUtils/StringHelper.cs): Support random, normlaize and base 64 format handle.
  + Type
    + [Type Helper](TypeUtils/TypeHelper.cs)
    + [Type Extensions](TypeUtils/TypeExtensions.cs)
  + XML
    + [Xml Helper](XmlUtils/XmlHelper.cs)
    + [XDocument Extensions](XmlUtils/XDocumentExtensions.cs)

## License
Elect.Core is licensed under the [MIT License](../../LICENSE).