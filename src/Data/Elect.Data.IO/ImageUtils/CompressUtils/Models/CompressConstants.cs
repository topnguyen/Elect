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
        public const int DefaultPngQualityPercent = 90;

        public const int DefaultJpegQualityPercent = 75;

        public const int DefaultGifQualityPercent = 75;

        public const int TimeoutMillisecond = 600000;

        public const string GifWorkerFileName = "Elect_ImageCompressor_GIF.exe";

        public const string JpegLibFileName = "libjpeg-62.dll";

        public const string JpegWorkerFileName = "Elect_ImageCompressor_JPEG.exe";

        public const string JpegOptimizeWorkerFileName = "Elect_ImageCompressor_JPEG_Optimize.exe";

        public const string PngPrimaryWorkerFileName = "Elect_ImageCompressor_PNG_Primary.exe";

        public const string PngSecondaryWorkerFileName = "Elect_ImageCompressor_PNG_Secondary.exe";

        public const string PngOptimizeWorkerFileName = "Elect_ImageCompressor_PNG_Optimize.exe";
    }
}