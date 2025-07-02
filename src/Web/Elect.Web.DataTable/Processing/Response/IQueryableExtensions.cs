namespace Elect.Web.DataTable.Processing.Response
{
    public static class IQueryableExtensions
    {
        public static DataTableResponseModel<T> GetDataTableResponse<T>(this IQueryable<T> data,
            DataTableRequestModel dataTableRequestModel) where T : class, new()
        {
            // Count or LongCount is very annoying cause an extra evaluation.
            var totalRecords = data.Count();
            var filters = new DataTableRequestHelper();
            var outputProperties = new DataTableTypeInfoModel<T>().Properties;
            var filteredData = filters.ApplyFiltersAndSort(dataTableRequestModel, data, outputProperties);
            var totalDisplayRecords = filteredData.Count();
            var skipped = filteredData.Skip(dataTableRequestModel.DisplayStart);
            var page = (dataTableRequestModel.DisplayLength <= 0
                ? skipped
                : skipped.Take(dataTableRequestModel.DisplayLength)).ToArray();
            var result = new DataTableResponseModel<T>
            {
                TotalRecord = totalRecords,
                TotalDisplayRecord = totalDisplayRecords,
                Echo = dataTableRequestModel.Echo,
                Data = page.Cast<object>().ToArray()
            };
            return result;
        }
    }
}
