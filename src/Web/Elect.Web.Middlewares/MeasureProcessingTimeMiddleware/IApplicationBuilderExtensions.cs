#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 21/03/2018 7:59:50 PM </Created>
//         <Key> 27673c36-026d-4372-8304-4126d09c6c73 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Elect.Web.Middlewares.MeasureProcessingTimeMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Response information about executed time 
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseElectMeasureProcessingTime(this IApplicationBuilder app)
        {
            app.UseMiddleware<ElectMeasureProcessingTimeMiddleware>();

            return app;
        }

        public class ElectMeasureProcessingTimeMiddleware
        {
            private readonly RequestDelegate _next;

            public ElectMeasureProcessingTimeMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public Task Invoke(HttpContext context)
            {
                var watch = new Stopwatch();

                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;

                    watch.Stop();

                    var elapsedMilliseconds = watch.ElapsedMilliseconds.ToString("N");

                    httpContext.Response.Headers.Add(HeaderKey.XProcessingTimeMilliseconds, elapsedMilliseconds);

                    return Task.CompletedTask;
                }, context);

                watch.Start();

                return _next(context);
            }
        }
    }
}