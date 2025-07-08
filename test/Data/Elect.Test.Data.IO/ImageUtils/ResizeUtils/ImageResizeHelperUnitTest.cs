namespace Elect.Test.Data.IO.ImageUtils.ResizeUtils
{
    [TestClass]
    public class ImageResizeHelperUnitTest
    {
        private byte[] CreateTestImage(int width, int height, IImageFormat format, out string tempFilePath)
        {
            using (var image = new Image<Rgba32>(width, height))
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                tempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(tempFilePath, ms.ToArray());
                return ms.ToArray();
            }
        }
        [TestMethod]
        public void Resize_FromFilePath_ResizesCorrectly()
        {
            var format = SixLabors.ImageSharp.Formats.Png.PngFormat.Instance;
            string tempFilePath;
            var imageBytes = CreateTestImage(100, 100, format, out tempFilePath);
            try
            {
                var resized = ImageResizeHelper.Resize(tempFilePath, 50, 50);
                Assert.IsNotNull(resized);
                Assert.IsTrue(resized.Length > 0);
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
        [TestMethod]
        public void Resize_FromBytes_ResizesCorrectly()
        {
            var format = SixLabors.ImageSharp.Formats.Png.PngFormat.Instance;
            string tempFilePath;
            var imageBytes = CreateTestImage(100, 100, format, out tempFilePath);
            try
            {
                var resized = ImageResizeHelper.Resize(imageBytes, 50, 50);
                Assert.IsNotNull(resized);
                Assert.IsTrue(resized.Length > 0);
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
        [TestMethod]
        public void Resize_ThrowsOnInvalidBytes()
        {
            byte[] invalidBytes = new byte[] { 0, 1, 2, 3 };
            Assert.ThrowsException<UnknownImageFormatException>(() =>
            {
                ImageResizeHelper.Resize(invalidBytes, 10, 10);
            });
        }
        [TestMethod]
        public void Resize_AllResizeTypes_Work()
        {
            var format = SixLabors.ImageSharp.Formats.Png.PngFormat.Instance;
            string tempFilePath;
            var imageBytes = CreateTestImage(100, 100, format, out tempFilePath);
            try
            {
                foreach (ResizeType type in Enum.GetValues(typeof(ResizeType)))
                {
                    var resized = ImageResizeHelper.Resize(imageBytes, 50, 50, type);
                    Assert.IsNotNull(resized);
                    Assert.IsTrue(resized.Length > 0);
                }
            }
            finally
            {
                File.Delete(tempFilePath);
            }
        }
    }
}
