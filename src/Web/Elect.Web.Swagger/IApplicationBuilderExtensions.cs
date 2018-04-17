#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 01/04/2018 11:37:54 PM </Created>
//         <Key> c5b6fb31-1b9e-41b0-92e7-c0748ff94744 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO;
using Elect.Web.Swagger.Models;
using Elect.Web.Swagger.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Elect.Web.Swagger
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectSwagger(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectSwaggerOptions>>().Value;

            app.UseSwagger(c =>
            {
                c.RouteTemplate = options.SwaggerRoutePrefix + "/{documentName}/" + options.SwaggerName;

                c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {
                // RoutePrefix of Swagger UI must not start by / or ~

                c.RoutePrefix = options.SwaggerRoutePrefix;

                c.SwaggerEndpoint(SwaggerHelper.GetSwaggerEndpoint(options), options.AccessKey);
            });

            // Path and GZip for Statics Content
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(PathHelper.GetFullPath(ElectSwaggerConstants.AssetFolderName)),

                RequestPath = ElectSwaggerConstants.AssetsUrl,

                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();

                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        MaxAge = new TimeSpan(365, 0, 0, 0)
                    };
                }
            });

            app.UseMiddleware<ElectSwaggerMiddleware>();

            return app;
        }

        /// <summary>
        ///     Keep swagger access middleware before UseSwagger and UseSwaggerUI to wrap a request 
        /// </summary>
        public class ElectSwaggerMiddleware
        {
            private readonly RequestDelegate _next;

            private readonly ElectSwaggerOptions _options;

            public ElectSwaggerMiddleware(RequestDelegate next, IOptions<ElectSwaggerOptions> configuration)
            {
                _next = next;

                _options = configuration.Value;
            }

            public async Task Invoke(HttpContext context)
            {
                // Check is request to Swagger
                if (!SwaggerHelper.IsAccessSwagger(context, _options))
                {
                    await _next.Invoke(context).ConfigureAwait(true);

                    return;
                }

                // Set cookie if need
                string requestAccessKey = context.Request.Query[ElectSwaggerConstants.AccessKeyName];

                if (!string.IsNullOrWhiteSpace(requestAccessKey) && context.Request.Cookies[ElectSwaggerConstants.AccessKeyName] != requestAccessKey)
                {
                    SetCookie(context, ElectSwaggerConstants.CookieAccessKeyName, requestAccessKey);
                }

                // Check Permission
                bool isCanAccess = SwaggerHelper.IsCanAccessSwagger(context, _options.AccessKey);

                if (!isCanAccess)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    await context.Response.WriteAsync(_options.UnAuthorizeMessage).ConfigureAwait(true);

                    return;
                }

                if (SwaggerHelper.IsAccessJsonViewer(context, _options))
                {
                    var jsonViewerContentResult = SwaggerHelper.GetApiJsonViewerHtml();

                    context.Response.ContentType = jsonViewerContentResult.ContentType;

                    context.Response.StatusCode = jsonViewerContentResult.StatusCode ?? StatusCodes.Status200OK;

                    await context.Response.WriteAsync(jsonViewerContentResult.Content, Encoding.UTF8).ConfigureAwait(true);
                    return;
                }

                if (SwaggerHelper.IsAccessUI(context, _options))
                {
                    var apiDocContentResult = SwaggerHelper.GetApiDocHtml();

                    context.Response.ContentType = apiDocContentResult.ContentType;

                    context.Response.StatusCode = apiDocContentResult.StatusCode ?? StatusCodes.Status200OK;

                    await context.Response.WriteAsync(apiDocContentResult.Content, Encoding.UTF8).ConfigureAwait(true);

                    return;
                }

                // Next middleware is swagger endpoint => generate document by swagger

                await _next.Invoke(context).ConfigureAwait(true);
            }

            private static void SetCookie(HttpContext context, string key, string value)
            {
                context.Response.Cookies.Append(key, value, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false // allow transmit via http and https
                });
            }
        }
    }
}