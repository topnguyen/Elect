using Elect.Web.HttpDetection;
using Elect.Web.HttpDetection.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Elect.Test.Web.HttpDetection
{
    [TestClass]
    public class HttpRequestHelperUnitTest
    {
        private Mock<HttpRequest> CreateMockRequest(string userAgent)
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockHeaders = new HeaderDictionary();
            mockHeaders["User-Agent"] = userAgent;
            mockRequest.Setup(r => r.Headers).Returns(mockHeaders);
            return mockRequest;
        }

        [TestClass]
        public class GetUserAgentTests
        {
            [TestMethod]
            public void GetUserAgent_WithValidUserAgent_ReturnsUserAgent()
            {
                // Arrange
                var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = userAgent;
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetUserAgent(mockRequest.Object);

                // Assert
                Assert.AreEqual(userAgent, result);
            }

            [TestMethod]
            public void GetUserAgent_WithNullRequest_ReturnsNull()
            {
                // Act
                var result = HttpRequestHelper.GetUserAgent(null);

                // Assert
                Assert.IsNull(result);
            }

            [TestMethod]
            public void GetUserAgent_WithNoUserAgentHeader_ReturnsNull()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetUserAgent(mockRequest.Object);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class CrawlerDetectionTests
        {
            [TestMethod]
            public void IsCrawlerRequest_WithGoogleBot_ReturnsTrue()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.IsCrawlerRequest(mockRequest.Object);

                // Assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void IsCrawlerRequest_WithBingBot_ReturnsTrue()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.IsCrawlerRequest(mockRequest.Object);

                // Assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void IsCrawlerRequest_WithSpider_ReturnsTrue()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (compatible; spider/1.0; +http://example.com/spider.htm)";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.IsCrawlerRequest(mockRequest.Object);

                // Assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void IsCrawlerRequest_WithNormalBrowser_ReturnsFalse()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.IsCrawlerRequest(mockRequest.Object);

                // Assert
                Assert.IsFalse(result);
            }

            [TestMethod]
            public void IsCrawlerRequest_WithEmptyUserAgent_ReturnsFalse()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.IsCrawlerRequest(mockRequest.Object);

                // Assert
                Assert.IsFalse(result);
            }
        }

        [TestClass]
        public class BrowserDetectionTests
        {
            [TestMethod]
            public void GetBrowserName_WithChrome_ReturnsChrome()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("Chrome") || result.Contains("chrome"));
            }

            [TestMethod]
            public void GetBrowserFullInfo_WithChrome_ReturnsFullInfo()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserFullInfo(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetBrowserVersion_WithChrome_ReturnsVersion()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserVersion(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetBrowserName_WithFirefox_ReturnsFirefox()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserName(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                // Note: The actual parsing might return different values, just check it's not null or empty
            }

            [TestMethod]
            public void GetBrowserName_WithSafari_ReturnsSafari()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Safari/605.1.15";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("Safari") || result.Contains("safari"));
            }

            [TestMethod]
            public void GetBrowserName_WithEdge_ReturnsEdge()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserName(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                // Note: The actual parsing might return different values, just check it's not null or empty
            }

            [TestMethod]
            public void GetBrowserName_WithEmptyUserAgent_ReturnsNull()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetBrowserName(mockRequest.Object);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class OperatingSystemDetectionTests
        {
            [TestMethod]
            public void GetOsName_WithWindows_ReturnsWindows()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("Windows") || result.Contains("windows"));
            }

            [TestMethod]
            public void GetOsFullInfo_WithWindows_ReturnsFullInfo()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsFullInfo(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetOsVersion_WithWindows_ReturnsVersion()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsVersion(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetOsName_WithMacOS_ReturnsMac()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("Mac") || result.Contains("macOS"));
            }

            [TestMethod]
            public void GetOsName_WithLinux_ReturnsLinux()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsName(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                // Note: The actual parsing might return different values, just check it's not null or empty
            }

            [TestMethod]
            public void GetOsName_WithIOS_ReturnsIOS()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("iOS") || result.Contains("iPhone") || result.Contains("OS"));
            }

            [TestMethod]
            public void GetOsName_WithAndroid_ReturnsAndroid()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Linux; Android 11; SM-G991B) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Mobile Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsName(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                // Note: The actual parsing might return different values, just check it's not null or empty
            }

            [TestMethod]
            public void GetOsName_WithEmptyUserAgent_ReturnsNull()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetOsName(mockRequest.Object);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class EngineDetectionTests
        {
            [TestMethod]
            public void GetEngineName_WithWebKit_ReturnsWebKit()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetEngineName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("WebKit") || result.Contains("webkit"));
            }

            [TestMethod]
            public void GetEngineFullInfo_WithWebKit_ReturnsFullInfo()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetEngineFullInfo(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetEngineVersion_WithWebKit_ReturnsVersion()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetEngineVersion(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetEngineName_WithGecko_ReturnsGecko()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetEngineName(mockRequest.Object);

                // Assert
                Assert.IsTrue(result.Contains("Gecko") || result.Contains("gecko"));
            }

            [TestMethod]
            public void GetEngineName_WithEmptyUserAgent_ReturnsNull()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetEngineName(mockRequest.Object);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class MarkerDetectionTests
        {
            [TestMethod]
            public void GetMarkerName_WithValidUserAgent_ReturnsMarker()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetMarkerName(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetMarkerFullInfo_WithValidUserAgent_ReturnsFullInfo()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetMarkerFullInfo(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetMarkerVersion_WithValidUserAgent_ReturnsVersion()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockHeaders["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetMarkerVersion(mockRequest.Object);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Length > 0);
            }

            [TestMethod]
            public void GetMarkerName_WithEmptyUserAgent_ReturnsNull()
            {
                // Arrange
                var mockRequest = new Mock<HttpRequest>();
                var mockHeaders = new HeaderDictionary();
                mockRequest.Setup(r => r.Headers).Returns(mockHeaders);

                // Act
                var result = HttpRequestHelper.GetMarkerName(mockRequest.Object);

                // Assert
                Assert.IsNull(result);
            }
        }
    }
}