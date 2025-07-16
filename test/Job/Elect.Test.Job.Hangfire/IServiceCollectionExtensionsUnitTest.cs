[TestClass]
public class IServiceCollectionExtensionsUnitTest
{
    private IServiceCollection _services;
    private IConfiguration _configuration;

    [TestInitialize]
    public void Setup()
    {
        _services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder();
        _configuration = configurationBuilder.Build();
    }

    [TestMethod]
    public void AddElectHangfire_WithConfiguration_ShouldConfigureServices()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            ["ElectHangfire:IsEnable"] = "true",
            ["ElectHangfire:Provider"] = "Memory",
            ["ElectHangfire:Url"] = "/test/job"
        });
        var config = configBuilder.Build();

        var result = _services.AddElectHangfire(config);

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<ElectHangfireOptions>>();
        Assert.IsNotNull(options);
    }

    [TestMethod]
    public void AddElectHangfire_WithDefaultParameters_ShouldUseDefaultSectionName()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            ["ElectHangfire:IsEnable"] = "true",
            ["ElectHangfire:Provider"] = "Memory"
        });
        var config = configBuilder.Build();

        var result = _services.AddElectHangfire(config);

        Assert.AreSame(_services, result);
    }

    [TestMethod]
    public void AddElectHangfire_WithCustomSectionName_ShouldUseCustomSection()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            ["CustomHangfire:IsEnable"] = "true",
            ["CustomHangfire:Provider"] = "Memory"
        });
        var config = configBuilder.Build();

        var result = _services.AddElectHangfire(config, "CustomHangfire");

        Assert.AreSame(_services, result);
    }

    [TestMethod]
    public void GetOptions_WithDefaultSectionName_ShouldReturnCorrectOptions()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            ["ElectHangfire:IsEnable"] = "false",
            ["ElectHangfire:Provider"] = "SqlServer",
            ["ElectHangfire:Url"] = "/custom/job",
            ["ElectHangfire:AccessKey"] = "test-key",
            ["ElectHangfire:DbConnectionString"] = "Server=.;Database=Test;"
        });
        var config = configBuilder.Build();

        var options = IServiceCollectionExtensions.GetOptions(config);

        Assert.IsFalse(options.IsEnable);
        Assert.AreEqual(HangfireProvider.SqlServer, options.Provider);
        Assert.AreEqual("/custom/job", options.Url);
        Assert.AreEqual("test-key", options.AccessKey);
        Assert.AreEqual("Server=.;Database=Test;", options.DbConnectionString);
    }

    [TestMethod]
    public void GetOptions_WithCustomSectionName_ShouldReturnCorrectOptions()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            ["CustomSection:IsEnable"] = "false",
            ["CustomSection:Provider"] = "SqlServer"
        });
        var config = configBuilder.Build();

        var options = IServiceCollectionExtensions.GetOptions(config, "CustomSection");

        Assert.IsFalse(options.IsEnable);
        Assert.AreEqual(HangfireProvider.SqlServer, options.Provider);
    }

    [TestMethod]
    public void AddElectHangfire_WithoutParameters_ShouldReturnServices()
    {
        var result = _services.AddElectHangfire();

        Assert.AreSame(_services, result);
    }

    [TestMethod]
    public void AddElectHangfire_WithOptions_ShouldConfigureCorrectly()
    {
        var options = new ElectHangfireOptions
        {
            IsEnable = true,
            Provider = HangfireProvider.Memory,
            Url = "/test/job",
            AccessKey = "test-key"
        };

        var result = _services.AddElectHangfire(options);

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectHangfireOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.AreEqual("/test/job", configuredOptions.Value.Url);
        Assert.AreEqual("test-key", configuredOptions.Value.AccessKey);
    }

    [TestMethod]
    public void AddElectHangfire_WithAction_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectHangfire(options =>
        {
            options.IsEnable = true;
            options.Provider = HangfireProvider.Memory;
            options.Url = "/action/job";
            options.AccessKey = "action-key";
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectHangfireOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.AreEqual("/action/job", configuredOptions.Value.Url);
        Assert.AreEqual("action-key", configuredOptions.Value.AccessKey);
    }

    [TestMethod]
    public void AddElectHangfire_WithDisabledOptions_ShouldNotAddHangfireServices()
    {
        var options = new ElectHangfireOptions { IsEnable = false };

        var result = _services.AddElectHangfire(options);

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var jobStorage = serviceProvider.GetService<JobStorage>();
        Assert.IsNull(jobStorage);
    }

    [TestMethod]
    public void AddElectHangfire_WithMemoryProvider_ShouldAddCorrectServices()
    {
        var options = new ElectHangfireOptions
        {
            IsEnable = true,
            Provider = HangfireProvider.Memory
        };

        var result = _services.AddElectHangfire(options);

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectHangfireOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.AreEqual(HangfireProvider.Memory, configuredOptions.Value.Provider);
    }

    [TestMethod]
    public void AddElectHangfire_WithSqlServerProvider_ShouldConfigureOptions()
    {
        var options = new ElectHangfireOptions
        {
            IsEnable = false, // Disabled to avoid SQL Server dependency
            Provider = HangfireProvider.SqlServer,
            DbConnectionString = "Server=.;Database=Test;"
        };

        var result = _services.AddElectHangfire(options);

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectHangfireOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.AreEqual(HangfireProvider.SqlServer, configuredOptions.Value.Provider);
        Assert.AreEqual("Server=.;Database=Test;", configuredOptions.Value.DbConnectionString);
    }

    [TestMethod]
    public void AddElectHangfire_WithExtendOptions_ShouldCallExtendFunction()
    {
        bool extendOptionsCalled = false;
        var options = new ElectHangfireOptions
        {
            IsEnable = true,
            Provider = HangfireProvider.Memory,
            ExtendOptions = (config, opts) => { extendOptionsCalled = true; }
        };

        _services.AddElectHangfire(options);

        Assert.IsTrue(extendOptionsCalled);
    }
}