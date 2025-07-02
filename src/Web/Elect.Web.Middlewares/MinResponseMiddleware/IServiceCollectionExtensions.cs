namespace Elect.Web.Middlewares.MinResponseMiddleware
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        ///     Mini and compress HTML, XML, CSS, JS 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddElectMinResponse(this IServiceCollection services)
        {
            services
                // Global
                .AddWebMarkupMin(options =>
                {
                    options.MaxResponseSize = -1; // Allow min all size
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.AllowCompressionInDevelopmentEnvironment = true;
                    options.DisablePoweredByHttpHeaders = true;
                    options.DisableCompression = true;
                    options.DisableMinification = false;
                })
                // HTML, CSS, JS Mini
                .AddHtmlMinification(options =>
                {
                    options.MinificationSettings.MinifyEmbeddedCssCode = true;
                    options.MinificationSettings.RemoveRedundantAttributes = false;
                    options.MinificationSettings.RemoveHttpProtocolFromAttributes = false;
                    options.MinificationSettings.RemoveHttpsProtocolFromAttributes = false;
                    options.MinificationSettings.RemoveOptionalEndTags = false; // Important, Don't enable/true this
                    options.CssMinifierFactory = new NUglifyCssMinifierFactory();
                    options.JsMinifierFactory = new NUglifyJsMinifierFactory();
                })
                // XML Mini
                .AddXmlMinification(options =>
                {
                    options.MinificationSettings.CollapseTagsWithoutContent = true;
                })
                // Compress
                .AddHttpCompression(options =>
                {
                    options.CompressorFactories = new List<ICompressorFactory>
                    {
                        new DeflateCompressorFactory(new DeflateCompressionSettings
                        {
                            Level = CompressionLevel.Fastest
                        }),
                        new GZipCompressorFactory(new GZipCompressionSettings
                        {
                            Level = CompressionLevel.Fastest
                        })
                    };
                });
            return services;
        }
    }
}
