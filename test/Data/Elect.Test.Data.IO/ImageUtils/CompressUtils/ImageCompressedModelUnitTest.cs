namespace Elect.Test.Data.IO.ImageUtils.CompressUtils
{
    [TestClass]
    public class ImageCompressedModelUnitTest
    {
        [TestMethod]
        public void Constructor_WithValidFile_SetsProperties()
        {
            string tempFile = Path.GetTempFileName();
            File.WriteAllBytes(tempFile, new byte[100]);
            var model = new ImageCompressedModel(tempFile, 200);
            Assert.AreEqual(200, model.OriginalFileSize);
            Assert.AreEqual(100, model.CompressedFileSize);
            File.Delete(tempFile);
        }
        [TestMethod]
        public void Constructor_WithInvalidFile_SetsCompressedFileSizeZero()
        {
            var model = new ImageCompressedModel("nonexistent.file", 123);
            Assert.AreEqual(123, model.OriginalFileSize);
            Assert.AreEqual(0, model.CompressedFileSize);
        }
        [TestMethod]
        public void FileExtension_ReturnsCorrectExtension()
        {
            string tempFile = Path.GetTempFileName();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(tempFile);
            }
            var model = new ImageCompressedModel(tempFile, 0) { FileType = CompressImageType.Png };
            Assert.AreEqual(".png", model.FileExtension);
            File.Delete(tempFile);
        }
        [TestMethod]
        public void BytesSaving_And_PercentSaving_CalculatedCorrectly()
        {
            string tempFile = Path.GetTempFileName();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(tempFile);
            }
            var fileInfo = new FileInfo(tempFile);
            var model = new ImageCompressedModel(tempFile, fileInfo.Length + 100) { CompressedFileSize = fileInfo.Length };
            Assert.AreEqual(100, model.BytesSaving);
            double expectedPercent = Math.Round(100.0 * 100 / (fileInfo.Length + 100), 2);
            Assert.AreEqual(expectedPercent, model.PercentSaving, 0.0001);
            File.Delete(tempFile);
        }
        [TestMethod]
        public void PercentSaving_ZeroIfNoSaving()
        {
            string tempFile = Path.GetTempFileName();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(tempFile);
            }
            var fileInfo = new FileInfo(tempFile);
            var model = new ImageCompressedModel(tempFile, fileInfo.Length) { CompressedFileSize = fileInfo.Length };
            Assert.AreEqual(0, model.PercentSaving);
            File.Delete(tempFile);
        }
    }
}
