using Elect.Location.Google.Interfaces;
using Elect.Location.Models;
using Elect.Test.AspNetCore.Models;
using Elect.Test.AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Elect.Test.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISampleService _sampleService;
        private readonly IElectGoogleClient _electGoogleClient;

        public HomeController(ISampleService sampleService, IElectGoogleClient electGoogleClient)
        {
            _sampleService = sampleService;
            _electGoogleClient = electGoogleClient;
        }

        public async Task<IActionResult> Index()
        {
            var resultMatrix = await _electGoogleClient.GetDistanceDurationMatrixAsync(_ =>
            {
                _.OriginalCoordinates = new List<CoordinateModel>
                {
                        new CoordinateModel(106.6989950, 10.7797840)
                };
                _.DestinationCoordinates = new List<CoordinateModel>
                {
                        new CoordinateModel(106.6640908, 10.7523744)
                };
                _.AdditionalValues = new Dictionary<string, string>
                {
                        {"language", "en-US" }
                };
            }).ConfigureAwait(true);

            var resultDirection = await _electGoogleClient.GetDirectionsAsync(_ =>
            {
                _.OriginalCoordinate = new CoordinateModel(106.6989950, 10.7797840);
                _.DestinationCoordinate = new CoordinateModel(106.6640908, 10.7523744);
            }).ConfigureAwait(true);

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