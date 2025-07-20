# Contributing to Elect

Thank you for your interest in contributing to Elect! This document provides guidelines and information for contributors.

## Code of Conduct

By participating in this project, you agree to abide by our code of conduct:
- Be respectful and inclusive
- Provide constructive feedback
- Focus on the benefit of the community
- Show empathy towards other community members

## How to Contribute

### Reporting Issues

1. **Search existing issues** first to avoid duplicates
2. **Use the issue template** if provided
3. **Provide clear reproduction steps** for bugs
4. **Include relevant environment information** (.NET version, OS, etc.)

### Submitting Pull Requests

1. **Fork the repository** and create your feature branch from `master`
2. **Write clear, concise commit messages**
3. **Include tests** for new functionality
4. **Update documentation** as needed
5. **Ensure all tests pass** before submitting
6. **Follow the coding standards** outlined below

### Development Setup

1. **Prerequisites:**
   - .NET 9.0 SDK or later
   - Visual Studio 2022+ or JetBrains Rider
   - Git

2. **Clone and build:**
   ```bash
   git clone https://github.com/topnguyen/Elect.git
   cd Elect
   dotnet restore
   dotnet build
   ```

3. **Run tests:**
   ```bash
   dotnet test
   ```

4. **Working with GlobalUsings:**
   - All `using` statements should be placed in `GlobalUsings.cs` files
   - Each project has its own `GlobalUsings.cs` for project-specific dependencies
   - Follow existing patterns when adding new global usings

## Coding Standards

### General Principles
- Follow SOLID principles
- Write clean, readable code
- Use meaningful names for variables, methods, and classes
- Keep methods small and focused
- Write comprehensive unit tests

### C# Conventions
- Use PascalCase for public members
- Use camelCase for private fields and parameters
- Use descriptive names rather than abbreviations
- Add XML documentation for public APIs
- Follow Microsoft's C# coding conventions
- Use `global using` statements in `GlobalUsings.cs` files only
- Prefer explicit types over `var` for public APIs
- Use nullable reference types where appropriate

### Project Structure
- Place related functionality in appropriate modules
- Maintain consistent namespace structure
- Keep dependencies minimal and well-defined
- Separate concerns appropriately

## Testing

### Requirements
- All new features must include unit tests
- Aim for high test coverage (90%+)
- Use MSTest framework (consistent with existing tests)
- Write integration tests for complex scenarios

### Test Organization
- Mirror the source code structure in test projects
- Name test methods descriptively
- Use Arrange-Act-Assert pattern
- Mock external dependencies

### Running Tests
```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test test/Elect.Test.Core
```

## Documentation

### Code Documentation
- Add XML documentation for all public APIs
- Include parameter descriptions and return value information
- Provide usage examples for complex features
- Document any breaking changes

### README Updates
- Update README.md when adding new features
- Include code examples for new functionality
- Keep documentation current with code changes

## Release Process

### Version Numbering
- Follow semantic versioning (MAJOR.MINOR.PATCH)
- Increment MAJOR for breaking changes
- Increment MINOR for new features
- Increment PATCH for bug fixes
- All Elect packages maintain the same version number

### NuGet Packages
- All packages are versioned together for consistency
- Update all `.nuspec` files with appropriate dependencies
- Internal Elect package dependencies must reference the same version
- Test packages locally before release
- Automated publishing via GitHub Actions on release creation

### Automated Workflows
- **CI Pipeline**: Automatically runs tests and generates coverage reports
- **NuGet Publishing**: Triggered on GitHub releases or manual dispatch
- **Quality Gates**: All tests must pass before packages are published

## Community

### Getting Help
- Check the documentation first
- Search existing issues to avoid duplicates
- Use GitHub Issues for bug reports and feature requests
- Use GitHub Discussions for questions and general discussion
- Check the project README and module-specific documentation

### Best Practices
- Be patient with review processes
- Respond to feedback constructively
- Help review other contributors' work
- Share knowledge and learnings

## License

By contributing to Elect, you agree that your contributions will be licensed under the MIT License.

## Security

If you discover a security vulnerability, please send an email to the maintainer instead of using the issue tracker. Security issues should be handled privately until a fix is available.

## Recognition

Contributors will be recognized in:
- Release notes for their contributions
- Project documentation
- GitHub Contributors section
- Special mentions for significant contributions

## Helpful Resources

- [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
- [.NET Core Testing Guidelines](https://docs.microsoft.com/en-us/dotnet/core/testing/)
- [Semantic Versioning](https://semver.org/)
- [GitHub Flow](https://guides.github.com/introduction/flow/)

Thank you for helping make Elect better! 🚀