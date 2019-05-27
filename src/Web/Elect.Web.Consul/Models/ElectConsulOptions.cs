using System;
using System.Collections.Generic;
using Elect.Core.Interfaces;
using Microsoft.Extensions.PlatformAbstractions;

namespace Elect.Web.Consul.Models
{
    public class ElectConsulOptions : IElectOptions
    {
        public bool IsEnable { get; set; } = true;
        
        /// <summary>
        ///     Default is "http://127.0.0.1:8500"
        /// </summary>
        public string ConsulEndpoint { get; set; } = "http://127.0.0.1:8500";

        /// <summary>
        ///     Access token to Consul Agent
        /// </summary>
        public string ConsulAccessToken { get; set; }

        /// <summary>
        ///     Default is Application Name
        /// </summary>
        public string ServiceName { get; set; } = PlatformServices.Default.Application.ApplicationName;

        /// <summary>
        ///     Default is Application Name
        /// </summary>
        public string ServiceId { get; set; } = PlatformServices.Default.Application.ApplicationName;

        /// <summary>
        ///     Default is Application Name with prefix is "urlprefix-/" to support the Fabio Load Balancing
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>
        {
            $"urlprefix-/{PlatformServices.Default.Application.ApplicationName} strip=/{PlatformServices.Default.Application.ApplicationName}"
        };

        /// <summary>
        ///    Timeout to check healthy 
        /// </summary>
        public TimeSpan CheckTimeOutInSeconds { get; set; } = TimeSpan.FromSeconds(3);

        /// <summary>
        ///     Interval to check healthy
        /// </summary>
        public TimeSpan CheckInternalInSeconds { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        ///     Deregister dead service after a period, default is 5 mins.
        /// </summary>
        public TimeSpan? DeregisterDeadServiceAfter = TimeSpan.FromMinutes(5);
        
        // Fabio
        
        /// <summary>
        ///     Domain of Fabio - Load Balancing Service. Default is "http://127.0.0.1:9999"
        /// </summary>
        public string FabioEndpoint { get; set; } = "http://127.0.0.1:9999";
    }
}