#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 02/04/2018 12:37:29 AM </Created>
//         <Key> 5d244157-9dc8-40db-a727-8ea8161226e8 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Elect.Web.Middlewares.HttpContextMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectHttpContext(this IServiceCollection services)
        {
            if (services.All(x => x.ServiceType != typeof(IHttpContextAccessor)))
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }

            return services;
        }
    }
}