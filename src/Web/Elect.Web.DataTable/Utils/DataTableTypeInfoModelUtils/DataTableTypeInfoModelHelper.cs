#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
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
using Elect.Web.DataTable.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

                var listPropertyInfo = t.GetProperties();

                // Scan Interface
                var listInterface = type.GetInterfaces();
                
                var allInterfaceListPropertyInfo = new List<PropertyInfo>();

                foreach (var @interface in listInterface)
                {
                    var interfaceListPropertyInfo = 
                        @interface
                            .GetProperties()
                            .Where(x => x.GetCustomAttribute<DataTableBaseAttribute>() != null);
                    
                    allInterfaceListPropertyInfo.AddRange(interfaceListPropertyInfo);
                }
                

                foreach (var propertyInfo in listPropertyInfo)
                {
                    if (propertyInfo.GetCustomAttribute<DataTableIgnoreAttribute>() != null)
                    {
                        // Ignore Property have DataTableIgnoreAttribute
                        continue;
                    }

                    var attributes = propertyInfo.GetCustomAttributes<DataTableBaseAttribute>().ToArray();

                    // If Property in Class not have any DataTable Attributes, then find it in the Interface
                    if (!attributes.Any())
                    {
                        var matchPropertyInfo = allInterfaceListPropertyInfo.FirstOrDefault(x => x.Name == propertyInfo.Name);

                        if (matchPropertyInfo != null)
                        {
                            if (matchPropertyInfo.GetCustomAttribute<DataTableIgnoreAttribute>() != null)
                            {
                                // Ignore Property have DataTableIgnoreAttribute
                                continue;
                            }
                            
                            attributes =  matchPropertyInfo.GetCustomAttributes<DataTableBaseAttribute>().ToArray();
                        }
                    }

                    // Add to List Property
                    var dataTablePropertyInfo = new DataTablePropertyInfoModel(propertyInfo, attributes)
                    {
                        Order = attributes.OfType<DataTableAttribute>().Select(a => a.Order).SingleOrDefault()
                    };

                    listDataTablePropertyInfo.Add(dataTablePropertyInfo);
                }

                // Order

                listDataTablePropertyInfo = listDataTablePropertyInfo.OrderBy(x => x.Order).ToList();
                
                return listDataTablePropertyInfo.ToArray();
            });
        }
    }
}