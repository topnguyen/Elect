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
                string value;
                if (propertyData == null)
                {
                    value = null;
                }
                else if (propertyData is string s)
                {
                    value = s;
                }
                else if (propertyData.GetType().IsValueType)
                {
                    value = propertyData.ToString();
                }
                else
                {
                    value = JsonConvert.SerializeObject(propertyData, Constants.Formatting.JsonSerializerSettings).Trim('"');
                }
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
