namespace Elect.Data.IO.ImageUtils.CompressUtils
{
    internal sealed class Bootstrapper
    {
        /// <summary>
        ///     A new instance of the <see cref="T:.Web.Config.Config" /> class. with lazy initialization. 
        /// </summary>
        private static readonly Lazy<Bootstrapper> Lazy = new Lazy<Bootstrapper>(() => new Bootstrapper());
        /// <summary>
        ///     Prevents a default instance of the <see cref="Bootstrapper" /> class from being created. 
        /// </summary>
        private Bootstrapper()
        {
            if (!Lazy.IsValueCreated)
            {
                RegisterExecutable();
            }
        }
        /// <summary>
        ///     Gets the current instance of the <see cref="Bootstrapper" /> class. 
        /// </summary>
        public static Bootstrapper Instance => Lazy.Value;
        /// <summary>
        ///     Gets the working directory path. 
        /// </summary>
        public string WorkingFolder { get; private set; }
        /// <summary>
        ///     Registers the embedded executable. 
        /// </summary>
        public void RegisterExecutable()
        {
            // None of the tools used here are called using dll import so we don't go through the
            // normal registration channel.
            Assembly assembly = Assembly.GetExecutingAssembly();
            // Load tools
            var librariesNameSpace = $"{nameof(Elect)}.{nameof(Data)}.{nameof(IO)}.{nameof(ImageUtils)}.{nameof(CompressUtils)}.Tools";
            // Get the resources and copy them across.
            Dictionary<string, string> resources = new Dictionary<string, string>
            {
                // JPEG
                { CompressConstants.JpegWorkerFileNameWindows, $"{librariesNameSpace}.{CompressConstants.JpegWorkerFileNameWindows}" },
                { CompressConstants.JpegLibFileNameWindows, $"{librariesNameSpace}.{CompressConstants.JpegLibFileNameWindows}" },
                // JPEG Lossless
                { CompressConstants.JpegLosslessWorkerFileNameWindows, $"{librariesNameSpace}.{CompressConstants.JpegLosslessWorkerFileNameWindows}" },
                { CompressConstants.JpegLosslessWorkerFileNameLinux, $"{librariesNameSpace}.{CompressConstants.JpegLosslessWorkerFileNameLinux}" },
                // PNG
                { CompressConstants.PngWorkerFileNameWindows, $"{librariesNameSpace}.{CompressConstants.PngWorkerFileNameWindows}" },
                { CompressConstants.PngWorkerFileNameLinux, $"{librariesNameSpace}.{CompressConstants.PngWorkerFileNameLinux}" },
                // GIF
                { CompressConstants.GifWorkerFileNameWindows, $"{librariesNameSpace}.{CompressConstants.GifWorkerFileNameWindows}" },
                { CompressConstants.GifWorkerFileNameLinux, $"{librariesNameSpace}.{CompressConstants.GifWorkerFileNameLinux}" },
            };
            // Create the folder for storing temporary images and tools.
            WorkingFolder = Path.GetFullPath(Path.Combine(new Uri(assembly.Location).LocalPath,
                $"..{Path.DirectorySeparatorChar.ToString()}Elect_ImageCompressor{Path.DirectorySeparatorChar.ToString()}"));
            var workingFolder = Path.GetDirectoryName(WorkingFolder) ?? Directory.GetCurrentDirectory();
            var directoryInfo = new DirectoryInfo(workingFolder);
            // Check all needed resources in directory or not
            var isAllResourceInFolder = false;
            if (directoryInfo.Exists)
            {
                var files = Directory.GetFiles(workingFolder);
                var filesName = files.Select(Path.GetFileName).ToList();
                foreach (var resourceFileName in resources.Keys)
                {
                    if (filesName.All(x => x != resourceFileName))
                    {
                        isAllResourceInFolder = false;
                        break;
                    }
                    isAllResourceInFolder = true;
                }
                if (!isAllResourceInFolder)
                {
                    Directory.Delete(workingFolder, true);
                }
            }
            // Delete Directory if any required resources missing
            if (isAllResourceInFolder)
            {
                // If not missing any resource, then return (no need to copy file again)
                return;
            }
            directoryInfo.Create();
            // Write the files out to the bin folder.
            foreach (KeyValuePair<string, string> resource in resources)
            {
                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource.Value))
                {
                    if (resourceStream == null)
                    {
                        continue;
                    }
                    var toolFullPath = Path.Combine(WorkingFolder, resource.Key);
                    using (var fileStream = File.OpenWrite(toolFullPath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                    FileHelper.SetLinuxFilePermission( UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute | UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute, toolFullPath);
                }
            }
        }
    }
}
