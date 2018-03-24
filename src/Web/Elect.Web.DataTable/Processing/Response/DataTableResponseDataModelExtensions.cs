#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
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

using System;
using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Utils.DataTableActionResultUtils;

namespace Elect.Web.DataTable.Processing.Response
{
    public static class DataTableResponseDataModelExtensions
    {
        public static DataTableActionResult<T> GetDataTableActionResult<T>(this DataTableResponseDataModel<T> responseData, Func<T, object> transform, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            return DataTableActionResultHelper.Create(responseData, transform, responseOption);
        }

        public static DataTableActionResult<T> GetDataTableActionResult<T>(this DataTableResponseDataModel<T> responseData, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            return DataTableActionResultHelper.Create(responseData, responseOption);
        }
    }
}