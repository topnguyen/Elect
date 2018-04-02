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

using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Constants;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Utils.TypeUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Web.DataTable.Utils.DataTableActionResultUtils
{
    internal class DataTableActionResultHelper
    {
        /// <typeparam name="T"></typeparam>
        /// <param name="response">  
        ///     The properties of this can be marked up with [DataTablesAttribute] to control sorting/searchability/visibility
        /// </param>
        /// <param name="transform">     
        ///     // a transform for custom column rendering e.g. to do a custom date row =&gt; new {
        ///     CreatedDate = row.CreatedDate.ToString("dd MM yy") }
        /// </param>
        /// <param name="responseOption"></param>
        /// <returns></returns>
        internal static DataTableActionResult<T> Create<T>(DataTableResponseModel<T> response, Func<T, object> transform, ResponseOptionModel<T> responseOption = null) where T : class, new()
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
                    .Transform<Dictionary<string, object>, Dictionary<string, object>>(StringTransformer.StringifyValues);

            result.Data = ApplyOutputRules(result.Data, responseOption);

            return result;
        }

        internal static DataTableActionResult<T> Create<T>(DataTableResponseModel<T> response, ResponseOptionModel<T> responseOption = null) where T : class, new()
        {
            var result = new DataTableActionResult<T>(response);

            var dictionaryTransformFunc = new DataTableTypeInfoModel<T>().ToFuncDictionary(responseOption);

            result.Data =
                result
                    .Data
                    .Transform(dictionaryTransformFunc)
                    .Transform<Dictionary<string, object>, Dictionary<string, object>>(StringTransformer.StringifyValues);

            result.Data = ApplyOutputRules(result.Data, responseOption);

            return result;
        }

        private static DataTableResponseModel<T> ApplyOutputRules<T>(DataTableResponseModel<T> response, ResponseOptionModel<T> responseOption) where T : class, new()
        {
            responseOption = responseOption ?? new ResponseOptionModel<T> { ArrayOutputType = ArrayOutputType.BiDimensionalArray };

            DataTableResponseModel<T> outputData = response;

            switch (responseOption.ArrayOutputType)
            {
                case ArrayOutputType.ArrayOfObjects:
                    {
                        // Nothing is needed
                        break;
                    }
                default:
                    {
                        outputData = response.Transform<Dictionary<string, object>, object[]>(d => d.Values.ToArray());
                        break;
                    }
            }

            return outputData;
        }
    }
}