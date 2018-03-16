#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> AssemblyHelper.cs </Name>
//         <Created> 15/03/2018 8:41:07 PM </Created>
//         <Key> b3f7b151-a3db-429b-b640-5b8bc468eecb </Key>
//     </File>
//     <Summary>
//         AssemblyHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Elect.Core.AssemblyUtils
{
    public class AssemblyHelper
    {
        public static string GetDirectoryPath(Assembly assembly)
        {
            UriBuilder uri = new UriBuilder(assembly.CodeBase);

            string path = Uri.UnescapeDataString(uri.Path);

            var directoryPath = Path.GetDirectoryName(path);

            return directoryPath;
        }

        public static List<Assembly> LoadAssemblies(params string[] dllFileFullPaths)
        {
            if (dllFileFullPaths?.Any() != true)
            {
                throw new ArgumentException($"{nameof(dllFileFullPaths)} can not be empty.", nameof(dllFileFullPaths));
            }

            dllFileFullPaths = dllFileFullPaths.Distinct().ToArray();

            AssemblyLoader assemblyLoader = new AssemblyLoader();

            List<Assembly> assemblies = new List<Assembly>();

            foreach (var dllFileFullPath in dllFileFullPaths)
            {
                var dllNameWithoutExtension = Path.GetFileNameWithoutExtension(dllFileFullPath);

                var assemblyName = new AssemblyName(dllNameWithoutExtension);

                var assembly = assemblyLoader.LoadFromAssemblyName(assemblyName);

                assemblies.Add(assembly);
            }

            return assemblies;
        }
    }
}