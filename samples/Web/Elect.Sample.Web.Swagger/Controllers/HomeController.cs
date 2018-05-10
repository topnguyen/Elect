using Elect.Web.HttpUtils;
using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Sample.Web.Swagger.Controllers
{
    [HideInApiDoc]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect($"{HttpContext.Request.GetDomain()}/developers");
        }
    }
}