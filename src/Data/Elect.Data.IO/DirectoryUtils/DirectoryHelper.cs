#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DirectoryHelper.cs </Name>
//         <Created> 02/04/2018 8:26:02 PM </Created>
//         <Key> 7340c81c-3c24-4336-9439-2fdbf16e9f9e </Key>
//     </File>
//     <Summary>
//         DirectoryHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.CheckUtils;
using System;
using System.IO;
using System.Linq;

namespace Elect.Data.IO.DirectoryUtils
{
    public class DirectoryHelper
    {
        public static void CreateIfNotExist(params string[] paths)
        {
            foreach (var path in paths)
            {
                CheckHelper.CheckNullOrWhiteSpace(path, nameof(paths));

                var fullPath = PathHelper.GetFullPath(path);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
            }
        }

        /// <summary>
        ///     Clear all content in the directory 
        /// </summary>
        /// <param name="paths"></param>
        public static void Empty(params string[] paths)
        {
            foreach (var path in paths)
            {
                // Remove all files
                var listFileInDirectory = Directory.GetFiles(path);

                foreach (var filePath in listFileInDirectory)
                {
                    File.Delete(filePath);
                }

                // Remove all directories
                var listDirectoryInDirectory = Directory.GetDirectories(path);

                foreach (var directoryPath in listDirectoryInDirectory)
                {
                    Directory.Delete(directoryPath);
                }
            }
        }

        /// <summary>
        ///     Check directory is empty or not 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public class SpecialFolder
        {
            public static string GetCurrentWindowUserFolder()
            {
                string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;

                if (Environment.OSVersion.Version.Major >= 6)
                {
                    path = Directory.GetParent(path).ToString();
                }

                return path;
            }
        }
    }
}