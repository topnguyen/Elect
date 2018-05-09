#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 21/03/2018 7:45:21 PM </Created>
//         <Key> 645801bd-8150-42e8-ac55-6248c218e2c4 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.DictionaryUtils;
using Elect.Web.Middlewares.ServerInfoMiddleware.Models;
using Elect.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Elect.Web.Middlewares.ServerInfoMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectServerInfo(this IApplicationBuilder app)
        {
            app.UseMiddleware<ElectServerInfoMiddleware>();

            return app;
        }

        public class ElectServerInfoMiddleware
        {
            private readonly RequestDelegate _next;

            private readonly ElectServerInfoOptions _options;

            public ElectServerInfoMiddleware(RequestDelegate next, IOptions<ElectServerInfoOptions> configuration)
            {
                _next = next;
                _options = configuration.Value;
            }

            public Task Invoke(HttpContext context)
            {
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;

                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.Server, _options.ServerName);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XPoweredBy, _options.PoweredBy);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorName, _options.AuthorName);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorWebsite, _options.AuthorWebsite);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorEmail, _options.AuthorEmail);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorEmail, _options.Version);

                    return Task.CompletedTask;
                }, context);

                return _next(context);
            }
        }
    }
}