using System;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Jaeger.Models;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing.Contrib.NetCore.CoreFx;
using OpenTracing.Util;

namespace Elect.Jaeger
{
    public static class IServiceCollectionExtenions
    {
        public static IServiceCollection AddElectJaeger(this IServiceCollection services)
        {
            return services.AddElectJaeger(_ => { });
        }

        public static IServiceCollection AddElectJaeger(this IServiceCollection services,
            [NotNull] ElectJaegerOptions configure)
        {
            return services.AddElectJaeger(_ =>
            {
                _.IsEnable = configure.IsEnable;
                _.ServiceName = configure.ServiceName;
                _.Domain = configure.Domain;
                _.SamplerPort = configure.SamplerPort;
                _.ReporterPort = configure.ReporterPort;
                _.TracesPort = configure.TracesPort;
                _.AuthUsername = configure.AuthUsername;
                _.AuthPassword = configure.AuthPassword;
                _.AuthToken = configure.AuthToken;
                _.AfterSamplerConfig = configure.AfterSamplerConfig;
                _.AfterReporterConfig = configure.AfterReporterConfig;
                _.AfterGlobalConfig = configure.AfterGlobalConfig;
                _.AfterTracer = configure.AfterTracer;
            });
        }

        public static IServiceCollection AddElectJaeger(this IServiceCollection services,
            [NotNull] Action<ElectJaegerOptions> configure)
        {
            services.Configure(configure);

            var electJaegerOptions = configure.GetValue();

            if (!electJaegerOptions.IsEnable)
            {
                return services;
            }

            // Add open Tracing
            services.AddOpenTracing();
            
            // Add ITracer
            services.AddSingleton(serviceProvider =>
            {
                var loggerFactory = GetLoggerFactory(serviceProvider);

                // Sampler
                var samplerConfig = new Configuration.SamplerConfiguration(loggerFactory);
                samplerConfig.WithManagerHostPort($"{electJaegerOptions.Domain}:{electJaegerOptions.SamplerPort}");
                samplerConfig.WithType(ConstSampler.Type);
                samplerConfig = electJaegerOptions.AfterSamplerConfig?.Invoke(samplerConfig) ?? samplerConfig;

                // Reporter
                var reporterConfig = new Configuration.ReporterConfiguration(loggerFactory);
                reporterConfig.SenderConfig.WithAgentHost(electJaegerOptions.Domain);
                reporterConfig.SenderConfig.WithAgentPort(electJaegerOptions.ReporterPort);
                reporterConfig.SenderConfig.WithEndpoint($"http://{electJaegerOptions.Domain}:{electJaegerOptions.TracesPort}/api/traces");
                if (!string.IsNullOrWhiteSpace(electJaegerOptions.AuthUsername))
                {
                    reporterConfig.SenderConfig.WithAuthUsername(electJaegerOptions.AuthUsername);
                }
                if (!string.IsNullOrWhiteSpace(electJaegerOptions.AuthPassword))
                {
                    reporterConfig.SenderConfig.WithAuthPassword(electJaegerOptions.AuthPassword);
                } 
                if (!string.IsNullOrWhiteSpace(electJaegerOptions.AuthToken))
                {
                    reporterConfig.SenderConfig.WithAuthToken(electJaegerOptions.AuthToken);
                }
                reporterConfig = electJaegerOptions.AfterReporterConfig?.Invoke(reporterConfig) ?? reporterConfig;
                
                // Global Config
                var config =
                    new Configuration(electJaegerOptions.ServiceName, loggerFactory)
                        .WithSampler(samplerConfig)
                        .WithReporter(reporterConfig);
                
                config = electJaegerOptions.AfterGlobalConfig?.Invoke(config) ?? config;

                // Tracer
                var tracer = config.GetTracer();
                tracer = electJaegerOptions.AfterTracer?.Invoke(tracer) ?? tracer;
                
                // Register Tracer
                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });
            
            services.Configure<HttpHandlerDiagnosticOptions>(options =>
            {
                options.IgnorePatterns.Add(x => !x.RequestUri.IsLoopback);
            });

            return services;
        }

        private static ILoggerFactory GetLoggerFactory(IServiceProvider serviceProvider)
        {
            ILoggerFactory loggerFactory;

            try
            {
                loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Elect Jaeger > Get Logger Factory Error");
                Console.WriteLine(e);
                Console.WriteLine();

                loggerFactory = new LoggerFactory();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Elect Jaeger > Auto Use New Logger Factory!");
                Console.ResetColor();
                Console.WriteLine();

                return loggerFactory;
            }

            return loggerFactory;
        }
    }
}