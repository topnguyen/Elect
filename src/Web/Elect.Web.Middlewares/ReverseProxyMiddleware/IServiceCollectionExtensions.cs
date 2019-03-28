#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 28/03/2019 10:54:00 AM </Created>
//         <Key> 207c8dd7-3b5b-4c5f-b960-1f0e97b24630 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using Elect.Core.Attributes;
using Elect.Web.Middlewares.ReverseProxyMiddleware.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Web.Middlewares.ReverseProxyMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectReserveProxy(this IServiceCollection services, [NotNull] ElectReserveProxyOptions configuration)
        {
            return services.AddElectReserveProxy(_ =>
            {
                _.ServiceRootUrl = configuration.ServiceRootUrl;
            });
        }

        public static IServiceCollection AddElectReserveProxy(this IServiceCollection services, [NotNull] Action<ElectReserveProxyOptions> configuration)
        {
            services.Configure(configuration);

            return services;
        }
    }
}