namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Complex type used to describe filter field. 
    /// </summary>
    public class NotificationFilterFieldModel : INotificationFilter
    {
        /// <summary>
        ///     The type of the filter field. 
        /// </summary>
        [JsonProperty("field")]
        [JsonConverter(typeof(NotificationFilterFieldTypeConverter))]
        public NotificationFilterFieldTypeEnum Field { get; set; }
        /// <summary>
        ///     The key used for comparison. 
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }
        /// <summary>
        ///     The relation between key and value. 
        /// </summary>
        [JsonProperty("relation")]
        public string Relation { get; set; }
        /// <summary>
        ///     The value. 
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
