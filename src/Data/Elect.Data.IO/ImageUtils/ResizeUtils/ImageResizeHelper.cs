using System;
using System.IO;
using System.Threading.Tasks;
using Elect.Data.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

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

        public static async Task<byte[]> ResizeAsync(string path, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            path = PathHelper.GetFullPath(path);
            byte[] imageBytes = await File.ReadAllBytesAsync(path).ConfigureAwait(false);
            return await ResizeAsync(imageBytes, newWidthPx, newHeightPx, resizeType).ConfigureAwait(false);
        }

        public static byte[] Resize(byte[] imageBytes, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            using var inStream = new MemoryStream(imageBytes);
            using var outStream = new MemoryStream();
            
            // Get the image format from the byte array
            var imageInfo = Image.Identify(imageBytes);
            if (imageInfo == null)
            {
                throw new ArgumentException("Invalid image format", nameof(imageBytes));
            }

            // Configure encoder based on format
            IImageEncoder encoder = GetOptimizedEncoder(imageInfo.Metadata.DecodedImageFormat);
            
            // Load and process image
            using (var image = Image.Load(inStream))
            {
                // Configure resize options for better quality
                var resizeOptions = new ResizeOptions
                {
                    Size = new Size(newWidthPx, newHeightPx),
                    Mode = (ResizeMode)((int)resizeType),
                    Sampler = KnownResamplers.Lanczos3,
                    Compand = true,
                    PremultiplyAlpha = false
                };

                image.Mutate(x => x.Resize(resizeOptions));
                image.Save(outStream, encoder);
            }

            return outStream.ToArray();
        }

        public static async Task<byte[]> ResizeAsync(byte[] imageBytes, int newWidthPx, int newHeightPx, ResizeType resizeType = ResizeType.Max)
        {
            return await Task.Run(() => Resize(imageBytes, newWidthPx, newHeightPx, resizeType)).ConfigureAwait(false);
        }

        public static byte[] ResizeWithQuality(byte[] imageBytes, int newWidthPx, int newHeightPx, int quality = 85, ResizeType resizeType = ResizeType.Max)
        {
            using var inStream = new MemoryStream(imageBytes);
            using var outStream = new MemoryStream();
            
            var imageInfo = Image.Identify(imageBytes);
            if (imageInfo == null)
            {
                throw new ArgumentException("Invalid image format", nameof(imageBytes));
            }

            // Configure encoder with custom quality
            IImageEncoder encoder = GetOptimizedEncoder(imageInfo.Metadata.DecodedImageFormat, quality);
            
            using (var image = Image.Load(inStream))
            {
                var resizeOptions = new ResizeOptions
                {
                    Size = new Size(newWidthPx, newHeightPx),
                    Mode = (ResizeMode)((int)resizeType),
                    Sampler = KnownResamplers.Lanczos3,
                    Compand = true,
                    PremultiplyAlpha = false
                };

                image.Mutate(x => x.Resize(resizeOptions));
                image.Save(outStream, encoder);
            }

            return outStream.ToArray();
        }

        /// <summary>
        /// Resize image maintaining aspect ratio
        /// </summary>
        public static byte[] ResizeKeepAspectRatio(byte[] imageBytes, int maxWidthPx, int maxHeightPx)
        {
            using var inStream = new MemoryStream(imageBytes);
            using var outStream = new MemoryStream();
            
            var imageInfo = Image.Identify(imageBytes);
            if (imageInfo == null)
            {
                throw new ArgumentException("Invalid image format", nameof(imageBytes));
            }

            IImageEncoder encoder = GetOptimizedEncoder(imageInfo.Metadata.DecodedImageFormat);
            
            using (var image = Image.Load(inStream))
            {
                // Calculate new dimensions maintaining aspect ratio
                double widthRatio = (double)maxWidthPx / image.Width;
                double heightRatio = (double)maxHeightPx / image.Height;
                double ratio = Math.Min(widthRatio, heightRatio);

                int newWidth = (int)(image.Width * ratio);
                int newHeight = (int)(image.Height * ratio);

                var resizeOptions = new ResizeOptions
                {
                    Size = new Size(newWidth, newHeight),
                    Mode = ResizeMode.Max,
                    Sampler = KnownResamplers.Lanczos3,
                    Compand = true,
                    PremultiplyAlpha = false
                };

                image.Mutate(x => x.Resize(resizeOptions));
                image.Save(outStream, encoder);
            }

            return outStream.ToArray();
        }

        /// <summary>
        /// Get optimized encoder based on format with default quality settings
        /// </summary>
        private static IImageEncoder GetOptimizedEncoder(IImageFormat format, int quality = 85)
        {
            if (format == null)
            {
                // Default to JPEG for unknown formats
                return new JpegEncoder { Quality = quality };
            }

            return format.Name.ToLowerInvariant() switch
            {
                "jpeg" or "jpg" => new JpegEncoder 
                { 
                    Quality = quality
                },
                "png" => new PngEncoder 
                { 
                    CompressionLevel = PngCompressionLevel.BestCompression,
                    ColorType = PngColorType.RgbWithAlpha,
                    BitDepth = PngBitDepth.Bit8
                },
                "webp" => new WebpEncoder 
                { 
                    Quality = quality,
                    Method = WebpEncodingMethod.BestQuality
                },
                _ => new JpegEncoder { Quality = quality }
            };
        }
    }
}