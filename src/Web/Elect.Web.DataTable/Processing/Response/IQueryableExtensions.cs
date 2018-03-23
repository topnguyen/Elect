#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IQueryableExtensions.cs </Name>
//         <Created> 23/03/2018 5:43:49 PM </Created>
//         <Key> 32a7cb87-f826-4ee8-9da0-28ee3e8fd319 </Key>
//     </File>
//     <Summary>
//         IQueryableExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.DataTableParamModelUtils;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using System;
using System.Linq;

namespace Elect.Web.DataTable.Processing.Response
{
    public static class IQueryableExtensions
    {
        public static DataTableResponseDataModel<T> GetDataTableResponse<T>(this IQueryable<T> data, DataTableParamModel dataTableParamModel) where T : class, new()
        {
            var totalRecords = data.Count(); // annoying this, as it causes an extra evaluation..

            var filters = new DataTableParamModelHelper();

            var outputProperties = DataTableTypeInfo<T>.Properties;

            var filteredData = filters.ApplyFiltersAndSort(dataTableParamModel, data, outputProperties);

            var totalDisplayRecords = filteredData.Count();

            var skipped = filteredData.Skip(dataTableParamModel.DisplayStart);

            var page = (dataTableParamModel.DisplayLength <= 0 ? skipped : skipped.Take(dataTableParamModel.DisplayLength)).ToArray();

            var result = new DataTableResponseDataModel<T>
            {
                TotalRecord = totalRecords,
                TotalDisplayRecord = totalDisplayRecords,
                Echo = dataTableParamModel.Echo,
                Data = page.Cast<object>().ToArray()
            };

            return result;
        }

        public static DataTableActionResult<T> GetDataTableActionResult<T>(this DataTableResponseDataModel<T> responseData, Func<T, object> transform, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            return DataTableActionResult.Create(responseData, transform, responseOption);
        }

        public static DataTableActionResult<T> GetDataTableActionResult<T>(this DataTableResponseDataModel<T> responseData, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            return DataTableActionResult.Create(responseData, responseOption);
        }
    }
}