#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 20/03/2018 4:07:24 PM </Created>
//         <Key> 12a8128d-1882-4c29-9f65-daa20120c63a </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Location.Google.Interfaces;
using Elect.Location.Google.Models;
using Elect.Location.Google.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Elect.Location.Google
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectLocationGoogle(this IServiceCollection services)
        {
            return services.AddElectLocationGoogle(_ => { });
        }

        public static IServiceCollection AddElectLocationGoogle(this IServiceCollection services, [NotNull]ElectLocationGoogleOptions configuration)
        {
            return services.AddElectLocationGoogle(_ =>
            {
                _.GoogleApiKey = configuration.GoogleApiKey;
            });
        }

        public static IServiceCollection AddElectLocationGoogle(this IServiceCollection services, [NotNull] Action<ElectLocationGoogleOptions> configuration)
        {
            services.Configure(configuration);

            services.TryAddScoped<IElectGoogleClient, ElectGoogleClient>();

            return services;
        }
    }
}