using System;
using System.Linq;
using Elect.Logger.Logging;
using Elect.Web.Middlewares.ReverseProxyMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddElectReserveProxy(_ =>
            //{
            //    _.ServiceRootUrl = "http://127.0.0.1:8080/";
            //    _.AfterReserveProxy = context =>
            //    {
            //        Console.WriteLine("URL " + context.Request.GetDisplayUrl());
            //    };
            //});

            services.AddElectLog(_ =>
            {
                _.BeforeLogResponse = (context, models) =>
                {
                    Console.WriteLine("Total Logs " + models.Count());

                    return models;
                };
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseElectReserveProxy();

            app.UseStaticFiles();

            app.UseElectLog();

            app.UseMvcWithDefaultRoute();
        }
    }
}