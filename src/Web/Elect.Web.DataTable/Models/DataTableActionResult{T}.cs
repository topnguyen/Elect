#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableActionResult_T_.cs </Name>
//         <Created> 24/03/2018 2:10:51 PM </Created>
//         <Key> 0ddfacd1-381b-41bc-ba45-fbfbd858dd38 </Key>
//     </File>
//     <Summary>
//         DataTableActionResult_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elect.Web.DataTable.Processing.Response;

namespace Elect.Web.DataTable.Models
{
    public class DataTableActionResult<T> : DataTableActionResult where T : class, new()
    {
        public DataTableResponseDataModel<T> Data { get; set; }

        public DataTableActionResult(IQueryable<T> queryable, DataTableParamModel paramModel)
        {
            Data = queryable.GetDataTableResponse(paramModel);
        }

        public DataTableActionResult(DataTableResponseDataModel<T> data)
        {
            Data = data;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpResponse response = context.HttpContext.Response;

            return response.WriteAsync(JsonConvert.SerializeObject(Data));
        }
    }
}