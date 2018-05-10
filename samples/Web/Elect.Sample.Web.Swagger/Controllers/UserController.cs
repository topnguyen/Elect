using Elect.Web.Swagger.Attributes;
using Elect.Web.Swagger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Sample.Web.Swagger.Controllers
{
    [ShowInApiDoc]
    [ApiDocGroup("User API")]
    public class UserController : Controller
    {
        /// <summary>
        ///     Get User
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        [ApiParameter("id", Description = "User Id", In = ParameterInType.Query, Type = "string", IsRequire = true)]
        public IActionResult GetUser([FromQuery] string userName)
        {
            return Ok(new
            {
                userId = HttpContext.Request.Query["id"],
                userName
            });
        }

        /// <summary>
        ///     Get Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("profiles")]
        [ApiParameter("id", Description = "User Id", In = ParameterInType.Query, Type = "string", IsRequire = true)]
        [ApiDocGroup("Profile API")]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                userId = HttpContext.Request.Query["id"]
            });
        }
    }
}