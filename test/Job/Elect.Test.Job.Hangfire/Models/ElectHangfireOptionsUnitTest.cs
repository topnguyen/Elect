[TestClass]
public class ElectHangfireOptionsUnitTest
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var options = new ElectHangfireOptions();

        Assert.IsTrue(options.IsEnable);
        Assert.IsFalse(options.IsDisableJobDashboard);
        Assert.AreEqual("/developers/job", options.Url);
        Assert.AreEqual("/", options.BackToUrl);
        Assert.AreEqual(string.Empty, options.AccessKey);
        Assert.AreEqual("You don't have permission to access Job Dashboard, please contact your administrator.", options.UnAuthorizeMessage);
        Assert.AreEqual(HangfireProvider.Memory, options.Provider);
        Assert.IsNull(options.DbConnectionString);
        Assert.AreEqual(3000, options.StatsPollingInterval);
        Assert.IsNull(options.ExtendOptions);
    }

    [TestMethod]
    public void Properties_ShouldBeSettable()
    {
        var options = new ElectHangfireOptions
        {
            IsEnable = false,
            IsDisableJobDashboard = true,
            Url = "/custom/job",
            BackToUrl = "/home",
            AccessKey = "test-key",
            UnAuthorizeMessage = "Custom unauthorized message",
            Provider = HangfireProvider.SqlServer,
            DbConnectionString = "Server=.;Database=Test;",
            StatsPollingInterval = 5000,
            ExtendOptions = (config, opts) => { }
        };

        Assert.IsFalse(options.IsEnable);
        Assert.IsTrue(options.IsDisableJobDashboard);
        Assert.AreEqual("/custom/job", options.Url);
        Assert.AreEqual("/home", options.BackToUrl);
        Assert.AreEqual("test-key", options.AccessKey);
        Assert.AreEqual("Custom unauthorized message", options.UnAuthorizeMessage);
        Assert.AreEqual(HangfireProvider.SqlServer, options.Provider);
        Assert.AreEqual("Server=.;Database=Test;", options.DbConnectionString);
        Assert.AreEqual(5000, options.StatsPollingInterval);
        Assert.IsNotNull(options.ExtendOptions);
    }

    [TestMethod]
    public void IsEnable_DefaultValue_ShouldBeTrue()
    {
        var options = new ElectHangfireOptions();
        Assert.IsTrue(options.IsEnable);
    }

    [TestMethod]
    public void IsDisableJobDashboard_DefaultValue_ShouldBeFalse()
    {
        var options = new ElectHangfireOptions();
        Assert.IsFalse(options.IsDisableJobDashboard);
    }

    [TestMethod]
    public void Url_DefaultValue_ShouldBeCorrect()
    {
        var options = new ElectHangfireOptions();
        Assert.AreEqual("/developers/job", options.Url);
    }

    [TestMethod]
    public void BackToUrl_DefaultValue_ShouldBeCorrect()
    {
        var options = new ElectHangfireOptions();
        Assert.AreEqual("/", options.BackToUrl);
    }

    [TestMethod]
    public void AccessKey_DefaultValue_ShouldBeEmpty()
    {
        var options = new ElectHangfireOptions();
        Assert.AreEqual(string.Empty, options.AccessKey);
    }

    [TestMethod]
    public void Provider_DefaultValue_ShouldBeMemory()
    {
        var options = new ElectHangfireOptions();
        Assert.AreEqual(HangfireProvider.Memory, options.Provider);
    }

    [TestMethod]
    public void StatsPollingInterval_DefaultValue_ShouldBe3000()
    {
        var options = new ElectHangfireOptions();
        Assert.AreEqual(3000, options.StatsPollingInterval);
    }
}