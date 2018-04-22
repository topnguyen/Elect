using Elect.Web.HttpDetection;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Test.AspNetCore.Controllers
{
    public class TestApiController : BaseController
    {
        /// <summary>
        ///     Get device info 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("device-info")]
        public IActionResult Device()
        {
            var httpContext = Elect.Web.Middlewares.HttpContextMiddleware.HttpContext.Current;

            var device = HttpContext.Request.GetDeviceInformation();

            return Ok(device);
        }
    }
}