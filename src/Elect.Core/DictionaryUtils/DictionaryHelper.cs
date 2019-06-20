#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DictionaryHelper.cs </Name>
//         <Created> 15/03/2018 11:10:29 PM </Created>
//         <Key> b9928845-563f-4190-b96d-8df5906b6ab5 </Key>
//     </File>
//     <Summary>
//         DictionaryHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Core.DictionaryUtils
{
    public class DictionaryHelper
    {
        public static Dictionary<string, object> ToDictionary(object obj)
        {
            JObject json = JObject.FromObject(obj);

            var properties = json.Properties();

            var directory = new Dictionary<string, object>();

            foreach (var property in properties)
            {
                directory[property.Name] = property.Value;
            }

            return directory;
        }

        public static Dictionary<string, string> ToDictionary<T>(T obj)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            var listPropertyInfo = typeof(T).GetProperties().ToList();

            foreach (var propertyInfo in listPropertyInfo)
            {
                var propertyData = propertyInfo.GetValue(obj);

                var value =
                    Convert.GetTypeCode(propertyData) != TypeCode.Object
                        ? propertyData as string
                        : JsonConvert
                            .SerializeObject(propertyData, Constants.Formatting.JsonSerializerSettings)
                            .Trim('"');

                if (!string.IsNullOrWhiteSpace(value))
                {
                    dictionary.Add(propertyInfo.Name, value);
                }
            }

            return dictionary;
        }

        public static T GetValue<T>(IDictionary<string, object> dictionary, string key)
        {
            return dictionary.TryGetValue(key, out var data) ? data.ConvertTo<T>() : default;
        }

        public static T GetValue<T>(IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            if (!dictionary.TryGetValue(key, out var value))
            {
                return defaultValue;
            }

            var result = value.ConvertTo<T>();

            result = result == null ? defaultValue : result;

            return result;
        }

        public static void AddOrUpdate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}