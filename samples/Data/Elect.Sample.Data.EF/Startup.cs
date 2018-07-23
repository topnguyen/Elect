using System;
using Elect.Web.Middlewares.InitialMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Data.EF
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Elect.Data.EF.Interfaces.DbContext.IDbContext, Elect.Sample.Data.EF.Services.DbContext>();
            services.AddScoped(typeof(Elect.Sample.Data.EF.Interfaces.IRepository<>), typeof(Elect.Sample.Data.EF.Services.Repository<>));
            services.AddScoped<Elect.Sample.Data.EF.Interfaces.IUnitOfWork, Elect.Sample.Data.EF.Services.UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseElectInitial((serviceProvider) =>
            {
                var dbContext = serviceProvider.GetService<Elect.Data.EF.Interfaces.DbContext.IDbContext>();
                
                Console.WriteLine("Initial Elect");
            });
        }
    }
}