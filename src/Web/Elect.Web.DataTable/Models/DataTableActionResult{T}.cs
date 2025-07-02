namespace Elect.Web.DataTable.Models
{
    public class DataTableActionResult<T> : DataTableActionResult where T : class, new()
    {
        public DataTableActionResult(IQueryable<T> queryable, DataTableRequestModel requestModel)
        {
            Data = queryable.GetDataTableResponse(requestModel);
        }
        public DataTableActionResult(DataTableResponseModel<T> data)
        {
            Data = data;
        }
        public DataTableResponseModel<T> Data { get; set; }
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var response = context.HttpContext.Response;
            return response.WriteAsync(JsonConvert.SerializeObject(Data));
        }
    }
}
