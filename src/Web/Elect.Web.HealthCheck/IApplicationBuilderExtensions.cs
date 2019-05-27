using System.Threading.Tasks;
using Elect.Web.HealthCheck.Models;
using Elect.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Elect.Web.HealthCheck
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectHealthCheck(this IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService<IOptions<ElectHealthCheckOptions>>().Value;

            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            
            var logger = loggerFactory?.CreateLogger<IApplicationBuilder>();

            if (!config.IsEnable)
            {
                return app;
            }

            logger?.Log(LogLevel.Information, "Health Check > Starting...");

            var healthCheckOptions = new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            };

            if (config.Options != null)
            {
                healthCheckOptions = config.Options(healthCheckOptions);
            }

            if (healthCheckOptions != null)
            {
                app.UseHealthChecks(config.Endpoint, healthCheckOptions);
            }
            else
            {
                app.UseHealthChecks(config.Endpoint);
            }

            logger?.Log(LogLevel.Information, "Health Check > Started!");

            return app;
        }

        public static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = ContentType.Json;

            Core.Constants.Formatting.JsonSerializerSettings.Converters.Add(new StringEnumConverter
                {AllowIntegerValues = false});

            var json = JsonConvert.SerializeObject(result, Core.Constants.Formatting.JsonSerializerSettings);

            return httpContext.Response.WriteAsync(json);
        }
    }
}