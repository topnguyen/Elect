namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public static class ColorExtensions
    {
        public static string ToHexCode(this System.Drawing.Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
