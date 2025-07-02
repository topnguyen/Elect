namespace Elect.Web.SiteMap.Models
{
    /// <summary>
    ///     How frequently the page is likely to change. This value provides general information to
    ///     search engines and may not correlate exactly to how often they crawl the page.
    /// </summary>
    /// <remarks>
    ///     The value "always" should be used to describe documents that change each time they are
    ///     accessed. The value "never" should be used to describe archived URLs.
    /// </remarks>
    public enum SiteMapItemFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }
}
