using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Elect.AppMetrics.Models;
using Elect.Core.ActionUtils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.AppMetrics
{
    public static class IWebHostBuilderExtensions
    {
        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="configuration"></param>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder, ElectAppMetricsOptions configuration)
        {
            return webHostBuilder.UseElectAppMetrics(_ =>
            {
                _.IsEnable = configuration.IsEnable;
                _.IsInfluxEnabled = configuration.IsInfluxEnabled;
                _.InfluxEndpoint = configuration.InfluxEndpoint;
                _.InFluxDatabase = configuration.InFluxDatabase;
                _.InFluxInterval = configuration.InFluxInterval;
                _.IsPrometheusEnabled = configuration.IsPrometheusEnabled;
                _.PrometheusFormatter = configuration.PrometheusFormatter;
                _.Tags = configuration.Tags;
            });
        }
        
        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder, Action<ElectAppMetricsOptions> configuration)
        {
            // Service Config
            var metricsOptions = new ElectAppMetricsOptions();
            
            webHostBuilder.ConfigureServices((context, services) =>
            {
                services.Configure(configuration);

                metricsOptions = configuration.GetValue();
            });

            return webHostBuilder.ConfigureMetricsWithDefaults((context, builder) =>
                {
                    if (!metricsOptions.IsEnable)
                    {
                        return;
                    }

                    // Config
                    
                    builder.Configuration.Configure(cfg =>
                        {
                            var tags = metricsOptions.Tags;
                            
                            if (tags == null)
                            {
                                return;
                            }

                            tags.TryGetValue("app", out var app);
                            
                            tags.TryGetValue("env", out var env);
                            
                            tags.TryGetValue("server", out var server);
                            
                            cfg.AddAppTag(string.IsNullOrWhiteSpace(app) ? null : app);
                            
                            cfg.AddEnvTag(string.IsNullOrWhiteSpace(env) ? null : env);
                            
                            cfg.AddServerTag(string.IsNullOrWhiteSpace(server) ? null : server);
                            
                            foreach (var tag in tags)
                            {
                                if (!cfg.GlobalTags.ContainsKey(tag.Key))
                                {
                                    cfg.GlobalTags.Add(tag.Key, tag.Value);
                                }
                            }
                        }
                    );

                    // Influx
                    
                    if (metricsOptions.IsInfluxEnabled)
                    {
                        builder.Report.ToInfluxDb(o =>
                        {
                            o.InfluxDb.Database = metricsOptions.InFluxDatabase;
                            o.InfluxDb.BaseUri = new Uri(metricsOptions.InfluxEndpoint);
                            o.InfluxDb.CreateDataBaseIfNotExists = true;
                            o.FlushInterval = TimeSpan.FromSeconds(metricsOptions.InFluxInterval);
                        });
                    }
                })
                .UseMetricsWebTracking()
                .UseMetrics((context, options) =>
                {
                    // Prometheus

                    if (!metricsOptions.IsPrometheusEnabled)
                    {
                        return;
                    }

                    options.EndpointOptions = endpointOptions =>
                    {
                        switch (metricsOptions.PrometheusFormatter)
                        {
                            case ElectPrometheusFormatter.Protobuf:
                            {
                                endpointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                                break;
                            }
                            case ElectPrometheusFormatter.Text:
                            default:
                            {
                                endpointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                                break;
                            }
                        }
                    };
                });
        }
    }
}