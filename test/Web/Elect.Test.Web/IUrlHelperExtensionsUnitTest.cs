namespace Elect.Test.Web
{
    [TestClass]
    public class IUrlHelperExtensionsUnitTest
    {
        [TestMethod]
        public void GetPagedMeta_ReturnsPagedMetaModel()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/test";
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("localhost");
            var actionContext = new ActionContext { HttpContext = httpContext };
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.SetupGet(x => x.ActionContext).Returns(actionContext);
            urlHelperMock.Setup(x => x.Content(It.IsAny<string>())).Returns((string s) => s);
            var urlHelper = urlHelperMock.Object;
            var pagedRequest = new TestPagedRequestModel();
            var pagedResponse = new TestPagedResponseModel();
            var result = urlHelper.GetPagedMeta(pagedRequest, pagedResponse);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AbsoluteAction_ReturnsAbsoluteUrl()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("example.com");
            var actionContext = new ActionContext { HttpContext = httpContext };
            var urlHelper = new FakeUrlHelper(actionContext)
            {
                ActionResult = "https://example.com/Home/Index"
            };
            var result = urlHelper.AbsoluteAction("Index", "Home");
            Assert.AreEqual("https://example.com/Home/Index", result);
        }

        [TestMethod]
        public void AbsoluteContent_ReturnsAbsoluteUrl()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("example.com");
            var actionContext = new ActionContext { HttpContext = httpContext };
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.SetupGet(x => x.ActionContext).Returns(actionContext);
            urlHelperMock.Setup(x => x.Content("/content/img.png")).Returns("/content/img.png");
            var urlHelper = urlHelperMock.Object;
            var result = urlHelper.AbsoluteContent("/content/img.png");
            Assert.AreEqual("https://example.com/content/img.png", result);
        }

        [TestMethod]
        public void AbsoluteRouteUrl_ReturnsAbsoluteUrl()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("example.com");
            var actionContext = new ActionContext { HttpContext = httpContext };
            var urlHelper = new FakeUrlHelper(actionContext)
            {
                ActionResult = "https://example.com/home/index"
            };
            var result = urlHelper.AbsoluteRouteUrl("default");
            Assert.AreEqual("https://example.com/home/index", result);
        }

        private class FakeUrlHelper : IUrlHelper
        {
            public ActionContext ActionContext { get; }
            public string ActionResult { get; set; }
            public FakeUrlHelper(ActionContext actionContext) => ActionContext = actionContext;
            public string Action(UrlActionContext actionContext) => ActionResult;
            public string Action(string action, string controller, object values, string scheme) => ActionResult;
            public string Action(string action, string controller, object values) => ActionResult;
            public string Content(string contentPath) => contentPath;
            public bool IsLocalUrl(string url) => true;
            public string Link(string routeName, object values) => ActionResult;
            public string RouteUrl(UrlRouteContext routeContext) => ActionResult;
            public string RouteUrl(string routeName, object values, string scheme) => ActionResult;
            public string RouteUrl(string routeName, object values) => ActionResult;
        }

        private class TestPagedRequestModel : PagedRequestModel { }
        private class TestPagedResponseModel : PagedResponseModel<TestResponseModel> { }
        private class TestResponseModel { }
    }
}
