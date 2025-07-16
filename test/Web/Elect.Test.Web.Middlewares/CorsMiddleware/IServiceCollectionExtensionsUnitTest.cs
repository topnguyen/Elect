
[TestClass]
public class IServiceCollectionExtensionsUnitTest
{
    private IServiceCollection _services;

    [TestInitialize]
    public void Setup()
    {
        _services = new ServiceCollection();
        _services.AddLogging();
    }

    [TestMethod]
    public void AddElectCors_WithoutParameters_ShouldReturnServices()
    {
        var result = _services.AddElectCors();

        Assert.AreSame(_services, result);
    }

    [TestMethod]
    public void AddElectCors_WithOptions_ShouldConfigureCorrectly()
    {
        var options = new ElectCorsOptions
        {
            PolicyName = "TestPolicy",
            AllowOrigins = new List<string> { "https://example.com" },
            AllowHeaders = new List<string> { "Content-Type" },
            AllowMethods = new List<string> { "GET", "POST" },
            IsAllowCredentials = false
        };

        var result = _services.AddElectCors(options);

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectCorsOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.AreEqual("TestPolicy", configuredOptions.Value.PolicyName);
    }

    [TestMethod]
    public void AddElectCors_WithAction_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.PolicyName = "ActionPolicy";
            options.AllowOrigins = new List<string> { "https://test.com" };
            options.IsAllowCredentials = true;
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectCorsOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.AreEqual("ActionPolicy", configuredOptions.Value.PolicyName);
        Assert.IsTrue(configuredOptions.Value.IsAllowCredentials);
    }

    [TestMethod]
    public void AddElectCors_WithWildcardOrigins_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.AllowOrigins = new List<string> { "*" };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithSpecificOrigins_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.AllowOrigins = new List<string> { "https://example.com", "https://test.com" };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithWildcardHeaders_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.AllowHeaders = new List<string> { "*" };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithSpecificHeaders_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.AllowHeaders = new List<string> { "Content-Type", "Authorization" };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithWildcardMethods_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.AllowMethods = new List<string> { "*" };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithSpecificMethods_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.AllowMethods = new List<string> { "GET", "POST" };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithCredentialsEnabled_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.IsAllowCredentials = true;
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithCredentialsDisabled_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.IsAllowCredentials = false;
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithCustomOriginChecker_ShouldConfigureCorrectly()
    {
        var result = _services.AddElectCors(options =>
        {
            options.IsOriginAllowed = origin => origin.StartsWith("https://");
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }

    [TestMethod]
    public void AddElectCors_WithExtendPolicyBuilder_ShouldCallExtension()
    {
        bool extendCalled = false;
        var result = _services.AddElectCors(options =>
        {
            options.ExtendPolicyBuilder = builder => { extendCalled = true; };
        });

        Assert.AreSame(_services, result);
        Assert.IsTrue(extendCalled);
    }

    [TestMethod]
    public void AddElectCors_WithExtendPolicyOptions_ShouldSetCallback()
    {
        var result = _services.AddElectCors(options =>
        {
            options.ExtendPolicyOptions = corsOptions => { /* callback set */ };
        });

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var configuredOptions = serviceProvider.GetService<IOptions<ElectCorsOptions>>();
        Assert.IsNotNull(configuredOptions);
        Assert.IsNotNull(configuredOptions.Value.ExtendPolicyOptions);
    }

    [TestMethod]
    public void AddElectCors_ShouldRegisterCorsServices()
    {
        var result = _services.AddElectCors();

        Assert.AreSame(_services, result);
        var serviceProvider = _services.BuildServiceProvider();
        var corsService = serviceProvider.GetService<ICorsService>();
        Assert.IsNotNull(corsService);
    }
}