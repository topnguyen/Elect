namespace Elect.Sample.Web.DataTable
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElectDataTable();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
