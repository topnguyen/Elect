#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 21/03/2018 7:46:52 PM </Created>
//         <Key> 24ae010d-0856-46dc-92ef-49e3e1e60e9c </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Web.Middlewares.ServerInfoMiddleware.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Elect.Web.Middlewares.ServerInfoMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectServerInfo(this IServiceCollection services)
        {
            return services.AddElectServerInfo(_ => { });
        }

        public static IServiceCollection AddElectServerInfo(this IServiceCollection services, [NotNull] ElectServerInfoOptions configuration)
        {
            return services.AddElectServerInfo(_ =>
            {
                _.ServerName = configuration.ServerName;
                _.PoweredBy = configuration.PoweredBy;
                _.AuthorName = configuration.AuthorName;
                _.AuthorWebsite = configuration.AuthorWebsite;
                _.AuthorEmail = configuration.AuthorEmail;
            });
        }

        public static IServiceCollection AddElectServerInfo(this IServiceCollection services, [NotNull]Action<ElectServerInfoOptions> configuration)
        {
            services.Configure(configuration);

            return services;
        }
    }
}