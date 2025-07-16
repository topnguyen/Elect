[TestClass]
public class ElectCorsOptionsUnitTest
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var options = new ElectCorsOptions();

        Assert.AreEqual("Elect", options.PolicyName);
        Assert.IsNotNull(options.AllowOrigins);
        Assert.AreEqual(1, options.AllowOrigins.Count);
        Assert.AreEqual("*", options.AllowOrigins[0]);
        Assert.IsNull(options.IsOriginAllowed);
        Assert.IsNotNull(options.AllowHeaders);
        Assert.AreEqual(1, options.AllowHeaders.Count);
        Assert.AreEqual("*", options.AllowHeaders[0]);
        Assert.IsNotNull(options.AllowMethods);
        Assert.AreEqual(5, options.AllowMethods.Count);
        Assert.IsTrue(options.IsAllowCredentials);
        Assert.IsNull(options.ExtendPolicyBuilder);
        Assert.IsNull(options.ExtendPolicyOptions);
    }

    [TestMethod]
    public void PolicyName_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        var policyName = "CustomPolicy";

        options.PolicyName = policyName;

        Assert.AreEqual(policyName, options.PolicyName);
    }

    [TestMethod]
    public void AllowOrigins_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        var allowOrigins = new List<string> { "https://example.com", "https://test.com" };

        options.AllowOrigins = allowOrigins;

        Assert.AreEqual(allowOrigins, options.AllowOrigins);
        Assert.AreEqual(2, options.AllowOrigins.Count);
    }

    [TestMethod]
    public void IsOriginAllowed_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        Func<string, bool> originCheck = origin => origin.StartsWith("https://");

        options.IsOriginAllowed = originCheck;

        Assert.AreEqual(originCheck, options.IsOriginAllowed);
        Assert.IsTrue(options.IsOriginAllowed("https://example.com"));
        Assert.IsFalse(options.IsOriginAllowed("http://example.com"));
    }

    [TestMethod]
    public void AllowHeaders_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        var allowHeaders = new List<string> { "Content-Type", "Authorization" };

        options.AllowHeaders = allowHeaders;

        Assert.AreEqual(allowHeaders, options.AllowHeaders);
        Assert.AreEqual(2, options.AllowHeaders.Count);
    }

    [TestMethod]
    public void AllowMethods_DefaultValues_ShouldContainStandardMethods()
    {
        var options = new ElectCorsOptions();

        Assert.IsTrue(options.AllowMethods.Contains("GET"));
        Assert.IsTrue(options.AllowMethods.Contains("POST"));
        Assert.IsTrue(options.AllowMethods.Contains("PUT"));
        Assert.IsTrue(options.AllowMethods.Contains("DELETE"));
        Assert.IsTrue(options.AllowMethods.Contains("OPTIONS"));
    }

    [TestMethod]
    public void AllowMethods_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        var allowMethods = new List<string> { "GET", "POST" };

        options.AllowMethods = allowMethods;

        Assert.AreEqual(allowMethods, options.AllowMethods);
        Assert.AreEqual(2, options.AllowMethods.Count);
    }

    [TestMethod]
    public void IsAllowCredentials_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();

        options.IsAllowCredentials = false;

        Assert.IsFalse(options.IsAllowCredentials);
    }

    [TestMethod]
    public void ExtendPolicyBuilder_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        Action<CorsPolicyBuilder> extendBuilder = builder => { };

        options.ExtendPolicyBuilder = extendBuilder;

        Assert.AreEqual(extendBuilder, options.ExtendPolicyBuilder);
    }

    [TestMethod]
    public void ExtendPolicyOptions_ShouldBeSettable()
    {
        var options = new ElectCorsOptions();
        Action<CorsOptions> extendOptions = corsOptions => { };

        options.ExtendPolicyOptions = extendOptions;

        Assert.AreEqual(extendOptions, options.ExtendPolicyOptions);
    }

    [TestMethod]
    public void AllowOrigins_DefaultValue_ShouldAllowAll()
    {
        var options = new ElectCorsOptions();

        Assert.IsTrue(options.AllowOrigins.Contains("*"));
    }

    [TestMethod]
    public void AllowHeaders_DefaultValue_ShouldAllowAll()
    {
        var options = new ElectCorsOptions();

        Assert.IsTrue(options.AllowHeaders.Contains("*"));
    }

    [TestMethod]
    public void IsAllowCredentials_DefaultValue_ShouldBeTrue()
    {
        var options = new ElectCorsOptions();

        Assert.IsTrue(options.IsAllowCredentials);
    }
}