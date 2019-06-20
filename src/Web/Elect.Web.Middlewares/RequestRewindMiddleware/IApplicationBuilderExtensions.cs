#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 21/03/2018 8:04:13 PM </Created>
//         <Key> 07b75768-5c2b-45b2-9317-263b48f7c615 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Threading.Tasks;

namespace Elect.Web.Middlewares.RequestRewindMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Enable Rewind help to get Request Body content. 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElectRequestRewind(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ElectRequestRewindMiddleware>();
        }

        public class ElectRequestRewindMiddleware
        {
            private readonly RequestDelegate _next;

            public ElectRequestRewindMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public Task Invoke(HttpContext context)
            {
                // Allows using several time the stream in ASP.Net Core. Enable Rewind help to get
                // Request Body content.
                context.Request.EnableRewind();

                return _next(context);
            }
        }
    }
}