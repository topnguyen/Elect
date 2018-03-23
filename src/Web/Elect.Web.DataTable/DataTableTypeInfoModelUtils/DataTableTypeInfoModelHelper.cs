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

using Elect.Web.DataTable.Attributes;
using Elect.Web.DataTable.Models.Constants;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Elect.Web.DataTable.DataTableTypeInfoModelUtils
{
    internal class DataTableTypeInfoModelHelper
    {
        private static readonly ConcurrentDictionary<Type, DataTablePropertyInfoModel[]> PropertiesCache = new ConcurrentDictionary<Type, DataTablePropertyInfoModel[]>();

        internal static DataTablePropertyInfoModel[] GetProperties(Type type)
        {
            return PropertiesCache.GetOrAdd(type, t =>
            {
                var info = from propertyInfo in t.GetProperties()
                           where propertyInfo.GetCustomAttribute<DataTableIgnoreAttribute>() == null
                           let attributes = propertyInfo.GetCustomAttributes().OfType<DataTableBaseAttribute>().ToArray()
                           orderby attributes.OfType<DataTableAttribute>().Select(a => a.Order as int?).SingleOrDefault() ?? ConfigConstants.DefaultOrder
                           select new DataTablePropertyInfoModel(propertyInfo, attributes);

                return info.ToArray();
            });
        }
    }
}