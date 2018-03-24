#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SwaggerGenOptionsExtensions.cs </Name>
//         <Created> 24/03/2018 11:53:04 PM </Created>
//         <Key> 16d02e06-c572-4538-b6a4-a6fb76c056a7 </Key>
//     </File>
//     <Summary>
//         SwaggerGenOptionsExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Elect.Web.Swagger.Utils.SwaggerGenOptionsUtils
{
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        ///     Includes the XML comment file if it has the same name as the assembly, a .xml file
        ///     extension and exists in the same directory as the assembly.
        /// </summary>
        /// <param name="options">  The Swagger options. </param>
        /// <param name="assembly"> The assembly. </param>
        /// <returns>
        ///     <c> true </c> if the comment file exists and was added, otherwise <c> false </c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"> options or assembly. </exception>
        public static SwaggerGenOptions IncludeXmlCommentsIfExists(this SwaggerGenOptions options, Assembly assembly)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var filePath = Path.ChangeExtension(assembly.Location, ".xml");

            if (!IncludeXmlCommentsIfExists(options, filePath) && assembly.CodeBase != null)
            {
                filePath = Path.ChangeExtension(new Uri(assembly.CodeBase).AbsolutePath, ".xml");

                IncludeXmlCommentsIfExists(options, filePath);
            }

            return options;
        }

        /// <summary>
        ///     Includes the XML comment file if it exists at the specified file path. 
        /// </summary>
        /// <param name="options">  The Swagger options. </param>
        /// <param name="filePath"> The XML comment file path. </param>
        /// <returns>
        ///     <c> true </c> if the comment file exists and was added, otherwise <c> false </c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"> options or filePath. </exception>
        public static bool IncludeXmlCommentsIfExists(this SwaggerGenOptions options, string filePath)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            options.IncludeXmlComments(filePath);

            return true;
        }
    }
}