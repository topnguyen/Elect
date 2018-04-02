#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 01/04/2018 11:30:59 PM </Created>
//         <Key> d83cde9a-dbf7-44a1-a611-2d5472c7f4c0 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Web.Middlewares.HttpContextMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();

            HttpContext.Configure(httpContextAccessor);

            return app;
        }
    }
}