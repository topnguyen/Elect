using Newtonsoft.Json;

namespace Elect.Logger.Models.Event
{
    /// <summary>
    ///     Urchin Tracking Module (UTM)
    /// </summary>
    public class UtmModel
    {
        /// <summary>
        ///     Where user is going
        /// </summary>
        [JsonProperty(UtmConstant.Url)]
        public string WebSiteUrl { get; set; }

        /// <summary>
        ///     Where user is coming form - Source. e.g: twitter, facebook, pinterest
        /// </summary>
        [JsonProperty(UtmConstant.Source)]
        public string CampaignSource { get; set; }

        /// <summary>
        ///     How do user get them - Type of Source, or place/site in system. e.g: social, ecommerce, cpc, banner, email
        /// </summary>
        [JsonProperty(UtmConstant.Medium)]
        public string CampaignMedium { get; set; }

        /// <summary>
        ///     Why is user going there - Compaign or Product name
        /// </summary>
        [JsonProperty(UtmConstant.Campaign)]
        public string CampaignName { get; set; }

        [JsonProperty(UtmConstant.Term)]
        public string CampaignTerm { get; set; }

        [JsonProperty(UtmConstant.Content)]
        public string CampaignContent { get; set; }
    }
}