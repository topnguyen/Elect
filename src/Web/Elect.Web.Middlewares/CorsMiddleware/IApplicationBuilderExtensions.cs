#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 21/03/2018 6:49:24 PM </Created>
//         <Key> 35f29248-f5c2-4f62-a326-71ffb7cee678 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.DictionaryUtils;
using Elect.Web.Middlewares.CorsMiddleware.Models;
using Elect.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Elect.Web.Middlewares.CorsMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectCors(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectCorsOptions>>().Value;

            app.UseCors(options.PolicyName);

            app.UseMiddleware<ElectCorsMiddleware>();

            return app;
        }

        public class ElectCorsMiddleware
        {
            private readonly RequestDelegate _next;

            private readonly ElectCorsOptions _options;

            public ElectCorsMiddleware(RequestDelegate next, IOptions<ElectCorsOptions> configuration)
            {
                _next = next;
                _options = configuration.Value;
            }

            public Task Invoke(HttpContext context)
            {
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;

                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.AccessControlAllowOrigin, string.Join(", ", _options.AccessControlAllowOrigins));

                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.AccessControlAllowHeaders, string.Join(", ", _options.AccessControlAllowHeaders));

                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.AccessControlAllowMethods, string.Join(", ", _options.AccessControlAllowMethods));

                    return Task.CompletedTask;
                }, context);

                return _next(context);
            }
        }
    }
}