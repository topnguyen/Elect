using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Elect.AppMetrics.Models;
using Elect.Core.ActionUtils;
using Elect.Core.ConfigUtils;
using Microsoft.AspNetCore.Hosting;

namespace Elect.AppMetrics
{
    public static class IWebHostBuilderExtensions
    {
        private static bool _isInitialized;
        
        private static ElectAppMetricsOptions _configuration;
        
        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="configuration"></param>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder, Action<ElectAppMetricsOptions> configuration)
        {
            return webHostBuilder.UseElectAppMetrics(configuration.GetValue());
        }

        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="configuration"></param>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder, ElectAppMetricsOptions configuration)
        {
            _configuration = configuration;

            return webHostBuilder.UseElectAppMetrics(string.Empty);
        }
        
        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="configurationSectionKey">Section Name/Key in the Configuration File</param>
        /// <returns></returns>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder, string configurationSectionKey)
        {
            if (_isInitialized)
            {
                return webHostBuilder;
            }

            return webHostBuilder.ConfigureMetricsWithDefaults((context, builder) =>
                {
                    var metricsOptions = _configuration;
                    
                    if (!string.IsNullOrWhiteSpace(configurationSectionKey))
                    {
                        metricsOptions = context.Configuration.GetSection<ElectAppMetricsOptions>(configurationSectionKey);
                    }

                    if (!metricsOptions.IsEnable)
                    {
                        return;
                    }

                    _isInitialized = true;
                    
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
                    var metricsOptions = _configuration;
                    
                    if (!string.IsNullOrWhiteSpace(configurationSectionKey))
                    {
                        metricsOptions = context.Configuration.GetSection<ElectAppMetricsOptions>(configurationSectionKey);
                    }
                    
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