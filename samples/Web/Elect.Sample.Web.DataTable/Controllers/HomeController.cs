namespace Elect.Sample.Web.DataTable.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<UserModel> DummyUsers = GetDummyUsers();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            return Ok("Test");
        }
        /// <summary>
        ///     Get Users DataTable 
        /// </summary>
        /// <returns></returns>
        [Route("datatable")]
        [HttpPost]
        public DataTableActionResult<UserModel> GetDataTable([FromForm] DataTableRequestModel model)
        {
            // 1. In Service Layer
            DataTableResponseModel<UserModel> response = GetDataTableResponse(model);
            // 2. In Controller
            var result = response.GetDataTableActionResult(model, x => new
            {
                IsActive = x.IsActive ? "Yes" : "No" // Transform Data before Response
            });
            return result;
        }
        private static DataTableResponseModel<UserModel> GetDataTableResponse(DataTableRequestModel model)
        {
            // Queryable Data
            var query = DummyUsers.AsQueryable();
            // Generate DataTable Response
            var result = query.GetDataTableResponse(model);
            return result;
        }
        private static List<UserModel> GetDummyUsers()
        {
            var users = new List<UserModel>();
            for (int i = 0; i < 1000; i++)
            {
                users.Add(new UserModel
                {
                    Id = i + 1,
                    FullName = $"User {i + 1}",
                    CreatedTime = DateTimeOffset.Now,
                    IsActive = i % 2 == 0
                });
            }
            return users;
        }
    }
}
