using Elect.Core.StringUtils;
using Elect.DI;
using Elect.DI.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.DI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Register Dependency Injection
            services.AddElectDI();

            // Optional - Print out Registered Service with Information
            services.PrintServiceAddedToConsole();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        public interface ISampleService
        {
            string GetRandomString();
        }

        [ScopedDependency(ServiceType = typeof(ISampleService))]
        public class SampleService : ISampleService
        {
            public string GetRandomString()
            {
                return StringHelper.Generate(10);
            }
        }
    }
}