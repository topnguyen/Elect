#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> Filter.cs </Name>
//         <Created> 23/03/2018 4:39:52 PM </Created>
//         <Key> 5b8c7128-8c70-43fd-b09f-7301ea6a1191 </Key>
//     </File>
//     <Summary>
//         Filter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.DateTimeUtils;
using Elect.Core.TypeUtils;
using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Constants;
using Elect.Web.DataTable.Models.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Elect.Web.DataTable.Processing.Response
{
    internal static class Filter
    {
        public static string NumericFilter(string terms, string columnName, DataTablePropertyInfoModel propertyInfo, List<object> parametersForLinqQuery)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }
            
            var termsNorm = terms.Trim().ToLowerInvariant();
            
            if (termsNorm.StartsWith("^"))
            {
                termsNorm = termsNorm.TrimStart('^');
            }

            if (termsNorm.EndsWith("$"))
            {
                termsNorm = termsNorm.TrimEnd('$');
            }

            if (termsNorm == "~")
            {
                return string.Empty;
            }

            string clause = null;

            if (termsNorm.Contains("~"))
            {
                var parts = termsNorm.Split('~');
                try
                {
                    parametersForLinqQuery.Add(ChangeType(parts[0], propertyInfo));
                    clause = $"{columnName} >= @{parametersForLinqQuery.Count - 1}";
                }
                catch (FormatException)
                {
                }

                try
                {
                    parametersForLinqQuery.Add(ChangeType(parts[1], propertyInfo));
                    if (clause != null) clause += " and ";
                    {
                        clause += $"{columnName} <= @{parametersForLinqQuery.Count - 1}";
                    }
                }
                catch (FormatException)
                {
                }

                return clause ?? "true";
            }

            try
            {
                parametersForLinqQuery.Add(ChangeType(termsNorm, propertyInfo));
                return $"{columnName} == @{parametersForLinqQuery.Count - 1}";
            }
            catch (FormatException)
            {
            }

            return null;
        }

        public static string DateTimeOffsetFilter(string terms, string columnName, DataTablePropertyInfoModel propertyInfo, List<object> parametersForLinqQuery)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }
            
            var termsNorm = terms.Trim().ToLowerInvariant();

            if (termsNorm == "~")
            {
                return string.Empty;
            }

            string filterString = null;

            // Range Case
            if (termsNorm.Contains("~"))
            {
                var parts = termsNorm.Split('~');

                // FROM DATE TIME
                var fromDateTime = ToDateTimeOffset(parts[0]);

                if (fromDateTime != default)
                {
                    parametersForLinqQuery.Add(fromDateTime);
                    filterString = $"{columnName} >= @{parametersForLinqQuery.Count - 1}";
                }

                // TO DATE TIME
                var toDateTime = ToDateTimeOffset(parts[1]);

                if (toDateTime == default)
                {
                    return filterString ?? string.Empty;
                }

                parametersForLinqQuery.Add(toDateTime);
                filterString = (filterString == null ? null : $"{filterString} and ") + $"{columnName} <= @{parametersForLinqQuery.Count - 1}";

                return filterString;
            }

            // Single Case
            var dateTime = ToDateTimeOffset(termsNorm);

            if (dateTime == default)
            {
                return null;
            }

            // DateTime only have Date value => search value in same Date
            if (dateTime.Date == dateTime.DateTime)
            {
                parametersForLinqQuery.Add(dateTime);
                parametersForLinqQuery.Add(dateTime.AddDays(1));
                filterString = $"{columnName} >= @{parametersForLinqQuery.Count - 2} and {columnName} < @{parametersForLinqQuery.Count - 1}";

                return filterString;
            }

            // DateTime have Date and Time value => search value in same Date and Time.
            
            // If you store DateTime in database include milliseconds => no result match. Ex: in
            // Database "2017-10-16 10:51:09.9761005 +00:00" so user search "2017-10-16 10:51" will
            // return 0 result, because not exactly same (even user give full datetime with
            // milliseconds exactly - this is Linq2SQL issue).
            // Solution: filter in range of second

            parametersForLinqQuery.Add(dateTime);
            parametersForLinqQuery.Add(dateTime.AddSeconds(1));
            filterString = $"{columnName} >= @{parametersForLinqQuery.Count - 2} and {columnName} < @{parametersForLinqQuery.Count - 1}";

            return filterString;

          
        }

        public static string DateTimeFilter(string terms, string columnName, DataTablePropertyInfoModel propertyInfo,
            List<object> parametersForLinqQuery)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }
            
            var termsNorm = terms.Trim().ToLowerInvariant();

            if (termsNorm == "~")
            {
                return string.Empty;
            }

            string filterString = null;

            // Range Case
            if (termsNorm.Contains("~"))
            {
                var parts = termsNorm.Split('~');

                // FROM DATE TIME
                var fromDateTime = ToDateTime(parts[0]);

                if (fromDateTime != default)
                {
                    parametersForLinqQuery.Add(fromDateTime);

                    filterString = $"{columnName} >= @{parametersForLinqQuery.Count - 1}";
                }

                // TO DATE TIME
                var toDateTime = ToDateTime(parts[1]);

                if (toDateTime == default)
                {
                    return filterString ?? string.Empty;
                }

                parametersForLinqQuery.Add(toDateTime);

                filterString = (filterString == null ? null : $"{filterString} and ") + $"{columnName} <= @{parametersForLinqQuery.Count - 1}";

                return filterString;
            }

            // Single Case
            var dateTime = ToDateTime(termsNorm);

            if (dateTime == default)
            {
                return null;
            }

            // DateTime only have Date value => search value in same Date
            if (dateTime.Date == dateTime)
            {
                parametersForLinqQuery.Add(dateTime);
                parametersForLinqQuery.Add(dateTime.AddDays(1));
                filterString = $"{columnName} >= @{parametersForLinqQuery.Count - 2} and {columnName} < @{parametersForLinqQuery.Count - 1}";

                return filterString;
            }

            // DateTime have Date and Time value => search value in same Date and Time.
            
            // If you store DateTime in database include milliseconds => no result match. Ex: in
            // Database "2017-10-16 10:51:09.9761005 +00:00" so user search "2017-10-16 10:51" will
            // return 0 result, because not exactly same (even user give full datetime with
            // milliseconds exactly - this is Linq2SQL issue).
            // Solution: filter in range of second

            parametersForLinqQuery.Add(dateTime);
            parametersForLinqQuery.Add(dateTime.AddSeconds(1));
            filterString = $"{columnName} >= @{parametersForLinqQuery.Count - 2} and {columnName} < @{parametersForLinqQuery.Count - 1}";

            return filterString;
        }

        public static string BoolFilter(string terms, string columnName, DataTablePropertyInfoModel propertyInfo, List<object> parametersForLinqQuery)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }
            
            var termsNorm = terms.Trim().ToLowerInvariant();
            
            termsNorm = termsNorm.TrimStart('^').TrimEnd('$');

            var termsLowerCase = termsNorm?.ToLowerInvariant();

            if (termsLowerCase == DataConstants.FalseLower || termsLowerCase == DataConstants.TrueLower)
                return termsNorm == DataConstants.TrueLower
                    ? $"{columnName} == {DataConstants.TrueLower}"
                    : $"{columnName} == {DataConstants.FalseLower}";

            if (propertyInfo.Type != typeof(bool?)) return null;

            return DataConstants.Null.Equals(termsNorm, StringComparison.CurrentCultureIgnoreCase)
                ? $"{columnName} == {DataConstants.Null}"
                : null;
        }

        public static string StringFilter(string terms, string columnName, DataTablePropertyInfoModel propertyInfo, List<object> parametersForLinqQuery)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }
            
            var termsNorm = terms.Trim().ToLowerInvariant();
            
            if (termsNorm == ".*")
            {
                return string.Empty;
            }

            string parameterArg;

            string clause;
            
            if (termsNorm.StartsWith("^"))
            {
                if (termsNorm.EndsWith("$"))
                {
                    parametersForLinqQuery.Add(termsNorm.Substring(1, termsNorm.Length - 2));

                    parameterArg = $"@{parametersForLinqQuery.Count - 1}";

                    return $"{columnName} == {parameterArg}";
                }

                parametersForLinqQuery.Add(termsNorm.Substring(1));

                parameterArg = "@" + (parametersForLinqQuery.Count - 1);

                clause = $"({columnName} != {DataConstants.Null} && {columnName} != \"\" && ({columnName} ==  {parameterArg} || {columnName}.{ConditionalConstants.StartsWith}({parameterArg})))";
            }
            else
            {
                parametersForLinqQuery.Add(termsNorm);

                parameterArg = "@" + (parametersForLinqQuery.Count - 1);

                clause = $"({columnName} != {DataConstants.Null} && {columnName} != \"\" && ({columnName} ==  {parameterArg} || {columnName}.ToLower().{ConditionalConstants.Contain}({parameterArg})))";
            }
           
            return clause;
        }

        /// <summary>
        ///     Filter Enum by Label (Display Name ?? Description ?? Name) with conditional Equals,
        ///     StartsWith, Contains
        /// </summary>
        /// <param name="terms">                 </param>
        /// <param name="columnName">            </param>
        /// <param name="propertyInfo">          </param>
        /// <param name="parametersForLinqQuery"></param>
        /// <returns></returns>
        /// <remarks>
        ///     terms is "null" with Type is Nullable Enum work as search null value
        /// </remarks>
        public static string EnumFilter(string terms, string columnName, DataTablePropertyInfoModel propertyInfo, List<object> parametersForLinqQuery)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }
            
            var termsNorm = terms.Trim().ToLowerInvariant();
            
            if (termsNorm.StartsWith("^"))
            {
                termsNorm = termsNorm.Substring(1);
            }

            if (termsNorm.EndsWith("$")) termsNorm = termsNorm.Substring(0, termsNorm.Length - 1);

            if (propertyInfo.Type.IsNullableType() && propertyInfo.Type.IsEnum)
            {
                // Enum Nullable type, handle for "null" case ("null" string as null obj)
                if (DataConstants.Null.Equals(termsNorm, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(termsNorm))
                {
                    return $"{columnName} == {DataConstants.Null}";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(termsNorm)) return null;
            }

            var type = propertyInfo.Type.GetNotNullableType();

            object enumObject = null;

            // Search condition for Enum: Equals, StartsWith, Contains
            foreach (var enumName in Enum.GetNames(type))
            {
                var enumValue = (Enum)TypeDescriptor.GetConverter(type).ConvertFrom(enumName);

                var valueLowerCase = enumName.ToLowerInvariant();

                if (valueLowerCase.Equals(termsNorm, StringComparison.OrdinalIgnoreCase) || valueLowerCase.Contains(termsNorm))
                {
                    enumObject = enumValue;

                    // Found, return first found item
                    break;
                }
            }

            // Can't parse string to enum, return null
            if (enumObject == null) return null;

            parametersForLinqQuery.Add(enumObject);
            
            return $"{columnName} == @{parametersForLinqQuery.Count - 1}";
        }

        #region Internal Methods

        internal static string FilterMethod(string terms, List<object> parametersForLinqQuery, Type type)
        {
            string Clause(string conditional, string query)
            {
                parametersForLinqQuery.Add(TypeDescriptor.GetConverter(type).ConvertFrom(query));
                var indexOfParameter = parametersForLinqQuery.Count - 1;
                return $"{conditional}(@{indexOfParameter})";
            }

            if (terms.StartsWith("^"))
            {
                if (!terms.EndsWith("$")) return Clause(ConditionalConstants.StartsWith, terms.Substring(1));

                parametersForLinqQuery
                    .Add(TypeDescriptor.GetConverter(type).ConvertFrom(terms.Substring(1, terms.Length - 2)));
                
                var indexOfParameter = parametersForLinqQuery.Count - 1;
                
                return $"{ConditionalConstants.Equal}((object)@{indexOfParameter})";
            }

            return terms.EndsWith("$")
                ? Clause(ConditionalConstants.EndsWith, terms.Substring(0, terms.Length - 1))
                : Clause(ConditionalConstants.Contain, terms);
        }

        internal static object ChangeType(string terms, DataTablePropertyInfoModel propertyInfo)
        {
            if (propertyInfo.PropertyInfo.PropertyType.GetTypeInfo().IsGenericType && propertyInfo.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var u = Nullable.GetUnderlyingType(propertyInfo.Type);
                return Convert.ChangeType(terms, u);
            }

            return Convert.ChangeType(terms, propertyInfo.Type);
        }

        internal static DateTimeOffset ToDateTimeOffset(string value)
        {
            value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;

            if (ElectDataTableOptions.Instance.RequestDateTimeFormatType == DateTimeFormatType.Auto && DateTimeOffset.TryParse(value, out var result))
            {
                result = result.DateTime.WithTimeZone(ElectDataTableOptions.Instance.DateTimeTimeZone);

                return result;
            }

            if (DateTimeOffset.TryParseExact(value, ElectDataTableOptions.Instance.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dateTime))
            {
                result = dateTime;
            }
            else if (DateTimeOffset.TryParseExact(value, ElectDataTableOptions.Instance.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
            {
                result = date;
            }
            else
            {
                return default;
            }

            result = result.DateTime.WithTimeZone(ElectDataTableOptions.Instance.DateTimeTimeZone);

            return result;
        }
        
        internal static DateTime ToDateTime(string value)
        {
            value = string.IsNullOrWhiteSpace(value) ? string.Empty : value;

            if (ElectDataTableOptions.Instance.RequestDateTimeFormatType == DateTimeFormatType.Auto && DateTime.TryParse(value, out var result))
            {
                return result;
            }

            if (DateTime.TryParseExact(value, ElectDataTableOptions.Instance.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dateTime))
            {
                result = dateTime;
            }
            else if (DateTime.TryParseExact(value, ElectDataTableOptions.Instance.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
            {
                result = date;
            }
            else
            {
                return default;
            }

            return result;
        }
        
        #endregion Internal Methods
    }
}