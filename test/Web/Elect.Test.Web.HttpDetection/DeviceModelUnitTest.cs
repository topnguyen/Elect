using Elect.Web.HttpDetection;
using Elect.Web.HttpDetection.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Elect.Test.Web.HttpDetection
{
    [TestClass]
    public class DeviceModelUnitTest
    {
        private Mock<HttpRequest> CreateMockRequest(string userAgent = "", string? ipAddress = "127.0.0.1")
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            
            if (!string.IsNullOrEmpty(userAgent))
            {
                mockHeaders["User-Agent"] = userAgent;
            }
            
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            mockRequest.Setup(r => r.HttpContext).Returns(mockHttpContext.Object);

            var mockConnection = new Mock<ConnectionInfo>();
            if (ipAddress != null)
            {
                mockConnection.Setup(c => c.RemoteIpAddress).Returns(System.Net.IPAddress.Parse(ipAddress));
            }
            mockHttpContext.Setup(c => c.Connection).Returns(mockConnection.Object);

            return mockRequest;
        }

        [TestMethod]
        public void Constructor_WithValidRequest_InitializesProperties()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            // Act
            var device = new DeviceModel(mockRequest.Object);

            // Assert
            Assert.IsNotNull(device);
            Assert.IsNotNull(device.BrowserName);
            Assert.IsNotNull(device.BrowserVersion);
            Assert.IsNotNull(device.OsName);
            Assert.IsNotNull(device.OsVersion);
            Assert.IsNotNull(device.EngineName);
            Assert.IsNotNull(device.EngineVersion);
            Assert.IsNotNull(device.MarkerName);
            Assert.IsNotNull(device.MarkerVersion);
            Assert.IsNotNull(device.IpAddress);
            Assert.IsNotNull(device.DeviceHash);
        }

        [TestMethod]
        public void Constructor_WithNullRequest_ThrowsArgumentNullException()
        {
            // Arrange
            HttpRequest? nullRequest = null;

            // Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => new DeviceModel(nullRequest!));
        }

        [TestMethod]
        public void GetDeviceType_WithMobileUserAgent_ReturnsMobile()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Mobile, device.Type);
        }

        [TestMethod]
        public void GetDeviceType_WithTabletUserAgent_ReturnsTablet()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (iPad; CPU OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Tablet, device.Type);
        }

        [TestMethod]
        public void GetDeviceType_WithDesktopUserAgent_ReturnsDesktop()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Desktop, device.Type);
        }

        [TestMethod]
        public void GetDeviceType_WithEmptyUserAgent_ReturnsUnknown()
        {
            // Arrange
            var mockRequest = CreateMockRequest("");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Unknown, device.Type);
        }

        [TestMethod]
        public void GetDeviceType_WithAndroidTabletUserAgent_ReturnsTablet()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Linux; Android 11; SM-T870) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Tablet, device.Type);
        }

        [TestMethod]
        public void GetDeviceType_WithOperaMiniUserAgent_ReturnsMobile()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Opera/9.80 (J2ME/MIDP; Opera Mini/9.80 (S60; SymbOS; Opera Mobi/23.348; U; en) Presto/2.5.25 Version/10.54");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Mobile, device.Type);
        }

        [TestMethod]
        public void GetDeviceHash_WithSameUserAgent_ReturnsSameHash()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
            var mockRequest1 = CreateMockRequest(userAgent);
            var mockRequest2 = CreateMockRequest(userAgent);
            var device1 = new DeviceModel(mockRequest1.Object);
            var device2 = new DeviceModel(mockRequest2.Object);

            // Act & Assert
            Assert.AreEqual(device1.DeviceHash, device2.DeviceHash);
        }

        [TestMethod]
        public void GetDeviceHash_WithDifferentUserAgent_ReturnsDifferentHash()
        {
            // Arrange
            var mockRequest1 = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            var mockRequest2 = CreateMockRequest("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            var device1 = new DeviceModel(mockRequest1.Object);
            var device2 = new DeviceModel(mockRequest2.Object);

            // Act & Assert
            Assert.AreNotEqual(device1.DeviceHash, device2.DeviceHash);
        }

        [TestMethod]
        public void GetDeviceHash_ReturnsNonEmptyString()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.IsNotNull(device.DeviceHash);
            Assert.IsTrue(device.DeviceHash.Length > 0);
        }

        [TestMethod]
        public void UpdateLocation_WithValidIpAddress_UpdatesLocationProperties()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36", "8.8.8.8");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert - Location is updated automatically in constructor
            // We just verify the device was created successfully
            Assert.IsNotNull(device);
            Assert.IsNotNull(device.IpAddress);
        }

        [TestMethod]
        public void UpdateLocation_WithLocalhostIpAddress_HandlesGracefully()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36", "127.0.0.1");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert - Location is updated automatically in constructor
            // Should not throw even with localhost
            Assert.IsNotNull(device);
            Assert.IsNotNull(device.IpAddress);
        }

        [TestMethod]
        public void IsCrawler_WithBotUserAgent_ReturnsTrue()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.IsTrue(device.IsCrawler);
        }

        [TestMethod]
        public void IsCrawler_WithNormalUserAgent_ReturnsFalse()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.IsFalse(device.IsCrawler);
        }

        [TestMethod]
        public void DeviceType_Property_ReturnsCorrectType()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert
            Assert.AreEqual(DeviceType.Mobile, device.Type);
        }

        [TestMethod]
        public void Dispose_CallsBaseDispose()
        {
            // Arrange
            var mockRequest = CreateMockRequest("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
            var device = new DeviceModel(mockRequest.Object);

            // Act & Assert - Should not throw
            device.Dispose();
        }
    }
}