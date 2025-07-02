namespace Elect.Notification.OneSignal.Models.Notifications
{
    public class AndroidBackgroundLayoutFieldModel
    {
        /// <summary>
        ///     Background image. 
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }
        /// <summary>
        ///     Background heading color. 
        /// </summary>
        [JsonProperty("headings_color")]
        public string HeadingsColor { get; set; }
        /// <summary>
        ///     Background content color. 
        /// </summary>
        [JsonProperty("contents_color")]
        public string ContentsColor { get; set; }
    }
}
