using Elect.Web.HttpDetection;
using Elect.Web.HttpDetection.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Elect.Test.Web.HttpDetection
{
    [TestClass]
    public class HttpRequestExtensionsUnitTest
    {
        [TestMethod]
        public void GetDeviceInformation_WithValidRequest_ReturnsDeviceModel()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            // Act
            var result = mockRequest.Object.GetDeviceInformation();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.BrowserName);
            Assert.IsNotNull(result.BrowserVersion);
            Assert.IsNotNull(result.OsName);
            Assert.IsNotNull(result.OsVersion);
        }

        [TestMethod]
        public void GetDeviceInformation_WithNullRequest_HandlesGracefully()
        {
            // Arrange
            HttpRequest? nullRequest = null;

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => nullRequest!.GetDeviceInformation());
        }

        [TestMethod]
        public void GetDeviceInformation_WithEmptyUserAgent_ReturnsDefaultValues()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            // Act
            var result = mockRequest.Object.GetDeviceInformation();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(DeviceType.Unknown, result.Type);
        }

        [TestMethod]
        public void GetDeviceInformation_WithMobileUserAgent_DetectsMobile()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            mockHeaders["User-Agent"] = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1";
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            // Act
            var result = mockRequest.Object.GetDeviceInformation();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(DeviceType.Mobile, result.Type);
        }

        [TestMethod]
        public void GetDeviceInformation_WithTabletUserAgent_DetectsTablet()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            mockHeaders["User-Agent"] = "Mozilla/5.0 (iPad; CPU OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1";
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            // Act
            var result = mockRequest.Object.GetDeviceInformation();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(DeviceType.Tablet, result.Type);
        }

        [TestMethod]
        public void GetDeviceInformation_WithDesktopUserAgent_DetectsDesktop()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            // Act
            var result = mockRequest.Object.GetDeviceInformation();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(DeviceType.Desktop, result.Type);
        }

        [TestMethod]
        public void GetDeviceInformation_WithCrawlerUserAgent_DetectsCrawler()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            mockHeaders["User-Agent"] = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            // Act
            var result = mockRequest.Object.GetDeviceInformation();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCrawler);
        }
    }
}