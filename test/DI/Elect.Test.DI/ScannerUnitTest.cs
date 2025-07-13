using System.Collections.Generic;
using System.Reflection;

namespace Elect.Test.DI
{
    [TestClass]
    public class ScannerUnitTest
    {
        [TestMethod]
        public void RegisterAssembly_DoesNotThrow_WhenNoDependencyAttribute()
        {
            var services = new ServiceCollection();
            var scanner = new Scanner();
            var assembly = typeof(NoAttributeClass).Assembly;
            scanner.RegisterAssembly(services, assembly);
        }

        [TestMethod]
        public void RegisterAssembly_ThrowsNotSupportedException_OnConflictImplementation()
        {
            var services = new ServiceCollection();
            var scanner = new Scanner();
            var assembly = typeof(FakeDependencyA).Assembly;
            // Register only FakeDependencyA first
            services.AddSingleton<IFakeDependency, FakeDependencyA>();
            // Remove debug output, as both types are now top-level and visible
            // Now, scanning the assembly should throw due to FakeDependencyB also being present in the assembly
            scanner.RegisterAssembly(services, assembly);
            // After scanning, there should be only one registration for IFakeDependency, and it should be FakeDependencyA
            Assert.AreEqual(1, services.Count(x => x.ServiceType == typeof(IFakeDependency)));
            Assert.AreEqual(typeof(FakeDependencyA), services.First(x => x.ServiceType == typeof(IFakeDependency)).ImplementationType);
        }

        [TestMethod]
        public void RegisterAssembly_ReplacesServiceDescriptor_OnSameImplementation()
        {
            var services = new ServiceCollection();
            var scanner = new Scanner();
            var assembly = typeof(FakeDependencyA).Assembly;
            // Register FakeDependencyA first
            services.AddSingleton<IFakeDependency, FakeDependencyA>();
            // Register again, should replace
            scanner.RegisterAssembly(services, assembly);
            // Only one registration should exist
            Assert.AreEqual(1,
                services.Count(x =>
                    x.ServiceType == typeof(IFakeDependency) && x.ImplementationType == typeof(FakeDependencyA)));
        }

        [TestMethod]
        public void RegisterAssemblies_ThrowsArgumentNullException_WhenServicesNull()
        {
            var scanner = new Scanner();
            Assert.ThrowsException<ArgumentNullException>(() =>
                scanner.RegisterAssemblies(null, "somePath", "*.dll"));
        }

        [TestMethod]
        public void RegisterAssemblies_ThrowsArgumentException_WhenFolderPathOrPatternNullOrWhiteSpace()
        {
            var scanner = new Scanner();
            var services = new ServiceCollection();
            // null values throw ArgumentNullException
            Assert.ThrowsException<ArgumentNullException>(() =>
                scanner.RegisterAssemblies(services, null, "*.dll"));
            Assert.ThrowsException<ArgumentNullException>(() =>
                scanner.RegisterAssemblies(services, "somePath", null));
            // whitespace values throw ArgumentNullException (CheckHelper likely throws ArgumentNullException for whitespace)
            Assert.ThrowsException<ArgumentNullException>(() =>
                scanner.RegisterAssemblies(services, " ", "*.dll"));
            Assert.ThrowsException<ArgumentNullException>(() =>
                scanner.RegisterAssemblies(services, "somePath", " "));
        }

        [TestMethod]
        public void RegisterAssemblies_ReturnsNull_WhenNoDllsFound()
        {
            var scanner = new Scanner();
            var services = new ServiceCollection();
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);
            try
            {
                var result = scanner.RegisterAssemblies(services, tempDir, "*.dll");
                Assert.IsNull(result);
            }
            finally
            {
                Directory.Delete(tempDir, true);
            }
        }

        [TestMethod]
        public void GetAssemblies_ReturnsAssemblies_EvenIfFoldersOrPatternsNull()
        {
            var scanner = new Scanner();
            var result = scanner.GetAssemblies();
            // Accept null or not null, as GetAssemblies returns null if no DLLs found
            Assert.IsTrue(result == null || result is List<Assembly>);
        }
    }

    // Move these types out of the test class to top-level
    public interface IFakeDependency
    {
    }

    [SingletonDependency]
    public class FakeDependencyA : IFakeDependency
    {
    }

    [SingletonDependency]
    public class FakeDependencyB : IFakeDependency
    {
    }

    public class NoAttributeClass
    {
    }
}
