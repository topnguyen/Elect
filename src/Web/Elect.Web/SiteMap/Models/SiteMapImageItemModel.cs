namespace Elect.Web.SiteMap.Models
{
    public class SiteMapImageItemModel : ElectDisposableModel, ISiteMapItem
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SiteMapImageItemDetailModel" /> 
        /// </summary>
        /// <param name="url">    URL of the page. </param>
        /// <param name="images"> List image </param>
        /// <exception cref="System.ArgumentNullException">
        ///     If the <paramref name="url" /> is null or empty.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     If the <paramref name="images" /> is null or empty.
        /// </exception>
        public SiteMapImageItemModel(string url, params SiteMapImageItemDetailModel[] images)
        {
            CheckHelper.CheckNullOrWhiteSpace(url, nameof(url));
            if (images?.Any() != true)
            {
                throw new ArgumentNullException($"{nameof(images)} is null");
            }
            Url = url;
            Images = images.ToList();
        }
        public List<SiteMapImageItemDetailModel> Images { get; protected set; }
        /// <summary>
        ///     URL of the page. 
        /// </summary>
        public string Url { get; protected set; }
    }
}
