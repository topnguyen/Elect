namespace Elect.Test.Data.IO.ImageUtils
{
    [TestClass]
    public class ImageHelperUnitTest
    {
        [TestMethod]
        public void GetImageInfo_FromBase64_ReturnsImageModel()
        {
            // Arrange
            var img = new Image<Rgba32>(10, 10);
            using var ms = new MemoryStream();
            img.SaveAsJpeg(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            // Act
            var result = ImageHelper.GetImageInfo(base64);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.WidthPx > 0 && result.HeightPx > 0);
        }
        [TestMethod]
        public void GetImageInfo_FromBytes_ReturnsImageModel()
        {
            var img = new Image<Rgba32>(10, 10);
            using var ms = new MemoryStream();
            img.SaveAsJpeg(ms);
            var bytes = ms.ToArray();
            var result = ImageHelper.GetImageInfo(bytes);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.WidthPx > 0 && result.HeightPx > 0);
        }
        [TestMethod]
        public void GetImageInfo_FromStream_ReturnsImageModel()
        {
            var img = new Image<Rgba32>(10, 10);
            using var ms = new MemoryStream();
            img.SaveAsJpeg(ms);
            ms.Position = 0;
            var result = ImageHelper.GetImageInfo(ms);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.WidthPx > 0 && result.HeightPx > 0);
        }
        [TestMethod]
        public void IsSvgImage_ValidSvg_ReturnsTrue()
        {
            var svg = "<svg xmlns='http://www.w3.org/2000/svg'></svg>";
            var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svg));
            Assert.IsTrue(ImageHelper.IsSvgImage(ms));
        }
        [TestMethod]
        public void IsSvgImage_InvalidSvg_ReturnsFalse()
        {
            var ms = new MemoryStream(new byte[] { 1, 2, 3 });
            Assert.IsFalse(ImageHelper.IsSvgImage(ms));
        }
        [TestMethod]
        public void GenerateTextImageBase64_ReturnsBase64()
        {
            var base64 = ImageHelper.GenerateTextImageBase64("A");
            Assert.IsFalse(string.IsNullOrWhiteSpace(base64));
        }
        [TestMethod]
        public void GenerateTextImage_ReturnsImage()
        {
            var font = new Font(SystemFonts.Families.First(), 10);
            var img = ImageHelper.GenerateTextImage("A", 50, 50, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, font);
            Assert.IsNotNull(img);
        }
        [TestMethod]
        public void GetImageBase64Format_ReturnsFormattedString()
        {
            var result = ImageHelper.GetImageBase64Format("abc123", ".jpg");
            Assert.IsTrue(result.StartsWith("data:image/jpeg;base64,"));
        }
        [TestMethod]
        public void GetBase64Format_ReturnsBase64()
        {
            var input = "data:image/jpeg;base64,abc123";
            var result = ImageHelper.GetBase64Format(input);
            Assert.AreEqual("abc123", result);
        }
        [TestMethod]
        public void RotateByExifOrientation_StringAndBytes_ReturnsValid()
        {
            var img = new Image<Rgba32>(10, 10);
            using var ms = new MemoryStream();
            img.SaveAsJpeg(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            var rotatedBase64 = ImageHelper.RotateByExifOrientation(base64);
            Assert.IsFalse(string.IsNullOrWhiteSpace(rotatedBase64));
            var rotatedBytes = ImageHelper.RotateByExifOrientation(ms.ToArray());
            Assert.IsNotNull(rotatedBytes);
        }
        [TestMethod]
        public void RotateByExifOrientation_Image_ReturnsImage()
        {
            var img = new Image<Rgba32>(10, 10);
            var rotated = ImageHelper.RotateByExifOrientation(img);
            Assert.IsNotNull(rotated);
        }
    }
}
