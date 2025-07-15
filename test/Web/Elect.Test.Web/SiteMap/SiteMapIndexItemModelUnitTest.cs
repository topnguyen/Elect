using Elect.Web.SiteMap.Models;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapIndexItemModelUnitTest
    {
        [TestMethod]
        public void Constructor_WithValidUrl_SetsPropertiesCorrectly()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.IsNull(model.LastModified);
        }

        [TestMethod]
        public void Constructor_WithUrlAndLastModified_SetsPropertiesCorrectly()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.AreEqual(lastModified, model.LastModified);
        }

        [TestMethod]
        public void Constructor_WithNullLastModified_SetsLastModifiedToNull()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url, null);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.IsNull(model.LastModified);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullUrl_ThrowsArgumentNullException()
        {
            // Act
            new SiteMapIndexItemModel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyUrl_ThrowsArgumentNullException()
        {
            // Act
            new SiteMapIndexItemModel("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithWhitespaceUrl_ThrowsArgumentNullException()
        {
            // Act
            new SiteMapIndexItemModel("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithTabsAndNewlines_ThrowsArgumentNullException()
        {
            // Act
            new SiteMapIndexItemModel("\t\n\r ");
        }

        [TestMethod]
        public void Constructor_WithValidInputs_UrlIsReadOnly()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
            
            // Verify that the Url property doesn't have a public setter
            var urlProperty = typeof(SiteMapIndexItemModel).GetProperty("Url");
            Assert.IsNotNull(urlProperty);
            Assert.IsTrue(urlProperty.CanRead);
            Assert.IsFalse(urlProperty.SetMethod?.IsPublic ?? false);
        }

        [TestMethod]
        public void Constructor_WithValidInputs_LastModifiedIsReadOnly()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(lastModified, model.LastModified);
            
            // Verify that the LastModified property doesn't have a public setter
            var lastModifiedProperty = typeof(SiteMapIndexItemModel).GetProperty("LastModified");
            Assert.IsNotNull(lastModifiedProperty);
            Assert.IsTrue(lastModifiedProperty.CanRead);
            Assert.IsFalse(lastModifiedProperty.SetMethod?.IsPublic ?? false);
        }

        [TestMethod]
        public void Constructor_WithUtcDateTime_PreservesDateTimeKind()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(lastModified, model.LastModified);
            Assert.AreEqual(DateTimeKind.Utc, model.LastModified.Value.Kind);
        }

        [TestMethod]
        public void Constructor_WithLocalDateTime_PreservesDateTimeKind()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(lastModified, model.LastModified);
            Assert.AreEqual(DateTimeKind.Local, model.LastModified.Value.Kind);
        }

        [TestMethod]
        public void Constructor_WithUnspecifiedDateTime_PreservesDateTimeKind()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Unspecified);

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(lastModified, model.LastModified);
            Assert.AreEqual(DateTimeKind.Unspecified, model.LastModified.Value.Kind);
        }

        [TestMethod]
        public void Constructor_WithMinDateTime_HandlesCorrectly()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = DateTime.MinValue;

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(lastModified, model.LastModified);
        }

        [TestMethod]
        public void Constructor_WithMaxDateTime_HandlesCorrectly()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var lastModified = DateTime.MaxValue;

            // Act
            var model = new SiteMapIndexItemModel(url, lastModified);

            // Assert
            Assert.AreEqual(lastModified, model.LastModified);
        }

        [TestMethod]
        public void Model_ImplementsISiteMapItem()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.IsInstanceOfType(model, typeof(ISiteMapItem));
        }

        [TestMethod]
        public void Model_InheritsFromElectDisposableModel()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.IsInstanceOfType(model, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void Constructor_WithComplexUrl_PreservesUrl()
        {
            // Arrange
            var url = "https://subdomain.example.com:8080/path/to/sitemap.xml?param=value&other=test#fragment";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
        }

        [TestMethod]
        public void Constructor_WithHttpUrl_PreservesUrl()
        {
            // Arrange
            var url = "http://example.com/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
        }

        [TestMethod]
        public void Constructor_WithRelativeUrl_PreservesUrl()
        {
            // Arrange
            var url = "/sitemap.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
        }

        [TestMethod]
        public void Dispose_CanBeCalledWithoutError()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var model = new SiteMapIndexItemModel(url);

            // Act & Assert - Should not throw
            model.Dispose();
        }

        [TestMethod]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Arrange
            var url = "https://example.com/sitemap.xml";
            var model = new SiteMapIndexItemModel(url);

            // Act & Assert - Should not throw
            model.Dispose();
            model.Dispose();
            model.Dispose();
        }

        [TestMethod]
        public void Constructor_WithVeryLongUrl_HandlesCorrectly()
        {
            // Arrange
            var baseUrl = "https://example.com/";
            var longPath = new string('a', 2000); // Very long path
            var url = baseUrl + longPath + ".xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.IsTrue(model.Url.Length > 2000);
        }

        [TestMethod]
        public void Constructor_WithUnicodeUrl_HandlesCorrectly()
        {
            // Arrange
            var url = "https://example.com/网站地图.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
        }

        [TestMethod]
        public void Constructor_WithSpecialCharactersUrl_HandlesCorrectly()
        {
            // Arrange
            var url = "https://example.com/site-map_v2.0.xml";

            // Act
            var model = new SiteMapIndexItemModel(url);

            // Assert
            Assert.AreEqual(url, model.Url);
        }
    }
}