[TestClass]
public class ElectDashboardAuthorizationFilterUnitTest
{
    private ElectDashboardAuthorizationFilter _filter;
    private AspNetCoreDashboardContext _dashboardContext;
    private Mock<HttpContext> _mockHttpContext;
    private Mock<IServiceProvider> _mockServiceProvider;
    private Mock<IOptions<ElectHangfireOptions>> _mockOptions;
    private Mock<IQueryCollection> _mockQuery;
    private Mock<IRequestCookieCollection> _mockCookies;
    private ElectHangfireOptions _options;

    [TestInitialize]
    public void Setup()
    {
        _filter = new ElectDashboardAuthorizationFilter();
        _mockHttpContext = new Mock<HttpContext>();
        _mockServiceProvider = new Mock<IServiceProvider>();
        _mockOptions = new Mock<IOptions<ElectHangfireOptions>>();
        _mockQuery = new Mock<IQueryCollection>();
        _mockCookies = new Mock<IRequestCookieCollection>();
        
        _options = new ElectHangfireOptions();
        
        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(x => x.Query).Returns(_mockQuery.Object);
        mockRequest.Setup(x => x.Cookies).Returns(_mockCookies.Object);
        
        _mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
        _mockHttpContext.Setup(x => x.RequestServices).Returns(_mockServiceProvider.Object);
        _mockServiceProvider.Setup(x => x.GetService(typeof(IOptions<ElectHangfireOptions>))).Returns(_mockOptions.Object);
        _mockOptions.Setup(x => x.Value).Returns(_options);
        
        _dashboardContext = new AspNetCoreDashboardContext(Mock.Of<JobStorage>(), new DashboardOptions(), _mockHttpContext.Object);
    }

    [TestMethod]
    public void ElectDashboardAuthorizationFilter_ShouldImplementInterface()
    {
        var filter = new ElectDashboardAuthorizationFilter();

        Assert.IsInstanceOfType(filter, typeof(IDashboardAuthorizationFilter));
    }

    [TestMethod]
    public void ElectDashboardAuthorizationFilter_ShouldHaveAuthorizeMethod()
    {
        var filter = new ElectDashboardAuthorizationFilter();
        var method = filter.GetType().GetMethod("Authorize");

        Assert.IsNotNull(method);
        Assert.AreEqual(typeof(bool), method.ReturnType);
    }

    [TestMethod]
    public void Authorize_WithNoAccessKey_ShouldReturnTrue()
    {
        // Setup
        _options.AccessKey = null;

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Authorize_WithEmptyAccessKey_ShouldReturnTrue()
    {
        // Setup
        _options.AccessKey = string.Empty;

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Authorize_WithValidQueryKey_ShouldReturnTrue()
    {
        // Setup
        _options.AccessKey = "test-key";
        _mockQuery.Setup(x => x["key"]).Returns("test-key");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns((string)null);

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Authorize_WithInvalidQueryKey_ShouldReturnFalse()
    {
        // Setup
        _options.AccessKey = "test-key";
        _mockQuery.Setup(x => x["key"]).Returns("wrong-key");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns((string)null);

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Authorize_WithValidCookieKey_ShouldReturnTrue()
    {
        // Setup
        _options.AccessKey = "test-key";
        _mockQuery.Setup(x => x["key"]).Returns((string)null);
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("test-key");

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Authorize_WithInvalidCookieKey_ShouldReturnFalse()
    {
        // Setup
        _options.AccessKey = "test-key";
        _mockQuery.Setup(x => x["key"]).Returns((string)null);
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("wrong-key");

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Authorize_QueryKeyTakesPrecedenceOverCookie_ShouldReturnTrue()
    {
        // Setup
        _options.AccessKey = "test-key";
        _mockQuery.Setup(x => x["key"]).Returns("test-key");
        _mockCookies.Setup(x => x["Elect_Hangfire_AccessKey"]).Returns("wrong-key");

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Authorize_WithWhitespaceAccessKey_ShouldReturnTrue()
    {
        // Setup
        _options.AccessKey = "   ";

        // Act
        var result = _filter.Authorize(_dashboardContext);

        // Assert
        Assert.IsTrue(result);
    }
}