using Elect.Web.SiteMap.Models;
using Elect.Web.SiteMap.Services;
using System.Xml.Linq;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapImageGeneratorUnitTest
    {
        private SiteMapImageGenerator _generator;

        [TestInitialize]
        public void Setup()
        {
            _generator = new SiteMapImageGenerator();
        }

        [TestMethod]
        public void GenerateContentResult_WithValidItems_ReturnsContentResult()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg"
            };
            var items = new[]
            {
                new SiteMapImageItemModel("https://example.com/page1", imageDetail),
                new SiteMapImageItemModel("https://example.com/page2", imageDetail)
            };

            // Act
            var result = _generator.GenerateContentResult(items);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("application/xml", result.ContentType);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Contains("https://example.com/page1"));
            Assert.IsTrue(result.Content.Contains("https://example.com/page2"));
            Assert.IsTrue(result.Content.Contains("https://example.com/image.jpg"));
        }

        [TestMethod]
        public void GenerateXmlString_WithSingleItem_ReturnsValidXml()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg"
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsNotNull(xml);
            Assert.IsTrue(xml.Contains("<?xml") || xml.Contains("<urlset"));
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsTrue(xml.Contains("urlset"));
            Assert.IsTrue(xml.Contains("xmlns:image"));
        }

        [TestMethod]
        public void GenerateXmlString_WithCompleteImageDetails_IncludesAllElements()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = "Test Caption",
                GeoLocation = "Test Location",
                Title = "Test Title",
                License = "https://example.com/license"
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsTrue(xml.Contains("Test Caption"));
            Assert.IsTrue(xml.Contains("Test Location"));
            Assert.IsTrue(xml.Contains("Test Title"));
            Assert.IsTrue(xml.Contains("https://example.com/license"));
            Assert.IsTrue(xml.Contains("caption"));
            Assert.IsTrue(xml.Contains("geo_location"));
            Assert.IsTrue(xml.Contains("title"));
            Assert.IsTrue(xml.Contains("license"));
        }

        [TestMethod]
        public void GenerateXmlString_WithMultipleImages_IncludesAllImages()
        {
            // Arrange
            var imageDetail1 = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image1.jpg",
                Caption = "Image 1"
            };
            var imageDetail2 = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image2.jpg",
                Caption = "Image 2"
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail1, imageDetail2);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/image1.jpg"));
            Assert.IsTrue(xml.Contains("https://example.com/image2.jpg"));
            Assert.IsTrue(xml.Contains("Image 1"));
            Assert.IsTrue(xml.Contains("Image 2"));
        }

        [TestMethod]
        public void GenerateXmlString_WithMultipleItems_IncludesAllItems()
        {
            // Arrange
            var imageDetail1 = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image1.jpg"
            };
            var imageDetail2 = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image2.jpg"
            };
            var items = new[]
            {
                new SiteMapImageItemModel("https://example.com/page1", imageDetail1),
                new SiteMapImageItemModel("https://example.com/page2", imageDetail2)
            };

            // Act
            var xml = _generator.GenerateXmlString(items);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page1"));
            Assert.IsTrue(xml.Contains("https://example.com/page2"));
            Assert.IsTrue(xml.Contains("https://example.com/image1.jpg"));
            Assert.IsTrue(xml.Contains("https://example.com/image2.jpg"));
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
            _generator.GenerateXmlString(new SiteMapImageItemModel[0]);
        }

        [TestMethod]
        public void GenerateXmlString_UrlsAreLowercase()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://EXAMPLE.COM/IMAGE.JPG"
            };
            var item = new SiteMapImageItemModel("https://EXAMPLE.COM/PAGE", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsFalse(xml.Contains("https://EXAMPLE.COM/PAGE"));
            Assert.IsFalse(xml.Contains("https://EXAMPLE.COM/IMAGE.JPG"));
        }

        [TestMethod]
        public void GenerateXmlString_ValidXmlStructure()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg"
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            // Should be able to parse as valid XML
            var doc = XDocument.Parse(xml);
            Assert.IsNotNull(doc);
            Assert.AreEqual("urlset", doc.Root.Name.LocalName);
            
            // Check namespaces
            var imageNamespace = doc.Root.GetNamespaceOfPrefix("image");
            Assert.IsNotNull(imageNamespace);
            Assert.AreEqual("http://www.google.com/schemas/sitemap-image/1.1", imageNamespace.NamespaceName);
        }

        [TestMethod]
        public void GenerateXmlString_WithMinimalImageData_OnlyIncludesRequiredElements()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg"
                // Only required field set
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsFalse(xml.Contains("image:caption"));
            Assert.IsFalse(xml.Contains("image:geo_location"));
            Assert.IsFalse(xml.Contains("image:title"));
            Assert.IsFalse(xml.Contains("image:license"));
        }

        [TestMethod]
        public void GenerateXmlString_WithNullOptionalValues_OnlyIncludesRequiredElements()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = null,
                GeoLocation = null,
                Title = null,
                License = null
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsFalse(xml.Contains("image:caption"));
            Assert.IsFalse(xml.Contains("image:geo_location"));
            Assert.IsFalse(xml.Contains("image:title"));
            Assert.IsFalse(xml.Contains("image:license"));
        }

        [TestMethod]
        public void GenerateXmlString_WithEmptyOptionalValues_OnlyIncludesRequiredElements()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = "",
                GeoLocation = "",
                Title = "",
                License = ""
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsFalse(xml.Contains("image:caption"));
            Assert.IsFalse(xml.Contains("image:geo_location"));
            Assert.IsFalse(xml.Contains("image:title"));
            Assert.IsFalse(xml.Contains("image:license"));
        }

        [TestMethod]
        public void GenerateXmlString_WithWhitespaceOptionalValues_OnlyIncludesRequiredElements()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = "   ",
                GeoLocation = "\t",
                Title = "\n",
                License = " \t\n "
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("https://example.com/page"));
            Assert.IsTrue(xml.Contains("https://example.com/image.jpg"));
            Assert.IsFalse(xml.Contains("image:caption"));
            Assert.IsFalse(xml.Contains("image:geo_location"));
            Assert.IsFalse(xml.Contains("image:title"));
            Assert.IsFalse(xml.Contains("image:license"));
        }

        [TestMethod]
        public void GenerateXmlString_CorrectNamespaces()
        {
            // Arrange
            var imageDetail = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg"
            };
            var item = new SiteMapImageItemModel("https://example.com/page", imageDetail);

            // Act
            var xml = _generator.GenerateXmlString(item);

            // Assert
            Assert.IsTrue(xml.Contains("xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\""));
            Assert.IsTrue(xml.Contains("xmlns:image=\"http://www.google.com/schemas/sitemap-image/1.1\""));
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
            _generator.GenerateContentResult(new SiteMapImageItemModel[0]);
        }
    }
}