using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.Swagger.IDocumentFilters;
using Elect.Web.Swagger.Models;
using Elect.Web.Swagger.Utils;
using Elect.Web.Swagger.Utils.SwaggerGenOptionsUtils;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace Elect.Web.Swagger
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectSwagger(this IServiceCollection services)
        {
            return services.AddElectSwagger(_ => { });
        }

        public static IServiceCollection AddElectSwagger(this IServiceCollection services, [NotNull] ElectSwaggerOptions configuration)
        {
            return services.AddElectSwagger(_ =>
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

        public static IServiceCollection AddElectSwagger(this IServiceCollection services, [NotNull] Action<ElectSwaggerOptions> configuration)
        {
            services.Configure(configuration);

            var options = configuration.GetValue();

            // Update File Content base on Configuration

            SwaggerHelper.UpdateApiDocFileContent(options.Title, options.SwaggerRoutePrefix, options.AuthTokenType, options.JsonViewerUrl);

            SwaggerHelper.UpdateApiJsonViewerFileContent(options.Title);

            // Config Swagger

            services.AddSwaggerGen(swaggerGenOptions =>
            {
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
                swaggerGenOptions.IncludeXmlCommentsIfExists(Assembly.GetCallingAssembly());

                // Filers
                swaggerGenOptions.DocumentFilter<HideInApiDocDocumentFilter>();

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
                swaggerGenOptions.OrderActionsBy(apiDesc => $"[{apiDesc.HttpMethod}]{apiDesc.RelativePath}");
            });

            return services;
        }
    }
}