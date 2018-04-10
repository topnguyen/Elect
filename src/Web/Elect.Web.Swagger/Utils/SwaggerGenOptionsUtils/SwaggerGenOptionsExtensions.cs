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

using Elect.Core.Attributes;
using Elect.Web.Swagger.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace Elect.Web.Swagger.Utils.SwaggerGenOptionsUtils
{
    public static class SwaggerGenOptionsExtensions
    {
        public static SwaggerGenOptions AddElectSwaggerGenOptions([NotNull] this SwaggerGenOptions swaggerGenOptions, [NotNull] ElectSwaggerOptions configuration)
        {
            return SwaggerGenOptionsHelper.AddElectSwaggerGenOptions(swaggerGenOptions, configuration);
        }

        public static SwaggerGenOptions AddElectSwaggerGenOptions([NotNull] this SwaggerGenOptions swaggerGenOptions, [NotNull] Action<ElectSwaggerOptions> configuration)
        {
            return SwaggerGenOptionsHelper.AddElectSwaggerGenOptions(swaggerGenOptions, configuration);
        }

        /// <summary>
        ///     Includes the XML comment file if it has the same name as the assembly, a .xml file
        ///     extension and exists in the same directory as the assembly.
        /// </summary>
        /// <param name="swaggerGenOptions"> The Swagger options. </param>
        /// <param name="assembly">          The assembly. </param>
        /// <returns>
        ///     <c> true </c> if the comment file exists and was added, otherwise <c> false </c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"> options or assembly. </exception>
        public static SwaggerGenOptions IncludeXmlCommentsIfExists(this SwaggerGenOptions swaggerGenOptions, Assembly assembly)
        {
            return SwaggerGenOptionsHelper.IncludeXmlCommentsIfExists(swaggerGenOptions, assembly);
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
            return SwaggerGenOptionsHelper.IncludeXmlCommentsIfExists(options, filePath);
        }
    }
}