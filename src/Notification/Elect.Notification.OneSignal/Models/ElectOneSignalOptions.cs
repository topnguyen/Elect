namespace Elect.Notification.OneSignal.Models
{
    public class ElectOneSignalOptions
    {
        /// <summary>
        ///     Auth/Account Key use for manage apps
        /// </summary>
        public string AuthKey { get; set; }
        /// <summary>
        ///     Pre-define apps used
        /// </summary>
        public List<ElectOneSignalAppOption> Apps { get; set; } = new List<ElectOneSignalAppOption>();
    }
}
