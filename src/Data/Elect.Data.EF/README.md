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

### 1. Entity
Just create your entity inheric from one of base Entity below:
- [`BaseEntity`](Models/BaseEntity.cs)
- [`Entity<TKey>`](Models/Entity{TKey}.cs)
- [`Entity`](Models/Entity.cs): Inherit from [`Entity<TKey>`](Models/Entity{TKey}.cs) with `TKey` is `int`.
- [`StringEntity`](Models/StringEntity.cs)
- [`VersionEntity`](Models/VersionEntity.cs)
- [`VersionEntity<Tkey>`](Models/VersionEntity{Tkey}.cs)
- [`VersionStringEntity`](Models/VersionStringEntity.cs)

### 2. Entity Map/Type Configuration
Depend on base entity type have difference type configuration
- [`EntityTypeConfiguration<T,TKey>`](Services/Map/EntityTypeConfiguration{T,TKey}.cs)
- [`EntityTypeConfiguration<T>`](Services/Map/EntityTypeConfiguration{T}.cs)
- [`StringEntityTypeConfiguration<T>`](Services/Map/StringEntityTypeConfiguration{T}.cs)
- [`TypeConfiguration<T>`](Services/Map/TypeConfiguration{T}.cs)
- [`VersionEntityTypeConfiguration{<T,TKey>`](Services/Map/VersionEntityTypeConfiguration{T,TKey}.cs)
- [`VersionEntityTypeConfiguration<T>`](Services/Map/VersionEntityTypeConfiguration{T}.cs)
- [`VersionStringEntityTypeConfiguration<T>`](Services/Map/VersionStringEntityTypeConfiguration{T}.cs)

Then in your `DbContext` enable scan type configuration
```csharp
 protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.AddConfigFromAssembly<DbContext>(typeof(DbContext).GetTypeInfo().Assembly);
}
```

### 3. DbContext
Just inherit your `DbContext` from [`Elect.Data.EF.Services.DbContext`](Services/DbContext/DbContext.cs)

### 4. Repository

Elect.Data.EF serve 4 difference Repositories Interface for difference Entity type.
- [`IRepository<T>`](Interfaces/Repository/IRepository{T}.cs): Support for Entity by any type.
- [`IBaseEntityRepository<T>`](Interfaces/Repository/IBaseEntityRepository{T}.cs): Support for Entity inherit [`BaseEntity`](Models/BaseEntity.cs).
- [`IEntityRepository<T, TKey>`](Interfaces/Repository/IEntityRepository{T,TKey}.cs): Inherit [`IBaseEntityRepository<T>`](Interfaces/Repository/IBaseEntityRepository{T}.cs) and support for Entity inherit [`Entity<TKey>`](Models/Entity{TKey}.cs).
- [`IStringEntityRepository<T>`](Interfaces/Repository/IStringEntityRepository{T}.cs): Inherit [`IBaseEntityRepository<T>`](Interfaces/Repository/IBaseEntityRepository{T}.cs) and support for Entity inherit [`StringEntity`](Models/StringEntity.cs).

Then you can implement the repository interface above, you can save your time by inherit from base implement:
- [`Repository<T>`](Services/Repository/Repository{T}.cs): Implement `IRepository<T>`.
- [`BaseEntityRepository<T>`](Services/Repository/BaseEntityRepository{T}.cs): Implement `IBaseEntityRepository<T>`.
- [`EntityRepository<T, TKey>`](Services/Repository/EntityRepository{T,Tkey}.cs): Implement `IEntityRepository<T, TKey>`.
- [`EntityRepository<T>`](Services/Repository/EntityRepository{T}.cs): Implement `IEntityRepository<T, TKey>` with `TKey` is `int`.
- [`StringEntityRepository<T>`](Services/Repository/StringEntityRepository{T}.cs): Implement `IStringEntityRepository<T>`.

### 5. Unit of Work and Transaction

Implement [`IUnitOfWork`](Interfaces/UnitOfWork/IUnitOfWork.cs) to use Unit of Work with Transaction.

You can save your time to implement [`IUnitOfWork`](Interfaces/UnitOfWork/IUnitOfWork.cs) by inherit from base implement:
- [`UnitOfWork`](Services/UnitOfWork/UnitOfWork.cs)
- [`BaseEntityUnitOfWork`](Services/UnitOfWork/BaseEntityUnitOfWork.cs): extend from [`UnitOfWork`](Services/UnitOfWork/UnitOfWork.cs), standardize Entities (Entity inherit from [`BaseEntity`](Models/BaseEntity.cs)) - Auto fill `CreatedTime`, `LastUpdatedTime` and `DeletedTime` on `SaveChanges`.

Then in your service layer, just inject `IUnitOfWork` then you can enable Transaction and Get Repositories you need.

- Normal usecase
```csharp
var userRepo = _unitOfWork.GetRepository<UserEntity>();

var userEntity = new UserEntity{};

userRepo.Add(userEntity);

_unitOfWork.SaveChanges();
```

- Transaction usecase

```csharp
using(var transaction = _unitOfWork.BeginTransaction())
{
    // Save User
    
    var userRepo = _unitOfWork.GetRepository<UserEntity>();

    var userEntity = new UserEntity{};

    userRepo.Add(userEntity);

    // Save Change to have User Id

    _unitOfWork.SaveChanges();

    // Save Profile for User

    var profileRepo = _unitOfWork..GetRepository<profileEntity>();

    var profileEntity = new ProfileEntity{userId = userEntity.Id};

    profileRepo.Add(profileEntity);

    _unitOfWork.SaveChanges();

    // Submit Transaction

    transaction.Commit();
}
```

## License
Elect.Data.EF is licensed under the [MIT License](../../../LICENSE).