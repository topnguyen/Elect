#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ImageCompressor.cs </Name>
//         <Created> 02/04/2018 8:41:29 PM </Created>
//         <Key> c339e947-0a8c-4842-aeb6-7e338ca57f83 </Key>
//     </File>
//     <Summary>
//         ImageCompressor.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO.FileUtils;
using Elect.Data.IO.ImageUtils.Compress.Models;
using Elect.Data.IO.StreamUtils;
using EnumsNET;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Elect.Data.IO.ImageUtils.Compress
{
    public class ImageCompressor
    {
        #region Compress

        public static ImageCompressedModel Compress(string inputPath, string outputPath)
        {
            return Compress(inputPath, outputPath, 0, CompressConstants.TimeoutMillisecond);
        }

        /// <summary>
        ///     Runs the process to optimize the image. 
        /// </summary>
        /// <param name="inputPath">     </param>
        /// <param name="outputPath">    </param>
        /// <param name="qualityPercent">
        ///     Quality of image after compress, 0 is auto quality by image type
        /// </param>
        /// <param name="timeout">        timeout of process in millisecond </param>
        /// <returns> The Task containing processing information. </returns>
        /// <exception cref="ArgumentException"> input path is invalid image format </exception>
        public static ImageCompressedModel Compress(string inputPath, string outputPath, int qualityPercent, int timeout)
        {
            inputPath = PathHelper.GetFullPath(inputPath);

            outputPath = PathHelper.GetFullPath(outputPath);

            using (MemoryStream stream = new MemoryStream())
            {
                using (FileStream file = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    // Copy file to stream

                    file.Position = 0;

                    file.CopyTo(stream);

                    // Do compress
                    ImageCompressedModel imageCompressedModel = Compress(stream, qualityPercent, timeout);

                    // Save to file
                    imageCompressedModel?.ResultFileStream.Save(outputPath);

                    return imageCompressedModel;
                }
            }
        }

        public static ImageCompressedModel Compress(MemoryStream stream)
        {
            return Compress(stream, 0, CompressConstants.TimeoutMillisecond);
        }

        /// <summary>
        ///     Runs the process to optimize the image. 
        /// </summary>
        /// <param name="stream">         The source image stream. </param>
        /// <param name="qualityPercent">
        ///     Quality of image after compress, 0 is auto quality by image type
        /// </param>
        /// <param name="timeout">        timeout of process in millisecond </param>
        /// <returns> The Task containing processing information. </returns>
        /// <exception cref="ArgumentException"> stream is invalid image format </exception>
        public static ImageCompressedModel Compress(MemoryStream stream, int qualityPercent, int timeout)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            bool isValidImage = Helper.TryGetCompressImageType(stream, out var imageType);

            if (!isValidImage || imageType == CompressImageType.Invalid)
            {
                throw new ArgumentException($"{nameof(stream)} is invalid image format", nameof(stream));
            }

            // Handle default value
            qualityPercent = Helper.GetQualityPercent(qualityPercent, imageType);

            ImageCompressedModel imageCompressedModel = null;

            // Begin timing.
            stopwatch.Start();

            bool isCanOptimize = false;

            switch (imageType)
            {
                case CompressImageType.Png:
                    {
                        while (qualityPercent > 0)
                        {
                            imageCompressedModel = Process(stream, CompressAlgorithm.PngPrimary, qualityPercent, timeout);

                            if (imageCompressedModel == null || (imageCompressedModel.PercentSaving > 0 && imageCompressedModel.PercentSaving < 100))
                            {
                                isCanOptimize = true;

                                break;
                            }

                            qualityPercent -= 10;
                        }

                        // if quality percent < 0 then try compress by png secondary algorithm (this
                        // algorithm not related to quality percent)
                        if (!isCanOptimize)
                        {
                            imageCompressedModel = Process(stream, CompressAlgorithm.PngSecondary, qualityPercent, timeout);
                        }

                        break;
                    }
                case CompressImageType.Jpeg:
                    {
                        while (qualityPercent > 0)
                        {
                            imageCompressedModel = Process(stream, CompressAlgorithm.Jpeg, qualityPercent, timeout);

                            if (imageCompressedModel == null || (imageCompressedModel.PercentSaving > 0 && imageCompressedModel.PercentSaving < 100))
                            {
                                isCanOptimize = true;

                                break;
                            }

                            qualityPercent -= 10;
                        }

                        break;
                    }
                case CompressImageType.Gif:
                    {
                        while (qualityPercent > 0)
                        {
                            imageCompressedModel = Process(stream, CompressAlgorithm.Gif, qualityPercent, timeout);

                            if (imageCompressedModel == null || (imageCompressedModel.PercentSaving > 0 && imageCompressedModel.PercentSaving < 100))
                            {
                                isCanOptimize = true;

                                break;
                            }

                            qualityPercent -= 10;
                        }

                        break;
                    }
            }

            // Stop timing.
            stopwatch.Stop();

            // if cannot at all, return null

            if (imageCompressedModel == null)
            {
                return null;
            }

            if (imageCompressedModel.PercentSaving > 0 && imageCompressedModel.PercentSaving < 100)
            {
                // update total millisecond took only
                imageCompressedModel.TotalMillisecondsTook = stopwatch.ElapsedMilliseconds;
            }
            else
            {
                // update total millisecond took
                imageCompressedModel.TotalMillisecondsTook = stopwatch.ElapsedMilliseconds;

                // Cannot optimize Use origin for destination => update file size and stream
                imageCompressedModel.CompressedFileSize = imageCompressedModel.OriginalFileSize;

                // Copy origin steam to result
                imageCompressedModel.ResultFileStream.SetLength(0);

                stream.Position = 0;

                stream.CopyTo(imageCompressedModel.ResultFileStream);
            }

            imageCompressedModel.QualityPercent = isCanOptimize ? qualityPercent : 100;

            return imageCompressedModel;
        }

        #endregion

        #region Private Helper

        #region Process

        /// <summary>
        ///     Runs the process to optimize the image. 
        /// </summary>
        /// <param name="stream">        </param>
        /// <param name="algorithm">     
        ///     Default is auto depend on file extension, others is force algorithm
        /// </param>
        /// <param name="qualityPercent">
        ///     Quality of image after compress, 0 is default it mean auto quality by image type
        /// </param>
        /// <param name="timeout">        TimeoutMillisecond of process in millisecond </param>
        /// <returns> The Task containing processing information. </returns>
        /// <exception cref="ArgumentException"> stream is invalid image format </exception>
        private static ImageCompressedModel Process(MemoryStream stream, CompressAlgorithm algorithm, int qualityPercent = 0, int timeout = 0)
        {
            bool isValidImage = Helper.TryGetCompressImageType(stream, out var imageType);

            if (!isValidImage || imageType == CompressImageType.Invalid)
            {
                throw new ArgumentException($"{nameof(stream)} is invalid image format", nameof(stream));
            }

            // Create a source temporary file with the correct extension.
            var filePath = FileHelper.CreateTempFile(stream, imageType.AsString(EnumFormat.Description));

            ImageCompressedModel imageCompressedModel = Process(filePath, algorithm, qualityPercent, timeout);

            if (imageCompressedModel != null)
            {
                // update file type, because in process not update it
                imageCompressedModel.FileType = imageType;
            }

            // Cleanup temp file
            FileHelper.SafeDelete(filePath);

            return imageCompressedModel;
        }

        /// <summary>
        ///     Runs the process to optimize the image. 
        /// </summary>
        /// <param name="filePath">       The source file. </param>
        /// <param name="algorithm">     
        ///     Default is auto depend on file extension, others is force algorithm
        /// </param>
        /// <param name="qualityPercent">
        ///     Quality of image after compress, 0 is default it mean auto quality by image type
        /// </param>
        /// <param name="timeout">        TimeoutMillisecond of process in millisecond </param>
        /// <returns> The Task containing processing information. </returns>
        /// <exception cref="ArgumentException">
        ///     file path is invalid, argument of command is invalid
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     Some security policies don't allow execution of programs in this way
        /// </exception>
        private static ImageCompressedModel Process(string filePath, CompressAlgorithm algorithm, int qualityPercent = 0, int timeout = 0)
        {
            Helper.CheckFilePath(filePath);

            long fileSizeBeforeCompress = new FileInfo(filePath).Length;

            ImageCompressedModel imageCompressedModel = null;

            var processInfo = new ProcessStartInfo("cmd")
            {
                WorkingDirectory = Bootstrapper.Instance.WorkingPath,

                Arguments = GetArguments(filePath, out var fileTempPath, algorithm, qualityPercent),

                UseShellExecute = false,

                CreateNoWindow = true,

                WindowStyle = ProcessWindowStyle.Hidden,

                RedirectStandardOutput = false,

                RedirectStandardError = false
            };

            if (string.IsNullOrWhiteSpace(processInfo.Arguments))
            {
                throw new ArgumentException($"Command {nameof(processInfo.Arguments)} is empty", $"{nameof(processInfo.Arguments)}");
            }

            int elapsedTime = 0;

            bool eventHandled = false;

            try
            {
                Process process = new Process
                {
                    StartInfo = processInfo,
                    EnableRaisingEvents = true
                };

                process.Exited += (sender, args) =>
                {
                    // Done compress
                    imageCompressedModel = new ImageCompressedModel(filePath, fileSizeBeforeCompress);
                    process.Dispose();
                    eventHandled = true;

                    // Remove temp file if have
                    FileHelper.SafeDelete(fileTempPath);
                };

                process.Start();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                throw new NotSupportedException("Some security policies don't allow execution of programs in this way", ex);
            }

            // Wait for Exited event, but not more than config timeout time.
            const int sleepAmount = 100;

            while (!eventHandled)
            {
                elapsedTime += sleepAmount;

                if (elapsedTime > timeout && timeout > 0)
                {
                    break;
                }

                Thread.Sleep(sleepAmount);
            }

            // update compress result stream
            if (imageCompressedModel != null)
            {
                FileHelper.WriteToStream(filePath, imageCompressedModel.ResultFileStream);
            }
            return imageCompressedModel;
        }

        #endregion Process

        #region Command

        /// <summary>
        ///     Gets the correct arguments to pass to the compressor 
        /// </summary>
        /// <param name="filePath">       The source file. </param>
        /// <param name="fileTempPath">  </param>
        /// <param name="algorithm">     
        ///     Default is auto depend on file extension, others is force algorithm
        /// </param>
        /// <param name="qualityPercent"> Quality PercentSaving - Process </param>
        /// <returns> The <see cref="string" /> containing the correct command arguments. </returns>
        /// <exception cref="ArgumentException"> file path is invalid </exception>
        private static string GetArguments(string filePath, out string fileTempPath, CompressAlgorithm algorithm, int qualityPercent = 0)
        {
            fileTempPath = null;

            Helper.CheckFilePath(filePath);

            qualityPercent = Helper.GetQualityPercent(qualityPercent, algorithm);

            switch (algorithm)
            {
                case CompressAlgorithm.Gif:
                    {
                        return GetGifCommand(filePath);
                    }
                case CompressAlgorithm.Jpeg:
                    {
                        return GetJpegCommand(filePath, out fileTempPath, qualityPercent);
                    }
                case CompressAlgorithm.PngPrimary:
                    {
                        return GetPngPrimaryCommand(filePath, qualityPercent);
                    }
            }

            return GetPngSecondaryCommand(filePath);
        }

        /// <summary>
        ///     GIF Command 
        /// </summary>
        /// <param name="filePath">      </param>
        /// <param name="qualityPercent"></param>
        /// <returns></returns>
        private static string GetGifCommand(string filePath, int qualityPercent = 0)
        {
            if (qualityPercent == 0)
            {
                qualityPercent = CompressConstants.DefaultGifQualityPercent;
            }

            int qualityLossyPercent = (100 - qualityPercent) * 2;

            // https://www.lcdf.org/gifsicle/man.html https://linux.die.net/man/1/gifsicle + lossy (https://github.com/pornel/giflossy/releases)
            // --use-col=web Adjust --lossy argument to suit quality (30 is very light compression,
            // 200 is heavy).
            var cmd = $"/c {CompressConstants.GifWorkerFileName} --no-warnings --no-app-extensions --no-comments --no-extensions --no-names -optimize=03 --lossy={qualityLossyPercent} \"{filePath}\" --output=\"{filePath}\"";

            return cmd;
        }

        // Jpeg
        private static string GetJpegCommand(string filePath, out string fileTempPath, int qualityPercent = 0)
        {
            if (qualityPercent == 0)
            {
                qualityPercent = CompressConstants.DefaultJpegQualityPercent;
            }

            // Idea: create temp file from source file then optimize temp file and copy to source
            // file (because cjpeg not support override input file) temporary file will be delete
            // after process exit

            MemoryStream streamTemp = new MemoryStream();

            FileHelper.WriteToStream(filePath, streamTemp);

            fileTempPath = FileHelper.CreateTempFile(streamTemp, CompressImageType.Jpeg.AsString(EnumFormat.Description));

            // cjpeg after optimize => copy temp file to source file
            string jpegCommand = $"{CompressConstants.JpegWorkerFileName} -optimize -quality {qualityPercent} \"{fileTempPath}\" > \"{filePath}\"";

            // jpegoptim lossless not lossy
            string jpegOptimizeCommand = GetJpegOptimizeCommand(filePath);

            return $"/c {jpegCommand} & {jpegOptimizeCommand}";
        }

        private static string GetJpegOptimizeCommand(string filePath)
        {
            // jpegoptim lossless not lossy
            string jpegOptimizeCommand = $"{CompressConstants.JpegOptimizeWorkerFileName} -o -q -p --force --strip-all --strip-iptc --strip-icc --all-progressive \"{filePath}\"";
            return jpegOptimizeCommand;
        }

        // Png
        private static string GetPngPrimaryCommand(string filePath, int qualityPercent = 0)
        {
            if (qualityPercent == 0)
            {
                qualityPercent = CompressConstants.DefaultPngQualityPercent;
            }

            // First, use pnguqnat to compress
            int maxQuality = qualityPercent + 15;

            // max is 99
            maxQuality = maxQuality > 99 ? 99 : maxQuality;
            string pngPrimaryCommand = $"{CompressConstants.PngPrimaryWorkerFileName} --speed 1 --quality={qualityPercent}-{maxQuality} --skip-if-larger --strip --output \"{filePath}\" --force \"{filePath}\"";

            // Then use pngout to optimize Recompress by pngout to make maximum recompress
            var pngOptimizeCommand = GetPngOptimizeCommand(filePath);
            return $"/c {pngPrimaryCommand} & {pngOptimizeCommand}";
        }

        private static string GetPngSecondaryCommand(string filePath)
        {
            // First, use pngqn to compress view more detail http://pngnq.sourceforge.net/
            string pngSecondaryCommand = $"{CompressConstants.PngSecondaryWorkerFileName} -f -s1 -e.png -n 256 \"{filePath}\"";

            // Then use pngout to optimize Recompress by pngout to make maximum recompress
            var pngOptimizeCommand = GetPngOptimizeCommand(filePath);
            return $"/c {pngSecondaryCommand} & {pngOptimizeCommand}";
        }

        private static string GetPngOptimizeCommand(string filePath)
        {
            // view more detail http://www.advsys.net/ken/util/pngout.htm use s2 f5 to make fastest
            // with quality (s0 and f0 take minutes)
            string pngOptimize = $"{CompressConstants.PngOptimizeWorkerFileName} \"{filePath}\" \"{filePath}\" /s2 /f5 /y /q";
            return pngOptimize;
        }

        #endregion Command

        #endregion
    }
}