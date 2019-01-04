using Elect.Web.Middlewares.CorsMiddleware;
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
            services.AddElectCors();

            services.AddElectSwagger();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseElectCors();

            app.UseElectSwagger();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}