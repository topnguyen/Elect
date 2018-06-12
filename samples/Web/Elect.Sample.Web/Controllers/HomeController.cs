using Elect.Logger.Logging;
using Elect.Web.SiteMap.Attributes;
using Elect.Web.SiteMap.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElectLog _electLog;

        public HomeController(IElectLog electLog)
        {
            _electLog = electLog;
        }
        
        [SiteMapItem(SiteMapItemFrequency.Hourly, 0.9)]
        public IActionResult Index()
        {
            _electLog.BeforeLog = (logInfo) =>
            {
                var log = logInfo;
                return logInfo;
            };
            
            _electLog.Capture("message credit cart is 378282246310005");
            return View();
        }

        [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
        public IActionResult About()
        {
            return View();
        }
    }
}