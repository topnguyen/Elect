using Elect.Logger.Logging;
using Elect.Web.SiteMap.Attributes;
using Elect.Web.SiteMap.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        [SiteMapItem(SiteMapItemFrequency.Hourly, 0.9)]
        public IActionResult Index()
        {
            var logger = ElectLog.Instance;

            logger.Capture("message credit cart is 378282246310005");
            
            return View();
        }

        [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
        public IActionResult About()
        {
            return View();
        }
    }
}