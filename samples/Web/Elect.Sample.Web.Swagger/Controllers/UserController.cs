using Elect.Web.Swagger.Attributes;
using Elect.Web.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Cors;

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
            return Ok();
        }
        
        /// <summary>
        ///     Get User 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users")]
        [ApiParameter("id", Description = "User Id", In = ParameterIn.Query, Type = ParameterType.String, IsRequire = true)]
        [ApiParameter("avatar1", Description = "Avatar File", In = ParameterIn.FormData, Type = ParameterType.File, IsRequire = true)]
        [ApiParameter("avatar2", Description = "Avatar File", In = ParameterIn.FormData, Type = ParameterType.File, IsRequire = true)]
        public IActionResult GetUser([FromQuery] string userName)
        {
            var avatarFile1 = HttpContext.Request.Form.Files.GetFile("avatar1");
            var avatarFile2 = HttpContext.Request.Form.Files.GetFile("avatar2");

            return Ok(new
            {
                userId = HttpContext.Request.Query["id"].FirstOrDefault(),
                userName,
                avatarFile1 = avatarFile1.FileName,
                avatarFile2 = avatarFile2.FileName
            });
        }

        /// <summary>
        ///     Get Profile 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("profiles")]
        [ApiParameter("id", Description = "User Id", In = ParameterIn.Query, Type = ParameterType.String, IsRequire = true)]
        [ApiParameter("avatar1", Description = "Avatar File", In = ParameterIn.FormData, Type = ParameterType.File, IsRequire = true)]
        [ApiParameter("avatar2", Description = "Avatar File", In = ParameterIn.FormData, Type = ParameterType.File, IsRequire = true)]
        [ApiDocGroup("Profile API")] // Make this Action appear in "Profile API" group
        [ApiDocGroup("User API")] // Make this Action appear in "User API" group
        public IActionResult GetProfile()
        {
            var avatarFile1 = HttpContext.Request.Form.Files.GetFile("avatar1");
            var avatarFile2 = HttpContext.Request.Form.Files.GetFile("avatar2");
            
            return Ok(new
            {
                userId = HttpContext.Request.Query["id"].FirstOrDefault(),
                avatarFile1 = avatarFile1.FileName,
                avatarFile2 = avatarFile2.FileName
            });
        }
    }
}