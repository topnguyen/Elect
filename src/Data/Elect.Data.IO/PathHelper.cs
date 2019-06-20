#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> PathHelper.cs </Name>
//         <Created> 16/03/2018 6:12:12 PM </Created>
//         <Key> 5566d213-86bb-4e52-90a2-5d0c466db47c </Key>
//     </File>
//     <Summary>
//         PathHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;

namespace Elect.Data.IO
{
    public class PathHelper
    {
        /// <summary>
        ///     Return absolute path 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            path = CorrectPathSeparatorChar(path);
            
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            if (!Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out var pathUri))
            {
                throw new ArgumentException($"Invalid path: {path}");
            }

            if (pathUri.IsAbsoluteUri)
            {
                return path;
            }

            path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, path);

            return path;
        }

        /// <summary>
        ///     Correct path separator char fit with runtime OS.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CorrectPathSeparatorChar(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            path = path.Replace('\\', Path.DirectorySeparatorChar);
            path = path.Replace('/', Path.DirectorySeparatorChar);

            return path;
        }
    }
}