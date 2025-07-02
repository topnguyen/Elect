namespace Elect.Web.DataTable.Models
{
    public abstract class DataTableActionResult : IActionResult
    {
        public abstract Task ExecuteResultAsync(ActionContext context);
        /// <typeparam name="T"></typeparam>
        /// <param name="request">  </param>
        /// <param name="response"> 
        ///     The properties of this can be marked up with [DataTablesAttribute] to control sorting/searchability/visibility
        /// </param>
        /// <param name="transform">
        ///     // a transform for custom column rendering e.g. to do a custom date row =&gt; new {
        ///     CreatedDate = row.CreatedDate.ToString("dd MM yy") }
        /// </param>
        /// <returns></returns>
        protected DataTableActionResult<T> Create<T>(DataTableRequestModel request, DataTableResponseModel<T> response,
            Func<T, object> transform) where T : class, new()
        {
            return DataTableActionResultHelper.Create(request, response, transform);
        }
        /// <typeparam name="T"></typeparam>
        /// <param name="request"> </param>
        /// <param name="response">
        ///     The properties of this can be marked up with [DataTablesAttribute] to control sorting/searchability/visibility
        /// </param>
        /// <returns></returns>
        protected DataTableActionResult<T> Create<T>(DataTableRequestModel request, DataTableResponseModel<T> response)
            where T : class, new()
        {
            return DataTableActionResultHelper.Create(request, response);
        }
    }
}
