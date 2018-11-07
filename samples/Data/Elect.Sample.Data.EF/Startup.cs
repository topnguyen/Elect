using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Data.EF
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Elect.Data.EF.Interfaces.DbContext.IDbContext, Services.DbContext>();
            services.AddScoped(typeof(Interfaces.IRepository<>), typeof(Services.Repository<>));
            services.AddScoped<Interfaces.IUnitOfWork, Services.UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}