![Logo](../../../Logo.png)
# Elect.Mapper.AutoMapper
> Author [**Top Nguyen**](http://topnguyen.net)

## Overview

- Extend for AutoMapper, save your time to register profiles to AutoMapper by Scanner.
- Support more utitlies for mapping.
  + `.IgnoreAllNonExisting()`: Ignore all non existing between 2 classes.
  + `.MapTo<T>()`: Create new instance of `T` and map data from source.
  + `.MapTo()`: Map data from source to existing destination instance.
  + `.QueryTo<T>()`: Support `IQueryable` map data to destination `T` type, useful for query data in Entity Framework Core.

## Installation
- Package Manager
```
PM> Install-Package Elect.Mapper.AutoMapper
```
- .NET CLI
```
dotnet add package Elect.Mapper.AutoMapper
```

See more information in [Nuget Package](https://www.nuget.org/packages/Elect.Mapper.AutoMapper/).

## Usage

- `Startup.cs`: add `Scanner` - Auto register auto mapper profiles.
```csharp
 public void ConfigureServices(IServiceCollection services)
{
    // Add Auto Mapper Services and Auto Register Auto Mapper Profiles
    services.AddElectAutoMaper();
}
```

- Auto Mapper Profile: Create class and inherit `AutoMapper.Profile`
```csharp
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .IgnoreAllNonExisting()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.Profile.FullName));
    }
}
```

- `MapTo<T>()` and `MapTo()`
```csharp
// Map from UserEntity to UserModel
var userEntity = new UserEntity{Id = 1};

// Create new instance of UserModel with data from UserEntity.
var userModel = userEntity.MapTo<UserModel>(); 

var userEntity2 = new UserEntity{Id = 2};

// Update userModel by userEntity data.
userEntity2.MapTo(userModel);
```

- `QueryTo<T>()` to map data from `IQueryable` - useful for query data form Entity Framework Core.
```csharp
var query = _userRepo.Get(x => x.DeletedTime == null);

// Work same way with ".Select(x => new UserModel{...})" to select alias from Database.
var listUserModel = query.QueryTo<UserModel>().ToList();
```

## [View Sample](../../../samples/Mapper/Elect.Sample.Mapper.AutoMapper/README.md)

## License
Elect.Mapper.AutoMapper is licensed under the [MIT License](../../../LICENSE).