#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 24/03/2018 4:58:36 PM </Created>
//         <Key> 7a17c7d9-577f-40f6-8548-e9addfd7ef4f </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.DataTable.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Elect.Web.DataTable
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectDataTable(this IServiceCollection services)
        {
            return services.AddElectDataTable(_ => { });
        }

        public static IServiceCollection AddElectDataTable(this IServiceCollection services, [NotNull]ElectDataTableOptions configure)
        {
            return services.AddElectDataTable(_ =>
            {
                _.DateTimeTimeZone = configure.DateTimeTimeZone;
                _.DateFormat = configure.DateFormat;
                _.DateTimeFormat = configure.DateTimeFormat;
                _.RequestDateTimeFormatType = configure.RequestDateTimeFormatType;
                _.SharedResourceType = configure.SharedResourceType;
            });
        }

        public static IServiceCollection AddElectDataTable(this IServiceCollection services, [NotNull] Action<ElectDataTableOptions> configure)
        {
            services.Configure(configure);

            if (ElectDataTableOptions.Instance == null)
            {
                ElectDataTableOptions.Instance = configure.GetValue();
            }

            // Add DataTable Model Binder
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddDataTableModelBinder();
            });

            return services;
        }
    }
}