namespace Elect.Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        [SiteMapItem(SiteMapItemFrequency.Hourly, 0.9)]
        public IActionResult Index()
        {
            return View();
        }

        [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
        public IActionResult About()
        {
            return View();
        }
    }
}
