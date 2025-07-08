namespace Elect.Data.IO.ImageUtils.ColorUtils
{
    public static class ImageDominantColorHelper
    {
        public static string GetHexCode(string imagePath)
        {
            return GetColor(imagePath).ToHexCode();
        }

        public static string GetHexCode(MemoryStream memoryStream)
        {
            return GetColor(memoryStream).ToHexCode();
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

        public static bool TryGetHexCode(MemoryStream memoryStream, out string dominantColorHexCode)
        {
            try
            {
                dominantColorHexCode = GetHexCode(memoryStream);
                return true;
            }
            catch
            {
                dominantColorHexCode = null;
                return false;
            }
        }

        public static Color GetColor(string imagePath, SixLabors.ImageSharp.Rectangle? region = null)
        {
            using (var memoryStream = new MemoryStream(File.ReadAllBytes(imagePath)))
            {
                return GetColor(memoryStream, region);
            }
        }

        public static Color GetColor(MemoryStream memoryStream, SixLabors.ImageSharp.Rectangle? region = null)
        {
            using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(memoryStream);

            // Use entire image if no region is specified
            var targetRegion = region ?? new SixLabors.ImageSharp.Rectangle(0, 0, image.Width, image.Height);

            // Validate region
            if (!new SixLabors.ImageSharp.Rectangle(0, 0, image.Width, image.Height).Contains(targetRegion))
            {
                throw new ArgumentOutOfRangeException("Region is outside image bounds.");
            }

            long rSum = 0, gSum = 0, bSum = 0;
            int pixelCount = 0;

            // Iterate over the region
            for (int y = targetRegion.Top; y < targetRegion.Bottom; y++)
            {
                for (int x = targetRegion.Left; x < targetRegion.Right; x++)
                {
                    Rgba32 pixel = image[x, y];
                    rSum += pixel.R;
                    gSum += pixel.G;
                    bSum += pixel.B;
                    pixelCount++;
                }
            }

            if (pixelCount == 0)
            {
                throw new InvalidOperationException("No pixels found in the specified region.");
            }

            // Calculate average color
            byte rAvg = (byte)(rSum / pixelCount);
            byte gAvg = (byte)(gSum / pixelCount);
            byte bAvg = (byte)(bSum / pixelCount);

            Color color = new Color(new Rgba32(rAvg, gAvg, bAvg));

            return color;
        }

        public static bool TryGetColor(string imagePath, out Color? dominantColor)
        {
            try
            {
                dominantColor = GetColor(imagePath);
                return true;
            }
            catch
            {
                dominantColor = null;
                return false;
            }
        }

        public static bool TryGetColor(MemoryStream memoryStream, out Color? dominantColor)
        {
            try
            {
                dominantColor = GetColor(memoryStream);
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
