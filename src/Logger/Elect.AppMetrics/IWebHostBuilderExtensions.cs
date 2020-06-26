using System;
using System.Collections.Generic;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.AspNetCore.Endpoints;
using App.Metrics.Formatters.Prometheus;
using Elect.AppMetrics.Models;
using Elect.Core.ActionUtils;
using Elect.Core.ConfigUtils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder,
            Action<ElectAppMetricsOptions> configuration)
        {
            return webHostBuilder.UseElectAppMetrics(configuration.GetValue());
        }

        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="configuration"></param>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder,
            ElectAppMetricsOptions configuration)
        {
            _configuration = configuration;

            return webHostBuilder.UseElectAppMetrics(string.Empty);
        }

        /// <summary>
        ///     Add App Metrics automatically by Elect
        /// </summary>
        /// <param name="webHostBuilder"></param>
        /// <param name="sectionName">Section Name/Key in the Configuration File</param>
        /// <returns></returns>
        public static IWebHostBuilder UseElectAppMetrics(this IWebHostBuilder webHostBuilder,
            string sectionName = "ElectAppMetrics")
        {
            if (_isInitialized)
            {
                return webHostBuilder;
            }

            // Config App Metric

            webHostBuilder = webHostBuilder.ConfigureMetricsWithDefaults((context, builder) =>
            {
                var metricsOptions = _configuration;

                if (!string.IsNullOrWhiteSpace(sectionName))
                {
                    metricsOptions = GetElectAppMetricsOptions(context.Configuration, sectionName);
                }

                // Config

                builder.Configuration.Configure(cfg =>
                    {
                        // Check App Metric Enable/Disable
                        cfg.Enabled = metricsOptions.IsEnable;

                        if (!metricsOptions.IsEnable)
                        {
                            return;
                        }

                        // Mask as finish Initial
                        _isInitialized = true;

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
                        o.InfluxDb.Database = metricsOptions.InfluxDatabase;
                        o.InfluxDb.BaseUri = new Uri(metricsOptions.InfluxEndpoint);
                        o.InfluxDb.UserName = metricsOptions.InfluxUserName;
                        o.InfluxDb.Password = metricsOptions.InfluxPassword;
                        o.FlushInterval = TimeSpan.FromSeconds(metricsOptions.InfluxInterval);
                        o.InfluxDb.CreateDataBaseIfNotExists = true;
                    });
                }
            });

            // Config Enable Endpoint, Formatter

            webHostBuilder = webHostBuilder.UseMetrics((context, options) =>
            {
                var metricsOptions = _configuration;

                if (!string.IsNullOrWhiteSpace(sectionName))
                {
                    metricsOptions = GetElectAppMetricsOptions(context.Configuration, sectionName);
                }

                // Check App Metric Enable/Disable
                if (!metricsOptions.IsEnable)
                {
                    return;
                }

                // Mask as finish Initial
                _isInitialized = true;

                options.EndpointOptions = endpointOptions =>
                {
                    // Endpoint Enable/Disable

                    endpointOptions.MetricsEndpointEnabled = metricsOptions.IsEnableMetricsEndpoint;
                    endpointOptions.MetricsTextEndpointEnabled = metricsOptions.IsEnableMetricsTextEndpoint;
                    endpointOptions.EnvironmentInfoEndpointEnabled = metricsOptions.IsEnableEnvEndpoint;

                    // Prometheus Formatter

                    if (!metricsOptions.IsPrometheusEnabled)
                    {
                        return;
                    }

                    switch (metricsOptions.PrometheusFormatter)
                    {
                        case ElectPrometheusFormatter.Protobuf:
                        {
                            endpointOptions.MetricsEndpointOutputFormatter =
                                new MetricsPrometheusProtobufOutputFormatter();
                            break;
                        }

                        case ElectPrometheusFormatter.Text:
                        {
                            endpointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
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

            // Config Endpoint

            webHostBuilder = webHostBuilder.ConfigureServices((context, services) =>
            {
                var metricsOptions = _configuration;

                if (!string.IsNullOrWhiteSpace(sectionName))
                {
                    metricsOptions = GetElectAppMetricsOptions(context.Configuration, sectionName);
                }

                // Check App Metric Enable/Disable
                if (!metricsOptions.IsEnable)
                {
                    return;
                }

                // Mask as finish Initial
                _isInitialized = true;

                // Endpoint

                services.Configure<MetricsEndpointsHostingOptions>(hostingOptions =>
                {
                    hostingOptions.MetricsEndpoint = $"/{metricsOptions.MetricsEndpoint.Trim('/')}";
                    hostingOptions.MetricsTextEndpoint = $"/{metricsOptions.MetricsTextEndpoint.Trim('/')}";
                    hostingOptions.EnvironmentInfoEndpoint = $"/{metricsOptions.EnvEndpoint.Trim('/')}";
                });
            });

            return webHostBuilder;
        }

        private static ElectAppMetricsOptions GetElectAppMetricsOptions(IConfiguration configuration,
            string sectionName = "ElectAppMetrics")
        {
            var electAppMetricsOptions = new ElectAppMetricsOptions();

            electAppMetricsOptions.IsEnable =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electAppMetricsOptions.IsEnable)}");

            electAppMetricsOptions.IsEnableMetricsEndpoint =
                configuration.GetValueByEnv<bool>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.IsEnableMetricsEndpoint)}");

            electAppMetricsOptions.MetricsEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electAppMetricsOptions.MetricsEndpoint)}");

            electAppMetricsOptions.IsEnableMetricsTextEndpoint =
                configuration.GetValueByEnv<bool>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.IsEnableMetricsTextEndpoint)}");

            electAppMetricsOptions.MetricsTextEndpoint =
                configuration.GetValueByEnv<string>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.MetricsTextEndpoint)}");

            electAppMetricsOptions.IsEnableEnvEndpoint =
                configuration.GetValueByEnv<bool>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.IsEnableEnvEndpoint)}");

            electAppMetricsOptions.EnvEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electAppMetricsOptions.EnvEndpoint)}");

            electAppMetricsOptions.IsInfluxEnabled =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electAppMetricsOptions.IsInfluxEnabled)}");

            electAppMetricsOptions.InfluxEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electAppMetricsOptions.InfluxEndpoint)}");
            
            electAppMetricsOptions.InfluxDatabase =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electAppMetricsOptions.InfluxDatabase)}");
            
            electAppMetricsOptions.InfluxUserName =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electAppMetricsOptions.InfluxUserName)}");
            
            electAppMetricsOptions.InfluxPassword =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electAppMetricsOptions.InfluxPassword)}");
            
            electAppMetricsOptions.InfluxInterval =
                configuration.GetValueByEnv<int>($"{sectionName}:{nameof(electAppMetricsOptions.InfluxInterval)}");

            electAppMetricsOptions.IsPrometheusEnabled =
                configuration.GetValueByEnv<bool>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.IsPrometheusEnabled)}");

            electAppMetricsOptions.PrometheusFormatter =
                configuration.GetValueByEnv<ElectPrometheusFormatter>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.PrometheusFormatter)}");
            
            electAppMetricsOptions.Tags =
                configuration.GetValueByEnv<Dictionary<string, string>>(
                    $"{sectionName}:{nameof(electAppMetricsOptions.PrometheusFormatter)}");

            return electAppMetricsOptions;
        }
    }
}