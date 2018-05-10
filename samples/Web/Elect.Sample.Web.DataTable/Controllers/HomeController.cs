using Microsoft.AspNetCore.Mvc;

namespace Elect.Sample.Web.DataTable.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}