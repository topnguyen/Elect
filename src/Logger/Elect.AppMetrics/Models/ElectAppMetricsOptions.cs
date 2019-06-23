using System.Collections.Generic;
using Elect.Core.Interfaces;

namespace Elect.AppMetrics.Models
{
    /// <summary>
    ///     Config for the App Metrics, see more https://www.app-metrics.io
    /// </summary>
    public class ElectAppMetricsOptions : IElectOptions
    {
        public bool IsEnable { get; set; } = true;
        
        /// <summary>
        ///     Enable or disable the <see cref="MetricsEndpoint"/>, default is true
        /// </summary>
        public bool IsEnableMetricsEndpoint { get; set; } = true;
        
        /// <summary>
        ///     App Metrics endpoint, default is "/developers/metrics"
        /// </summary>
        public string MetricsEndpoint { get; set; } = "/developers/metrics";
        
        /// <summary>
        ///     Enable or disable the <see cref="MetricsTextEndpoint"/>, default is false
        /// </summary>
        public bool IsEnableMetricsTextEndpoint { get; set; } = false;
                
        /// <summary>
        ///     App Metrics Text endpoint, default is "/developers/metrics-text"
        /// </summary>
        public string MetricsTextEndpoint { get; set; } = "/developers/metrics-text";

        /// <summary>
        ///     Enable or disable the <see cref="EnvEndpoint"/>, default is false
        /// </summary>
        public bool IsEnableEnvEndpoint { get; set; } = false;

        /// <summary>
        ///     Environment Info endpoint, default is "/developers/env"
        /// </summary>
        public string EnvEndpoint { get; set; } = "/developers/env";

        // Influx
        
        public bool IsInfluxEnabled { get; set; }
        
        /// <summary>
        ///     InfluxDB Endpoint
        /// </summary>
        public string InfluxEndpoint { get; set; }
        
        /// <summary>
        ///     InfluxDB Database Name
        /// </summary>
        public string InFluxDatabase{ get; set; }
        
        /// <summary>
        ///     InfluxDB Database access - UserName
        /// </summary>
        public string InFluxUserName { get; set; }
        
        /// <summary>
        ///     InfluxDB Database access - Password
        /// </summary>
        public string InFluxPassword { get; set; }
        
        public int InFluxInterval { get; set; }
        
        // Prometheus
        
        public bool IsPrometheusEnabled { get; set; }

        /// <summary>
        ///     Support "text" or "protobuf" (gRPC). Default is "text"
        /// </summary>
        public ElectPrometheusFormatter PrometheusFormatter { get; set; } = ElectPrometheusFormatter.Text;

        // Tag
        
        public IDictionary<string, string> Tags { get; set; }
    }
}