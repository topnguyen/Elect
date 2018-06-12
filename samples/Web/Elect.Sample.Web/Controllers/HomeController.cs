using System;
using Elect.Logger.Logging;
using Elect.Logger.Models.Event;
using Elect.Logger.Models.Logging;
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
            var logger = new ElectLog();

            for (int i = 0; i < 10; i++)
            {
                var message = "message " + i;
                logger.Capture(message);
            }
            
            return View();
        }

        [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
        public IActionResult About()
        {
            return View();
        }
    }
}