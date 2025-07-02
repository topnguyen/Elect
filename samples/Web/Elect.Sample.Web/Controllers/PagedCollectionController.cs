namespace Elect.Sample.Web.Controllers
{
    public class PagedCollectionController : Controller
    {
        public IActionResult Index([FromQuery] PagedRequestModel model)
        {
            model.Skip = model.Take <= 0 ? 0 : model.Skip;
            model.Take = model.Take <= 0 ? 10 : model.Take;
            // Sample Data
            var users = new List<UserModel>();
            for (int i = 0; i < 100; i++)
            {
                users.Add(new UserModel
                {
                    Id = i + 1,
                    FullName = $"User {i + 1}"
                });
            }
            // Queryable Data
            var query = users.AsQueryable();
            // Paged Result
            var total = query.Count();
            var data = query.OrderBy(x => x.Id).Skip(model.Skip).Take(model.Take).ToList();
            var pagedResult = new PagedResponseModel<UserModel>
            {
                Total = total,
                Items = data,
                AdditionalData = new Dictionary<string, object>
                {
                    {"Sum of Id", data.Select(x => x.Id).DefaultIfEmpty(0).Sum()}
                    // More additional Data if need
                }
            };
            // Generate Paged Meta
            var pagedMeta = Url.GetPagedMeta(model, pagedResult);
            return Ok(pagedMeta);
        }
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
