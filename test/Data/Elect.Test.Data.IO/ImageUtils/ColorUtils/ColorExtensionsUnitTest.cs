namespace Elect.Test.Data.IO.ImageUtils.ColorUtils
{
    [TestClass]
    public class ColorExtensionsUnitTest
    {
        [TestMethod]
        public void ToHexCode_ShouldReturnCorrectHex()
        {
            var color = System.Drawing.Color.FromArgb(255, 18, 52, 86); // #123456
            var hex = color.ToHexCode();
            Assert.AreEqual("#123456", hex);
        }
        [TestMethod]
        public void ToHexCode_BlackColor()
        {
            var color = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            var hex = color.ToHexCode();
            Assert.AreEqual("#000000", hex);
        }
        [TestMethod]
        public void ToHexCode_WhiteColor()
        {
            var color = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            var hex = color.ToHexCode();
            Assert.AreEqual("#FFFFFF", hex);
        }
    }
}
