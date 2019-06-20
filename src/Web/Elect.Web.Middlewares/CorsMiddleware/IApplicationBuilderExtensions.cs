#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
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

using Elect.Web.Middlewares.CorsMiddleware.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Elect.Web.Middlewares.CorsMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectCors(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectCorsOptions>>().Value;

            app.UseCors(options.PolicyName);

            return app;
        }
    }
}