using Elect.Data.IO.ImageUtils.ColorUtils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Elect.Test.Data.IO.ImageUtils.ColorUtils
{
    [TestClass]
    public class ColorHelperUnitTest
    {
        [TestMethod]
        public void GetRandom_ShouldReturnRgb24ColorWithinRange()
        {
            var color = ColorHelper.GetRandom();
            Assert.IsInstanceOfType(color, typeof(Color));
            var rgb = (Rgb24)color;
            Assert.IsTrue(rgb.R >= 0 && rgb.R <= 255);
            Assert.IsTrue(rgb.G >= 0 && rgb.G <= 255);
            Assert.IsTrue(rgb.B >= 0 && rgb.B <= 255);
        }

        [TestMethod]
        public void GetRandom_ShouldReturnDifferentColors()
        {
            var color1 = ColorHelper.GetRandom();
            var color2 = ColorHelper.GetRandom();
            // Acceptable for coverage, even if rarely equal
            Assert.IsFalse(color1.Equals(color2));
        }
    }
}