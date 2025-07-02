namespace Elect.Web.ITempDataDictionaryUtils
{
    public static class ITempDataDictionaryExtensions
    {
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            ITempDataDictionaryHelper.Set<T>(tempData, key, value);
        }
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            return ITempDataDictionaryHelper.Get<T>(tempData, key);
        }
    }
}
