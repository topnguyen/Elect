namespace Elect.Web.SiteMap.Models
{
    public class SiteMapImageItemDetailModel: ElectDisposableModel
    {
        /// <summary>
        ///     Caption/description of image. 
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        ///     Geo location of image. 
        /// </summary>
        public string GeoLocation { get; set; }
        /// <summary>
        ///     URL of image. 
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        ///     Url to License of image 
        /// </summary>
        public string License { get; set; }
        /// <summary>
        ///     Title of image. 
        /// </summary>
        public string Title { get; set; }
    }
}
