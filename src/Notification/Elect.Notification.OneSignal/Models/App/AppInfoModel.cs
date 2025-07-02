namespace Elect.Notification.OneSignal.Models.App
{
    public class AppInfoModel
    {
        [JsonProperty("id")] 
        public string Id { get; set; }
        [JsonProperty("name")] 
        public string Name { get; set; }
        [JsonProperty("players")]
        public int Players { get; set; }
        [JsonProperty("messageable_players")]
        public int MessageablePlayers { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("gcm_key")]
        public string GcmKey { get; set; }
        [JsonProperty("chrome_web_origin")]
        public string ChromeWebOrigin { get; set; }
        [JsonProperty("chrome_web_default_notification_icon")]
        public string ChromeWebDefaultNotificationIcon { get; set; }
        [JsonProperty("chrome_web_sub_domain")]
        public string ChromeWebSubDomain { get; set; }
        [JsonProperty("apns_env")]
        public string ApnsEnv { get; set; }
        [JsonProperty("apns_certificates")]
        public string ApnsCertificates { get; set; }
        [JsonProperty("safari_apns_certificate")]
        public string SafariApnsCertificate { get; set; }
        [JsonProperty("safari_site_origin")]
        public string SafariSiteOrigin { get; set; }
        [JsonProperty("safari_push_id")]
        public string SafariPushId { get; set; }
        [JsonProperty("safari_icon_16_16")]
        public string SafariIcon1616 { get; set; }
        [JsonProperty("safari_icon_32_32")]
        public string SafariIcon3232 { get; set; }
        [JsonProperty("safari_icon_64_64")]
        public string SafariIcon6464 { get; set; }
        [JsonProperty("safari_icon_128_128")]
        public string SafariIcon128128 { get; set; }
        [JsonProperty("safari_icon_256_256")]
        public string SafariIcon256256 { get; set; }
        [JsonProperty("site_name")]
        public string SiteName { get; set; }
        [JsonProperty("basic_auth_key")]
        public string BasicAuthKey { get; set; }
    }
}
