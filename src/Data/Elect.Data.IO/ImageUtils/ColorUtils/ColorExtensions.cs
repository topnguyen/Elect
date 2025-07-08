namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public static class ColorExtensions
    {
        public static string ToHexCode(this System.Drawing.Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
        public static string ToHexCode(this SixLabors.ImageSharp.Color color)
        {
            var rgba = color.ToPixel<Rgba32>();
            return $"#{rgba.R:X2}{rgba.G:X2}{rgba.B:X2}";
        }
    }
}
