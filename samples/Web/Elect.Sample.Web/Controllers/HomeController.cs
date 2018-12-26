using System;
using System.Collections.Generic;
using System.IO;
using Elect.Logger.Logging;
using Elect.Logger.Logging.Models;
using Elect.Logger.Models.Logging;
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
            _electLog.Capture("Message Sample", LogType.Info);

            return View();
        }

        [SiteMapItem(SiteMapItemFrequency.Weekly, 0.9)]
        public IActionResult About()
        {
            return View();
        }
    }
}