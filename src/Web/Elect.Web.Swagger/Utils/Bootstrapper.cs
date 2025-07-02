namespace Elect.Web.Swagger.Utils
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
            WorkingFolder = Path.GetFullPath(Path.Combine(new Uri(assembly.Location).LocalPath, $"..{Path.DirectorySeparatorChar.ToString()}Elect_Swagger{Path.DirectorySeparatorChar.ToString()}"));
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(WorkingFolder));
            if (directoryInfo.Exists)
            {
                // Already have folder mean already copied tools before => return
                return;
            }
            DirectoryHelper.CreateIfNotExist(WorkingFolder,
                Path.Combine(WorkingFolder, "css"),
                Path.Combine(WorkingFolder, "fonts"),
                Path.Combine(WorkingFolder, "images"),
                Path.Combine(WorkingFolder, "JsonViewer"),
                Path.Combine(WorkingFolder, "lib"));
            // Load tools
            var librariesNameSpace = $"{nameof(Elect)}.{nameof(Web)}.{nameof(Swagger)}.Elect_Swagger";
            // Get the resources and copy them across.
            Dictionary<string, string> resources = new Dictionary<string, string>
            {
                { @"index.html", $@"{librariesNameSpace}.index.html" },
                { @"json-viewer.html", $@"{librariesNameSpace}.json-viewer.html" },
                // css
                { $@"css{Path.DirectorySeparatorChar.ToString()}api-explorer.css", $@"{librariesNameSpace}.css.api-explorer.css" },
                { $@"css{Path.DirectorySeparatorChar.ToString()}index.css", $@"{librariesNameSpace}.css.index.css" },
                { $@"css{Path.DirectorySeparatorChar.ToString()}standalone.css", $@"{librariesNameSpace}.css.standalone.css" },
                // fonts
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-700.eot", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-700.eot" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-700.svg", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-700.svg" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-700.ttf", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-700.ttf" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-700.woff", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-700.woff" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-700.woff2", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-700.woff2" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-regular.eot", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-regular.eot" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-regular.svg", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-regular.svg" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-regular.ttf", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-regular.ttf" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-regular.woff", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-regular.woff" },
                { $@"fonts{Path.DirectorySeparatorChar.ToString()}droid-sans-v6-latin-regular.woff2", $@"{librariesNameSpace}.fonts.droid-sans-v6-latin-regular.woff2" },
                // images
                { $@"images{Path.DirectorySeparatorChar.ToString()}explorer_icons.png", $@"{librariesNameSpace}.images.explorer_icons.png" },
                { $@"images{Path.DirectorySeparatorChar.ToString()}favicon.ico", $@"{librariesNameSpace}.images.favicon.ico" },
                { $@"images{Path.DirectorySeparatorChar.ToString()}logo.png", $@"{librariesNameSpace}.images.logo.png" },
                { $@"images{Path.DirectorySeparatorChar.ToString()}throbber.gif", $@"{librariesNameSpace}.images.throbber.gif" },
                // JsonViewer
                { $@"JsonViewer{Path.DirectorySeparatorChar.ToString()}index.html", $@"{librariesNameSpace}.JsonViewer.index.html" },
                { $@"JsonViewer{Path.DirectorySeparatorChar.ToString()}logo.png", $@"{librariesNameSpace}.JsonViewer.logo.png" },
                { $@"JsonViewer{Path.DirectorySeparatorChar.ToString()}main.css", $@"{librariesNameSpace}.JsonViewer.main.css" },
                { $@"JsonViewer{Path.DirectorySeparatorChar.ToString()}main.js", $@"{librariesNameSpace}.JsonViewer.main.js" },
                // lib
                { $@"lib{Path.DirectorySeparatorChar.ToString()}backbone-min.js", $@"{librariesNameSpace}.lib.backbone-min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}bootstrap.min.js", $@"{librariesNameSpace}.lib.bootstrap.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}clipboard.min.js", $@"{librariesNameSpace}.lib.clipboard.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}handlebars-2.0.0.min.js", $@"{librariesNameSpace}.lib.handlebars-2.0.0.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}highlight.7.3.pack.min.js", $@"{librariesNameSpace}.lib.highlight.7.3.pack.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}jquery.ba-bbq.min.js", $@"{librariesNameSpace}.lib.jquery.ba-bbq.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}jquery.slideto.min.js", $@"{librariesNameSpace}.lib.jquery.slideto.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}jquery.wiggle.min.js", $@"{librariesNameSpace}.lib.jquery.wiggle.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}jquery-1.8.0.min.js", $@"{librariesNameSpace}.lib.jquery-1.8.0.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}marked.min.js", $@"{librariesNameSpace}.lib.marked.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}o2c.html", $@"{librariesNameSpace}.lib.o2c.html" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}swagger-oauth.min.js", $@"{librariesNameSpace}.lib.swagger-oauth.min.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}swagger-ui.js", $@"{librariesNameSpace}.lib.swagger-ui.js" },
                { $@"lib{Path.DirectorySeparatorChar.ToString()}underscore-min.js", $@"{librariesNameSpace}.lib.underscore-min.js" },
            };
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
                }
            }
        }
    }
}
