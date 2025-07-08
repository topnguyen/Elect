using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elect.Data.IO.ImageUtils.ColorUtils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Drawing;
using System.IO;
using Color = SixLabors.ImageSharp.Color;

namespace Elect.Test.Data.IO.ImageUtils.ColorUtils
{
    [TestClass]
    public class ImageDominantColorHelperUnitTest
    {
        private MemoryStream CreateTestImage(byte r, byte g, byte b, int width = 2, int height = 2)
        {
            var image = new SixLabors.ImageSharp.Image<Rgba32>(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    image[x, y] = new Rgba32(r, g, b);
            var ms = new MemoryStream();
            image.SaveAsPng(ms);
            ms.Position = 0;
            return ms;
        }

        [TestMethod]
        public void GetHexCode_FromMemoryStream_ShouldReturnCorrectHex()
        {
            using var ms = CreateTestImage(18, 52, 86);
            var hex = ImageDominantColorHelper.GetHexCode(ms);
            Assert.AreEqual("#123456", hex, true);
        }

        [TestMethod]
        public void TryGetHexCode_FromMemoryStream_ShouldReturnTrueAndHex()
        {
            using var ms = CreateTestImage(255, 255, 255);
            var result = ImageDominantColorHelper.TryGetHexCode(ms, out var hex);
            Assert.IsTrue(result);
            Assert.AreEqual("#FFFFFF", hex, true);
        }

        [TestMethod]
        public void TryGetHexCode_FromInvalidMemoryStream_ShouldReturnFalse()
        {
            using var ms = new MemoryStream(new byte[] { 0, 1, 2 });
            var result = ImageDominantColorHelper.TryGetHexCode(ms, out var hex);
            Assert.IsFalse(result);
            Assert.IsNull(hex);
        }

        [TestMethod]
        public void GetColor_FromMemoryStream_ShouldReturnCorrectColor()
        {
            using var ms = CreateTestImage(10, 20, 30);
            var color = ImageDominantColorHelper.GetColor(ms);
            var rgba = (Rgba32)color;
            Assert.AreEqual(10, rgba.R);
            Assert.AreEqual(20, rgba.G);
            Assert.AreEqual(30, rgba.B);
        }

        [TestMethod]
        public void GetColor_WithRegion_ShouldReturnCorrectColor()
        {
            using var ms = CreateTestImage(100, 150, 200, 4, 4);
            var region = new SixLabors.ImageSharp.Rectangle(1, 1, 2, 2);
            var color = ImageDominantColorHelper.GetColor(ms, region);
            var rgba = (Rgba32)color;
            Assert.AreEqual(100, rgba.R);
            Assert.AreEqual(150, rgba.G);
            Assert.AreEqual(200, rgba.B);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetColor_WithInvalidRegion_ShouldThrow()
        {
            using var ms = CreateTestImage(1, 2, 3, 2, 2);
            var region = new SixLabors.ImageSharp.Rectangle(10, 10, 2, 2);
            ImageDominantColorHelper.GetColor(ms, region);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetColor_WithEmptyRegion_ShouldThrow()
        {
            using var ms = CreateTestImage(1, 2, 3, 2, 2);
            var region = new SixLabors.ImageSharp.Rectangle(0, 0, 0, 0);
            ImageDominantColorHelper.GetColor(ms, region);
        }

        [TestMethod]
        public void GetHexCode_FromFilePath_ShouldReturnCorrectHex()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test1.png");
            using (var ms = CreateTestImage(18, 52, 86))
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fs);
            }
            var hex = ImageDominantColorHelper.GetHexCode(filePath);
            Assert.AreEqual("#123456", hex, true);
        }

        [TestMethod]
        public void TryGetHexCode_FromFilePath_ShouldReturnTrueAndHex()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test2.png");
            using (var ms = CreateTestImage(255, 255, 255))
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fs);
            }
            var result = ImageDominantColorHelper.TryGetHexCode(filePath, out var hex);
            Assert.IsTrue(result);
            Assert.AreEqual("#FFFFFF", hex, true);
        }

        [TestMethod]
        public void TryGetHexCode_FromInvalidFilePath_ShouldReturnFalse()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "notfound.png");
            var result = ImageDominantColorHelper.TryGetHexCode(filePath, out var hex);
            Assert.IsFalse(result);
            Assert.IsNull(hex);
        }

        [TestMethod]
        public void GetColor_FromFilePath_ShouldReturnCorrectColor()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test3.png");
            using (var ms = CreateTestImage(10, 20, 30))
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fs);
            }
            var color = ImageDominantColorHelper.GetColor(filePath);
            var rgba = (Rgba32)color;
            Assert.AreEqual(10, rgba.R);
            Assert.AreEqual(20, rgba.G);
            Assert.AreEqual(30, rgba.B);
        }

        [TestMethod]
        public void TryGetColor_FromFilePath_ShouldReturnTrueAndColor()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test4.png");
            using (var ms = CreateTestImage(1, 2, 3))
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fs);
            }
            var result = ImageDominantColorHelper.TryGetColor(filePath, out var color);
            Assert.IsTrue(result);
            var rgba = (Rgba32)color;
            Assert.AreEqual(1, rgba.R);
            Assert.AreEqual(2, rgba.G);
            Assert.AreEqual(3, rgba.B);
        }

        [TestMethod]
        public void TryGetColor_FromInvalidFilePath_ShouldReturnFalse()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "notfound2.png");
            var result = ImageDominantColorHelper.TryGetColor(filePath, out var color);
            Assert.IsFalse(result);
            Assert.IsNull(color);
        }

        [TestMethod]
        public void TryGetColor_FromInvalidMemoryStream_ShouldReturnFalse()
        {
            using var ms = new MemoryStream(new byte[] { 0, 1, 2 });
            var result = ImageDominantColorHelper.TryGetColor(ms, out var color);
            Assert.IsFalse(result);
            Assert.IsNull(color);
        }

        [TestMethod]
        public void TryGetColor_Success_SetsDominantColor()
        {
            using var ms = CreateTestImage(10, 20, 30);
            var result = ImageDominantColorHelper.TryGetColor(ms, out Color? color);
            Assert.IsTrue(result);
            Assert.IsNotNull(color);
            var rgba = (Rgba32)color;
            Assert.AreEqual(10, rgba.R);
            Assert.AreEqual(20, rgba.G);
            Assert.AreEqual(30, rgba.B);
        }

        [TestMethod]
        public void TryGetColor_Failure_SetsDominantColorToNull()
        {
            using var ms = new MemoryStream(new byte[] { 0, 1, 2 });
            Color? color = Color.Red;
            var result = ImageDominantColorHelper.TryGetColor(ms, out color);
            Assert.IsFalse(result);
            Assert.IsNull(color);
        }
    }
}
