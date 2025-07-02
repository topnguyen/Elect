namespace Elect.Sample.Web.Controllers
{
    public class SiteMapController : Controller
    {
        /// <summary>
        ///     Get all site map index 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/sitemap.xml")]
        [ResponseCache(Duration = 600)]
        public ContentResult Index()
        {
            var listSiteMapIndex = new List<SiteMapIndexItemModel>
            {
                new SiteMapIndexItemModel(Url.AbsoluteAction("Main", "SiteMap"))
            }.ToArray();
            var contentResult = new SiteMapIndexGenerator().GenerateContentResult(listSiteMapIndex);
            return contentResult;
        }
        /// <summary>
        ///     Get all site map item by attributes 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/sitemap/main.xml")]
        [ResponseCache(Duration = 600)]
        public ContentResult Main()
        {
            return SiteMapHelper.GetSiteMapContentResult(Url);
        }
    }
}
