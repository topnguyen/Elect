#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> AssemblyLoader.cs </Name>
//         <Created> 16/03/2018 11:09:48 AM </Created>
//         <Key> 88b43622-7525-4ea3-b79a-7f580f3f4b46 </Key>
//     </File>
//     <Summary>
//         AssemblyLoader.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Elect.Core.AssemblyUtils
{
    public class AssemblyLoader : AssemblyLoadContext
    {
        public readonly string AssemblyFolderPath;

        public List<AssemblyName> ListLoadedAssemblyName { get; } = new List<AssemblyName>();

        /// <summary>
        ///     Assembly Loader 
        /// </summary>
        /// <param name="assemblyFolderPath"> null or empty will use Application Base Path </param>
        public AssemblyLoader(string assemblyFolderPath = null)
        {
            if (string.IsNullOrWhiteSpace(assemblyFolderPath))
            {
                assemblyFolderPath = Path.GetFullPath(PlatformServices.Default.Application.ApplicationBasePath);
            }

            if (!Directory.Exists(assemblyFolderPath))
            {
                throw new ArgumentException($"'{assemblyFolderPath}' is not exist", nameof(assemblyFolderPath));
            }

            AssemblyFolderPath = assemblyFolderPath;

            // Update List Loaded Assembly
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();

            var listLoadedAssemblyName = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId);

            foreach (var assemblyName in listLoadedAssemblyName)
            {
                ListLoadedAssemblyName.Add(assemblyName);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load an assembly, if the assembly already loaded then return null 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            Assembly assembly;

            // Check if assembly already added by Dependency (Reference)
            if (ListLoadedAssemblyName.Any(x => x.Name.ToLower() == assemblyName.Name.ToLower()))
            {
                return null;
            }

            // Load Assembly not yet load
            var assemblyFileInfo = new FileInfo($"{AssemblyFolderPath}{Path.DirectorySeparatorChar}{assemblyName.Name}.dll");

            if (File.Exists(assemblyFileInfo.FullName))
            {
                var assemblyLoader = new AssemblyLoader(assemblyFileInfo.DirectoryName);

                assembly = assemblyLoader.LoadFromAssemblyPath(assemblyFileInfo.FullName);
            }
            else
            {
                assembly = Assembly.Load(assemblyName);
            }

            // Add to loaded
            ListLoadedAssemblyName.Add(assembly.GetName());

            return assembly;
        }
    }
}