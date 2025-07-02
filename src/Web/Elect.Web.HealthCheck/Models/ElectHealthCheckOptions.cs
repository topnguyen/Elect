namespace Elect.Web.HealthCheck.Models
{
    public class ElectHealthCheckOptions : IElectOptions
    {
        public bool IsEnable { get; set; } = true;
        /// <summary>
        ///     Endpoint to access healthy report, default is "/health"
        /// </summary>
        public string Endpoint { get; set; } = "/health";
        /// <summary>
        ///     Config Database Connection String for Health Check
        /// </summary>
        /// <remarks>Leave this null for disable Database Check</remarks>
        public string DbConnectionString { get; set; }
        /// <summary>
        ///     Do more action after finish config Health Check Builder
        /// </summary>
        public Action<IHealthChecksBuilder> Builder { get; set; } 
        /// <summary>
        ///     Adjust <see cref="Options"/> before this service use it
        /// </summary>
        public Func<HealthCheckOptions, HealthCheckOptions> Options { get; set; }
    }
}
