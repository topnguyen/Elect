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
        ///     Service endpoint for Consul connect to the service.
        /// </summary>
        /// <remarks>This endpoint must valid URI, if not set will use the running localhost with port.</remarks>
        public string ServiceEndpoint { get; set; }
        /// <summary>
        ///     Default is Application Name
        /// </summary>
        public string ServiceName { get; set; } = PlatformServices.Default.Application.ApplicationName;
        /// <summary>
        ///     Default is Application Name
        /// </summary>
        public string ServiceId { get; set; } = PlatformServices.Default.Application.ApplicationName;
        public List<string> Tags { get; set; } = new List<string>();
        /// <summary>
        ///    Timeout to check healthy 
        /// </summary>
        public TimeSpan CheckTimeOut { get; set; } = TimeSpan.FromSeconds(3);
        /// <summary>
        ///     Interval to check healthy
        /// </summary>
        public TimeSpan CheckInternal { get; set; } = TimeSpan.FromSeconds(10);
        /// <summary>
        ///     Deregister dead service after a period, default is 24 hours.
        /// </summary>
        public TimeSpan? DeregisterDeadServiceAfter = TimeSpan.FromHours(24);
        // Fabio
        /// <summary>
        ///     If enable, will add Tag: $"urlprefix-/{<see cref="ServiceName"/>} strip=/{<see cref="ServiceName"/>}" to support the Fabio Load Balancing. <br />
        ///     Default is true.
        /// </summary>
        public bool IsFabioEnable { get; set; } = true;
        /// <summary>
        ///     Domain of Fabio - Load Balancing Service. Default is "http://127.0.0.1:9999"
        /// </summary>
        public string FabioEndpoint { get; set; } = "http://127.0.0.1:9999";
    }
}
