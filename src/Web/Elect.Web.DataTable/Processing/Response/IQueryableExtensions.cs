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

using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Utils.DataTableParamModelUtils;
using System.Linq;

namespace Elect.Web.DataTable.Processing.Response
{
    public static class IQueryableExtensions
    {
        public static DataTableResponseModel<T> GetDataTableResponse<T>(this IQueryable<T> data, DataTableRequestModel dataTableRequestModel) where T : class, new()
        {
            // Count or LongCount is very annoying cause an extra evaluation.

            var totalRecords = data.Count();

            var filters = new DataTableParamModelHelper();

            var outputProperties = new DataTableTypeInfoModel<T>().Properties;

            var filteredData = filters.ApplyFiltersAndSort(dataTableRequestModel, data, outputProperties);

            var totalDisplayRecords = filteredData.Count();

            var skipped = filteredData.Skip(dataTableRequestModel.DisplayStart);

            var page = (dataTableRequestModel.DisplayLength <= 0 ? skipped : skipped.Take(dataTableRequestModel.DisplayLength)).ToArray();

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