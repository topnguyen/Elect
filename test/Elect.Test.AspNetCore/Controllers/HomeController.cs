using Elect.Test.AspNetCore.Models;
using Elect.Test.AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Elect.Test.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISampleService _sampleService;

        public HomeController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public IActionResult Index()
        {
            ViewBag.SampleText = _sampleService.GetSampleText();
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