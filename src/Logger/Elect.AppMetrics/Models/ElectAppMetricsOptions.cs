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
        ///     App Metrics endpoint, default is "/metrics"
        /// </summary>
        public string MetricsEndpoint { get; set; } = "/metrics";
        
        /// <summary>
        ///     Enable or disable the <see cref="MetricsTextEndpoint"/>, default is false
        /// </summary>
        public bool IsEnableMetricsTextEndpoint { get; set; } = false;
                
        /// <summary>
        ///     App Metrics Text endpoint, default is "/metrics-text"
        /// </summary>
        public string MetricsTextEndpoint { get; set; } = "/metrics-text";

        /// <summary>
        ///     Enable or disable the <see cref="EnvEndpoint"/>, default is false
        /// </summary>
        public bool IsEnableEnvEndpoint { get; set; } = false;

        /// <summary>
        ///     Environment Info endpoint, default is "/env"
        /// </summary>
        public string EnvEndpoint { get; set; } = "/env";

        // Influx
        
        public bool IsInfluxEnabled { get; set; }
        
        /// <summary>
        ///     InfluxDB Endpoint
        /// </summary>
        public string InfluxEndpoint { get; set; }
        
        /// <summary>
        ///     InfluxDB Database Name
        /// </summary>
        public string InfluxDatabase{ get; set; }
        
        /// <summary>
        ///     InfluxDB Database access - UserName
        /// </summary>
        public string InfluxUserName { get; set; }
        
        /// <summary>
        ///     InfluxDB Database access - Password
        /// </summary>
        public string InfluxPassword { get; set; }
        
        public int InfluxInterval { get; set; }
        
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