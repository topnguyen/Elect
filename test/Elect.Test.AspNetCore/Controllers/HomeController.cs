using Elect.Test.AspNetCore.Models;
using Elect.Web.HttpDetection;
using Elect.Web.HttpUtils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elect.Test.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var deviceInfo = HttpContext.Request.GetDeviceInformation();

            var ip = HttpContext.Request.GetIpAddress();

            var boolSameIpAddress = ip == deviceInfo.IpAddress;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}