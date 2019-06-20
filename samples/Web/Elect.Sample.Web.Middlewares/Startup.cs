using System.Collections.Generic;
using Elect.Web.Middlewares.CorsMiddleware;
using Elect.Web.Middlewares.HttpContextMiddleware;
using Elect.Web.Middlewares.MeasureProcessingTimeMiddleware;
using Elect.Web.Middlewares.MinResponseMiddleware;
using Elect.Web.Middlewares.ServerInfoMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Web.Middlewares
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElectCors(_ =>
            {
                _.AllowOrigins = new List<string>
                {
                    "http://*.localhost:8000",
                    "http://localhost:8001",
                };
            });

            services.AddElectHttpContext();

            services.AddElectServerInfo();

            services.AddElectMinResponse();

            services.AddElectServerInfo(_ =>
            {
                _.AuthorName = "Top Nguyen";
                _.AuthorEmail = "topnguyen92@gmail.com";
                _.AuthorWebsite = "http://topnguyen.com";
                _.ServerName = "cloudflare-nginx";
                _.PoweredBy = "PHP/5.6.30";
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseElectCors();

            app.UseElectHttpContext();

            app.UseElectMeasureProcessingTime();

            app.UseElectMinResponse();

            app.UseElectServerInfo();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}