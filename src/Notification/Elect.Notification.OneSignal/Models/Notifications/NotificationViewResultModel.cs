namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Delivery and convert report result refered to single notification. View
    ///     <see cref="!:https://documentation.onesignal.com/reference#view-notification"> API
    ///     Documentation </see> for more info.
    /// </summary>
    public class NotificationViewResultModel
    {
        /// <summary>
        ///     The id of the result. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        ///     The number of devices that received notification. 
        /// </summary>
        [JsonProperty("successful")]
        public int Successful { get; set; }
        /// <summary>
        ///     The number of devices that failed to receive notification. 
        /// </summary>
        [JsonProperty("failed")]
        public int Failed { get; set; }
        /// <summary>
        ///     The number of users who clicked notification. 
        /// </summary>
        [JsonProperty("converted")]
        public int Converted { get; set; }
        /// <summary>
        ///     The number of remaining devices where notification will be delivered 
        /// </summary>
        [JsonProperty("remaining")]
        public int Remaining { get; set; }
        /// <summary>
        ///     The number of remaining devices where notification will be delivered 
        /// </summary>
        [JsonProperty("queued_at")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public int QueuedAt { get; set; }
        /// <summary>
        ///     The number of remaining devices where notification will be delivered 
        /// </summary>
        [JsonProperty("send_after")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public int SendAfter { get; set; }
        /// <summary>
        ///     <br /> The URL to open in the browser when a user clicks on the notification. <br /> 
        ///     <code>
        /// Example: http://www.google.com
        ///     </code>
        ///     <br /> This field supports
        ///     <see cref="!:https://documentation.onesignal.com/docs/notification-content#section-notification-content-substitution">
        ///     inline substitutions </see> . <br />
        ///     Platforms: ALL <br />
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        ///     <br /> The notification's content (excluding the title), a map of language codes to
        ///     text for each language. <br /> Each hash must have a language code string for a key,
        ///     mapped to the localized text you would like users to receive for that language.
        ///     <br /> English must be included in the hash. <br />
        ///     <code>
        /// Example: {"en": "English Message", "es": "Spanish Message"}
        ///     </code>
        ///     <br /> See the language codes you can use
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here
        ///     </see> . <br />
        /// </summary>
        [JsonProperty("contents")]
        [JsonExtensionData]
        public Dictionary<string, string> Contents { get; set; }
        /// <summary>
        ///     <br /> The notification's title, a map of language codes to text for each language.
        ///     <br /> Each hash must have a language code string for a key, mapped to the localized
        ///     text you would like users to receive for that language. <br /> A default title may be
        ///     displayed if a title is not provided. <br />
        ///     <code>
        /// Example: {"en": "English Title", "es": "Spanish Title"}
        ///     </code>
        ///     <br /> See the language codes you can use
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here
        ///     </see> . <br />
        /// </summary>
        [JsonProperty("headings")]
        [JsonExtensionData]
        public Dictionary<string, string> Headings { get; set; }
        /// <summary>
        ///     <br /> A custom map of data that is passed back to your app. <br /> 
        ///     <code>
        /// Example: {"abc": "123", "foo": "bar"}
        ///     </code>
        ///     <br /> See the language codes you can use
        ///     <see cref="!:https://documentation.onesignal.com/docs/language-localization"> here
        ///     </see> . <br />
        /// </summary>
        [JsonProperty("data")]
        [JsonExtensionData]
        public Dictionary<string, string> Data { get; set; }
        /// <summary>
        ///     The number of remaining devices where notification will be delivered 
        /// </summary>
        [JsonProperty("canceled")]
        public bool Canceled { get; set; }
    }
}
