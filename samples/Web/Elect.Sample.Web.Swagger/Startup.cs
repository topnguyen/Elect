namespace Elect.Sample.Web.Swagger
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddElectCors();
            services.AddElectSwagger();
            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
