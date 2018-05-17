#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableTypeInfoModelModel.cs </Name>
//         <Created> 23/03/2018 5:20:19 PM </Created>
//         <Key> f2ecfde2-0af8-4461-ae11-6e049e81a031 </Key>
//     </File>
//     <Summary>
//         DataTableTypeInfoModelModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elect.Web.DataTable.Attributes;
using Elect.Web.DataTable.Models;

namespace Elect.Web.DataTable.Utils.DataTableTypeInfoModelUtils
{
    internal class DataTableTypeInfoModelHelper
    {
        private static readonly ConcurrentDictionary<Type, DataTablePropertyInfoModel[]> PropertiesCache =
            new ConcurrentDictionary<Type, DataTablePropertyInfoModel[]>();

        internal static DataTablePropertyInfoModel[] GetProperties(Type type)
        {
            return PropertiesCache.GetOrAdd(type, t =>
            {
                var listDataTablePropertyInfo = new List<DataTablePropertyInfoModel>();

                foreach (var propertyInfo in t.GetProperties())
                {
                    if (propertyInfo.GetCustomAttribute<DataTableIgnoreAttribute>() != null)
                    {
                        // Ignore Property have DataTableIgnoreAttribute
                        continue;
                    }
                    
                    var attributes = propertyInfo.GetCustomAttributes<DataTableBaseAttribute>().ToArray();

                    var dataTablePropertyInfo = new DataTablePropertyInfoModel(propertyInfo, attributes);

                    listDataTablePropertyInfo.Add(dataTablePropertyInfo);
                }

                return listDataTablePropertyInfo.ToArray();
            });
        }
    }
}