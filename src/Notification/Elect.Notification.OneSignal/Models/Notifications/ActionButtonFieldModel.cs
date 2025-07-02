namespace Elect.Notification.OneSignal.Models.Notifications
{
    public class ActionButtonFieldModel
    {
        /// <summary>
        ///     Action button ID. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        ///     Action button text. 
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        /// <summary>
        ///     Action button icon. 
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
