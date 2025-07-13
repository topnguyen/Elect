namespace Elect.Test.Web
{
    [TestClass]
    public class HttpRequestHelperUnitTest
    {
        [TestMethod]
        public void IsAjaxRequest_ReturnsTrue_WhenHeaderPresent()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            Assert.IsTrue(HttpRequestHelper.IsAjaxRequest(context.Request));
        }

        [TestMethod]
        public void IsAjaxRequest_ReturnsFalse_WhenHeaderMissing()
        {
            var context = new DefaultHttpContext();
            Assert.IsFalse(HttpRequestHelper.IsAjaxRequest(context.Request));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsAjaxRequest_Throws_WhenNull()
        {
            HttpRequestHelper.IsAjaxRequest(null);
        }

        [TestMethod]
        public void IsLocalRequest_ReturnsTrue_WhenRemoteEqualsLocal()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Loopback;
            context.Connection.LocalIpAddress = IPAddress.Loopback;
            Assert.IsTrue(HttpRequestHelper.IsLocalRequest(context.Request));
        }

        [TestMethod]
        public void IsLocalRequest_ReturnsTrue_WhenRemoteIsLoopback()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Loopback;
            context.Connection.LocalIpAddress = null;
            Assert.IsTrue(HttpRequestHelper.IsLocalRequest(context.Request));
        }

        [TestMethod]
        public void IsLocalRequest_ReturnsTrue_WhenBothNull()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = null;
            context.Connection.LocalIpAddress = null;
            Assert.IsTrue(HttpRequestHelper.IsLocalRequest(context.Request));
        }

        [TestMethod]
        public void IsLocalRequest_ReturnsFalse_WhenNotLocal()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("8.8.8.8");
            context.Connection.LocalIpAddress = IPAddress.Parse("1.1.1.1");
            Assert.IsFalse(HttpRequestHelper.IsLocalRequest(context.Request));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsLocalRequest_Throws_WhenNull()
        {
            HttpRequestHelper.IsLocalRequest(null);
        }

        [TestMethod]
        public void IsRequestFor_True_Match()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/api/test";
            Assert.IsTrue(HttpRequestHelper.IsRequestFor(context.Request, "/api/test"));
        }

        [TestMethod]
        public void IsRequestFor_False_NoMatch()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/api/other";
            Assert.IsFalse(HttpRequestHelper.IsRequestFor(context.Request, "/api/test"));
        }

        [TestMethod]
        public void GetEndpoint_ReturnsFullUrl()
        {
            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("localhost:5001");
            context.Request.Path = "/api/test";
            Assert.AreEqual("https://localhost:5001/api/test", HttpRequestHelper.GetEndpoint(context.Request));
        }

        [TestMethod]
        public void GetDomain_ReturnsDomain()
        {
            var context = new DefaultHttpContext();
            context.Request.Scheme = "http";
            context.Request.Host = new HostString("example.com");
            Assert.AreEqual("http://example.com", HttpRequestHelper.GetDomain(context.Request));
        }

        [TestMethod]
        public void GetCultureInfo_ReturnsCulture()
        {
            var context = new DefaultHttpContext();
            var feature = new Mock<IRequestCultureFeature>();
            feature.Setup(f => f.RequestCulture).Returns(new RequestCulture("fr-FR"));
            context.Features.Set(feature.Object);
            var culture = HttpRequestHelper.GetCultureInfo(context.Request);
            Assert.AreEqual("fr-FR", culture.Name);
        }

        [TestMethod]
        public void GetBody_ReturnsDeserializedObject()
        {
            var context = new DefaultHttpContext();
            var obj = new { Name = "Test" };
            var json = JsonConvert.SerializeObject(obj);
            var bytes = Encoding.UTF8.GetBytes(json);
            context.Request.Body = new MemoryStream(bytes);
            var result = HttpRequestHelper.GetBody<dynamic>(context.Request);
            Assert.AreEqual("Test", (string)result.Name);
        }

        [TestMethod]
        public void GetBody_ReturnsDefault_OnException()
        {
            var context = new DefaultHttpContext();
            context.Request.Body = new MemoryStream(); // empty, not valid JSON
            var result = HttpRequestHelper.GetBody<dynamic>(context.Request);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetIpAddress_ReturnsCFConnectingIP()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["CF-Connecting-IP"] = "1.2.3.4";
            Assert.AreEqual("1.2.3.4", HttpRequestHelper.GetIpAddress(context.Request));
        }

        [TestMethod]
        public void GetIpAddress_ReturnsCFTrueClientIP()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["CF-True-Client-IP"] = "5.6.7.8";
            Assert.AreEqual("5.6.7.8", HttpRequestHelper.GetIpAddress(context.Request));
        }

        [TestMethod]
        public void GetIpAddress_ReturnsXForwardedFor()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Forwarded-For"] = "9.8.7.6, 4.3.2.1";
            Assert.AreEqual("9.8.7.6", HttpRequestHelper.GetIpAddress(context.Request));
        }

        [TestMethod]
        public void GetIpAddress_ReturnsRemoteIpAddress()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("10.0.0.1");
            Assert.AreEqual("10.0.0.1", HttpRequestHelper.GetIpAddress(context.Request));
        }

        [TestMethod]
        public void GetIpAddress_StandardizesLoopback()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.IPv6Loopback;
            Assert.AreEqual("127.0.0.1", HttpRequestHelper.GetIpAddress(context.Request));
        }

        [TestMethod]
        public void GetIpAddress_RemovesPort()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["CF-Connecting-IP"] = "1.2.3.4:1234";
            Assert.AreEqual("1.2.3.4", HttpRequestHelper.GetIpAddress(context.Request));
        }

        [TestMethod]
        public void GetIpAddress_ReturnsNull_WhenNoIp()
        {
            var context = new DefaultHttpContext();
            Assert.IsNull(HttpRequestHelper.GetIpAddress(context.Request));
        }
    }
}
