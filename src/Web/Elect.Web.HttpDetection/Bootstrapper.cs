namespace Elect.Web.HttpDetection
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
            // Create the folder for storing temporary images and tools.
            WorkingFolder = Path.GetFullPath(Path.Combine(new Uri(assembly.Location).LocalPath, $"..{Path.DirectorySeparatorChar.ToString()}"));
            // Load tools
            var librariesNameSpace = $"{nameof(Elect)}.{nameof(Web)}.{nameof(HttpDetection)}";
            // Get the resources and copy them across.
            Dictionary<string, string> resources = new Dictionary<string, string>
            {
                { ElectHttpDetectionConstants.DbName, $"{librariesNameSpace}.{ElectHttpDetectionConstants.DbName}" },
            };
            // Write the files out to the bin folder.
            foreach (KeyValuePair<string, string> resource in resources)
            {
                using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource.Value))
                {
                    if (resourceStream == null)
                    {
                        continue;
                    }
                    var toolFullPath = Path.Combine(WorkingFolder, resource.Key);
                    using (FileStream fileStream = File.OpenWrite(toolFullPath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
            }
        }
    }
}
