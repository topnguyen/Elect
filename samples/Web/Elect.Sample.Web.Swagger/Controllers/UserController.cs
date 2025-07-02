namespace Elect.Sample.Web.Swagger.Controllers
{
    [ShowInApiDoc]
    [ApiDocGroup("User API")]
    public class UserController : Controller
    {
        /// <summary>
        ///     Test Response 
        /// </summary>
        [HttpPost]
        [ApiDocGroup("Test")]
        [Route("test")]
        public IActionResult TestResponse([FromBody]UserModel user)
        {
            return Ok(user);
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
    public class UserModel
    {
        public string Name { get; set; }
        public UserType Type { get; set; }
    }
    public enum UserType
    {
        Admin,
        Member
    }
}
