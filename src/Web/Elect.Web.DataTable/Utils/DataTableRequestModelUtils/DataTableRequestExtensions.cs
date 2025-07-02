namespace Elect.Web.DataTable.Utils.DataTableRequestModelUtils
{
    public static class DataTableRequestExtensions
    {
        /// <summary>
        ///     Get Filter Values to Dictionary, key is property name and value is filter value of the property.
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>If the filter value not found, then not have the property name (key) in Dictionary</remarks>
        public static Dictionary<string, string> GetFilterValues<T>(this DataTableRequestModel model) where T : class, new()
        {
            return DataTableRequestHelper.GetFilterValues<T>(model);
        }
        /// <summary>
        ///     Get Filter Value from DataTableRequest by property name of T object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetFilterValue<T>(this DataTableRequestModel model, string propertyName) where T : class, new()
        {
            return DataTableRequestHelper.GetFilterValue<T>(model, propertyName);
        }  
        /// <summary>
        ///     Set Filter Value to DataTableRequest by property name of T object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetFilterValue<T>(this DataTableRequestModel model, string propertyName, string value) where T : class, new()
        {
            DataTableRequestHelper.SetFilterValue<T>(model, propertyName, value);
        }
        /// <summary>
        ///     Get DateTimeOffsets Filter Value from DataTableRequest by property name of T object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <param name="dateTimeOffsetFrom"></param>
        /// <param name="dateTimeOffsetTo"></param>
        /// <typeparam name="T"></typeparam>
        public static void GetDateTimeOffsetFilter<T>(this DataTableRequestModel model, string propertyName, out DateTimeOffset? dateTimeOffsetFrom, out DateTimeOffset? dateTimeOffsetTo) where T : class, new()
        {
            var filter = model.GetFilterValue<T>(propertyName);
            DataTableRequestHelper.GetDateTimeOffsetFilter(filter, out dateTimeOffsetFrom, out dateTimeOffsetTo);
        }      
        /// <summary>
        ///     Get String Filter Value from DataTableRequest by property name of T object.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyName"></param>
        /// <param name="filterValue1"></param>
        /// <param name="filterValue2"></param>
        /// <typeparam name="T"></typeparam>
        public static void GetFilterValues<T>(this DataTableRequestModel model, string propertyName, out string filterValue1, out string filterValue2) where T : class, new()
        {
            var filter = model.GetFilterValue<T>(propertyName);
            DataTableRequestHelper.GetFilterValue(filter, out filterValue1, out filterValue2);
        }
    }
}
