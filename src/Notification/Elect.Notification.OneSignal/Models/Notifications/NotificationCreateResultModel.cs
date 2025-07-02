namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Result of notification create operation. 
    /// </summary>
    public class NotificationCreateResultModel
    {
        /// <summary>
        ///     Returns the number of recipients who received the message. 
        /// </summary>
        [JsonProperty("recipients")]
        public int Recipients { get; set; }
        /// <summary>
        ///     Returns the id of the result. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
