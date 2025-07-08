namespace Elect.Data.IO.ImageUtils.ResizeUtils
{
    public class ImageResizeHelper
    {
        public static byte[] Resize(string path, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            path = PathHelper.GetFullPath(path);
            byte[] imageBytes = File.ReadAllBytes(path);
            return Resize(imageBytes, newWidthPx, newHeightPx, resizeType);
        }
        public static byte[] Resize(byte[] imageBytes, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            using (MemoryStream inStream = new MemoryStream(imageBytes))
            {
                // Get the image format from the byte array
                var format = SixLabors.ImageSharp.Image.DetectFormat(imageBytes);
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (var image = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(inStream))
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new SixLabors.ImageSharp.Size(newWidthPx, newHeightPx),
                            Mode = (ResizeMode)((int)resizeType)
                        }));
                        image.Save(outStream, format);
                        return outStream.ToArray();
                    }
                }
            }
        }
    }
}
