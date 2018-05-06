![Logo](../../../Logo.png)
# Elect.Sample.Data.EF
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

AspNetCore version 2 Web Application - Sample using [Elect.Data.EF](../../../src/Data/Elect.Data.EF/README.md).

## Structure  

- Interfaces: Unit of Work and Repository interfaces.
- Models: Contain all Entities.
- Maps: Define map rule between Entity and Table in Database.
- Services: Implement of Unit of Work, Repository and DbContext (Partial Class, DbSet content all property of DbContext).
- [`add-migration.cmd`](add-migration.cmd): script to add new migration.
- [`Startup`](Startup.cs): Register dependency for `IUnitOfWork`, `IRepository` and `IDbContext`.
- [`Program`](Program.cs): Contain all usecase, add/update/remove/get database and transaction usecase.
- `connectionconfig.json`: Database connection string base on Environment. Remember to setup it as copy always to output dictionary to make sure add migration and database connect work well.
```xml
<ItemGroup>
<Content Update="connectionconfig.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
</Content>
</ItemGroup>
```

## Installation
1. Create database "Elect.Sample" in your SQL Server and update "Development" connection string in [`connectionconfig.json`](connectionconfig.json).
2. Open [`Program`](Program.cs) set breakpoint then start project with debug to understand how the [Elect.Data.EF](../../../src/Data/Elect.Data.EF/README.md) work.

## License
Elect.Data.EF is licensed under the [MIT License](../../../LICENSE).