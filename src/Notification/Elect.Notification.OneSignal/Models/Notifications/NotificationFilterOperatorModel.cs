namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Notification filter operator is used to define logical AND, OR 
    /// </summary>
    public class NotificationFilterOperatorModel : INotificationFilter
    {
        /// <summary>
        ///     Can be AND or OR operator 
        /// </summary>
        [JsonProperty("operator")]
        public string Operator { get; set; }
    }
}
