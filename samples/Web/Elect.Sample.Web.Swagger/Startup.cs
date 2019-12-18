using Elect.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Web.Swagger
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
//            services.AddElectCors();

            services.AddElectSwagger();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
//            app.UseElectCors();

            app.UseStaticFiles();

            app.UseElectSwagger();

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