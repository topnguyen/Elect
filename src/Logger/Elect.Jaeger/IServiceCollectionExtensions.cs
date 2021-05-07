using System;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Core.ConfigUtils;
using Elect.Jaeger.Models;
using Jaeger;
using Jaeger.Samplers;
using Jaeger.Senders;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;

namespace Elect.Jaeger
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectJaeger(this IServiceCollection services, IConfiguration configuration,
            string sectionName = "ElectJaeger")
        {
            var electJaegerOptions = GetOptions(configuration, sectionName);
            
            return services.AddElectJaeger(electJaegerOptions);
        }

        public static ElectJaegerOptions GetOptions(IConfiguration configuration,
            string sectionName = "ElectJaeger")
        {
            var electJaegerOptions = new ElectJaegerOptions();

            electJaegerOptions.IsEnable =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electJaegerOptions.IsEnable)}");

            electJaegerOptions.ServiceName =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electJaegerOptions.ServiceName)}");

            electJaegerOptions.SamplerDomain =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electJaegerOptions.SamplerDomain)}");

            electJaegerOptions.ReporterDomain =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electJaegerOptions.ReporterDomain)}");

            electJaegerOptions.TracesEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electJaegerOptions.TracesEndpoint)}");

            return electJaegerOptions;
        }

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
                _.SamplerDomain = configure.SamplerDomain;
                _.SamplerPort = configure.SamplerPort;
                _.ReporterDomain = configure.ReporterDomain;
                _.ReporterPort = configure.ReporterPort;
                _.TracesEndpoint = configure.TracesEndpoint;
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
                samplerConfig.WithSamplingEndpoint(
                    $"http://{electJaegerOptions.SamplerDomain}:{electJaegerOptions.SamplerPort}");
                samplerConfig.WithType(ConstSampler.Type);
                samplerConfig = electJaegerOptions.AfterSamplerConfig?.Invoke(samplerConfig) ?? samplerConfig;

                // Sender
                var senderConfig = new Configuration.SenderConfiguration(loggerFactory);
                senderConfig.WithAgentHost(electJaegerOptions.ReporterDomain);
                senderConfig.WithAgentPort(electJaegerOptions.ReporterPort);
                senderConfig.WithEndpoint(electJaegerOptions.TracesEndpoint);
                if (!string.IsNullOrWhiteSpace(electJaegerOptions.AuthUsername))
                {
                    senderConfig.WithAuthUsername(electJaegerOptions.AuthUsername);
                }

                if (!string.IsNullOrWhiteSpace(electJaegerOptions.AuthPassword))
                {
                    senderConfig.WithAuthPassword(electJaegerOptions.AuthPassword);
                }

                if (!string.IsNullOrWhiteSpace(electJaegerOptions.AuthToken))
                {
                    senderConfig.WithAuthToken(electJaegerOptions.AuthToken);
                }
                
                // Reporter
                var reporterConfig = new Configuration.ReporterConfiguration(loggerFactory);
                reporterConfig.WithSender(senderConfig);

                reporterConfig = electJaegerOptions.AfterReporterConfig?.Invoke(reporterConfig) ?? reporterConfig;

                // Global Config
                
                Configuration.SenderConfiguration.DefaultSenderResolver = 
                    new SenderResolver(loggerFactory)
                        .RegisterSenderFactory<ThriftSenderFactory>();
                
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