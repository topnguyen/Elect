namespace Elect.Core.CrawlerUtils.Models
{
    public class MetaTagModel
    {
        public string Html { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }
}
