namespace Elect.Data.IO.ImageUtils.CompressUtils.Models
{
    internal class CompressConstants
    {
        public const int DefaultPngQualityPercent = 62;
        public const int DefaultJpegQualityPercent = 62;
        public const int DefaultGifQualityPercent = 62;
        public const int TimeoutMillisecond = 600000;
        // PNG
        public const string PngWorkerFileNameWindows = "Elect_ImageCompressor_PNG.exe";
        public const string PngWorkerFileNameLinux = "Elect_ImageCompressor_PNG";
        // JPEG
        public const string JpegWorkerFileNameWindows = "Elect_ImageCompressor_JPEG.exe";
        public const string JpegLibFileNameWindows = "libjpeg-62.dll";
        // JPEG Lossless
        public const string JpegLosslessWorkerFileNameWindows = "Elect_ImageCompressor_JPEG_Lossless.exe";
        public const string JpegLosslessWorkerFileNameLinux = "Elect_ImageCompressor_JPEG_Lossless";
        // GIF
        public const string GifWorkerFileNameWindows = "Elect_ImageCompressor_GIF.exe";
        public const string GifWorkerFileNameLinux = "Elect_ImageCompressor_GIF";
    }
}
