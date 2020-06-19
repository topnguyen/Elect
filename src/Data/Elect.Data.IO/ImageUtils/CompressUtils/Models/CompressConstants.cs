#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ImageCompressorConstants.cs </Name>
//         <Created> 02/04/2018 8:33:52 PM </Created>
//         <Key> 06c781a3-fcf6-4e93-8aaa-8457275f527d </Key>
//     </File>
//     <Summary>
//         ImageCompressorConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Data.IO.ImageUtils.CompressUtils.Models
{
    internal class CompressConstants
    {
        public const int DefaultPngQualityPercent = 75;

        public const int DefaultJpegQualityPercent = 75;

        public const int DefaultGifQualityPercent = 75;

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