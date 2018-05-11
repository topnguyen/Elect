#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ImageCompressorBootstrapper.cs </Name>
//         <Created> 02/04/2018 8:33:28 PM </Created>
//         <Key> 7769f3ba-641c-4cdc-8795-bd9ed380047c </Key>
//     </File>
//     <Summary>
//         ImageCompressorBootstrapper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO.ImageUtils.CompressUtils.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

            // Create the folder for storing temporary images and tools.

            WorkingFolder = Path.GetFullPath(Path.Combine(new Uri(assembly.Location).LocalPath, $"..{Path.DirectorySeparatorChar.ToString()}Elect_ImageCompressor{Path.DirectorySeparatorChar.ToString()}"));

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(WorkingFolder));

            if (directoryInfo.Exists)
            {
                // Already have folder mean already copied tools before => return
                return;
            }

            directoryInfo.Create();

            // Load tools
            var librariesNameSpace = $"{nameof(Elect)}.{nameof(Data)}.{nameof(IO)}.{nameof(ImageUtils)}.{nameof(CompressUtils)}.Tools";

            // Get the resources and copy them across.
            Dictionary<string, string> resources = new Dictionary<string, string>
            {
                { CompressConstants.GifWorkerFileName, $"{librariesNameSpace}.{CompressConstants.GifWorkerFileName}" },
                { CompressConstants.JpegLibFileName, $"{librariesNameSpace}.{CompressConstants.JpegLibFileName}" },
                { CompressConstants.JpegWorkerFileName, $"{librariesNameSpace}.{CompressConstants.JpegWorkerFileName}" },
                { CompressConstants.JpegOptimizeWorkerFileName, $"{librariesNameSpace}.{CompressConstants.JpegOptimizeWorkerFileName}" },
                { CompressConstants.PngPrimaryWorkerFileName, $"{librariesNameSpace}.{CompressConstants.PngPrimaryWorkerFileName}" },
                { CompressConstants.PngSecondaryWorkerFileName, $"{librariesNameSpace}.{CompressConstants.PngSecondaryWorkerFileName}" },
                { CompressConstants.PngOptimizeWorkerFileName, $"{librariesNameSpace}.{CompressConstants.PngOptimizeWorkerFileName}" }
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