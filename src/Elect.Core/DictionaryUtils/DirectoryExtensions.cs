namespace Elect.Core.DictionaryUtils
{
    public static class DirectoryExtensions
    {
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            return DictionaryHelper.ToDictionary(obj);
        }
        public static Dictionary<string, string> ToDictionary<T>(this T obj)
        {
            return DictionaryHelper.ToDictionary(obj);
        }
        public static T GetValue<T>(IDictionary<string, object> dictionary, string key)
        {
            return DictionaryHelper.GetValue<T>(dictionary, key);
        }
        public static T GetValue<T>(IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            return DictionaryHelper.GetValue(dictionary, key, defaultValue);
        }
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            DictionaryHelper.AddOrUpdate(dictionary, key, value);
        }
    }
}
