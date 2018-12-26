#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 26/12/2018 11:37:54 PM </Created>
//         <Key> e8edd9c1-9bdd-4eae-adf6-54e5f6b169cf </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System.Text;
using System.Threading.Tasks;
using Elect.Logger.Logging.Models;
using Elect.Logger.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Elect.Logger.Logging
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<ElectLogMiddleware>();

            return app;
        }

        public class ElectLogMiddleware
        {
            private readonly RequestDelegate _next;

            private readonly ElectLogOptions _options;

            public ElectLogMiddleware(RequestDelegate next, IOptions<ElectLogOptions> configuration)
            {
                _next = next;

                _options = configuration.Value;
            }

            public async Task Invoke(HttpContext context)
            {
                if (!LogHelper.IsAccessLog(context, _options))
                {
                    await _next.Invoke(context).ConfigureAwait(true);

                    return;
                }

                // Set cookie if need
                string accessKey = context.Request.Query[ElectLogConstants.AccessKeyName];

                if (!string.IsNullOrWhiteSpace(accessKey) && context.Request.Cookies[ElectLogConstants.AccessKeyName] != accessKey)
                {
                    context.Response.Cookies.Append(ElectLogConstants.CookieAccessKeyName, accessKey, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false // allow transmit via http and https
                    });
                }

                // Check Permission
                bool isCanAccess = LogHelper.IsCanAccessLog(context, _options.AccessKey);

                if (!isCanAccess)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    await context.Response.WriteAsync(_options.UnAuthorizeMessage).ConfigureAwait(true);

                    return;
                }

                var logContentResult = LogHelper.GetLogContentResult(context, _options);

                context.Response.ContentType = logContentResult.ContentType;

                context.Response.StatusCode = logContentResult.StatusCode ?? StatusCodes.Status200OK;

                await context.Response.WriteAsync(logContentResult.Content, Encoding.UTF8).ConfigureAwait(true);
            }
        }
    }
}