﻿using Elect.Web.Middlewares.CorsMiddleware;
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

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElectCors();

            app.UseStaticFiles();

            app.UseElectSwagger();

            app.UseMvcWithDefaultRoute();
        }
    }
}