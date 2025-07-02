namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public class ImageDominantColorHelper
    {
        public static string GetHexCode(string imagePath)
        {
            using (var image = Image.FromFile(imagePath))
            {
                using (var bitmap = new Bitmap(image))
                {
                    return GetColor(bitmap).ToHexCode();
                }
            }
        }
        public static bool TryGetHexCode(string imagePath, out string dominantColorHexCode)
        {
            try
            {
                dominantColorHexCode = GetHexCode(imagePath);
                return true;
            }
            catch
            {
                dominantColorHexCode = null;
                return false;
            }
        }
        public static string GetHexCode(Bitmap bmp)
        {
            return GetColor(bmp).ToHexCode();
        }
        public static bool TryGetHexCode(Bitmap bmp, out string dominantColorHexCode)
        {
            try
            {
                dominantColorHexCode = GetHexCode(bmp);
                return true;
            }
            catch
            {
                dominantColorHexCode = null;
                return false;
            }
        }
        public static Color GetColor(Bitmap bmp)
        {
            // Scale image to standard size (Max width is 1024, max height is 768)
            float width = Math.Min(bmp.Width, 1024);
            float height = Math.Min(bmp.Height, 768);
            int scale = (int)Math.Min(bmp.Width / width, bmp.Height / height);
            Bitmap bmpResize = new Bitmap(bmp, new Size(bmp.Width / scale, bmp.Height / scale));
            var r = 0;
            var g = 0;
            var b = 0;
            var total = 0;
            for (var x = 0; x < bmpResize.Width; x++)
            {
                for (var y = 0; y < bmpResize.Height; y++)
                {
                    var clr = bmpResize.GetPixel(x, y);
                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                    total++;
                }
            }
            //Calculate Average
            r /= total;
            g /= total;
            b /= total;
            Color color = Color.FromArgb(r, g, b);
            return color;
        }
        public static bool TryGetColor(Bitmap bmp, out Color? dominantColor)
        {
            try
            {
                dominantColor = GetColor(bmp);
                return true;
            }
            catch
            {
                dominantColor = null;
                return false;
            }
        }
    }
}
