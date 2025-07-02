namespace Elect.Web.DataTable.Utils
{
    public static class ElectDataTableTranslator
    {
        /// <summary>
        ///     Get Translate string for the <see paramref="key"/> by lookup on <see cref=" ElectDataTableOptions.SharedResourceType"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            return Get(key, ElectDataTableOptions.Instance.SharedResourceType);
        }
        /// <summary>
        ///     Get Translate string for the <see paramref="key"/> by lookup on <paramref name="resourceType"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static string Get(string key, Type resourceType)
        {
            if (string.IsNullOrWhiteSpace(key) || resourceType == null)
            {
                return key;
            }
            var resourceLookup = TypeHelper.GetResourceLookup<string>(resourceType, key);
            if (string.IsNullOrWhiteSpace(resourceLookup))
            {
                return key;
            }
            return key;
        }
    }
}
