[TestClass]
public class HangfireHelperUnitTest
{
    private Mock<HttpContext> _mockHttpContext;
    private Mock<HttpRequest> _mockRequest;
    private Mock<IQueryCollection> _mockQuery;
    private Mock<IRequestCookieCollection> _mockCookies;

    [TestInitialize]
    public void Setup()
    {
        _mockHttpContext = new Mock<HttpContext>();
        _mockRequest = new Mock<HttpRequest>();
        _mockQuery = new Mock<IQueryCollection>();
        _mockCookies = new Mock<IRequestCookieCollection>();

        _mockHttpContext.Setup(x => x.Request).Returns(_mockRequest.Object);
        _mockRequest.Setup(x => x.Query).Returns(_mockQuery.Object);
        _mockRequest.Setup(x => x.Cookies).Returns(_mockCookies.Object);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithEmptyAccessKey_ShouldReturnTrue()
    {
        var options = new ElectHangfireOptions { AccessKey = string.Empty };

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithNullAccessKey_ShouldReturnTrue()
    {
        var options = new ElectHangfireOptions { AccessKey = null };

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithWhitespaceAccessKey_ShouldReturnTrue()
    {
        var options = new ElectHangfireOptions { AccessKey = "   " };

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithCorrectQueryKey_ShouldReturnTrue()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns("test-key");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns((string)null);

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithIncorrectQueryKey_ShouldReturnFalse()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns("wrong-key");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns((string)null);

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithCorrectCookieKey_ShouldReturnTrue()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns((string)null);
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("test-key");

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithIncorrectCookieKey_ShouldReturnFalse()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns((string)null);
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("wrong-key");

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_QueryKeyTakesPrecedenceOverCookie_ShouldReturnTrue()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns("test-key");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("wrong-key");

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithEmptyQueryKey_ShouldFallbackToCookie()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns("");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("test-key");

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsCanAccessHangfireDashboard_WithWhitespaceQueryKey_ShouldFallbackToCookie()
    {
        var options = new ElectHangfireOptions { AccessKey = "test-key" };
        _mockQuery.Setup(x => x["key"]).Returns("   ");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("test-key");

        var result = HangfireHelper.IsCanAccessHangfireDashboard(_mockHttpContext.Object, options);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AccessKeyName_ShouldHaveCorrectValue()
    {
        var accessKeyName = typeof(HangfireHelper).GetField("AccessKeyName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
        Assert.AreEqual("key", accessKeyName);
    }

    [TestMethod]
    public void CookieAccessKeyName_ShouldHaveCorrectValue()
    {
        var cookieAccessKeyName = typeof(HangfireHelper).GetField("CookieAccessKeyName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
        Assert.AreEqual("Elect_Hangfire_AccessKey", cookieAccessKeyName);
    }
}