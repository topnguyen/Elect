#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableResponseDataModelExtensions.cs </Name>
//         <Created> 24/03/2018 3:52:44 PM </Created>
//         <Key> 75cb3879-7568-45bd-8fd3-8acefbab228c </Key>
//     </File>
//     <Summary>
//         DataTableResponseDataModelExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Utils.DataTableActionResultUtils;
using System;

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