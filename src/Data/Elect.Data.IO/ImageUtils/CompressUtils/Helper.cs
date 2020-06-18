#region License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> Helper.cs </Name>
//         <Created> 02/04/2018 8:44:24 PM </Created>
//         <Key> 91b0acdc-8753-4cee-815d-9144ee0767a0 </Key>
//     </File>
//     <Summary>
//         Helper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Data.IO.ImageUtils.CompressUtils.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Elect.Data.IO.ImageUtils.CompressUtils
{
    internal static class Helper
    {
        /// <summary>
        ///     Throw argument exception if path not valid 
        /// </summary>
        /// <param name="filePath"></param>
        /// <example cref="ArgumentException"></example>
        internal static void CheckFilePath(string filePath)
        {
            if (!Uri.IsWellFormedUriString(filePath, UriKind.RelativeOrAbsolute) && !File.Exists(filePath))
            {
                throw new ArgumentException($"{nameof(filePath)} is invalid: {filePath}", nameof(filePath));
            }
        }

        internal static bool TryGetCompressImageType(string extension, out CompressImageType compressImageType)
        {
            compressImageType = CompressImageType.Invalid;

            if (string.IsNullOrWhiteSpace(extension))
            {
                return false;
            }

            if (extension == ".png")
            {
                compressImageType = CompressImageType.Png;

                return true;
            }
            
            if (extension == ".jpg" || extension == ".jpeg")
            {
                compressImageType = CompressImageType.Jpeg;
                
                return true;
            }
            
            if (extension == ".gif")
            {
                compressImageType = CompressImageType.Gif;
                
                return true;
            }

            return false;
        }

        internal static bool TryGetCompressImageType(MemoryStream imageStream, out CompressImageType compressImageType)
        {
            bool isValid = false;

            compressImageType = CompressImageType.Invalid;

            try
            {
                // Raster Image
                using (Image image = Image.FromStream(imageStream))
                {
                    isValid = true;

                    if (ImageFormat.Jpeg.Equals(image.RawFormat))
                    {
                        compressImageType = CompressImageType.Jpeg;
                    }
                    else if (ImageFormat.Png.Equals(image.RawFormat))
                    {
                        compressImageType = CompressImageType.Png;
                    }
                    else if (ImageFormat.Gif.Equals(image.RawFormat))
                    {
                        compressImageType = CompressImageType.Gif;
                    }
                    else
                    {
                        isValid = false;

                        compressImageType = CompressImageType.Invalid;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return isValid;
        }

        internal static CompressImageType GetCompressImageType(CompressAlgorithm compressAlgorithm)
        {
            var compressImageType = CompressImageType.Invalid;

            if (compressAlgorithm == CompressAlgorithm.Png)
            {
                compressImageType = CompressImageType.Png;
            }
            else if (compressAlgorithm == CompressAlgorithm.Jpeg)
            {
                compressImageType = CompressImageType.Jpeg;
            }
            else if (compressAlgorithm == CompressAlgorithm.Gif)
            {
                compressImageType = CompressImageType.Gif;
            }

            return compressImageType;
        }

        /// <summary>
        ///     Min 0 - max 99 
        /// </summary>
        /// <param name="compressImageType"></param>
        /// <returns></returns>
        internal static CompressAlgorithm GetCompressAlgorithm(CompressImageType compressImageType)
        {
            var compressAlgorithm = CompressAlgorithm.Png;

            if (compressImageType == CompressImageType.Png)
            {
                compressAlgorithm = CompressAlgorithm.Png;
            }
            else if (compressImageType == CompressImageType.Jpeg)
            {
                compressAlgorithm = CompressAlgorithm.Jpeg;
            }
            else if (compressImageType == CompressImageType.Gif)
            {
                compressAlgorithm = CompressAlgorithm.Gif;
            }

            return compressAlgorithm;
        }

        /// <summary>
        ///     Min 0 - max 99 
        /// </summary>
        /// <param name="qualityPercent"></param>
        /// <param name="algorithm">     </param>
        /// <returns></returns>
        internal static int GetQualityPercent(int qualityPercent, CompressAlgorithm algorithm)
        {
            qualityPercent = qualityPercent < 0 ? 0 : (qualityPercent > 99 ? 99 : qualityPercent);

            if (qualityPercent <= 0)
            {
                switch (algorithm)
                {
                    case CompressAlgorithm.Png:
                        qualityPercent = CompressConstants.DefaultPngQualityPercent;
                        break;

                    case CompressAlgorithm.Jpeg:
                        qualityPercent = CompressConstants.DefaultJpegQualityPercent;
                        break;

                    case CompressAlgorithm.Gif:
                        qualityPercent = CompressConstants.DefaultGifQualityPercent;
                        break;
                }
            }

            return qualityPercent;
        }
    }
}