using Elect.Web.SiteMap.Models;
using Elect.Web.SiteMap.Services;
using System.Xml.Linq;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapGeneratorUnitTest
    {
        private SiteMapGenerator _generator;

        [TestInitialize]
        public void Setup()
        {
            _generator = new SiteMapGenerator();
        }

        [TestMethod]
        public void GenerateContentResult_WithValidItems_ReturnsContentResult()
        {
            var items = new[]
            {
                new SiteMapItem("https://example.com/page1"),
                new SiteMapItem("https://example.com/page2")
            };

            var result = _generator.GenerateContentResult(items);

            Assert.IsNotNull(result);
            Assert.AreEqual("application/xml", result.ContentType);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Contains("https://example.com/page1"));
            Assert.IsTrue(result.Content.Contains("https://example.com/page2"));
        }

        [TestMethod]
        public void GenerateXmlString_WithSingleItem_ReturnsValidXml()
        {
            var item = new SiteMapItem("https://example.com/page");
            
            var xml = _generator.GenerateXmlString(item);

            Assert.IsNotNull(xml);
            Assert.IsTrue(xml.Contains("<?xml") || xml.Contains("<urlset"));
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("urlset"));
            Assert.IsTrue(xml.Contains("sitemap") || xml.Contains("schemas"));
        }

        [TestMethod]
        public void GenerateXmlString_WithCompleteItem_IncludesAllElements()
        {
            var lastModified = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);
            var item = new SiteMapItem("https://example.com/page", lastModified, SiteMapItemFrequency.Daily, 0.8);
            
            var xml = _generator.GenerateXmlString(item);

            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("lastmod"));
            Assert.IsTrue(xml.Contains("changefreq"));
            Assert.IsTrue(xml.Contains("daily"));
            Assert.IsTrue(xml.Contains("priority"));
            Assert.IsTrue(xml.Contains("0.8"));
        }

        [TestMethod]
        public void GenerateXmlString_WithMultipleItems_IncludesAllItems()
        {
            var items = new[]
            {
                new SiteMapItem("https://example.com/page1", frequency: SiteMapItemFrequency.Weekly),
                new SiteMapItem("https://example.com/page2", priority: 0.9),
                new SiteMapItem("https://example.com/page3")
            };

            var xml = _generator.GenerateXmlString(items);

            Assert.IsTrue(xml.Contains("https://example.com/page1"));
            Assert.IsTrue(xml.Contains("https://example.com/page2"));
            Assert.IsTrue(xml.Contains("https://example.com/page3"));
            Assert.IsTrue(xml.Contains("weekly"));
            Assert.IsTrue(xml.Contains("0.9"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateXmlString_WithEmptyArray_ThrowsArgumentNullException()
        {
            _generator.GenerateXmlString();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateXmlString_WithEmptyItems_ThrowsArgumentNullException()
        {
            _generator.GenerateXmlString(new SiteMapItem[0]);
        }

        [TestMethod]
        public void GenerateXmlString_UrlsAreLowercase()
        {
            var item = new SiteMapItem("https://EXAMPLE.COM/PAGE");
            
            var xml = _generator.GenerateXmlString(item);

            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsFalse(xml.Contains("https://EXAMPLE.COM/PAGE"));
        }

        [TestMethod]
        public void GenerateXmlString_FrequencyIsLowercase()
        {
            var item = new SiteMapItem("https://example.com", frequency: SiteMapItemFrequency.Monthly);
            
            var xml = _generator.GenerateXmlString(item);

            Assert.IsTrue(xml.Contains("monthly"));
        }

        [TestMethod]
        public void GenerateXmlString_ValidXmlStructure()
        {
            var item = new SiteMapItem("https://example.com");
            
            var xml = _generator.GenerateXmlString(item);

            // Should be able to parse as valid XML
            var doc = XDocument.Parse(xml);
            Assert.IsNotNull(doc);
            Assert.AreEqual("urlset", doc.Root.Name.LocalName);
        }

        [TestMethod]
        public void GenerateXmlString_WithNullOptionalValues_OnlyIncludesRequiredElements()
        {
            var item = new SiteMapItem("https://example.com");
            
            var xml = _generator.GenerateXmlString(item);

            Assert.IsTrue(xml.Contains("https://example.com"));
            Assert.IsFalse(xml.Contains("lastmod"));
            Assert.IsFalse(xml.Contains("changefreq"));
            Assert.IsFalse(xml.Contains("priority"));
        }
    }
}