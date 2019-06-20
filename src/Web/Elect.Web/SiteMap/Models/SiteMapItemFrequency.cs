#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SiteMapItemFrequency.cs </Name>
//         <Created> 21/03/2018 2:26:51 PM </Created>
//         <Key> 20144dff-8af7-4c6e-98cf-3a55733501e5 </Key>
//     </File>
//     <Summary>
//         SiteMapItemFrequency.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

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