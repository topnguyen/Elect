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
