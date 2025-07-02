namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Result of notification cancel operation. 
    /// </summary>
    public class NotificationCancelResultModel
    {
        /// <summary>
        ///     Returns whether the message was canceled or not {'success': "true"} 
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public string Success { get; set; }
    }
}
