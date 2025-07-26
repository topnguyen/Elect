using System;
using System.IO;
using System.Threading.Tasks;
using Elect.Data.IO.FileUtils;
using Elect.Data.IO.ImageUtils.CompressUtils.Models;
using Elect.Data.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace Elect.Data.IO.ImageUtils.CompressUtils
{
    /// <summary>
    /// Cross-platform image compressor using ImageSharp library
    /// Works consistently on Windows, Linux, and macOS without external tools
    /// </summary>
    public class ImageCompressor
    {
        public static ImageCompressedModel Compress(string inputPath, string outputPath)
        {
            return Compress(inputPath, outputPath, 0);
        }

        public static async Task<ImageCompressedModel> CompressAsync(string inputPath, string outputPath)
        {
            return await CompressAsync(inputPath, outputPath, 0).ConfigureAwait(false);
        }

        public static ImageCompressedModel Compress(string inputPath, string outputPath, int qualityPercent)
        {
            inputPath = PathHelper.GetFullPath(inputPath);
            outputPath = PathHelper.GetFullPath(outputPath);

            using var stream = new MemoryStream();
            using var file = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            file.CopyTo(stream);
            stream.Position = 0;

            var imageCompressedModel = Compress(stream, qualityPercent, inputPath);
            
            if (imageCompressedModel?.ResultFileStream != null)
            {
                using var outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                imageCompressedModel.ResultFileStream.Position = 0;
                imageCompressedModel.ResultFileStream.CopyTo(outputFile);
            }

            return imageCompressedModel;
        }

        public static async Task<ImageCompressedModel> CompressAsync(string inputPath, string outputPath, int qualityPercent)
        {
            return await Task.Run(() => Compress(inputPath, outputPath, qualityPercent)).ConfigureAwait(false);
        }

        public static ImageCompressedModel Compress(MemoryStream stream)
        {
            return Compress(stream, 0);
        }

        public static ImageCompressedModel Compress(MemoryStream stream, int qualityPercent)
        {
            return Compress(stream, qualityPercent, null);
        }

        public static ImageCompressedModel Compress(MemoryStream stream, int qualityPercent, string fileName)
        {
            stream.Position = 0;
            var originalSize = stream.Length;

            // Identify image format
            var imageInfo = Image.Identify(stream);
            if (imageInfo == null)
            {
                throw new ArgumentException("Invalid image format", nameof(stream));
            }

            var format = imageInfo.Metadata.DecodedImageFormat;
            var imageType = GetImageType(format);

            // Set default quality if not specified
            if (qualityPercent == 0)
            {
                qualityPercent = GetDefaultQuality(imageType);
            }

            // Create result stream
            var resultStream = new MemoryStream();
            
            // Load and compress image
            stream.Position = 0;
            using (var image = Image.Load(stream))
            {
                // Get optimized encoder
                var encoder = GetOptimizedEncoder(format, qualityPercent);
                
                // Apply format-specific optimizations
                if (imageType == CompressImageType.Png && qualityPercent < 100)
                {
                    // For PNG, use quantization to reduce colors when quality is reduced
                    var quantizer = new WuQuantizer(new QuantizerOptions
                    {
                        MaxColors = CalculateMaxColors(qualityPercent)
                    });
                    image.Mutate(x => x.Quantize(quantizer));
                }

                // Save compressed image
                image.Save(resultStream, encoder);
            }

            resultStream.Position = 0;
            var compressedSize = resultStream.Length;
            var percentSaving = ((double)(originalSize - compressedSize) / originalSize) * 100;

            return new ImageCompressedModel
            {
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                ResultFileStream = resultStream,
                FileType = imageType,
                QualityPercent = qualityPercent,
                TotalMillisecondsTook = 0 // Will be set by caller if needed
            };
        }

        public static async Task<ImageCompressedModel> CompressAsync(MemoryStream stream, int qualityPercent = 0, string fileName = null)
        {
            return await Task.Run(() => Compress(stream, qualityPercent, fileName)).ConfigureAwait(false);
        }

        /// <summary>
        /// Compress with advanced options
        /// </summary>
        public static ImageCompressedModel CompressAdvanced(MemoryStream stream, CompressionOptions options)
        {
            stream.Position = 0;
            var originalSize = stream.Length;

            var imageInfo = Image.Identify(stream);
            if (imageInfo == null)
            {
                throw new ArgumentException("Invalid image format", nameof(stream));
            }

            var resultStream = new MemoryStream();
            
            stream.Position = 0;
            using (var image = Image.Load(stream))
            {
                // Apply preprocessing if requested
                if (options.AutoRotate)
                {
                    image.Mutate(x => x.AutoOrient());
                }

                if (options.RemoveMetadata)
                {
                    image.Metadata.ExifProfile = null;
                    image.Metadata.IccProfile = null;
                    image.Metadata.IptcProfile = null;
                    image.Metadata.XmpProfile = null;
                }

                if (options.MaxWidth > 0 || options.MaxHeight > 0)
                {
                    var maxWidth = options.MaxWidth > 0 ? options.MaxWidth : int.MaxValue;
                    var maxHeight = options.MaxHeight > 0 ? options.MaxHeight : int.MaxValue;
                    
                    if (image.Width > maxWidth || image.Height > maxHeight)
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(maxWidth, maxHeight),
                            Mode = ResizeMode.Max,
                            Sampler = KnownResamplers.Lanczos3
                        }));
                    }
                }

                // Get encoder with options
                var encoder = GetAdvancedEncoder(imageInfo.Metadata.DecodedImageFormat, options);
                image.Save(resultStream, encoder);
            }

            resultStream.Position = 0;
            var compressedSize = resultStream.Length;

            return new ImageCompressedModel
            {
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                ResultFileStream = resultStream,
                FileType = GetImageType(imageInfo.Metadata.DecodedImageFormat),
                QualityPercent = options.Quality,
                TotalMillisecondsTook = 0
            };
        }

        private static CompressImageType GetImageType(IImageFormat format)
        {
            if (format == null) return CompressImageType.Invalid;

            return format.Name.ToLowerInvariant() switch
            {
                "jpeg" or "jpg" => CompressImageType.Jpeg,
                "png" => CompressImageType.Png,
                "gif" => CompressImageType.Gif,
                _ => CompressImageType.Invalid
            };
        }

        private static int GetDefaultQuality(CompressImageType imageType)
        {
            return imageType switch
            {
                CompressImageType.Jpeg => 85,
                CompressImageType.Png => 95,
                CompressImageType.Gif => 90,
                _ => 85
            };
        }

        private static int CalculateMaxColors(int qualityPercent)
        {
            // Map quality percentage to color count
            // 100% = 256 colors, 50% = 64 colors, 0% = 16 colors
            var colors = (int)(16 + (qualityPercent / 100.0) * 240);
            return Math.Max(16, Math.Min(256, colors));
        }

        private static IImageEncoder GetOptimizedEncoder(IImageFormat format, int quality)
        {
            if (format == null)
            {
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
                "gif" => new GifEncoder
                {
                    ColorTableMode = GifColorTableMode.Local,
                    Quantizer = new WuQuantizer(new QuantizerOptions
                    {
                        MaxColors = CalculateMaxColors(quality)
                    })
                },
                "webp" => new WebpEncoder
                {
                    Quality = quality,
                    Method = WebpEncodingMethod.BestQuality,
                    FileFormat = WebpFileFormatType.Lossy
                },
                _ => new JpegEncoder { Quality = quality }
            };
        }

        private static IImageEncoder GetAdvancedEncoder(IImageFormat format, CompressionOptions options)
        {
            if (format == null)
            {
                return new JpegEncoder { Quality = options.Quality };
            }

            return format.Name.ToLowerInvariant() switch
            {
                "jpeg" or "jpg" => new JpegEncoder
                {
                    Quality = options.Quality
                },
                "png" => new PngEncoder
                {
                    CompressionLevel = options.FastCompression 
                        ? PngCompressionLevel.BestSpeed 
                        : PngCompressionLevel.BestCompression,
                    ColorType = options.PreserveTransparency 
                        ? PngColorType.RgbWithAlpha 
                        : PngColorType.Rgb,
                    BitDepth = PngBitDepth.Bit8
                },
                "webp" => new WebpEncoder
                {
                    Quality = options.Quality,
                    Method = options.FastCompression 
                        ? WebpEncodingMethod.Fastest 
                        : WebpEncodingMethod.BestQuality,
                    FileFormat = options.Lossless 
                        ? WebpFileFormatType.Lossless 
                        : WebpFileFormatType.Lossy
                },
                _ => GetOptimizedEncoder(format, options.Quality)
            };
        }
    }

    /// <summary>
    /// Advanced compression options
    /// </summary>
    public class CompressionOptions
    {
        public int Quality { get; set; } = 85;
        public bool RemoveMetadata { get; set; } = true;
        public bool AutoRotate { get; set; } = true;
        public int MaxWidth { get; set; } = 0;
        public int MaxHeight { get; set; } = 0;
        public bool HighQualityChroma { get; set; } = false;
        public bool PreserveTransparency { get; set; } = true;
        public bool FastCompression { get; set; } = false;
        public bool Lossless { get; set; } = false;
    }
}