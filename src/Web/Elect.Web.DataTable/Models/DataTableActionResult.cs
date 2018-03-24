#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableActionResult.cs </Name>
//         <Created> 24/03/2018 2:10:36 PM </Created>
//         <Key> a9ff8188-eba0-43fd-b4c9-c3c460a44622 </Key>
//     </File>
//     <Summary>
//         DataTableActionResult.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Utils.DataTableActionResultUtils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Elect.Web.DataTable.Models
{
    public abstract class DataTableActionResult : IActionResult
    {
        public abstract Task ExecuteResultAsync(ActionContext context);

        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">  
        ///     The properties of this can be marked up with [DataTablesAttribute] to control sorting/searchability/visibility
        /// </param>
        /// <param name="transform">     
        ///     // a transform for custom column rendering e.g. to do a custom date row =&gt; new {
        ///     CreatedDate = row.CreatedDate.ToString("dd MM yy") }
        /// </param>
        /// <param name="responseOption"></param>
        /// <returns></returns>
        protected DataTableActionResult<T> Create<T>(DataTableResponseDataModel<T> responseData, Func<T, object> transform, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            return DataTableActionResultHelper.Create(responseData, transform, responseOption);
        }

        protected DataTableActionResult<T> Create<T>(DataTableResponseDataModel<T> responseData, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            return DataTableActionResultHelper.Create(responseData, responseOption);
        }
    }
}