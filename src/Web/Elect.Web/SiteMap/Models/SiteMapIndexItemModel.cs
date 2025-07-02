namespace Elect.Web.SiteMap.Models
{
    /// <summary>
    ///     Represents a site map index item. 
    /// </summary>
    public class SiteMapIndexItemModel : ElectDisposableModel, ISiteMapItem
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SiteMapIndexItemModel" /> 
        /// </summary>
        /// <param name="url">          URL of the page. Optional. </param>
        /// <param name="lastModified"> The date of last modification of the file. Optional. </param>
        /// <exception cref="System.ArgumentNullException">
        ///     If the <paramref name="url" /> is null or empty.
        /// </exception>
        public SiteMapIndexItemModel(string url, DateTime? lastModified = null)
        {
            CheckHelper.CheckNullOrWhiteSpace(url, nameof(url));
            Url = url;
            LastModified = lastModified;
        }
        /// <summary>
        ///     The date of last modification of the file. 
        /// </summary>
        public DateTime? LastModified { get; protected set; }
        /// <summary>
        ///     URL of the page. 
        /// </summary>
        public string Url { get; protected set; }
    }
}
