using Elect.Web.HttpDetection.Models;

namespace Elect.Test.Web.HttpDetection
{
    [TestClass]
    public class ElectHttpDetectionConstantsUnitTest
    {
        [TestMethod]
        public void DbName_HasCorrectValue()
        {
            // Act & Assert
            Assert.AreEqual("GeoCity.mmdb", ElectHttpDetectionConstants.DbName);
        }

        [TestMethod]
        public void TabletAgentsRegex_IsNotNull()
        {
            // Act & Assert
            Assert.IsNotNull(ElectHttpDetectionConstants.TabletAgentsRegex);
        }

        [TestMethod]
        public void MobileAgentsRegex_IsNotNull()
        {
            // Act & Assert
            Assert.IsNotNull(ElectHttpDetectionConstants.MobileAgentsRegex);
        }

        [TestMethod]
        public void CrawlerAgentsRegex_IsNotNull()
        {
            // Act & Assert
            Assert.IsNotNull(ElectHttpDetectionConstants.CrawlerAgentsRegex);
        }

        [TestMethod]
        public void TabletAgentsRegex_MatchesIPad()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (iPad; CPU OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.TabletAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TabletAgentsRegex_MatchesAndroidTablet()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (Linux; Android 11; SM-T870) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.TabletAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MobileAgentsRegex_MatchesIPhone()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.MobileAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MobileAgentsRegex_MatchesAndroidMobile()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (Linux; Android 11; SM-G991B) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Mobile Safari/537.36";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.MobileAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CrawlerAgentsRegex_MatchesGoogleBot()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.CrawlerAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CrawlerAgentsRegex_MatchesBingBot()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.CrawlerAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CrawlerAgentsRegex_MatchesSpider()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (compatible; spider/1.0; +http://example.com/spider.htm)";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.CrawlerAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TabletAgentsRegex_DoesNotMatchDesktop()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.TabletAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MobileAgentsRegex_DoesNotMatchDesktop()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.MobileAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CrawlerAgentsRegex_DoesNotMatchDesktop()
        {
            // Arrange
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(userAgent, ElectHttpDetectionConstants.CrawlerAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AllRegexes_HandleEmptyString()
        {
            // Arrange
            var emptyUserAgent = string.Empty;

            // Act & Assert
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(emptyUserAgent, ElectHttpDetectionConstants.TabletAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(emptyUserAgent, ElectHttpDetectionConstants.MobileAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(emptyUserAgent, ElectHttpDetectionConstants.CrawlerAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
        }

        [TestMethod]
        public void AllRegexes_HandleNullString()
        {
            // Arrange
            string? nullUserAgent = null;

            // Act & Assert
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(nullUserAgent ?? string.Empty, ElectHttpDetectionConstants.TabletAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(nullUserAgent ?? string.Empty, ElectHttpDetectionConstants.MobileAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            Assert.IsFalse(System.Text.RegularExpressions.Regex.IsMatch(nullUserAgent ?? string.Empty, ElectHttpDetectionConstants.CrawlerAgentsRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase));
        }
    }
}