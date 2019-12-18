using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Elect.Sample.Web.Swagger.Controllers
{
    [ShowInApiDoc]
    [ApiDocGroup("User API")]
    public class UserController : Controller
    {
        /// <summary>
        ///     Test Response 
        /// </summary>
        [HttpGet]
        [ApiDocGroup("Test")]
        [Route("test")]
        public IActionResult TestResponse()
        {
            return Ok(new
            {
                Message = "OK Fine"
            });
        }

        /// <summary>
        ///     Get User 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users")]
        [ApiDocGroup("User API")] // Make this Action appear in "User API" group
        [ApiDocGroup("Profile API")] // Make this Action appear in "Profile API" group
        [ApiParameter("id", Description = "User Id", In = "Query", Style = "Form", Required = true)]
        [ApiParameter("info1", Description = "Info 1", In = "Header", Style = "Simple", Required = true)]
        public IActionResult GetUser([FromQuery] string userName)
        {
            var id = HttpContext.Request.Query["id"].FirstOrDefault();
            var info1 = HttpContext.Request.Headers["info1"].FirstOrDefault();

            return Ok(new
            {
                id,
                userName,
                info1,
            });
        }
    }
}