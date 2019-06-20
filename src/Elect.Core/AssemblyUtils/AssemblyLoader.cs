#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Elect.Core.AssemblyUtils
{
    internal class AssemblyLoader : AssemblyLoadContext
    {
        internal List<AssemblyName> ListLoadedAssemblyName { get; } = new List<AssemblyName>();

        internal readonly string AssemblyDirectoryPath;

        internal AssemblyLoader(string assemblyDirectoryPath)
        {
            AssemblyDirectoryPath = assemblyDirectoryPath;

            // Update List Loaded Assembly
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();

            var listLoadedAssemblyName = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId).ToList();

            foreach (var assemblyName in listLoadedAssemblyName)
            {
                if (!ListLoadedAssemblyName.Contains(assemblyName))
                {
                    ListLoadedAssemblyName.Add(assemblyName);
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Load an assembly, if the assembly already loaded then return 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            // Check if assembly already added by Dependency (Reference)

            if (ListLoadedAssemblyName.Any(x => string.Equals(x.Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            // Load Assembly not yet load

            var assemblyFilePath = Path.Combine(AssemblyDirectoryPath, assemblyName.Name) + ".dll";

            var assembly = File.Exists(assemblyFilePath) ? LoadFromAssemblyPath(assemblyFilePath) : Assembly.Load(assemblyName);

            // Add to loaded

            ListLoadedAssemblyName.Add(assemblyName);

            return assembly;
        }
    }
}