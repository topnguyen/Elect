using System;
using System.Linq;
using Elect.Logger.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElectLog(_ =>
            {
                _.BeforeLogResponse = (context, models) =>
                {
                    Console.WriteLine("Total Logs " + models.Count());

                    return models;
                };
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseElectReserveProxy();

            app.UseStaticFiles();

            app.UseElectLog();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}