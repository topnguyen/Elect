#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TransformTypeInfoHelper.cs </Name>
//         <Created> 24/03/2018 2:13:00 PM </Created>
//         <Key> 99460754-c57a-488d-a31d-4b43933baa2c </Key>
//     </File>
//     <Summary>
//         TransformTypeInfoHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Elect.Web.DataTable.Utils.TypeUtils
{
    public class TransformTypeInfoHelper
    {
        public static Dictionary<string, object> MergeTransformValuesIntoDictionary<TInput, TTransform>(Func<TInput, TTransform> transformInput, TInput input)
        {
            // Get the the properties from the input as a dictionary
            var dict = DataTableTypeInfoModel<TInput>.ToDictionary(input);

            // Get the transform object
            var transform = transformInput(input);

            if (transform == null)
            {
                return dict;
            }

            foreach (var propertyInfo in transform.GetType().GetTypeInfo().GetProperties())
            {
                dict[propertyInfo.Name] = propertyInfo.GetValue(transform, null);
            }

            return dict;
        }
    }
}