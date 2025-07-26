using System.IO;
using Elect.Data.IO.ImageUtils.CompressUtils.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Data.IO.ImageUtils.CompressUtils
{
    [TestClass]
    public class ImageCompressedModelUnitTest
    {
        [TestMethod]
        public void Constructor_Default_InitializesProperties()
        {
            var model = new ImageCompressedModel();
            
            Assert.IsNotNull(model.ResultFileStream);
            Assert.AreEqual(0, model.OriginalFileSize);
            Assert.AreEqual(0, model.CompressedFileSize);
            Assert.AreEqual(CompressImageType.Invalid, model.FileType);
        }

        [TestMethod]
        public void Properties_SetAndGet_WorksCorrectly()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 200,
                CompressedFileSize = 100,
                FileType = CompressImageType.Jpeg,
                QualityPercent = 85,
                TotalMillisecondsTook = 500
            };
            
            Assert.AreEqual(200, model.OriginalFileSize);
            Assert.AreEqual(100, model.CompressedFileSize);
            Assert.AreEqual(CompressImageType.Jpeg, model.FileType);
            Assert.AreEqual(85, model.QualityPercent);
            Assert.AreEqual(500, model.TotalMillisecondsTook);
        }

        [TestMethod]
        public void BytesSaving_CalculatesCorrectly()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 200,
                CompressedFileSize = 100
            };
            
            Assert.AreEqual(100, model.BytesSaving);
        }

        [TestMethod]
        public void PercentSaving_CalculatesCorrectly()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 200,
                CompressedFileSize = 100
            };
            
            Assert.AreEqual(50.0, model.PercentSaving);
        }

        [TestMethod]
        public void PercentSaving_ZeroSaving_ReturnsZero()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 100,
                CompressedFileSize = 100
            };
            
            Assert.AreEqual(0.0, model.PercentSaving);
        }

        [TestMethod]
        public void FileExtension_JpegType_ReturnsCorrectExtension()
        {
            var model = new ImageCompressedModel
            {
                FileType = CompressImageType.Jpeg
            };
            
            Assert.AreEqual(".jpeg", model.FileExtension);
        }

        [TestMethod]
        public void FileExtension_PngType_ReturnsCorrectExtension()
        {
            var model = new ImageCompressedModel
            {
                FileType = CompressImageType.Png
            };
            
            Assert.AreEqual(".png", model.FileExtension);
        }

        [TestMethod]
        public void FileExtension_GifType_ReturnsCorrectExtension()
        {
            var model = new ImageCompressedModel
            {
                FileType = CompressImageType.Gif
            };
            
            Assert.AreEqual(".gif", model.FileExtension);
        }

        [TestMethod]
        public void FileExtension_InvalidType_ReturnsInvalid()
        {
            var model = new ImageCompressedModel
            {
                FileType = CompressImageType.Invalid
            };
            
            Assert.AreEqual("invalid", model.FileExtension);
        }

        [TestMethod]
        public void ToString_FormatsCorrectly()
        {
            var model = new ImageCompressedModel
            {
                OriginalFileSize = 1000,
                CompressedFileSize = 500,
                QualityPercent = 85,
                TotalMillisecondsTook = 100
            };
            
            string result = model.ToString();
            
            Assert.IsTrue(result.Contains("1,000"));
            Assert.IsTrue(result.Contains("500"));
            Assert.IsTrue(result.Contains("50"));
            Assert.IsTrue(result.Contains("85"));
            Assert.IsTrue(result.Contains("100"));
        }
    }
}