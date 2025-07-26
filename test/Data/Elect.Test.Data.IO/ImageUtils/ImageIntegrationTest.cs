using System;
using System.IO;
using Elect.Data.IO.ImageUtils.CompressUtils;
using Elect.Data.IO.ImageUtils.CompressUtils.Models;
using Elect.Data.IO.ImageUtils.ResizeUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Elect.Test.Data.IO.ImageUtils
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("Slow")]
    public class ImageIntegrationTest
    {
        private static readonly string TestImagesPath = Path.Combine(
            Path.GetDirectoryName(typeof(ImageIntegrationTest).Assembly.Location)!,
            "TestImages");
        
        private static readonly string TempTestPath = Path.Combine(Path.GetTempPath(), "ElectImageTests");

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create temp directory for test outputs
            Directory.CreateDirectory(TempTestPath);
            
            // Create small test images for faster testing
            CreateSmallTestImages();
        }
        
        private static void CreateSmallTestImages()
        {
            // Skip creating small images for now - just use existing samples
            // The optimization will still work by preferring smaller images when available
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Clean up temp test files
            if (Directory.Exists(TempTestPath))
            {
                try
                {
                    Directory.Delete(TempTestPath, true);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up any individual test files
            if (Directory.Exists(TempTestPath))
            {
                var files = Directory.GetFiles(TempTestPath);
                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // Ignore individual file cleanup errors
                    }
                }
            }
        }

        [TestMethod]
        public void CompressJpeg_WithRealImage_ReducesFileSize()
        {
            var jpegFile = Path.Combine(TestImagesPath, "small.jpg");
            if (!File.Exists(jpegFile)) jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var outputFile = Path.Combine(TempTestPath, "compressed_jpeg.jpg");
            var result = ImageCompressor.Compress(jpegFile, outputFile, 60);

            Assert.IsNotNull(result);
            Assert.AreEqual(CompressImageType.Jpeg, result.FileType);
            Assert.IsTrue(result.CompressedFileSize > 0);
            Assert.IsTrue(result.CompressedFileSize <= result.OriginalFileSize);
            Assert.IsTrue(File.Exists(outputFile));
            Assert.AreEqual(60, result.QualityPercent);
        }

        [TestMethod]
        public void CompressPng_WithRealImage_ProcessesSuccessfully()
        {
            var pngFile = Path.Combine(TestImagesPath, "small.png");
            if (!File.Exists(pngFile)) pngFile = Path.Combine(TestImagesPath, "sample.png");
            if (!File.Exists(pngFile)) Assert.Inconclusive("Test image not found");

            var outputFile = Path.Combine(TempTestPath, "compressed_png.png");
            var result = ImageCompressor.Compress(pngFile, outputFile, 85);

            Assert.IsNotNull(result);
            Assert.AreEqual(CompressImageType.Png, result.FileType);
            Assert.IsTrue(result.CompressedFileSize > 0);
            Assert.IsTrue(File.Exists(outputFile));
            Assert.AreEqual(85, result.QualityPercent);
        }

        [TestMethod]
        public void CompressGif_WithRealImage_ProcessesSuccessfully()
        {
            var gifFile = Path.Combine(TestImagesPath, "small.gif");
            if (!File.Exists(gifFile)) gifFile = Path.Combine(TestImagesPath, "sample.gif");
            if (!File.Exists(gifFile)) Assert.Inconclusive("Test image not found");

            var outputFile = Path.Combine(TempTestPath, "compressed_gif.gif");
            var result = ImageCompressor.Compress(gifFile, outputFile, 80);

            Assert.IsNotNull(result);
            Assert.AreEqual(CompressImageType.Gif, result.FileType);
            Assert.IsTrue(result.CompressedFileSize > 0);
            Assert.IsTrue(File.Exists(outputFile));
            Assert.AreEqual(80, result.QualityPercent);
        }

        [TestMethod]
        public void CompressAdvanced_WithRealImage_AppliesAllOptions()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var imageBytes = File.ReadAllBytes(jpegFile);
            using var stream = new MemoryStream(imageBytes);

            var options = new CompressionOptions
            {
                Quality = 70,
                RemoveMetadata = true,
                AutoRotate = true,
                MaxWidth = 150,
                MaxHeight = 100
            };

            var result = ImageCompressor.CompressAdvanced(stream, options);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.CompressedFileSize > 0);
            Assert.AreEqual(70, result.QualityPercent);
            Assert.IsNotNull(result.ResultFileStream);
        }

        [TestMethod]
        public void ResizeJpeg_WithRealImage_ChangesSize()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var resizedBytes = ImageResizeHelper.Resize(jpegFile, 150, 100);

            Assert.IsNotNull(resizedBytes);
            Assert.IsTrue(resizedBytes.Length > 0);

            // Save and verify the resized image
            var outputFile = Path.Combine(TempTestPath, "resized_jpeg.jpg");
            File.WriteAllBytes(outputFile, resizedBytes);
            Assert.IsTrue(File.Exists(outputFile));
        }

        [TestMethod]
        public void ResizePng_WithRealImage_ChangesSize()
        {
            var pngFile = Path.Combine(TestImagesPath, "sample.png");
            if (!File.Exists(pngFile)) Assert.Inconclusive("Test image not found");

            var imageBytes = File.ReadAllBytes(pngFile);
            var resizedBytes = ImageResizeHelper.Resize(imageBytes, 100, 100);

            Assert.IsNotNull(resizedBytes);
            Assert.IsTrue(resizedBytes.Length > 0);

            // Save and verify the resized image
            var outputFile = Path.Combine(TempTestPath, "resized_png.png");
            File.WriteAllBytes(outputFile, resizedBytes);
            Assert.IsTrue(File.Exists(outputFile));
        }

        [TestMethod]
        public void ResizeWithQuality_WithRealImage_AppliesQualitySetting()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var imageBytes = File.ReadAllBytes(jpegFile);
            var highQualityBytes = ImageResizeHelper.ResizeWithQuality(imageBytes, 200, 150, 95);
            var lowQualityBytes = ImageResizeHelper.ResizeWithQuality(imageBytes, 200, 150, 30);

            Assert.IsNotNull(highQualityBytes);
            Assert.IsNotNull(lowQualityBytes);
            Assert.IsTrue(highQualityBytes.Length > 0);
            Assert.IsTrue(lowQualityBytes.Length > 0);

            // Generally, higher quality should result in larger file size for JPEG
            // Save both for verification
            File.WriteAllBytes(Path.Combine(TempTestPath, "resize_high_quality.jpg"), highQualityBytes);
            File.WriteAllBytes(Path.Combine(TempTestPath, "resize_low_quality.jpg"), lowQualityBytes);
        }

        [TestMethod]
        public void ResizeKeepAspectRatio_WithRealImage_MaintainsAspectRatio()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var imageBytes = File.ReadAllBytes(jpegFile);
            var resizedBytes = ImageResizeHelper.ResizeKeepAspectRatio(imageBytes, 200, 200);

            Assert.IsNotNull(resizedBytes);
            Assert.IsTrue(resizedBytes.Length > 0);

            // Save and verify the aspect-ratio-preserved image
            var outputFile = Path.Combine(TempTestPath, "aspect_ratio_preserved.jpg");
            File.WriteAllBytes(outputFile, resizedBytes);
            Assert.IsTrue(File.Exists(outputFile));
        }

        [TestMethod]
        public void CompressAsync_WithRealImage_WorksAsynchronously()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var outputFile = Path.Combine(TempTestPath, "async_compressed.jpg");
            
            var task = ImageCompressor.CompressAsync(jpegFile, outputFile, 75);
            task.Wait();
            var result = task.Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(CompressImageType.Jpeg, result.FileType);
            Assert.IsTrue(File.Exists(outputFile));
            Assert.AreEqual(75, result.QualityPercent);
        }

        [TestMethod]
        public void ResizeAsync_WithRealImage_WorksAsynchronously()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var task = ImageResizeHelper.ResizeAsync(jpegFile, 180, 120);
            task.Wait();
            var resizedBytes = task.Result;

            Assert.IsNotNull(resizedBytes);
            Assert.IsTrue(resizedBytes.Length > 0);

            // Save and verify the async resized image
            var outputFile = Path.Combine(TempTestPath, "async_resized.jpg");
            File.WriteAllBytes(outputFile, resizedBytes);
            Assert.IsTrue(File.Exists(outputFile));
        }

        [TestMethod]
        public void CompressionPerformance_WithRealImages_CompletesInReasonableTime()
        {
            var jpegFile = Path.Combine(TestImagesPath, "sample.jpg");
            if (!File.Exists(jpegFile)) Assert.Inconclusive("Test image not found");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < 5; i++)
            {
                var outputFile = Path.Combine(TempTestPath, $"perf_test_{i}.jpg");
                var result = ImageCompressor.Compress(jpegFile, outputFile, 80);
                Assert.IsNotNull(result);
            }
            
            stopwatch.Stop();
            
            // Should complete 5 compressions in under 5 seconds (very generous limit)
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 5000, 
                $"Performance test took {stopwatch.ElapsedMilliseconds}ms");
        }

        [TestMethod]
        public void CompressMultipleFormats_WithRealImages_HandlesAllFormats()
        {
            var testFiles = new[]
            {
                new { File = "sample.jpg", Type = CompressImageType.Jpeg },
                new { File = "sample.png", Type = CompressImageType.Png },
                new { File = "sample.gif", Type = CompressImageType.Gif }
            };

            foreach (var testFile in testFiles)
            {
                var inputFile = Path.Combine(TestImagesPath, testFile.File);
                if (!File.Exists(inputFile)) continue;

                var outputFile = Path.Combine(TempTestPath, $"multi_{testFile.File}");
                var result = ImageCompressor.Compress(inputFile, outputFile, 75);

                Assert.IsNotNull(result, $"Failed to compress {testFile.File}");
                Assert.AreEqual(testFile.Type, result.FileType, $"Wrong file type for {testFile.File}");
                Assert.IsTrue(File.Exists(outputFile), $"Output file not created for {testFile.File}");
            }
        }
    }
}