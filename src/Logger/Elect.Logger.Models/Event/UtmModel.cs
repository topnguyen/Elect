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
        public string WebSiteUrl { get; set; }

        /// <summary>
        ///     Where user is coming form - Source. e.g: twitter, facebook, pinterest
        /// </summary>
        public string CampaignSource { get; set; }

        /// <summary>
        ///     How do user get them - Type of Source, or place/site in system. e.g: social, ecommerce, cpc, banner, email
        /// </summary>
        public string CampaignMedium { get; set; }

        /// <summary>
        ///     Why is user going there - Compaign or Product name
        /// </summary>
        public string CampaignName { get; set; }

        public string CampaignTerm { get; set; }

        public string CampaignContent { get; set; }
    }
}