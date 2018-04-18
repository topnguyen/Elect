using Elect.Web.HttpDetection;
using Elect.Web.Swagger.Attributes;
using Elect.Web.Swagger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Test.AspNetCore.Controllers
{
    /// <summary>
    ///     Test Controller 
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    public class TestApiController : Controller
    {
        /// <summary>
        ///     Get device info 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ShowInApiDoc]
        [ApiParameter("test_header", In = ParameterInType.Header, Description = "test header field")]
        [ApiParameter("test_query", In = ParameterInType.Query, Description = "test query field")]
        [ApiParameter("test_route", In = ParameterInType.Route, Description = "test route field")]
        [Route("/{test_route}")]
        public IActionResult Device()
        {
            var request = Request;

            var device = HttpContext.Request.GetDeviceInformation();

            return Ok(device);
        }
    }
}