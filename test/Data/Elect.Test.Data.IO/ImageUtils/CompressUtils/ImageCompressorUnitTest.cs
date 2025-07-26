using System;
using System.IO;
using Elect.Data.IO.ImageUtils.CompressUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Elect.Test.Data.IO.ImageUtils.CompressUtils
{
    [TestClass]
    public class ImageCompressorUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Compress_InvalidFilePath_Throws()
        {
            ImageCompressor.Compress("invalid.file", "output.file");
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownImageFormatException))]
        public void Compress_InvalidStream_Throws()
        {
            using var ms = new MemoryStream(new byte[] { 0, 1, 2 });
            ImageCompressor.Compress(ms);
        }

        [TestMethod]
        public void Compress_ValidStream_ReturnsModel()
        {
            // Create a valid PNG image in memory
            using var ms = new MemoryStream();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(ms);
            }
            ms.Position = 0;
            var result = ImageCompressor.Compress(ms);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.OriginalFileSize > 0);
        }

        [TestMethod]
        public void Compress_ValidFilePath_ReturnsModel()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(tempFile);
            }
            string outputFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
            var result = ImageCompressor.Compress(tempFile, outputFile);
            Assert.IsNotNull(result);
            Assert.IsTrue(File.Exists(outputFile));
            File.Delete(tempFile);
            File.Delete(outputFile);
        }

        [TestMethod]
        public void Compress_WithQuality_Works()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(tempFile);
            }
            string outputFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
            var result = ImageCompressor.Compress(tempFile, outputFile, 80);
            Assert.IsNotNull(result);
            Assert.IsTrue(File.Exists(outputFile));
            File.Delete(tempFile);
            File.Delete(outputFile);
        }

        [TestMethod]
        public void Compress_StreamWithFileNameExtension_Works()
        {
            using var ms = new MemoryStream();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1))
            {
                img.SaveAsPng(ms);
            }
            ms.Position = 0;
            var result = ImageCompressor.Compress(ms, 80, "test.png");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.OriginalFileSize > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownImageFormatException))]
        public void Compress_StreamWithInvalidFileName_Throws()
        {
            using var ms = new MemoryStream(new byte[] { 0, 1, 2 });
            ImageCompressor.Compress(ms, 80, "test.txt");
        }

        [TestMethod]
        public void Compress_StreamWithHighQuality_ReturnsModelWithMinimalSaving()
        {
            using var ms = new MemoryStream();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(10, 10))
            {
                img.SaveAsPng(ms);
            }
            ms.Position = 0;
            // Use high quality to minimize savings
            var result = ImageCompressor.Compress(ms, 99, "test.png");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.QualityPercent == 99);
        }

        [TestMethod]
        public void Compress_StreamWithLowQuality_Works()
        {
            using var ms = new MemoryStream();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(10, 10))
            {
                img.SaveAsPng(ms);
            }
            ms.Position = 0;
            var result = ImageCompressor.Compress(ms, 10, "test.png");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.QualityPercent == 10);
        }

        [TestMethod]
        public void Compress_JpegFile_Works()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jpg");
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(10, 10))
            {
                img.SaveAsJpeg(tempFile);
            }
            string outputFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jpg");
            var result = ImageCompressor.Compress(tempFile, outputFile);
            Assert.IsNotNull(result);
            Assert.IsTrue(File.Exists(outputFile));
            Assert.AreEqual(Elect.Data.IO.ImageUtils.CompressUtils.Models.CompressImageType.Jpeg, result.FileType);
            File.Delete(tempFile);
            File.Delete(outputFile);
        }

        [TestMethod]
        public void Compress_GifFile_Works()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gif");
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(10, 10))
            {
                img.SaveAsGif(tempFile);
            }
            string outputFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gif");
            var result = ImageCompressor.Compress(tempFile, outputFile);
            Assert.IsNotNull(result);
            Assert.IsTrue(File.Exists(outputFile));
            Assert.AreEqual(Elect.Data.IO.ImageUtils.CompressUtils.Models.CompressImageType.Gif, result.FileType);
            File.Delete(tempFile);
            File.Delete(outputFile);
        }

        [TestMethod]
        public void CompressAdvanced_WithOptions_Works()
        {
            using var ms = new MemoryStream();
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(20, 20))
            {
                img.SaveAsJpeg(ms);
            }
            ms.Position = 0;

            var options = new Elect.Data.IO.ImageUtils.CompressUtils.CompressionOptions
            {
                Quality = 70,
                RemoveMetadata = true,
                AutoRotate = true,
                MaxWidth = 10,
                MaxHeight = 10
            };

            var result = ImageCompressor.CompressAdvanced(ms, options);
            Assert.IsNotNull(result);
            Assert.AreEqual(70, result.QualityPercent);
        }

        [TestMethod]
        public void CompressAsync_Works()
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
            using (var img = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(5, 5))
            {
                img.SaveAsPng(tempFile);
            }
            string outputFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
            
            var task = ImageCompressor.CompressAsync(tempFile, outputFile);
            task.Wait();
            var result = task.Result;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(File.Exists(outputFile));
            File.Delete(tempFile);
            File.Delete(outputFile);
        }
    }
}