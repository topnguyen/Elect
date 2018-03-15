using Microsoft.Extensions.Configuration;

namespace Elect.Core.ConfigurationUtils
{
    public static class IConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration, string key = null) where T : new()
        {
            return IConfigurationHelper.GetSection<T>(configuration, key);
        }

        /// <summary>
        ///     Get Value follow Priority: Key:[Machine Name] &gt; Key:[Environment] &gt; Key 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key">          </param>
        /// <returns></returns>
        public static T GetValueByEnvironment<T>(this IConfiguration configuration, string key)
        {
            return IConfigurationHelper.GetValueByEnvironment<T>(configuration, key);
        }
    }
}