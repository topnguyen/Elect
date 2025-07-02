namespace Elect.Sample.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseElectReserveProxy();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
