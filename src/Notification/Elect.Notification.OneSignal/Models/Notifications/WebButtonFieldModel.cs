namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Class used to describe web button. 
    /// </summary>
    public class WebButtonFieldModel
    {
        /// <summary>
        ///     Web button ID. This is required field. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        ///     Web button text. 
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        /// <summary>
        ///     Web button icon. 
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        /// <summary>
        ///     Web button url. 
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
