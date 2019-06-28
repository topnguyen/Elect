using System;
using Elect.Core.Interfaces;
using Jaeger;
using Microsoft.Extensions.PlatformAbstractions;
using OpenTracing;

namespace Elect.Jaeger.Models
{
    /// <summary>
    ///     Config for the Jaeger Client, see more https://github.com/jaegertracing/jaeger-client-csharp
    /// </summary>
    public class ElectJaegerOptions : IElectOptions
    {
        public bool IsEnable { get; set; } = true;
        
        /// <summary>
        ///     Service Name, default is the Application Name
        /// </summary>
        public string ServiceName { get; set; } = PlatformServices.Default.Application.ApplicationName;
        
        /// <summary>
        ///     Default is localhost
        /// </summary>
        public string SamplerDomain { get; set; } = "localhost";

        /// <summary>
        ///     Default is 5778
        /// </summary>
        public int SamplerPort { get; set; } = 5778;
        
        /// <summary>
        ///     Default is localhost
        /// </summary>
        public string ReporterDomain { get; set; } = "localhost";

        /// <summary>
        ///     Default is 6831
        /// </summary>
        public int ReporterPort { get; set; } = 6831;
        
        /// <summary>
        ///     Default is http://location:14268/api/traces
        /// </summary>
        public string TracesEndpoint { get; set; } = "http://location:14268/api/traces";
        
        public string AuthUsername { get; set; }
        
        public string AuthPassword { get; set; } 
        
        public string AuthToken { get; set; }

        /// <summary>
        ///     Callback after finish setup SamplerConfiguration, allow you adjust setup for SamplerConfiguration
        /// </summary>
        /// <remarks>If the result of your callback is <c>null</c> then stop add Jaeger Service</remarks>
        public Func<Configuration.SamplerConfiguration, Configuration.SamplerConfiguration> AfterSamplerConfig { get; set; }
        
        /// <summary>
        ///     Callback after finish setup SamplerConfiguration, allow you adjust setup for SamplerConfiguration
        /// </summary>
        /// <remarks>If the result of your callback is <c>null</c> then stop add Jaeger Service</remarks>
        public Func<Configuration.ReporterConfiguration, Configuration.ReporterConfiguration> AfterReporterConfig { get; set; }
        
        /// <summary>
        ///     Callback after finish setup Configuration, allow you adjust setup for Configuration
        /// </summary>
        /// <remarks>If the result of your callback is <c>null</c> then stop add Jaeger Service</remarks>
        public Func<Configuration, Configuration> AfterGlobalConfig { get; set; }
        
        /// <summary>
        ///     Callback after finish config Tracer, allow you adjust config for Tracer
        /// </summary>
        public Func<ITracer, ITracer> AfterTracer { get; set; }
    }
}