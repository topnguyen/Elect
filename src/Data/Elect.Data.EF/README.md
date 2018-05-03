![Logo](../../../Logo.png)
# Elect.Data.EF
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

Elect.Data.EF is extend and utilities for Entity Framework Core includes:
- Unit of Work and Repository parttern with Transaction supported.
- Entity best practise base class/interface:
  + Auditable.
  + Soft Deletable.
  + Version - Concurrency resolve.
  + Global Identity - Each row will have global id.
  + Support for both Entity with Id is `primitive` type and `string` type.
- Entity Mapping/Type Configuration utitlies.
- Model Builder extension:
  + Disable cascading delete.
  + Replace table name Convension.
  + Replace column name convension.

## Installation
- Package Manager
```
PM> Install-Package Elect.Data.EF
```
- .NET CLI
```
dotnet add package Elect.Data.EF
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Data.EF/).

## Usage

### 1. Setup Entity

### 2. Setup Entity Map/Type Configuration

### 3. Setup DbContext

### 4. Setup Repository

Elect.Data.EF serve 4 difference Repositories Interface for difference Entity type.
- [`IRepository<T>`](Interfaces/Repository/IRepository{T}.cs): Support for Entity by any type.
- [`IBaseEntityRepository<T>`](Interfaces/Repository/IBaseEntityRepository{T}.cs): Support for Entity inherit [`BaseEntity`](Models/BaseEntity.cs).
- [`IEntityRepository<T, TKey>`](Interfaces/Repository/IEntityRepository{T,TKey}.cs): Inherit [`IBaseEntityRepository<T>`](Interfaces/Repository/IBaseEntityRepository{T}.cs) and support for Entity inherit [`Entity<TKey>`](Models/Entity{TKey}.cs).
- [`IStringEntityRepository<T>`](Interfaces/Repository/IStringEntityRepository{T}.cs): Inherit [`IBaseEntityRepository<T>`](Interfaces/Repository/IBaseEntityRepository{T}.cs) and support for Entity inherit [`StringEntity`](Models/StringEntity.cs).

### 5. Setup Unit of Work and Transaction

## License
Elect.Data.EF is licensed under the [MIT License](../../../LICENSE).