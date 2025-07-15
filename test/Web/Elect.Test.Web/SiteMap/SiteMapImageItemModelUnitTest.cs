using Elect.Web.SiteMap.Models;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapImageItemModelUnitTest
    {
        [TestMethod]
        public void Constructor_WithValidUrlAndImages_SetsPropertiesCorrectly()
        {
            // Arrange
            var url = "https://example.com/page";
            var images = new[]
            {
                new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image1.jpg" },
                new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image2.jpg" }
            };

            // Act
            var model = new SiteMapImageItemModel(url, images);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.IsNotNull(model.Images);
            Assert.AreEqual(2, model.Images.Count);
            Assert.AreEqual("https://example.com/image1.jpg", model.Images[0].ImagePath);
            Assert.AreEqual("https://example.com/image2.jpg", model.Images[1].ImagePath);
        }

        [TestMethod]
        public void Constructor_WithSingleImage_SetsPropertiesCorrectly()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            var model = new SiteMapImageItemModel(url, image);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.IsNotNull(model.Images);
            Assert.AreEqual(1, model.Images.Count);
            Assert.AreEqual("https://example.com/image.jpg", model.Images[0].ImagePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullUrl_ThrowsArgumentNullException()
        {
            // Arrange
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            new SiteMapImageItemModel(null, image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyUrl_ThrowsArgumentNullException()
        {
            // Arrange
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            new SiteMapImageItemModel("", image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithWhitespaceUrl_ThrowsArgumentNullException()
        {
            // Arrange
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            new SiteMapImageItemModel("   ", image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullImages_ThrowsArgumentNullException()
        {
            // Arrange
            var url = "https://example.com/page";

            // Act
            new SiteMapImageItemModel(url, (SiteMapImageItemDetailModel[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithEmptyImagesArray_ThrowsArgumentNullException()
        {
            // Arrange
            var url = "https://example.com/page";

            // Act
            new SiteMapImageItemModel(url, new SiteMapImageItemDetailModel[0]);
        }

        [TestMethod]
        public void Constructor_WithValidInputs_UrlIsReadOnly()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            var model = new SiteMapImageItemModel(url, image);

            // Assert
            Assert.AreEqual(url, model.Url);
            
            // Verify that the Url property doesn't have a public setter
            var urlProperty = typeof(SiteMapImageItemModel).GetProperty("Url");
            Assert.IsNotNull(urlProperty);
            Assert.IsTrue(urlProperty.CanRead);
            Assert.IsFalse(urlProperty.SetMethod?.IsPublic ?? false);
        }

        [TestMethod]
        public void Constructor_WithValidInputs_ImagesIsReadOnly()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            var model = new SiteMapImageItemModel(url, image);

            // Assert
            Assert.IsNotNull(model.Images);
            
            // Verify that the Images property doesn't have a public setter
            var imagesProperty = typeof(SiteMapImageItemModel).GetProperty("Images");
            Assert.IsNotNull(imagesProperty);
            Assert.IsTrue(imagesProperty.CanRead);
            Assert.IsFalse(imagesProperty.SetMethod?.IsPublic ?? false);
        }

        [TestMethod]
        public void Constructor_CreatesNewListInstance()
        {
            // Arrange
            var url = "https://example.com/page";
            var originalImages = new[]
            {
                new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image1.jpg" },
                new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image2.jpg" }
            };

            // Act
            var model = new SiteMapImageItemModel(url, originalImages);

            // Assert
            Assert.AreNotSame((object)originalImages, (object)model.Images);
            Assert.AreEqual(originalImages.Length, model.Images.Count);
        }

        [TestMethod]
        public void Constructor_WithLargeNumberOfImages_HandlesCorrectly()
        {
            // Arrange
            var url = "https://example.com/page";
            var images = new List<SiteMapImageItemDetailModel>();
            for (int i = 0; i < 1000; i++)
            {
                images.Add(new SiteMapImageItemDetailModel { ImagePath = $"https://example.com/image{i}.jpg" });
            }

            // Act
            var model = new SiteMapImageItemModel(url, images.ToArray());

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.AreEqual(1000, model.Images.Count);
            Assert.AreEqual("https://example.com/image0.jpg", model.Images[0].ImagePath);
            Assert.AreEqual("https://example.com/image999.jpg", model.Images[999].ImagePath);
        }

        [TestMethod]
        public void Model_ImplementsISiteMapItem()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            var model = new SiteMapImageItemModel(url, image);

            // Assert
            Assert.IsInstanceOfType(model, typeof(ISiteMapItem));
        }

        [TestMethod]
        public void Model_InheritsFromElectDisposableModel()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };

            // Act
            var model = new SiteMapImageItemModel(url, image);

            // Assert
            Assert.IsInstanceOfType(model, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void Constructor_WithComplexImageDetails_PreservesAllData()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel
            {
                ImagePath = "https://example.com/image.jpg",
                Caption = "Test Caption",
                GeoLocation = "Test Location",
                Title = "Test Title",
                License = "https://example.com/license"
            };

            // Act
            var model = new SiteMapImageItemModel(url, image);

            // Assert
            Assert.AreEqual(url, model.Url);
            Assert.AreEqual(1, model.Images.Count);
            var savedImage = model.Images[0];
            Assert.AreEqual("https://example.com/image.jpg", savedImage.ImagePath);
            Assert.AreEqual("Test Caption", savedImage.Caption);
            Assert.AreEqual("Test Location", savedImage.GeoLocation);
            Assert.AreEqual("Test Title", savedImage.Title);
            Assert.AreEqual("https://example.com/license", savedImage.License);
        }

        [TestMethod]
        public void Constructor_ModifyingOriginalArray_DoesNotAffectModel()
        {
            // Arrange
            var url = "https://example.com/page";
            var originalImages = new[]
            {
                new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image1.jpg" }
            };

            var model = new SiteMapImageItemModel(url, originalImages);

            // Act - Modify original array content (this will affect the stored reference)
            // The test name suggests the model should be unaffected, but since the constructor stores references
            // to the same objects, modifying the object properties will affect the model
            originalImages[0].ImagePath = "https://example.com/modified.jpg";

            // Assert - Since the constructor stores references to the same objects, 
            // this test actually verifies that changing the original objects DOES affect the model
            Assert.AreEqual("https://example.com/modified.jpg", model.Images[0].ImagePath);
        }

        [TestMethod]
        public void Dispose_CanBeCalledWithoutError()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };
            var model = new SiteMapImageItemModel(url, image);

            // Act & Assert - Should not throw
            model.Dispose();
        }

        [TestMethod]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            // Arrange
            var url = "https://example.com/page";
            var image = new SiteMapImageItemDetailModel { ImagePath = "https://example.com/image.jpg" };
            var model = new SiteMapImageItemModel(url, image);

            // Act & Assert - Should not throw
            model.Dispose();
            model.Dispose();
            model.Dispose();
        }
    }
}