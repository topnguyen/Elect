![Logo](Logo.png)

# Elect - .NET Core Utility Library Collection

[![NuGet](https://img.shields.io/nuget/v/Elect.Core.svg)](https://www.nuget.org/packages?q=TopNguyen)
[![Downloads](https://img.shields.io/nuget/dt/Elect.Core.svg)](https://www.nuget.org/packages?q=TopNguyen)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Build Status](https://github.com/topnguyen/Elect/workflows/CI/badge.svg)](https://github.com/topnguyen/Elect/actions)

> **Created by [Top Nguyen](http://topnguyen.com)** - A comprehensive collection of .NET Core utilities designed to accelerate development and focus on business logic.

## 🚀 Overview

Elect is a powerful ecosystem of mini-libraries that significantly speeds up .NET Core development by providing:

- **🔧 Essential Utilities** - Core helper methods and extensions for common tasks
- **⚡ Rapid Development** - Pre-built solutions to reduce project setup time
- **🔄 Always Updated** - Continuously maintained with the latest .NET versions
- **🌐 Cross-Platform** - Works seamlessly on Windows, Mac, and Linux
- **📦 Modular Design** - Use only what you need with independent packages

## 📦 Installation

All Elect modules are available on [NuGet](https://www.nuget.org/packages?q=TopNguyen). Install individual packages as needed:

```bash
# Core utilities (foundation)
dotnet add package Elect.Core

# Dependency injection
dotnet add package Elect.DI

# Entity Framework utilities
dotnet add package Elect.Data.EF

# File and image processing
dotnet add package Elect.Data.IO

# Background job processing
dotnet add package Elect.Job.Hangfire

# Location services
dotnet add package Elect.Location.Google

# Web utilities
dotnet add package Elect.Web
```

## 🔧 Quick Start

```csharp
// Example: Using Elect.Core utilities
using Elect.Core.StringUtils;
using Elect.Core.ObjUtils;

// Generate unique IDs
string uniqueId = IdHelper.NewId();

// Object utilities
var config = new AppConfig();
config.SetValue("ConnectionString", "Data Source=...");

// Example: Using Elect.DI for service registration
[TransientDependency]
public class MyService : IMyService
{
    // Your implementation
}

// Services are automatically registered by assembly scanning
```

## 📚 Library Modules

Elect provides a comprehensive set of specialized modules, each designed to handle specific aspects of .NET Core development:

### 🏗️ Core Foundation
| Package | Description | NuGet |
|---------|-------------|-------|
| **[Elect.Core](https://github.com/topnguyen/Elect/tree/master/src/Elect.Core/README.md)** | Essential utilities and foundation of the ecosystem | [![NuGet](https://img.shields.io/nuget/v/Elect.Core.svg)](https://www.nuget.org/packages/Elect.Core) |

### 🔧 Dependency Injection
| Package | Description | NuGet |
|---------|-------------|-------|
| **[Elect.DI](https://github.com/topnguyen/Elect/tree/master/src/DI/Elect.DI/README.md)** | Attribute-based service registration with assembly scanning | [![NuGet](https://img.shields.io/nuget/v/Elect.DI.svg)](https://www.nuget.org/packages/Elect.DI) |

### 🗃️ Data Access
| Package | Description | NuGet |
|---------|-------------|-------|
| **[Elect.Data.EF](https://github.com/topnguyen/Elect/tree/master/src/Data/Elect.Data.EF/README.md)** | Entity Framework with Unit of Work & Repository patterns | [![NuGet](https://img.shields.io/nuget/v/Elect.Data.EF.svg)](https://www.nuget.org/packages/Elect.Data.EF) |
| **[Elect.Data.IO](https://github.com/topnguyen/Elect/tree/master/src/Data/Elect.Data.IO/README.md)** | File system utilities, image processing (resize, compress) | [![NuGet](https://img.shields.io/nuget/v/Elect.Data.IO.svg)](https://www.nuget.org/packages/Elect.Data.IO) |

### 🌐 Web Development
| Package | Description | NuGet |
|---------|-------------|-------|
| **[Elect.Web](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web/README.md)** | ASP.NET Core utilities with beautiful paged collections | [![NuGet](https://img.shields.io/nuget/v/Elect.Web.svg)](https://www.nuget.org/packages/Elect.Web) |
| **[Elect.Web.Middlewares](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Middlewares/README.md)** | Performance & optimization middlewares | [![NuGet](https://img.shields.io/nuget/v/Elect.Web.Middlewares.svg)](https://www.nuget.org/packages/Elect.Web.Middlewares) |
| **[Elect.Web.DataTable](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.DataTable/README.md)** | Server-side [jQuery DataTable](https://datatables.net/) with advanced features | [![NuGet](https://img.shields.io/nuget/v/Elect.Web.DataTable.svg)](https://www.nuget.org/packages/Elect.Web.DataTable) |
| **[Elect.Web.HttpDetection](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.HttpDetection/README.md)** | Device & location detection via HTTP requests | [![NuGet](https://img.shields.io/nuget/v/Elect.Web.HttpDetection.svg)](https://www.nuget.org/packages/Elect.Web.HttpDetection) |
| **[Elect.Web.Ajaxify](https://github.com/topnguyen/Elect/tree/master/src/Web/Elect.Web.Ajaxify/README.md)** | Convert multi-page websites to single-page applications | [![NuGet](https://img.shields.io/nuget/v/Elect.Web.Ajaxify.svg)](https://www.nuget.org/packages/Elect.Web.Ajaxify) |

### ⚙️ Background Processing
| Package | Description | NuGet |
|---------|-------------|-------|
| **[Elect.Job.Hangfire](https://github.com/topnguyen/Elect/tree/master/src/Job/Elect.Job.Hangfire/README.md)** | Enhanced [Hangfire](https://github.com/HangfireIO/Hangfire) with easier setup & security | [![NuGet](https://img.shields.io/nuget/v/Elect.Job.Hangfire.svg)](https://www.nuget.org/packages/Elect.Job.Hangfire) |

### 📍 Location Services
| Package | Description | NuGet |
|---------|-------------|-------|
| **[Elect.Location.Models](https://github.com/topnguyen/Elect/tree/master/src/Location/Elect.Location.Models/README.md)** | Common location data models | [![NuGet](https://img.shields.io/nuget/v/Elect.Location.Models.svg)](https://www.nuget.org/packages/Elect.Location.Models) |
| **[Elect.Location.Coordinate](https://github.com/topnguyen/Elect/tree/master/src/Location/Elect.Location.Coordinate/README.md)** | Coordinate calculations and clustering | [![NuGet](https://img.shields.io/nuget/v/Elect.Location.Coordinate.svg)](https://www.nuget.org/packages/Elect.Location.Coordinate) |
| **[Elect.Location.Google](https://github.com/topnguyen/Elect/tree/master/src/Location/Elect.Location.Google/README.md)** | Google Maps API integration | [![NuGet](https://img.shields.io/nuget/v/Elect.Location.Google.svg)](https://www.nuget.org/packages/Elect.Location.Google) |

## 🛠️ Requirements

- **.NET 9.0** or later
- **Visual Studio 2022** or **JetBrains Rider** (recommended)
- **Cross-platform:** Windows, macOS, Linux

## 📖 Documentation & Samples

- **📁 [Sample Projects](./samples/)** - Comprehensive examples for each library
- **📄 [Module Documentation](./src/)** - Detailed documentation for each package
- **🚀 [Contributing Guide](./CONTRIBUTING.md)** - How to contribute to the project

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details on:
- How to submit issues and feature requests
- Development setup and coding standards
- Pull request process

## 📊 Project Stats

- **🧪 1,400+ Unit Tests** with 90%+ coverage
- **📦 13 NuGet Packages** with consistent versioning
- **🔄 Continuous Integration** with automated testing
- **📈 Production Ready** - Used in enterprise applications

## 📄 License

This project is licensed under the **[MIT License](LICENSE)** - see the license file for details.

## 🙏 Acknowledgments

- Built with ❤️ by [Top Nguyen](http://topnguyen.com)
- Special thanks to all [contributors](https://github.com/topnguyen/Elect/graphs/contributors)
- Powered by the .NET ecosystem

---

⭐ **If you find this project useful, please give it a star!** ⭐
