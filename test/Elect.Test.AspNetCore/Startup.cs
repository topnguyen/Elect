using Elect.DI;
using Elect.Job.Hangfire;
using Elect.Test.AspNetCore.Data;
using Elect.Test.AspNetCore.Models;
using Elect.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Test.AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            // Elect Unit of Work
            services.AddElectDI();

            services.PrintServiceAddedToConsole();

            services.AddElectSwagger(_ =>
            {
                _.AuthorName = "Top Nguyen";
                _.AuthorEmail = "topnguyen92@gmail.com";
                _.AuthorWebsite = "http://topnguyen.net";
            });

            services.AddElectHangfire(_ =>
            {
                _.AccessKey = "topnguyen";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseElectHangfire();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseElectSwagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}