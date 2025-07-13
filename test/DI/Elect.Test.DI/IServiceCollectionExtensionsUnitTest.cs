namespace Elect.Test.DI
{
    [TestClass]
    public class ServiceCollectionExtensionsUnitTest
    {
        private IServiceCollection _services;

        [TestInitialize]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [TestMethod]
        public void Removes_RemovesRegisteredServices()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddSingleton<ITestService2, TestService2>();
            _services.Removes(typeof(ITestService), typeof(ITestService2));
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService)));
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService2)));
        }

        [TestMethod]
        public void IsRegistered_ReturnsTrueIfRegistered()
        {
            _services.AddSingleton<ITestService, TestService>();
            bool result = _services.IsRegistered<ITestService>();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsRegistered_ReturnsFalseIfNotRegistered()
        {
            bool result = _services.IsRegistered<ITestService>();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddScopedIfAny_AddsIfPredicateTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddScopedIfAny<ITestService, TestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [TestMethod]
        public void AddScopedIfAny_DoesNotAddIfPredicateFalse()
        {
            _services.AddScopedIfAny<ITestService, TestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddScopedIfAll_AddsIfAllTrue()
        {
            _services.AddScopedIfAll<ITestService, TestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [TestMethod]
        public void AddScopedIfAll_DoesNotAddIfNotAllTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddScopedIfAll<ITestService, TestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [TestMethod]
        public void AddScopedIfNotExist_AddsIfNotExist()
        {
            _services.AddScopedIfNotExist<ITestService, TestService>();
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddScopedIfNotExist_DoesNotAddIfExist()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddScopedIfNotExist<ITestService, TestService>();
            Assert.AreEqual(1, _services.Count(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddTransientIfAny_AddsIfPredicateTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddTransientIfAny<ITestService, TestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient));
        }

        [TestMethod]
        public void AddTransientIfAny_DoesNotAddIfPredicateFalse()
        {
            _services.AddTransientIfAny<ITestService, TestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddTransientIfAll_AddsIfAllTrue()
        {
            _services.AddTransientIfAll<ITestService, TestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient));
        }

        [TestMethod]
        public void AddTransientIfAll_DoesNotAddIfNotAllTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddTransientIfAll<ITestService, TestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient));
        }

        [TestMethod]
        public void AddTransientIfNotExist_AddsIfNotExist()
        {
            _services.AddTransientIfNotExist<ITestService, TestService>();
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddTransientIfNotExist_DoesNotAddIfExist()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddTransientIfNotExist<ITestService, TestService>();
            Assert.AreEqual(1, _services.Count(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddSingletonIfAny_AddsIfPredicateTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddSingletonIfAny<ITestService, TestService>(_ => true);
            Assert.IsTrue(_services.Count(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton) >= 1);
        }

        [TestMethod]
        public void AddSingletonIfAny_DoesNotAddIfPredicateFalse()
        {
            _services.AddSingletonIfAny<ITestService, TestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddSingletonIfAll_AddsIfAllTrue()
        {
            _services.AddSingletonIfAll<ITestService, TestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton));
        }

        [TestMethod]
        public void AddSingletonIfAll_DoesNotAddIfNotAllTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddSingletonIfAll<ITestService, TestService>(_ => false);
            // Should still have only the original registration
            Assert.AreEqual(1, _services.Count(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton && x.ImplementationType == typeof(TestService)));
        }

        [TestMethod]
        public void AddSingletonIfNotExist_AddsIfNotExist()
        {
            _services.AddSingletonIfNotExist<ITestService, TestService>();
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddSingletonIfNotExist_DoesNotAddIfExist()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddSingletonIfNotExist<ITestService, TestService>();
            Assert.AreEqual(1, _services.Count(x => x.ServiceType == typeof(ITestService)));
        }

        [TestMethod]
        public void AddScopedIfAny_ServiceOnly_AddsIfPredicateTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddScopedIfAny<ITestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped && x.ImplementationType == typeof(ITestService) && x.ImplementationFactory == null));
        }

        [TestMethod]
        public void AddScopedIfAny_ServiceOnly_DoesNotAddIfPredicateFalse()
        {
            _services.AddScopedIfAny<ITestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [TestMethod]
        public void AddScopedIfAll_ServiceOnly_AddsIfAllTrue()
        {
            _services.AddScopedIfAll<ITestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped && x.ImplementationType == typeof(ITestService) && x.ImplementationFactory == null));
        }

        [TestMethod]
        public void AddScopedIfAll_ServiceOnly_DoesNotAddIfNotAllTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddScopedIfAll<ITestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Scoped));
        }

        [TestMethod]
        public void AddTransientIfAny_ServiceOnly_AddsIfPredicateTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddTransientIfAny<ITestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient && x.ImplementationType == typeof(ITestService) && x.ImplementationFactory == null));
        }

        [TestMethod]
        public void AddTransientIfAny_ServiceOnly_DoesNotAddIfPredicateFalse()
        {
            _services.AddTransientIfAny<ITestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient));
        }

        [TestMethod]
        public void AddTransientIfAll_ServiceOnly_AddsIfAllTrue()
        {
            _services.AddTransientIfAll<ITestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient && x.ImplementationType == typeof(ITestService) && x.ImplementationFactory == null));
        }

        [TestMethod]
        public void AddTransientIfAll_ServiceOnly_DoesNotAddIfNotAllTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddTransientIfAll<ITestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Transient));
        }

        [TestMethod]
        public void AddSingletonIfAny_ServiceOnly_AddsIfPredicateTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddSingletonIfAny<ITestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton && x.ImplementationType == typeof(ITestService) && x.ImplementationFactory == null));
        }

        [TestMethod]
        public void AddSingletonIfAny_ServiceOnly_DoesNotAddIfPredicateFalse()
        {
            _services.AddSingletonIfAny<ITestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton && x.ImplementationType == null));
        }

        [TestMethod]
        public void AddSingletonIfAll_ServiceOnly_AddsIfAllTrue()
        {
            _services.AddSingletonIfAll<ITestService>(_ => true);
            Assert.IsTrue(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton && x.ImplementationType == typeof(ITestService) && x.ImplementationFactory == null));
        }

        [TestMethod]
        public void AddSingletonIfAll_ServiceOnly_DoesNotAddIfNotAllTrue()
        {
            _services.AddSingleton<ITestService, TestService>();
            _services.AddSingletonIfAll<ITestService>(_ => false);
            Assert.IsFalse(_services.Any(x => x.ServiceType == typeof(ITestService) && x.Lifetime == ServiceLifetime.Singleton && x.ImplementationType == null));
        }
    }

    public interface ITestService { }
    public class TestService : ITestService { }
    public interface ITestService2 { }
    public class TestService2 : ITestService2 { }
}
