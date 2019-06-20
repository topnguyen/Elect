#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 19/03/2018 9:12:37 PM </Created>
//         <Key> da7e0630-b4f3-487e-a550-6fc58157d848 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Elect.Notification.OneSignal
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectOneSignal(this IServiceCollection services, [NotNull]ElectOneSignalOptions configuration)
        {
            return services.AddElectOneSignal(_ =>
            {
                _.AuthKey = configuration.AuthKey;
                _.Apps = configuration.Apps;
            });
        }

        public static IServiceCollection AddElectOneSignal(this IServiceCollection services, [NotNull]Action<ElectOneSignalOptions> configuration)
        {
            services.Configure(configuration);

            services.TryAddScoped<IElectOneSignalApp, ElectOneSignalApp>();
            
            services.TryAddScoped<IElectOneSignalDevice, ElectOneSignalDevice>();

            services.TryAddScoped<IElectOneSignalNotification, ElectOneSignalNotification>();

            services.TryAddScoped<IElectOneSignalClient, ElectOneSignalClient>();

            return services;
        }
    }
}