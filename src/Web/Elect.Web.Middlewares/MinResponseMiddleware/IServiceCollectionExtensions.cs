#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 21/03/2018 8:06:58 PM </Created>
//         <Key> d6a913c0-195a-4b62-99b5-455bcf304151 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO.Compression;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNetCore5;
using WebMarkupMin.NUglify;

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