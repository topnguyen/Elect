namespace Elect.Web.Swagger
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectSwagger(this IServiceCollection services, IConfiguration configuration,
            string sectionName = "ElectSwagger")
        {
            var electSwaggerOptions = GetOptions(configuration, sectionName);
            return services.AddElectSwagger(electSwaggerOptions);
        }
        public static ElectSwaggerOptions GetOptions(IConfiguration configuration,
            string sectionName = "ElectSwagger")
        {
            var electSwaggerOptions = new ElectSwaggerOptions();
            electSwaggerOptions.IsEnable =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electSwaggerOptions.IsEnable)}");
            electSwaggerOptions.SwaggerRoutePrefix =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.SwaggerRoutePrefix)}");
            electSwaggerOptions.SwaggerName =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.SwaggerName)}");
            electSwaggerOptions.Url =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.Url)}");
            electSwaggerOptions.JsonViewerUrl =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.JsonViewerUrl)}");
            electSwaggerOptions.Title =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.Title)}");
            electSwaggerOptions.Version =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.Version)}");
            electSwaggerOptions.AccessKey =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.AccessKey)}");
            electSwaggerOptions.UnAuthorizeMessage =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.UnAuthorizeMessage)}");
            electSwaggerOptions.AuthTokenType =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.AuthTokenType)}");
            electSwaggerOptions.IsFullSchemaForType =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electSwaggerOptions.IsFullSchemaForType)}");
            electSwaggerOptions.AuthorName =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.AuthorName)}");
            electSwaggerOptions.AuthorEmail =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.AuthorEmail)}");
            electSwaggerOptions.AuthorWebsite =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electSwaggerOptions.AuthorWebsite)}");
            electSwaggerOptions.GlobalParameters =
                configuration.GetListValueByEnv<OpenApiParameter>(
                    $"{sectionName}:{nameof(electSwaggerOptions.GlobalParameters)}");
            return electSwaggerOptions;
        }
        public static IServiceCollection AddElectSwagger(this IServiceCollection services)
        {
            return services.AddElectSwagger(_ => { });
        }
        public static IServiceCollection AddElectSwagger(this IServiceCollection services,
            [NotNull] ElectSwaggerOptions configuration)
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
                _.AuthorName = configuration.AuthorName;
                _.AuthorEmail = configuration.AuthorEmail;
                _.AuthorWebsite = configuration.AuthorWebsite;
                _.GlobalParameters = configuration.GlobalParameters;
                _.ExtendOptions = configuration.ExtendOptions;
            });
        }
        public static IServiceCollection AddElectSwagger(this IServiceCollection services,
            [NotNull] Action<ElectSwaggerOptions> configuration)
        {
            services.Configure(configuration);
            var options = configuration.GetValue();
            if (!options.IsEnable)
            {
                return services;
            }
            // Update File Content base on Configuration
            SwaggerHelper.UpdateApiDocFileContent(options.Title, SwaggerHelper.GetSwaggerEndpoint(options),
                options.AuthTokenType, options.JsonViewerUrl);
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
