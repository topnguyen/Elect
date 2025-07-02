namespace Elect.Job.Hangfire.Models
{
    public class ElectHangfireOptions : IElectOptions
    {
        public bool IsEnable { get; set; } = true;
        /// <summary>
        ///     Disable or Enable Job Dashboard, default is false. 
        /// </summary>
        public bool IsDisableJobDashboard { get; set; }
        /// <summary>
        ///     Job Dashboard url, default is "/developers/job". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string Url { get; set; } = "/developers/job";
        /// <summary>
        ///     URL for back button in Job Dashboard. Set to <see langword="null" /> to hide the Back
        ///     To Site link, default is "/".
        /// </summary>
        public string BackToUrl { get; set; } = "/";
        /// <summary>
        ///     Access Key via uri param "key", default is "" - allow anonymous. 
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        ///     Un-authorize message when user access Job Dashboard with not correct key. Default is
        ///     "You don't have permission to view API Document, please contact your administrator."
        /// </summary>
        public string UnAuthorizeMessage { get; set; } = "You don't have permission to access Job Dashboard, please contact your administrator.";
        /// <summary>
        ///     Storage provider, default is Memory. 
        /// </summary>
        public HangfireProvider Provider { get; set; } = HangfireProvider.Memory;
        /// <summary>
        ///     Database Connection if <see cref="Provider " /> is <see cref="HangfireProvider.SqlServer" /> 
        /// </summary>
        public string DbConnectionString { get; set; }
        /// <summary>
        ///     The interval the /stats endpoint should be polled with (milliseconds), default is 2000.
        /// </summary>
        public int StatsPollingInterval { get; set; } = 3000;
        /// <summary>
        ///     Additional Options if you want to add your customize after Elect add Hangfire Global Config.
        /// </summary>
        public Action<IGlobalConfiguration, ElectHangfireOptions> ExtendOptions { get; set; }
    }
}
