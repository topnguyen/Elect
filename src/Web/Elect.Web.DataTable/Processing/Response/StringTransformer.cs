#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringTransformer.cs </Name>
//         <Created> 23/03/2018 5:01:38 PM </Created>
//         <Key> d9fe2ca9-7d89-459b-8ca9-9961a7a6b90e </Key>
//     </File>
//     <Summary>
//         StringTransformer.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.TypeUtils;
using Elect.Web.DataTable.EnumUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Elect.Web.DataTable.Processing.Response
{
    public class StringTransformers
    {
        internal static object GetStringedValue(Type type, object value)
        {
            object stringedValue;

            if (Transformers.ContainsKey(type))
            {
                stringedValue = Transformers[type](type, value);
            }
            else
            {
                if (type.GetNotNullableType().IsEnum)
                {
                    var t = type.GetNotNullableType();
                    Enum enumObj = (Enum)TypeDescriptor.GetConverter(t).ConvertFrom(value);
                    stringedValue = enumObj.GetDisplayName() ?? enumObj.GetDescription() ?? enumObj.GetName();
                }
                else
                {
                    stringedValue = value?.ToString();
                }
            }

            stringedValue = stringedValue ?? string.Empty;
            return stringedValue;
        }

        private static readonly Dictionary<Type, StringTransformer> Transformers = new Dictionary<Type, StringTransformer>();

        public delegate object GuardedValueTransformer<in TVal>(TVal value);

        public delegate object StringTransformer(Type type, object value);

        static StringTransformers()
        {
            RegisterFilter<DateTimeOffset>(dateTimeOffset => dateTimeOffset.ToString(DataTableGlobalConfig.DateTimeFormat));
            RegisterFilter<DateTime>(dateTime => dateTime.ToString(DataTableGlobalConfig.DateTimeFormat));
            RegisterFilter<IEnumerable<string>>(s => s.ToArray());
            RegisterFilter<IEnumerable<int>>(s => s.ToArray());
            RegisterFilter<IEnumerable<long>>(s => s.ToArray());
            RegisterFilter<IEnumerable<decimal>>(s => s.ToArray());
            RegisterFilter<IEnumerable<bool>>(s => s.ToArray());
            RegisterFilter<IEnumerable<double>>(s => s.ToArray());
            RegisterFilter<IEnumerable<object>>(s => s.Select(o => GetStringedValue(o.GetType(), o)).ToArray());
            RegisterFilter<bool>(s => s);
            RegisterFilter<object>(o => (o ?? string.Empty).ToString());
        }

        public static void RegisterFilter<TVal>(GuardedValueTransformer<TVal> filter)
        {
            if (Transformers.ContainsKey(typeof(TVal)))
                Transformers[typeof(TVal)] = Guard(filter);
            else
                Transformers.Add(typeof(TVal), Guard(filter));
        }

        private static StringTransformer Guard<TVal>(GuardedValueTransformer<TVal> transformer)
        {
            return (t, v) => !typeof(TVal).GetTypeInfo().IsAssignableFrom(t) ? null : transformer((TVal)v);
        }

        public static Dictionary<string, object> StringifyValues(Dictionary<string, object> dict)
        {
            var output = new Dictionary<string, object>();

            foreach (var row in dict)
            {
                output[row.Key] = row.Value == null ? string.Empty : GetStringedValue(row.Value.GetType(), row.Value);
            }

            return output;
        }
    }
}