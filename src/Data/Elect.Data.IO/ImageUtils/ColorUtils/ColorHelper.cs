namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public class ColorHelper
    {
        public static Color GetRandom()
        {
            Random random = new Random();
            const int max = 256;
            var red = random.Next(max);
            var green = random.Next(max);
            var blue = random.Next(max);
            Color color = Color.FromArgb(red, green, blue);
            return color;
        }
    }
}
