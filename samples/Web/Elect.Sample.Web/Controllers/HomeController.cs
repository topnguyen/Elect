using Elect.Logger.Logging;
using Elect.Logger.Models.Logging;
using Elect.Web.HttpDetection;
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
            var deviceInfo = HttpContext.Request.GetDeviceInformation();
            
            _electLog.Capture("Message Sample");

            var filePath = "Special Path";
            _electLog.Capture("Message Sample Force File Path 1", jsonFilePath: filePath);
            _electLog.Capture("Message Sample Force File Path 2", jsonFilePath: filePath);

            return View();
        }

        [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
        public IActionResult About()
        {
            return View();
        }
    }
}