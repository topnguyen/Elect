using Elect.Web.HttpDetection;
using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Test.AspNetCore.Controllers
{
    /// <summary>
    ///     Manage Controller 
    /// </summary>
    [Authorize]
    [Route("[controller]/[action]")]
    public class TestManageApiController : Controller
    {
        /// <summary>
        ///     Get device info 2 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ShowInApiDoc]
        public IActionResult Device2()
        {
            var device = HttpContext.Request.GetDeviceInformation();

            return Ok(device);
        }

        /// <summary>
        ///     Get device info 1 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ShowInApiDoc]
        [AllowAnonymous]
        public IActionResult Device1()
        {
            var device = HttpContext.Request.GetDeviceInformation();

            return Ok(device);
        }
    }
}