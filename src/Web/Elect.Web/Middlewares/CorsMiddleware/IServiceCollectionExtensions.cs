#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 21/03/2018 6:39:42 PM </Created>
//         <Key> 207c8dd7-3b5b-4c5f-b960-1f0e97b24630 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.Middlewares.CorsMiddleware.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Elect.Web.Middlewares.CorsMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectCors(this IServiceCollection services)
        {
            return services.AddElectCors(_ => { });
        }

        public static IServiceCollection AddElectCors(this IServiceCollection services, [NotNull] ElectCorsOptions configuration)
        {
            return services.AddElectCors(_ =>
            {
                _.PolicyName = configuration.PolicyName;
                _.AccessControlAllowOrigins = configuration.AccessControlAllowOrigins;
                _.AccessControlAllowHeaders = configuration.AccessControlAllowHeaders;
                _.AccessControlAllowMethods = configuration.AccessControlAllowMethods;
            });
        }

        public static IServiceCollection AddElectCors(this IServiceCollection services, [NotNull]Action<ElectCorsOptions> configuration)
        {
            services.Configure(configuration);

            var options = configuration.GetValue();

            var corsBuilder = new CorsPolicyBuilder();

            corsBuilder.WithOrigins(options.AccessControlAllowOrigins.ToArray());

            corsBuilder.WithHeaders(options.AccessControlAllowHeaders.ToArray());

            corsBuilder.WithMethods(options.AccessControlAllowMethods.ToArray());

            corsBuilder.AllowCredentials();

            services.AddCors(config =>
            {
                config.AddPolicy(options.PolicyName, corsBuilder.Build());
            });

            services.Configure<MvcOptions>(config =>
            {
                config.Filters.Add(new CorsAuthorizationFilterFactory(options.PolicyName));
            });

            return services;
        }
    }
}