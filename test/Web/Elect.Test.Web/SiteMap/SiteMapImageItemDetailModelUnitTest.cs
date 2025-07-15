using Elect.Web.SiteMap.Models;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapImageItemDetailModelUnitTest
    {
        [TestMethod]
        public void Constructor_DefaultValues_AreNull()
        {
            // Act
            var model = new SiteMapImageItemDetailModel();

            // Assert
            Assert.IsNull(model.Caption);
            Assert.IsNull(model.GeoLocation);
            Assert.IsNull(model.ImagePath);
            Assert.IsNull(model.License);
            Assert.IsNull(model.Title);
        }

        [TestMethod]
        public void ImagePath_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var imagePath = "https://example.com/image.jpg";

            // Act
            model.ImagePath = imagePath;

            // Assert
            Assert.AreEqual(imagePath, model.ImagePath);
        }

        [TestMethod]
        public void Caption_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var caption = "Test Caption";

            // Act
            model.Caption = caption;

            // Assert
            Assert.AreEqual(caption, model.Caption);
        }

        [TestMethod]
        public void GeoLocation_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var geoLocation = "Test Location";

            // Act
            model.GeoLocation = geoLocation;

            // Assert
            Assert.AreEqual(geoLocation, model.GeoLocation);
        }

        [TestMethod]
        public void Title_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var title = "Test Title";

            // Act
            model.Title = title;

            // Assert
            Assert.AreEqual(title, model.Title);
        }

        [TestMethod]
        public void License_CanBeSetAndGet()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var license = "https://example.com/license";

            // Act
            model.License = license;

            // Assert
            Assert.AreEqual(license, model.License);
        }

        [TestMethod]
        public void AllProperties_CanBeSetTogether()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var imagePath = "https://example.com/image.jpg";
            var caption = "Test Caption";
            var geoLocation = "Test Location";
            var title = "Test Title";
            var license = "https://example.com/license";

            // Act
            model.ImagePath = imagePath;
            model.Caption = caption;
            model.GeoLocation = geoLocation;
            model.Title = title;
            model.License = license;

            // Assert
            Assert.AreEqual(imagePath, model.ImagePath);
            Assert.AreEqual(caption, model.Caption);
            Assert.AreEqual(geoLocation, model.GeoLocation);
            Assert.AreEqual(title, model.Title);
            Assert.AreEqual(license, model.License);
        }

        [TestMethod]
        public void Properties_CanBeSetToNull()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = "Test Caption",
                GeoLocation = "Test Location",
                Title = "Test Title",
                License = "https://example.com/license"
            };

            // Act
            model.ImagePath = null;
            model.Caption = null;
            model.GeoLocation = null;
            model.Title = null;
            model.License = null;

            // Assert
            Assert.IsNull(model.ImagePath);
            Assert.IsNull(model.Caption);
            Assert.IsNull(model.GeoLocation);
            Assert.IsNull(model.Title);
            Assert.IsNull(model.License);
        }

        [TestMethod]
        public void Properties_CanBeSetToEmptyString()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();

            // Act
            model.ImagePath = "";
            model.Caption = "";
            model.GeoLocation = "";
            model.Title = "";
            model.License = "";

            // Assert
            Assert.AreEqual("", model.ImagePath);
            Assert.AreEqual("", model.Caption);
            Assert.AreEqual("", model.GeoLocation);
            Assert.AreEqual("", model.Title);
            Assert.AreEqual("", model.License);
        }

        [TestMethod]
        public void Properties_CanBeSetToWhitespace()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();

            // Act
            model.ImagePath = "   ";
            model.Caption = "\t";
            model.GeoLocation = "\n";
            model.Title = "\r";
            model.License = " \t\n\r ";

            // Assert
            Assert.AreEqual("   ", model.ImagePath);
            Assert.AreEqual("\t", model.Caption);
            Assert.AreEqual("\n", model.GeoLocation);
            Assert.AreEqual("\r", model.Title);
            Assert.AreEqual(" \t\n\r ", model.License);
        }

        [TestMethod]
        public void ImagePath_WithComplexUrl_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var complexUrl = "https://subdomain.example.com:8080/path/to/image.jpg?param=value&other=test#fragment";

            // Act
            model.ImagePath = complexUrl;

            // Assert
            Assert.AreEqual(complexUrl, model.ImagePath);
        }

        [TestMethod]
        public void Caption_WithLongText_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var longCaption = new string('A', 1000);

            // Act
            model.Caption = longCaption;

            // Assert
            Assert.AreEqual(longCaption, model.Caption);
            Assert.AreEqual(1000, model.Caption.Length);
        }

        [TestMethod]
        public void Title_WithSpecialCharacters_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var specialTitle = "Title with special chars: !@#$%^&*()_+-=[]{}|;':\",./<>?";

            // Act
            model.Title = specialTitle;

            // Assert
            Assert.AreEqual(specialTitle, model.Title);
        }

        [TestMethod]
        public void GeoLocation_WithCoordinates_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var coordinates = "40.7128, -74.0060";

            // Act
            model.GeoLocation = coordinates;

            // Assert
            Assert.AreEqual(coordinates, model.GeoLocation);
        }

        [TestMethod]
        public void License_WithHttpsUrl_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var licenseUrl = "https://creativecommons.org/licenses/by/4.0/";

            // Act
            model.License = licenseUrl;

            // Assert
            Assert.AreEqual(licenseUrl, model.License);
        }

        [TestMethod]
        public void Model_InheritsFromElectDisposableModel()
        {
            // Arrange & Act
            var model = new SiteMapImageItemDetailModel();

            // Assert
            Assert.IsInstanceOfType(model, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void Properties_WithUnicodeCharacters_HandlesCorrectly()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();
            var unicodeTitle = "图片标题";
            var unicodeCaption = "图片说明";
            var unicodeLocation = "北京市";

            // Act
            model.Title = unicodeTitle;
            model.Caption = unicodeCaption;
            model.GeoLocation = unicodeLocation;

            // Assert
            Assert.AreEqual(unicodeTitle, model.Title);
            Assert.AreEqual(unicodeCaption, model.Caption);
            Assert.AreEqual(unicodeLocation, model.GeoLocation);
        }

        [TestMethod]
        public void Properties_CanBeModifiedMultipleTimes()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();

            // Act & Assert - First assignment
            model.ImagePath = "https://example.com/image1.jpg";
            Assert.AreEqual("https://example.com/image1.jpg", model.ImagePath);

            // Act & Assert - Second assignment
            model.ImagePath = "https://example.com/image2.jpg";
            Assert.AreEqual("https://example.com/image2.jpg", model.ImagePath);

            // Act & Assert - Third assignment
            model.ImagePath = "https://example.com/image3.jpg";
            Assert.AreEqual("https://example.com/image3.jpg", model.ImagePath);
        }

        [TestMethod]
        public void ObjectInitializer_WorksCorrectly()
        {
            // Arrange & Act
            var model = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = "Test Caption",
                GeoLocation = "Test Location",
                Title = "Test Title",
                License = "https://example.com/license"
            };

            // Assert
            Assert.AreEqual("https://example.com/image.jpg", model.ImagePath);
            Assert.AreEqual("Test Caption", model.Caption);
            Assert.AreEqual("Test Location", model.GeoLocation);
            Assert.AreEqual("Test Title", model.Title);
            Assert.AreEqual("https://example.com/license", model.License);
        }

        [TestMethod]
        public void Dispose_CanBeCalledWithoutError()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();

            // Act & Assert - Should not throw
            model.Dispose();
        }

        [TestMethod]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Arrange
            var model = new SiteMapImageItemDetailModel();

            // Act & Assert - Should not throw
            model.Dispose();
            model.Dispose();
            model.Dispose();
        }

        [TestMethod]
        public void Properties_AllHavePublicGetters()
        {
            // Arrange
            var type = typeof(SiteMapImageItemDetailModel);

            // Act & Assert
            var imagePathProperty = type.GetProperty("ImagePath");
            Assert.IsNotNull(imagePathProperty);
            Assert.IsTrue(imagePathProperty.CanRead);
            Assert.IsTrue(imagePathProperty.GetMethod.IsPublic);

            var captionProperty = type.GetProperty("Caption");
            Assert.IsNotNull(captionProperty);
            Assert.IsTrue(captionProperty.CanRead);
            Assert.IsTrue(captionProperty.GetMethod.IsPublic);

            var geoLocationProperty = type.GetProperty("GeoLocation");
            Assert.IsNotNull(geoLocationProperty);
            Assert.IsTrue(geoLocationProperty.CanRead);
            Assert.IsTrue(geoLocationProperty.GetMethod.IsPublic);

            var titleProperty = type.GetProperty("Title");
            Assert.IsNotNull(titleProperty);
            Assert.IsTrue(titleProperty.CanRead);
            Assert.IsTrue(titleProperty.GetMethod.IsPublic);

            var licenseProperty = type.GetProperty("License");
            Assert.IsNotNull(licenseProperty);
            Assert.IsTrue(licenseProperty.CanRead);
            Assert.IsTrue(licenseProperty.GetMethod.IsPublic);
        }

        [TestMethod]
        public void Properties_AllHavePublicSetters()
        {
            // Arrange
            var type = typeof(SiteMapImageItemDetailModel);

            // Act & Assert
            var imagePathProperty = type.GetProperty("ImagePath");
            Assert.IsNotNull(imagePathProperty);
            Assert.IsTrue(imagePathProperty.CanWrite);
            Assert.IsTrue(imagePathProperty.SetMethod.IsPublic);

            var captionProperty = type.GetProperty("Caption");
            Assert.IsNotNull(captionProperty);
            Assert.IsTrue(captionProperty.CanWrite);
            Assert.IsTrue(captionProperty.SetMethod.IsPublic);

            var geoLocationProperty = type.GetProperty("GeoLocation");
            Assert.IsNotNull(geoLocationProperty);
            Assert.IsTrue(geoLocationProperty.CanWrite);
            Assert.IsTrue(geoLocationProperty.SetMethod.IsPublic);

            var titleProperty = type.GetProperty("Title");
            Assert.IsNotNull(titleProperty);
            Assert.IsTrue(titleProperty.CanWrite);
            Assert.IsTrue(titleProperty.SetMethod.IsPublic);

            var licenseProperty = type.GetProperty("License");
            Assert.IsNotNull(licenseProperty);
            Assert.IsTrue(licenseProperty.CanWrite);
            Assert.IsTrue(licenseProperty.SetMethod.IsPublic);
        }
    }
}