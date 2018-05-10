using Microsoft.AspNetCore.Mvc;

namespace Elect.Sample.Web.Swagger.Controllers
{
    [ShowInApiDoc]
    public class HomeController : Controller
    {
        public IActionResult GetUser()
        {
            return Ok();
        }
    }
}