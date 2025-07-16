namespace Elect.Test.Location.Google
{
    [TestClass]
    public class IServiceCollectionExtensionsUnitTest
    {
        [TestMethod]
        public void AddElectLocationGoogle_Default_RegistersServices()
        {
            var services = new ServiceCollection();
            
            services.AddElectLocationGoogle();
            
            var serviceProvider = services.BuildServiceProvider();
            var client = serviceProvider.GetService<IElectGoogleClient>();
            
            Assert.IsNotNull(client);
            Assert.IsInstanceOfType(client, typeof(ElectGoogleClient));
        }

        [TestMethod]
        public void AddElectLocationGoogle_WithOptions_RegistersServices()
        {
            var services = new ServiceCollection();
            var options = new ElectLocationGoogleOptions { GoogleApiKey = "test-api-key" };
            
            services.AddElectLocationGoogle(options);
            
            var serviceProvider = services.BuildServiceProvider();
            var client = serviceProvider.GetService<IElectGoogleClient>();
            var configuredOptions = serviceProvider.GetService<IOptions<ElectLocationGoogleOptions>>();
            
            Assert.IsNotNull(client);
            Assert.IsInstanceOfType(client, typeof(ElectGoogleClient));
            Assert.IsNotNull(configuredOptions);
            Assert.AreEqual("test-api-key", configuredOptions.Value.GoogleApiKey);
        }

        [TestMethod]
        public void AddElectLocationGoogle_WithAction_RegistersServices()
        {
            var services = new ServiceCollection();
            
            services.AddElectLocationGoogle(opt => opt.GoogleApiKey = "action-api-key");
            
            var serviceProvider = services.BuildServiceProvider();
            var client = serviceProvider.GetService<IElectGoogleClient>();
            var configuredOptions = serviceProvider.GetService<IOptions<ElectLocationGoogleOptions>>();
            
            Assert.IsNotNull(client);
            Assert.IsInstanceOfType(client, typeof(ElectGoogleClient));
            Assert.IsNotNull(configuredOptions);
            Assert.AreEqual("action-api-key", configuredOptions.Value.GoogleApiKey);
        }

        [TestMethod]
        public void AddElectLocationGoogle_RegistersAsScoped()
        {
            var services = new ServiceCollection();
            
            services.AddElectLocationGoogle();
            
            var descriptor = services.FirstOrDefault(x => x.ServiceType == typeof(IElectGoogleClient));
            
            Assert.IsNotNull(descriptor);
            Assert.AreEqual(ServiceLifetime.Scoped, descriptor.Lifetime);
            Assert.AreEqual(typeof(ElectGoogleClient), descriptor.ImplementationType);
        }

        [TestMethod]
        public void AddElectLocationGoogle_MultipleRegistrations_DoesNotDuplicate()
        {
            var services = new ServiceCollection();
            
            services.AddElectLocationGoogle();
            services.AddElectLocationGoogle();
            
            var descriptors = services.Where(x => x.ServiceType == typeof(IElectGoogleClient)).ToList();
            
            Assert.AreEqual(1, descriptors.Count);
        }

        [TestMethod]
        public void AddElectLocationGoogle_ReturnsServiceCollection()
        {
            var services = new ServiceCollection();
            
            var result = services.AddElectLocationGoogle();
            
            Assert.AreSame(services, result);
        }
    }
}