namespace Elect.Web.DataTable.Processing.Response
{
    public static class DataTableResponseDataModelExtensions
    {
        public static DataTableActionResult<T> GetDataTableActionResult<T>(this DataTableResponseModel<T> response,
            DataTableRequestModel request, Func<T, object> transform)
            where T : class, new()
        {
            return DataTableActionResultHelper.Create(request, response, transform);
        }
        public static DataTableActionResult<T> GetDataTableActionResult<T>(this DataTableResponseModel<T> response,
            DataTableRequestModel request) where T : class, new()
        {
            return DataTableActionResultHelper.Create(request, response);
        }
    }
}
