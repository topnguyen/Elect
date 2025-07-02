namespace Elect.Web.ITempDataDictionaryUtils
{
    public class ITempDataDictionaryHelper
    {
        public static void Set<T>(ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(ITempDataDictionary tempData, string key) where T : class
        {
            if (tempData.TryGetValue(key, out var o))
            {
                return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
            }
            return null;
        }
    }
}
