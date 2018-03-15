using Elect.Core.CheckUtils;
using Elect.Core.EnvironmentUtils;
using Microsoft.Extensions.Configuration;

namespace Elect.Core.ConfigurationUtils
{
    public static class IConfigurationHelper
    {
        public static T GetSection<T>(IConfiguration configuration, string key = null) where T : new()
        {
            var value = new T();

            key = string.IsNullOrWhiteSpace(key) ? typeof(T).Name : key;

            configuration.GetSection(key).Bind(value);

            return value;
        }

        /// <summary>
        ///     Get Value follow Priority: Key:[Machine Name] &gt; Key:[Environment] &gt; Key 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key">          </param>
        /// <returns></returns>
        public static T GetValueByEnvironment<T>(IConfiguration configuration, string key)
        {
            CheckHelper.CheckNullOrWhiteSpace(key, nameof(key));

            var value = configuration.GetValue<T>($"{key}:{EnvironmentHelper.MachineName}");

            if (value != null)
            {
                return value;
            }

            value = configuration.GetValue<T>($"{key}:{EnvironmentHelper.CurrentEnvironment}");

            if (value != null)
            {
                return value;
            }

            value = configuration.GetValue<T>($"{key}");

            return value;
        }
    }
}