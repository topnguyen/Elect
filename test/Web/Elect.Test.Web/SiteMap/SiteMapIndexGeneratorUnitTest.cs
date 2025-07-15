using Elect.Web.SiteMap.Models;
using Elect.Web.SiteMap.Services;
using System.Xml.Linq;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapIndexGeneratorUnitTest
    {
        private SiteMapIndexGenerator _generator;

        [TestInitialize]
        public void Setup()
        {
            _generator = new SiteMapIndexGenerator();
        }

        [TestMethod]
        public void GenerateContentResult_WithValidItems_ReturnsContentResult()
        {
            // Arrange
            var items = new[]
            {
                new SiteMapIndexItemModel("https://example.com/sitemap1.xml"),
                new SiteMapIndexItemModel("https://example.com/sitemap2.xml")
            };

            // Act
            var result = _generator.GenerateContentResult(items);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("application/xml", result.ContentType);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Contains("https://example.com/sitemap1.xml"));
            Assert.IsTrue(result.Content.Contains("https://example.com/sitemap2.xml"));
        }

        [TestMethod]
        public void GenerateXmlString_WithSingleItem_ReturnsValidXml()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsNotNull(xml);
            Assert.IsTrue(xml.Contains("<?xml") || xml.Contains("<sitemapindex"));
            Assert.IsTrue(xml.Contains("https://example.com/sitemap.xml"));
            Assert.IsTrue(xml.Contains("sitemapindex"));
            Assert.IsTrue(xml.Contains("sitemap"));
        }

        [TestMethod]
        public void GenerateXmlString_WithLastModified_IncludesLastModElement()
        {
            // Arrange
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml", lastModified);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/sitemap.xml"));
            Assert.IsTrue(xml.Contains("lastmod"));
            Assert.IsTrue(xml.Contains("2023-01-01T12:00:00"));
        }

        [TestMethod]
        public void GenerateXmlString_WithMultipleItems_IncludesAllItems()
        {
            // Arrange
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);
            var items = new[]
            {
                new SiteMapIndexItemModel("https://example.com/sitemap1.xml", lastModified),
                new SiteMapIndexItemModel("https://example.com/sitemap2.xml"),
                new SiteMapIndexItemModel("https://example.com/sitemap3.xml", lastModified)
            };

            // Act
            var xml = _generator.GenerateXmlString(items);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/sitemap1.xml"));
            Assert.IsTrue(xml.Contains("https://example.com/sitemap2.xml"));
            Assert.IsTrue(xml.Contains("https://example.com/sitemap3.xml"));
            Assert.IsTrue(xml.Contains("2023-01-01T12:00:00"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateXmlString_WithNullItems_ThrowsArgumentNullException()
        {
            // Act
            _generator.GenerateXmlString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateXmlString_WithEmptyArray_ThrowsArgumentNullException()
        {
            // Act
            _generator.GenerateXmlString(new SiteMapIndexItemModel[0]);
        }

        [TestMethod]
        public void GenerateXmlString_UrlsAreLowercase()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://EXAMPLE.COM/SITEMAP.XML");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/sitemap.xml"));
            Assert.IsFalse(xml.Contains("https://EXAMPLE.COM/SITEMAP.XML"));
        }

        [TestMethod]
        public void GenerateXmlString_ValidXmlStructure()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            // Should be able to parse as valid XML
            var doc = XDocument.Parse(xml);
            Assert.IsNotNull(doc);
            Assert.AreEqual("sitemapindex", doc.Root.Name.LocalName);
        }

        [TestMethod]
        public void GenerateXmlString_WithoutLastModified_OnlyIncludesRequiredElements()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/sitemap.xml"));
            Assert.IsFalse(xml.Contains("lastmod"));
        }

        [TestMethod]
        public void GenerateXmlString_CorrectNamespaces()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\""));
            Assert.IsTrue(xml.Contains("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\""));
            Assert.IsTrue(xml.Contains("xsi:schemaLocation"));
        }

        [TestMethod]
        public void GenerateXmlString_LastModifiedFormat_IsCorrect()
        {
            // Arrange
            var lastModified = new DateTime(2023, 12, 25, 14, 30, 45, DateTimeKind.Local);
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml", lastModified);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            // Should be in ISO 8601 format with timezone
            Assert.IsTrue(xml.Contains("2023-12-25T14:30:45"));
        }

        [TestMethod]
        public void GenerateXmlString_WithUtcDateTime_FormatsCorrectly()
        {
            // Arrange
            var lastModified = new DateTime(2023, 12, 25, 14, 30, 45, DateTimeKind.Utc);
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml", lastModified);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("2023-12-25T14:30:45"));
        }

        [TestMethod]
        public void GenerateXmlString_WithUnspecifiedDateTime_FormatsCorrectly()
        {
            // Arrange
            var lastModified = new DateTime(2023, 12, 25, 14, 30, 45, DateTimeKind.Unspecified);
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml", lastModified);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("2023-12-25T14:30:45"));
        }

        [TestMethod]
        public void GenerateXmlString_CorrectSchemaLocation()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd"));
        }

        [TestMethod]
        public void GenerateXmlString_XmlDeclaration_IsCorrect()
        {
            // Arrange
            var item = new SiteMapIndexItemModel("https://example.com/sitemap.xml");

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.StartsWith("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateContentResult_WithNullItems_ThrowsArgumentNullException()
        {
            // Act
            _generator.GenerateContentResult(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateContentResult_WithEmptyArray_ThrowsArgumentNullException()
        {
            // Act
            _generator.GenerateContentResult(new SiteMapIndexItemModel[0]);
        }

        [TestMethod]
        public void GenerateXmlString_WithLargeNumberOfItems_HandlesCorrectly()
        {
            // Arrange
            var items = new List<SiteMapIndexItemModel>();
            for (int i = 0; i < 100; i++)
            {
                items.Add(new SiteMapIndexItemModel($"https://example.com/sitemap{i}.xml"));
            }

            // Act
            var xml = _generator.GenerateXmlString(items.ToArray());

            // Assert
            Assert.IsNotNull(xml);
            Assert.IsTrue(xml.Contains("sitemap0.xml"));
            Assert.IsTrue(xml.Contains("sitemap99.xml"));
        }

        [TestMethod]
        public void GenerateXmlString_ParsedDocument_HasCorrectStructure()
        {
            // Arrange
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);
            var items = new[]
            {
                new SiteMapIndexItemModel("https://example.com/sitemap1.xml", lastModified),
                new SiteMapIndexItemModel("https://example.com/sitemap2.xml")
            };

            // Act
            var xml = _generator.GenerateXmlString(items);
            var doc = XDocument.Parse(xml);

            // Assert
            Assert.AreEqual("sitemapindex", doc.Root.Name.LocalName);
            var sitemapElements = doc.Root.Elements().ToList();
            Assert.AreEqual(2, sitemapElements.Count);
            
            foreach (var sitemapElement in sitemapElements)
            {
                Assert.AreEqual("sitemap", sitemapElement.Name.LocalName);
                var locElement = sitemapElement.Element(XName.Get("loc", "http://www.sitemaps.org/schemas/sitemap/0.9"));
                Assert.IsNotNull(locElement);
                Assert.IsTrue(locElement.Value.Contains("https://example.com/sitemap"));
            }
        }
    }
}