namespace Elect.Core.CrawlerUtils.Models
{
    public class MetadataModel
    {
        public string OriginalUrl { get; set; }
        /// <summary>
        ///     Meta Data og:url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        ///     Meta Data og:title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///     Meta Data og:description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     Meta Data og:image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        ///     Meta Data og:video
        /// </summary>
        public string Video { get; set; }
        /// <summary>
        ///     Meta Data og:type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///     Meta Data og:locale
        /// </summary>
        public string Locale { get; set; }
        /// <summary>
        ///     Meta Data og:site_name
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        ///     Meta Data author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        ///     Meta Data published_time, article:published_time
        /// </summary>
        public DateTimeOffset PublishedTime { get; set; }
        /// <summary>
        ///     Meta Data tag, tags
        /// </summary>
        public List<string> Tags { get; set; }
        public List<MetaTagModel> MetaTags { get; set; } = new List<MetaTagModel>();
    }
}
