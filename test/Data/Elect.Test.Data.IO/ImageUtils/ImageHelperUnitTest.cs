using Elect.Data.IO.ImageUtils;
using Elect.Data.IO.ImageUtils.CompressUtils;
using Elect.Data.IO.ImageUtils.CompressUtils.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Data.IO.ImageUtils
{
    [TestClass]
    [TestCategory("Unit")]
    [TestCategory("Fast")]
    public class ImageHelperUnitTest
    {
        // Create minimal test data in memory instead of loading real files
        private static byte[] CreateTestJpegBytes()
        {
            // Minimal JPEG header for testing (not a real image, just for testing business logic)
            return new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46 };
        }

        private static byte[] CreateTestPngBytes()
        {
            // Minimal PNG header for testing
            return new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        }

        [TestMethod]
        public void GetImageBase64Format_WithValidBase64_ReturnsFormattedString()
        {
            var testBytes = CreateTestJpegBytes();
            var base64 = Convert.ToBase64String(testBytes);
            
            var result = ImageHelper.GetImageBase64Format(base64, ".jpg");
            
            Assert.IsTrue(result.StartsWith("data:image/jpeg;base64,"));
            Assert.IsTrue(result.Contains(base64));
        }

        [TestMethod]
        public void GetBase64Format_WithValidBase64_ExtractsBase64String()
        {
            var testBytes = CreateTestJpegBytes();
            var base64 = Convert.ToBase64String(testBytes);
            var imageBase64Format = $"data:image/jpeg;base64,{base64}";
            
            var result = ImageHelper.GetBase64Format(imageBase64Format);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(base64, result);
            
            // Verify round-trip conversion
            var convertedBack = Convert.FromBase64String(result);
            CollectionAssert.AreEqual(testBytes, convertedBack);
        }

        [TestMethod]
        public void IsSvgImage_WithSvgContent_ReturnsTrue()
        {
            var svgContent = "<svg xmlns=\"http://www.w3.org/2000/svg\"><rect width=\"10\" height=\"10\"/></svg>";
            var svgBytes = System.Text.Encoding.UTF8.GetBytes(svgContent);
            
            using var stream = new MemoryStream(svgBytes);
            var result = ImageHelper.IsSvgImage(stream);
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSvgImage_WithNonSvgContent_ReturnsFalse()
        {
            var testBytes = CreateTestJpegBytes();
            
            using var stream = new MemoryStream(testBytes);
            var result = ImageHelper.IsSvgImage(stream);
            
            Assert.IsFalse(result);
        }
    }

    [TestClass]
    [TestCategory("Unit")]
    [TestCategory("Fast")]
    public class ImageCompressedModelUnitTest
    {
        [TestMethod]
        public void Constructor_Default_InitializesProperties()
        {
            var model = new ImageCompressedModel();
            
            Assert.IsNotNull(model.ResultFileStream);
            Assert.AreEqual(0, model.OriginalFileSize);
            Assert.AreEqual(0, model.CompressedFileSize);
            Assert.AreEqual(0, model.QualityPercent);
            Assert.AreEqual(0, model.TotalMillisecondsTook);
        }

        [TestMethod]
        public void BytesSaving_CalculatesCorrectly()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 1000,
                CompressedFileSize = 600
            };
            
            Assert.AreEqual(400, model.BytesSaving);
        }

        [TestMethod]
        public void PercentSaving_CalculatesCorrectly()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 1000,
                CompressedFileSize = 600
            };
            
            Assert.AreEqual(40.0, model.PercentSaving);
        }

        [TestMethod]
        public void PercentSaving_WithZeroOriginalSize_ReturnsZero()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 0,
                CompressedFileSize = 0
            };
            
            Assert.AreEqual(0.0, model.PercentSaving);
        }

        [TestMethod]
        public void FileExtension_WithDifferentTypes_ReturnsCorrectExtension()
        {
            var model = new ImageCompressedModel();
            
            model.FileType = CompressImageType.Jpeg;
            Assert.AreEqual(".jpeg", model.FileExtension);
            
            model.FileType = CompressImageType.Png;
            Assert.AreEqual(".png", model.FileExtension);
            
            model.FileType = CompressImageType.Gif;
            Assert.AreEqual(".gif", model.FileExtension);
        }

        [TestMethod]
        public void ToString_WithInvariantCulture_FormatsConsistently()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 1000,
                CompressedFileSize = 500,
                QualityPercent = 85,
                TotalMillisecondsTook = 100
            };
            
            string result = model.ToString();
            
            // Verify it uses invariant culture formatting (comma thousands separator)
            Assert.IsTrue(result.Contains("1,000"));
            Assert.IsTrue(result.Contains("500"));
            Assert.IsTrue(result.Contains("85"));
            Assert.IsTrue(result.Contains("100"));
        }
    }

    [TestClass]
    [TestCategory("Unit")]
    [TestCategory("Fast")]
    public class CompressionOptionsUnitTest
    {
        [TestMethod]
        public void CompressionOptions_DefaultValues_AreCorrect()
        {
            var options = new CompressionOptions();
            
            // Verify default values are sensible
            Assert.IsTrue(options.Quality >= 1 && options.Quality <= 100);
            Assert.IsNotNull(options);
        }

        [TestMethod]
        public void CompressionOptions_Properties_CanBeSetAndRetrieved()
        {
            var options = new CompressionOptions
            {
                Quality = 75,
                RemoveMetadata = true,
                AutoRotate = true,
                MaxWidth = 1920,
                MaxHeight = 1080
            };
            
            Assert.AreEqual(75, options.Quality);
            Assert.IsTrue(options.RemoveMetadata);
            Assert.IsTrue(options.AutoRotate);
            Assert.AreEqual(1920, options.MaxWidth);
            Assert.AreEqual(1080, options.MaxHeight);
        }
    }
}