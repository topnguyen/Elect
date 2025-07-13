namespace Elect.Test.Web
{
    [TestClass]
    public class HttpRequestExtensionsUnitTest
    {
        [TestMethod]
        public void IsAjaxRequest_ReturnsTrue_WhenHeaderPresent()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            Assert.IsTrue(context.Request.IsAjaxRequest());
        }

        [TestMethod]
        public void IsAjaxRequest_ReturnsFalse_WhenHeaderAbsent()
        {
            var context = new DefaultHttpContext();
            Assert.IsFalse(context.Request.IsAjaxRequest());
        }

        [TestMethod]
        public void IsLocalRequest_ReturnsTrue_ForLocalhost()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Loopback;
            context.Connection.LocalIpAddress = System.Net.IPAddress.Loopback;
            Assert.IsTrue(context.Request.IsLocalRequest());
        }

        [TestMethod]
        public void IsLocalRequest_ReturnsFalse_ForNonLocalhost()
        {
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("8.8.8.8");
            context.Connection.LocalIpAddress = System.Net.IPAddress.Loopback;
            Assert.IsFalse(context.Request.IsLocalRequest());
        }
    }
}
