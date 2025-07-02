namespace Elect.Data.IO.ImageUtils.CompressUtils
{
    public class ImageCompressor
    {
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
            using var stream = new MemoryStream();
            using var file = new FileStream(inputPath, FileMode.Open, FileAccess.Read)
            {
                Position = 0
            };
            // Copy file to stream
            file.CopyTo(stream);
            // Do compress
            var imageCompressedModel = Compress(stream, qualityPercent, timeout, inputPath);
            // Save to file
            imageCompressedModel?.ResultFileStream.Save(outputPath);
            return imageCompressedModel;
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
        /// <param name="fileName"></param>
        /// <returns> The Task containing processing information. </returns>
        /// <exception cref="ArgumentException"> stream is invalid image format </exception>
        public static ImageCompressedModel Compress(MemoryStream stream, int qualityPercent, int timeout, string fileName = null)
        {
            // Create new stopwatch.
            var stopwatch = new Stopwatch();
            var isValidImage = Helper.TryGetCompressImageType(stream, out var imageType);
            // Try to get Image Type 1 more time by file name extension
            if (!string.IsNullOrWhiteSpace(fileName) && (!isValidImage || imageType == CompressImageType.Invalid))
            {
                var fileExtension = Path.GetExtension(fileName);
                isValidImage = Helper.TryGetCompressImageType(fileExtension, out imageType);
            }
            if (!isValidImage || imageType == CompressImageType.Invalid)
            {
                throw new ArgumentException($"{nameof(stream)} is invalid image format", nameof(stream));
            }
            // Compress Algorithm
            var compressAlgorithm = Helper.GetCompressAlgorithm(imageType);
            // Handle default value
            qualityPercent = Helper.GetQualityPercent(qualityPercent, compressAlgorithm);
            ImageCompressedModel imageCompressedModel = null;
            // Begin timing
            stopwatch.Start();
            bool isCanOptimize = false;
            // Select Compress Algorithm based on Image Type
            while (qualityPercent > 0)
            {
                imageCompressedModel = Process(stream, compressAlgorithm, qualityPercent, timeout);
                if (imageCompressedModel == null ||
                    (imageCompressedModel.PercentSaving > 0 && imageCompressedModel.PercentSaving < 100))
                {
                    isCanOptimize = true;
                    break;
                }
                qualityPercent -= 10;
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
            var imageType = Helper.GetCompressImageType(algorithm);
            if (imageType == CompressImageType.Invalid)
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
            var processInfo = new ProcessStartInfo
            {
                Arguments =  GetArguments(filePath, algorithm, out var fileTempPaths, qualityPercent),
                WorkingDirectory = Bootstrapper.Instance.WorkingFolder,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                processInfo.FileName = "cmd";
                // "/c "  to allow " char in arguments
                processInfo.Arguments = $"/c {processInfo.Arguments}";
            }
            else
            {
                processInfo.FileName = "/bin/bash";
                // "-c " to allow " char in arguments
                processInfo.Arguments = $"-c \"{processInfo.Arguments}\"";
            }
            if (string.IsNullOrWhiteSpace(processInfo.Arguments))
            {
                throw new ArgumentException($"Command {nameof(processInfo.Arguments)} is empty", $"{nameof(processInfo.Arguments)}");
            }
            int elapsedTime = 0;
            bool eventHandled = false;
            try
            {
                var process = new Process
                {
                    StartInfo = processInfo,
                    EnableRaisingEvents = true
                };
                process.Exited += (sender, args) =>
                {
                    #if DEBUG
                    string standardOutput = process.StandardOutput.ReadToEnd();
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Process Standard Output: ");
                    Console.WriteLine(standardOutput);
                    Console.WriteLine("-------------------------");
                    string standardError = process.StandardError.ReadToEnd();
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Process Standard Error: ");
                    Console.WriteLine(standardError);
                    Console.WriteLine("-------------------------");
                    #endif
                    // Done compress
                    imageCompressedModel = new ImageCompressedModel(filePath, fileSizeBeforeCompress);
                    process.Dispose();
                    // Remove temp files if have
                    foreach (var fileTempPath in fileTempPaths)
                    {
                        FileHelper.SafeDelete(fileTempPath);
                    }
                    eventHandled = true;
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
        #region Command
        /// <summary>
        ///     Gets the correct arguments to pass to the compressor 
        /// </summary>
        /// <param name="filePath">       The source file. </param>
        /// <param name="fileTempPaths">  </param>
        /// <param name="algorithm">     
        ///     Default is auto depend on file extension, others is force algorithm
        /// </param>
        /// <param name="qualityPercent"> Quality PercentSaving - Process </param>
        /// <returns> The <see cref="string" /> containing the correct command arguments. </returns>
        /// <exception cref="ArgumentException"> file path is invalid </exception>
        private static string GetArguments(string filePath, CompressAlgorithm algorithm, out List<string> fileTempPaths, int qualityPercent = 0)
        {
            fileTempPaths = new List<string>();
            Helper.CheckFilePath(filePath);
            qualityPercent = Helper.GetQualityPercent(qualityPercent, algorithm);
            switch (algorithm)
            {
                case CompressAlgorithm.Jpeg:
                {
                    var jpegCommand = GetJpegCommand(filePath, out var fileJpegTempPath, qualityPercent);
                    fileTempPaths.Add(fileJpegTempPath);
                    var jpegLosslessCommand = GetJpegLosslessCommand(filePath, out var fileJpegLossessTempPath);
                    fileTempPaths.Add(fileJpegLossessTempPath);
                    return $"{jpegCommand} && {jpegLosslessCommand}";
                }
                case CompressAlgorithm.Png:
                {
                    return GetPngCommand(filePath, qualityPercent);
                }
                case CompressAlgorithm.Gif:
                {
                    return GetGifCommand(filePath);
                }
                default:
                    throw new NotSupportedException("The Compress Algorithm not support yet");
            }
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
            var streamTemp = new MemoryStream();
            FileHelper.WriteToStream(filePath, streamTemp);
            fileTempPath = FileHelper.CreateTempFile(streamTemp, CompressImageType.Jpeg.AsString(EnumFormat.Description));
            // cjpeg - lossy, after optimize => copy temp file to source file
            var jpegCompressor = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? CompressConstants.JpegWorkerFileNameWindows
                : CompressConstants.JpegLosslessWorkerFileNameLinux; // MacOs / Linux use same Lossless and Lossy JPEG Tool
            jpegCompressor =  Path.Combine(Bootstrapper.Instance.WorkingFolder, jpegCompressor);
            string jpegCommand = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows
                jpegCommand = $"{jpegCompressor} -quality {qualityPercent} \"{fileTempPath}\" > \"{filePath}\"";
            }
            else
            {
                // MacOS / Linux
                jpegCommand = $"{jpegCompressor} -m{qualityPercent} \"{filePath}\"";
            }
            return jpegCommand;  
        }
        // Jpeg Lossless
        private static string GetJpegLosslessCommand(string filePath, out string fileTempPath)
        {
            // Idea: create temp file from source file then optimize temp file and copy to source
            // file (because cjpeg not support override input file) temporary file will be delete
            // after process exit
            var streamTemp = new MemoryStream();
            FileHelper.WriteToStream(filePath, streamTemp);
            fileTempPath = FileHelper.CreateTempFile(streamTemp, CompressImageType.Jpeg.AsString(EnumFormat.Description));
            var jpegLosslessCompressor = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? CompressConstants.JpegLosslessWorkerFileNameWindows
                : CompressConstants.JpegLosslessWorkerFileNameLinux;
            jpegLosslessCompressor =  Path.Combine(Bootstrapper.Instance.WorkingFolder, jpegLosslessCompressor);
            string jpegLosslessCommand = string.Empty;
            // Windows
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // jpegtran - lossless, after optimize => copy temp file to source file
                jpegLosslessCommand = $"{jpegLosslessCompressor} -optimize -progressive -copy none \"{fileTempPath}\" > \"{filePath}\"";
            }
            else
            {
                // MacOS / Linux
                jpegLosslessCommand = $"{jpegLosslessCompressor} \"{filePath}\"";   
            }
            return jpegLosslessCommand;
        }
        // Png
        private static string GetPngCommand(string filePath, int qualityPercent = 0)
        {
            if (qualityPercent == 0)
            {
                qualityPercent = CompressConstants.DefaultPngQualityPercent;
            }
            // First, use pnguqnat to compress
            int maxQuality = qualityPercent + 15;
            // max is 99
            maxQuality = maxQuality > 99 ? 99 : maxQuality;
            var pngCompressor = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? CompressConstants.PngWorkerFileNameWindows
                : CompressConstants.PngWorkerFileNameLinux;
            pngCompressor =  Path.Combine(Bootstrapper.Instance.WorkingFolder, pngCompressor);
            // Windows and MacOS / Linux
            string pngCompressCommand = $"{pngCompressor} --speed 1 --quality={qualityPercent}-{maxQuality} --skip-if-larger --strip --output \"{filePath}\" --force \"{filePath}\"";
            return pngCompressCommand;
        }
        // GIF
        private static string GetGifCommand(string filePath, int qualityPercent = 0)
        {
            if (qualityPercent == 0)
            {
                qualityPercent = CompressConstants.DefaultGifQualityPercent;
            }
            int qualityLossyPercent = (100 - qualityPercent) * 2;
            // https://www.lcdf.org/gifsicle/man.html + lossy (https://github.com/pornel/giflossy/releases)
            // --use-col=web Adjust --lossy argument to suit quality (30 is very light compression, 200 is heavy).
            var gifCompressor = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? CompressConstants.GifWorkerFileNameWindows
                : CompressConstants.GifWorkerFileNameLinux;
            gifCompressor =  Path.Combine(Bootstrapper.Instance.WorkingFolder, gifCompressor);
            // Windows and MacOS / Linux
            var gifCommand = $"{gifCompressor} --no-warnings --no-app-extensions --no-comments --no-extensions --no-names -optimize=03 --lossy={qualityLossyPercent} \"{filePath}\" --output=\"{filePath}\"";
            return gifCommand;
        }
        #endregion Command
    }
}
