namespace Elect.Sample.Web.Swagger.Controllers
{
    [HideInApiDoc]
    public class HomeController : Controller
    {
        private readonly ElectSwaggerOptions _electSwaggerOptions;
        public HomeController(IOptions<ElectSwaggerOptions> electSwaggerConfig)
        {
            _electSwaggerOptions = electSwaggerConfig.Value;
        }
        public IActionResult Index()
        {
            return Redirect($"{HttpContext.Request.GetDomain()}{_electSwaggerOptions.Url}");
        }
    }
}
