#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableRequestHelper.cs </Name>
//         <Created> 23/03/2018 5:45:41 PM </Created>
//         <Key> bc4d8d81-736f-490a-89b3-97a0282d6327 </Key>
//     </File>
//     <Summary>
//         DataTableRequestHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Elect.Core.ActionUtils;
using Elect.Core.TypeUtils;
using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Processing.Response;

namespace Elect.Web.DataTable.Utils.DataTableRequestModelUtils
{
    public class DataTableRequestHelper
    {
        private static readonly List<ReturnedFilteredQueryForType> Filters = new List<ReturnedFilteredQueryForType>
        {
            Guard(IsBoolType, Filter.BoolFilter),
            Guard(IsDateTimeType, Filter.DateTimeFilter),
            Guard(IsDateTimeOffsetType, Filter.DateTimeOffsetFilter),
            Guard(IsNumericType, Filter.NumericFilter),
            Guard(IsEnumType, Filter.EnumFilter),
            Guard(arg => arg.Type == typeof(string), Filter.StringFilter)
        };
        
        /// <summary>
        ///     Get Filter Values to Dictionary, key is property name and value is filter value of the property.
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>If the filter value not found, then not have the property name (key) in Dictionary</remarks>
        public static Dictionary<string, string> GetFilterValues<T>(DataTableRequestModel model) where T : class, new()
        {
            var properties = new DataTableTypeInfoModel<T>().Properties;

            var filterValues = new Dictionary<string, string>();

            for (var i = 0; i < properties.Length; i++)
            {
                var index = i;
                
                ActionHelper.IgnoreError(() =>
                {
                    var filterValue = model.SearchValues[index];
                    filterValues.Add(properties[index].PropertyInfo.Name, filterValue);   
                });
            }

            return filterValues;
        }

        public static string GetFilterValue<T>(DataTableRequestModel model, string propertyName) where T : class, new()
        {
            var filerValues = GetFilterValues<T>(model);

            return filerValues.TryGetValue(propertyName, out var filterValue) ? filterValue : null;
        }
        
        public static void SetFilterValue<T>(DataTableRequestModel model, string propertyName, string value) where T : class, new()
        {
            var properties = new DataTableTypeInfoModel<T>().Properties;

            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyInfo.Name != propertyName)
                {
                    continue;
                }
                
                model.SearchValues[i] = value;
                    
                break;
            }
        }

        public static void GetFilterValue(string filter, out string filterValue1, out string filterValue2)
        {
            filterValue1 = null;
            filterValue2 = null;
            
            if (filter == "~")
            {
                return;
            }

            // Range Case
            if (filter.Contains("~"))
            {
                var parts = filter.Split('~');

                filterValue1 = parts[0];

                filterValue2 = parts[1];

                return;
            }

            // Single Case
            filterValue1 = filter;
        }

        public static void GetDateTimeOffsetFilter(string filter, out DateTimeOffset? dateTimeOffsetFrom, out DateTimeOffset? dateTimeOffsetTo)
        {
            dateTimeOffsetFrom = null;
            dateTimeOffsetTo = null;
            
            if (filter == "~")
            {
                return;
            }

            // Range Case
            if (filter.Contains("~"))
            {
                var parts = filter.Split('~');

                dateTimeOffsetFrom = Filter.ToDateTimeOffset(parts[0]);

                // TO DATE TIME
                dateTimeOffsetTo = Filter.ToDateTimeOffset(parts[1]);

                return;
            }

            // Single Case
            dateTimeOffsetFrom = Filter.ToDateTimeOffset(filter);
        }
        
        #region Internal and Private

        internal IQueryable<T> ApplyFiltersAndSort<T>(DataTableRequestModel dtParameters, IQueryable<T> data,
            DataTablePropertyInfoModel[] columns)
        {
            if (!string.IsNullOrEmpty(dtParameters.Search))
            {
                var parts = new List<string>();

                var parameters = new List<object>();

                for (var i = 0; i < dtParameters.Columns; i++)
                {
                    if (dtParameters.ListIsSearchable[i])
                    {
                        try
                        {
                            var column = FindColumn(dtParameters, columns, i);
                            parts.Add(GetFilterClause(dtParameters.Search, column, parameters));
                        }
                        catch (Exception)
                        {
                            // if the clause doesn't work, skip it!
                            // ex: can't parse a string to enum or datetime type
                        }
                    }
                }

                var values = parts.Where(p => p != null);
                
                data = data.Where(string.Join(" or ", values), parameters.ToArray());
            }

            for (var i = 0; i < dtParameters.SearchValues.Count; i++)
                if (dtParameters.ListIsSearchable[i])
                {
                    var searchColumn = dtParameters.SearchValues[i];
                    if (!string.IsNullOrWhiteSpace(searchColumn))
                    {
                        var column = FindColumn(dtParameters, columns, i);
                        var parameters = new List<object>();
                        var filterClause = GetFilterClause(searchColumn, column, parameters);
                        if (!string.IsNullOrWhiteSpace(filterClause))
                        {
                            data = data.Where(filterClause, parameters.ToArray());
                        }
                    }
                }

            var sortString = "";
            for (var i = 0; i < dtParameters.SortingCols; i++)
            {
                var columnNumber = dtParameters.SortCol[i];

                if (dtParameters.ColReorderIndexs?.Any() == true)
                {
                    columnNumber = dtParameters.ColReorderIndexs[columnNumber];
                }

                var column = FindColumn(dtParameters, columns, columnNumber);
                var columnName = column.PropertyInfo.Name;
                var sortDir = dtParameters.SortDir[i];
                if (i != 0)
                {
                    sortString += ", ";
                }
                sortString += columnName + " " + sortDir;
            }

            if (string.IsNullOrWhiteSpace(sortString))
            {
                sortString = columns[0].PropertyInfo.Name;
            }
            
            data = data.OrderBy(sortString);

            return data;
        }

        private static DataTablePropertyInfoModel FindColumn(DataTableRequestModel dtParameters, DataTablePropertyInfoModel[] columns, int i)
        {
            if (dtParameters.ColumnNames.Any())
            {
                return columns.First(x => x.PropertyInfo.Name == dtParameters.ColumnNames[i]);
            }

            return columns[i];
        }

        internal static void RegisterFilter<T>(GuardedFilter filter)
        {
            Filters.Add(Guard(arg => arg is T, filter));
        }

        private static ReturnedFilteredQueryForType Guard(Func<DataTablePropertyInfoModel, bool> guard, GuardedFilter filter)
        {
            return (q, c, t, p) => !guard(t) ? null : filter(q, c, t, p);
        }

        private static string GetFilterClause(string query, DataTablePropertyInfoModel column, List<object> parametersForLinqQuery)
        {
            string Clause(string queryPart)
            {
                return Filters
                           .Select(f => f(queryPart, column.PropertyInfo.Name, column, parametersForLinqQuery))
                           .FirstOrDefault(filterPart => filterPart != null) ?? string.Empty;
            }

            var queryParts = query.Split('|').Select(Clause).Where(clause => clause != string.Empty).ToArray();

            if (queryParts.Any()) return "(" + string.Join(") OR (", queryParts) + ")";

            return null;
        }

        private static bool IsNumericType(DataTablePropertyInfoModel propertyInfo)
        {
            var isNumericType = propertyInfo.Type.IsNumericType();
            return isNumericType;
        }

        private static bool IsEnumType(DataTablePropertyInfoModel propertyInfo)
        {
            var isEnumType = propertyInfo.Type.GetNotNullableType().IsEnum;

            return isEnumType;
        }

        private static bool IsBoolType(DataTablePropertyInfoModel propertyInfo)
        {
            var isBoolType = propertyInfo.Type == typeof(bool) || propertyInfo.Type == typeof(bool?);
            return isBoolType;
        }

        private static bool IsDateTimeType(DataTablePropertyInfoModel propertyInfo)
        {
            var isDateTimeType = propertyInfo.Type == typeof(DateTime) || propertyInfo.Type == typeof(DateTime?);
            return isDateTimeType;
        }

        private static bool IsDateTimeOffsetType(DataTablePropertyInfoModel propertyInfo)
        {
            var isDateTimeOffsetType = propertyInfo.Type == typeof(DateTimeOffset) ||
                                       propertyInfo.Type == typeof(DateTimeOffset?);
            return isDateTimeOffsetType;
        }

        internal delegate string GuardedFilter(string query, string columnName, DataTablePropertyInfoModel columnType,
            List<object> parametersForLinqQuery);

        private delegate string ReturnedFilteredQueryForType(string query, string columnName,
            DataTablePropertyInfoModel columnType, List<object> parametersForLinqQuery);

        #endregion
    }
}