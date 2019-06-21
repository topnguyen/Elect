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
        
        // Influx
        
        public bool IsInfluxEnabled { get; set; }
        
        public string InfluxEndpoint { get; set; }
        
        public string InFluxDatabase { get; set; }
        
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