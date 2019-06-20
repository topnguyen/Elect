#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 19/03/2018 8:11:01 PM </Created>
//         <Key> 673a5c51-e807-491d-83b5-979c44e5c795 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Notification.Esms.Interfaces;
using Elect.Notification.Esms.Models;
using Elect.Notification.Esms.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Elect.Notification.Esms
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectNotificationEsms(this IServiceCollection services, [NotNull]ElectEsmsOptions configuration)
        {
            return services.AddElectNotificationEsms(_ =>
            {
                _.ApiKey = configuration.ApiKey;
                _.ApiSecret = configuration.ApiSecret;
            });
        }

        public static IServiceCollection AddElectNotificationEsms(this IServiceCollection services, [NotNull]Action<ElectEsmsOptions> configuration)
        {
            services.Configure(configuration);

            services.TryAddScoped<IElectEsmsClient, ElectEsmsClient>();

            return services;
        }
    }
}