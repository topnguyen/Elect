namespace Elect.Notification.OneSignal.Models.Device
{
    /// <summary>
    ///     Device info defined in OneSignal app. 
    /// </summary>
    public class DeviceInfoModel
    {
        [JsonProperty("id")]
        public string PlayerId { get; set; }
        /// <summary>
        ///     Push notification identifier from Google or Apple. For Apple push identifiers, you
        ///     must strip all non alphanumeric characters.
        ///     Example: ce777617da7f548fe7a9ab6febb56
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
        /// <summary>
        ///     Number of times the user has played the game, defaults to 1 
        /// </summary>
        [JsonProperty("session_count")]
        public string SessionCount { get; set; }
        /// <summary>
        ///     Language code. Typically lower case two letters, except for Chinese where it must be
        ///     one of "zh-Hans" or "zh-Hant". Example: en
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }
        /// <summary>
        ///     Number of seconds away from UTC. Example: -28800 
        /// </summary>
        [JsonProperty("timezone")]
        public int? Timezone { get; set; }
        /// <summary>
        ///     Version of the app. 
        /// </summary>
        [JsonProperty("game_version")]
        public string GameVersion { get; set; }
        /// <summary>
        ///     Device operating system version. Example: 7.0.4 
        /// </summary>
        [JsonProperty("device_os")]
        public string DeviceOS { get; set; }
        /// <summary>
        ///     0 = iOS 1 = Android 2 = Amazon 3 = WindowsPhone(MPNS) 4 = ChromeApp 5 = ChromeWebsite
        ///     6 = WindowsPhone(WNS) 7 = Safari 8 = Firefox 9 = Mac OS X
        /// </summary>
        [JsonProperty("device_type")]
        public DeviceTypeEnum? DeviceType { get; set; }
        /// <summary>
        ///     Device make and model. Example: iPhone5,1 
        /// </summary>
        [JsonProperty("device_model")]
        public string DeviceModel { get; set; }
        /// <summary>
        ///     Android = The Advertising Id iOS = The identifierForVendor WP8.0 = The DeviceUniqueId
        ///     WP8.1 = The AdvertisingId
        /// </summary>
        [JsonProperty("ad_id")]
        public string AdId { get; set; }
        /// <summary>
        ///     Custom tags for the player.
        ///     Example: {"foo":"bar","this":"that"}
        /// </summary>
        [JsonProperty("tags")]
        public IDictionary<string, object> Tags { get; set; }
        /// <summary>
        ///     Unixtime when the player was last active 
        /// </summary>
        [JsonProperty("last_active")]
        public int? LastActive { get; set; }
        [JsonProperty("playtime")]
        public int? PlayTime { get; set; }
        /// <summary>
        ///     Amount the user has spent in USD, up to two decimal places 
        /// </summary>
        [JsonProperty("amount_spent")]
        public string AmountSpent { get; set; }
        /// <summary>
        ///     Unixtime when the player joined the game 
        /// </summary>
        [JsonProperty("created_at")]
        public int? CreatedAt { get; set; }
        [JsonProperty("invalid_identifier")]
        public bool InvalidIdentifier { get; set; }
        [JsonProperty("badge_count")]
        public int BadgeCount { get; set; }
        [JsonProperty("sdk")]
        public string SDK { get; set; }
        [JsonProperty("ip")]
        public string IpAddress { get; set; }
    }
}
