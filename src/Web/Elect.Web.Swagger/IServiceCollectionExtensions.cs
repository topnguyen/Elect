#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 01/04/2018 11:37:27 PM </Created>
//         <Key> 222a77d7-acb5-4190-961e-4eb51d20b734 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.Swagger.Models;
using Elect.Web.Swagger.Utils;
using Elect.Web.Swagger.Utils.SwaggerGenOptionsUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                _.IsEnable = configuration.IsEnable;
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

            if (!options.IsEnable)
            {
                return services;
            }

            // Update File Content base on Configuration

            SwaggerHelper.UpdateApiDocFileContent(options.Title, SwaggerHelper.GetSwaggerEndpoint(options), options.AuthTokenType, options.JsonViewerUrl);

            SwaggerHelper.UpdateApiJsonViewerFileContent(options.Title);

            // Config Swagger
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.AddElectSwaggerGenOptions(configuration);

                options.ExtendOptions?.Invoke(swaggerGenOptions, options);
            });

            return services;
        }
    }
}