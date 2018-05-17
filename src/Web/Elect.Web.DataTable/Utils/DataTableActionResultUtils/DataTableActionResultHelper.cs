#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableActionResultHelper.cs </Name>
//         <Created> 24/03/2018 3:54:18 PM </Created>
//         <Key> e17f02f0-73da-4fb3-b8d3-24f4d30b61a4 </Key>
//     </File>
//     <Summary>
//         DataTableActionResultHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using System.Collections.Generic;
using System.Linq;
using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Utils.TypeUtils;

namespace Elect.Web.DataTable.Utils.DataTableActionResultUtils
{
    internal class DataTableActionResultHelper
    {
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="response">
        ///     The properties of this can be marked up with [DataTablesAttribute] to control sorting/searchability/visibility
        /// </param>
        /// <param name="transform">
        ///     // a transform for custom column rendering e.g. to do a custom date row =&gt; new {
        ///     CreatedDate = row.CreatedDate.ToString("dd MM yy") }
        /// </param>
        /// <returns></returns>
        internal static DataTableActionResult<T> Create<T>(DataTableRequestModel request,
            DataTableResponseModel<T> response, Func<T, object> transform)
            where T : class, new()
        {
            transform = transform ?? (s => s);

            var result = new DataTableActionResult<T>(response);

            result.Data =
                result
                    .Data
                    .Transform<T, Dictionary<string, object>>
                    (
                        row => TransformTypeInfoHelper.MergeTransformValuesIntoDictionary(transform, row)
                    )
                    .Transform<Dictionary<string, object>, Dictionary<string, object>>(
                        StringTransformer.StringifyValues);

            result.Data = ApplyOutputRules(request, result.Data);

            return result;
        }

        internal static DataTableActionResult<T> Create<T>(DataTableRequestModel request,
            DataTableResponseModel<T> response) where T : class, new()
        {
            var result = new DataTableActionResult<T>(response);

            var dictionaryTransformFunc = new DataTableTypeInfoModel<T>().ToFuncDictionary();

            result.Data =
                result
                    .Data
                    .Transform(dictionaryTransformFunc)
                    .Transform<Dictionary<string, object>, Dictionary<string, object>>(
                        StringTransformer.StringifyValues);

            result.Data = ApplyOutputRules(request, result.Data);

            return result;
        }

        private static DataTableResponseModel<T> ApplyOutputRules<T>(DataTableRequestModel request,
            DataTableResponseModel<T> response) where T : class, new()
        {
            if (request.ColReorderIndexs?.Any() == true)
            {
                var actualData = new List<Dictionary<string, object>>();

                foreach (var row in response.Data)
                {
                    var correctIndexDictionary = new Dictionary<string, object>();

                    if (!(row is Dictionary<string, object> rowDictionary))
                    {
                        continue;
                    }

                    foreach (var acctualIndex in request.ColReorderIndexs)
                    {
                        if (rowDictionary.Count <= acctualIndex)
                        {
                            correctIndexDictionary.Add(acctualIndex.ToString(), string.Empty);

                            continue;
                        }
                        
                        var acctualCol = rowDictionary.ElementAt(acctualIndex);

                        correctIndexDictionary.Add(acctualCol.Key, acctualCol.Value);
                    }

                    actualData.Add(correctIndexDictionary);
                }

                response.Data = actualData.Cast<object>().ToArray();
            }

            var outputData = response.Transform<Dictionary<string, object>, object[]>(d => d.Values.ToArray());

            return outputData;
        }
    }
}