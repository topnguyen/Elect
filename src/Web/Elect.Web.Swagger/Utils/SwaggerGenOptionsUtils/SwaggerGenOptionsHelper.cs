#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SwaggerGenOptionsHelper.cs </Name>
//         <Created> 10/04/2018 5:10:12 PM </Created>
//         <Key> 780420bf-68dd-4db8-a520-665fc6429ea3 </Key>
//     </File>
//     <Summary>
//         SwaggerGenOptionsHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.Swagger.IDocumentFilters;
using Elect.Web.Swagger.IOperationFilter;
using Elect.Web.Swagger.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Web.Swagger.Utils.SwaggerGenOptionsUtils
{
    public class SwaggerGenOptionsHelper
    {
        public static SwaggerGenOptions AddElectSwaggerGenOptions([NotNull] SwaggerGenOptions swaggerGenOptions,
            [NotNull] ElectSwaggerOptions configuration)
        {
            return AddElectSwaggerGenOptions(swaggerGenOptions, _ =>
            {
                _.SwaggerRoutePrefix = configuration.SwaggerRoutePrefix;
                _.SwaggerName = configuration.SwaggerName;
                _.Url = configuration.Url;
                _.JsonViewerUrl = configuration.JsonViewerUrl;
                _.Title = configuration.Title;
                _.Version = configuration.Version;
                _.AccessKey = configuration.AccessKey;
                _.UnAuthorizeMessage = configuration.UnAuthorizeMessage;
                _.AuthTokenType = configuration.AuthTokenType;
                _.IsFullSchemaForType = configuration.IsFullSchemaForType;
                _.IsDescribeAllEnumsAsString = configuration.IsDescribeAllEnumsAsString;
                _.IsDescribeAllParametersInCamelCase = configuration.IsDescribeAllParametersInCamelCase;
                _.AuthorName = configuration.AuthorName;
                _.AuthorEmail = configuration.AuthorEmail;
                _.AuthorWebsite = configuration.AuthorWebsite;
            });
        }

        public static SwaggerGenOptions AddElectSwaggerGenOptions([NotNull] SwaggerGenOptions swaggerGenOptions,
            [NotNull] Action<ElectSwaggerOptions> configuration)
        {
            var options = configuration.GetValue();

            // Doc Info
            
            swaggerGenOptions.SwaggerDoc(options.Version, new Info
            {
                Title = options.Title,
                Version = options.Version,
                Contact = !string.IsNullOrWhiteSpace(options.AuthorName)
                          && !string.IsNullOrWhiteSpace(options.AuthorWebsite)
                          && !string.IsNullOrWhiteSpace(options.AuthorEmail)
                    ? new Contact
                    {
                        Name = options.AuthorName,
                        Url = options.AuthorWebsite,
                        Email = options.AuthorEmail
                    }
                    : null
            });

            // XML
            
            IncludeXmlCommentsIfExists(swaggerGenOptions, Assembly.GetEntryAssembly());

            // Operation Filters
            
            swaggerGenOptions.OperationFilter<ApiDescriptionPropertiesOperationFilter>();

            swaggerGenOptions.OperationFilter<ApiDocGroupOperationFilter>();

            swaggerGenOptions.OperationFilter<GlobalParameterOperationFilter>();

            swaggerGenOptions.OperationFilter<ParameterOperationFilter>();

            // Document Filters
            
            swaggerGenOptions.DocumentFilter<ShowHideInApiDocDocumentFilter>();
            
            swaggerGenOptions.IgnoreObsoleteProperties();

            swaggerGenOptions.IgnoreObsoleteActions();

            // Type / Properties
            
            if (options.IsFullSchemaForType)
            {
                swaggerGenOptions.CustomSchemaIds(type => type.FullName);
            }

            if (options.IsDescribeAllParametersInCamelCase)
            {
                swaggerGenOptions.DescribeAllParametersInCamelCase();
            }

            if (options.IsDescribeAllEnumsAsString)
            {
                swaggerGenOptions.DescribeAllEnumsAsStrings();

                if (options.IsDescribeAllParametersInCamelCase)
                {
                    swaggerGenOptions.DescribeStringEnumsInCamelCase();
                }
            }

            // Order
            
            swaggerGenOptions.OrderActionsBy((apiDesc) =>
            {
                var apiDisplayName = apiDesc.ActionDescriptor.DisplayName;

                if (!apiDesc.Properties.TryGetValue(nameof(Operation.Summary), out var summary))
                {
                    return apiDisplayName;
                }
                
                if (!string.IsNullOrWhiteSpace(summary?.ToString()))
                {
                    apiDisplayName = summary.ToString();
                }

                return apiDisplayName;
            });

            return swaggerGenOptions;
        }

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
        public static SwaggerGenOptions IncludeXmlCommentsIfExists(SwaggerGenOptions options, Assembly assembly)
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

            if (IncludeXmlCommentsIfExists(options, filePath) || string.IsNullOrWhiteSpace(assembly.CodeBase))
            {
                return options;
            }

            filePath = Path.ChangeExtension(new Uri(assembly.CodeBase).AbsolutePath, ".xml");

            IncludeXmlCommentsIfExists(options, filePath);

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
        public static bool IncludeXmlCommentsIfExists(SwaggerGenOptions options, string filePath)
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