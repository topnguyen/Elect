#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 11/05/2020 7:36:50 PM </Created>
//         <Key> 27673c36-026d-4372-8304-4126d09c6c79 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Elect.Web.Middlewares.GCCollectMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Release memory if possible by GC Collection <br />
        ///     [NOTE] Keep this Middleware at the top of the pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <remarks>Keep this Middleware at the top of the pipeline</remarks>
        public static IApplicationBuilder UseGCCollectMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GCCollectMiddleware>();

            return app;
        }

        public class GCCollectMiddleware
        {
            private readonly RequestDelegate _next;

            public GCCollectMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                await _next(context);

                GC.Collect(2, GCCollectionMode.Forced, true);
                
                GC.WaitForPendingFinalizers();
            }
        }
    }
}