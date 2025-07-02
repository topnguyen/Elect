namespace Elect.Core.ConfigUtils
{
    public class IConfigurationHelper
    {
        [CanBeNull]
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
        [CanBeNull]
        public static T GetValueByEnv<T>(IConfiguration configuration, string key)
        {
            CheckHelper.CheckNullOrWhiteSpace(key, nameof(key));
            var value = configuration.GetValue<T>($"{key}:{EnvHelper.MachineName}");
            if (!EqualityComparer<T>.Default.Equals(value, default))
            {
                return value;
            }
            var environmentName = !string.IsNullOrWhiteSpace(EnvHelper.CurrentEnvironment) ? EnvHelper.CurrentEnvironment : EnvHelper.EnvDevelopmentName;
            value = configuration.GetValue<T>($"{key}:{environmentName}");
            if (!EqualityComparer<T>.Default.Equals(value, default))
            {
                return value;
            }
            value = configuration.GetValue<T>($"{key}");
            return value;
        }
        /// <summary>
        ///     Get Value follow Priority: Key:[Machine Name] &gt; Key:[Environment] &gt; Key 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key">          </param>
        /// <returns></returns>
        [CanBeNull]
        public static List<T> GetListValueByEnv<T>(IConfiguration configuration, string key)
        {
            CheckHelper.CheckNullOrWhiteSpace(key, nameof(key));
            var value = configuration.GetSection($"{key}:{EnvHelper.MachineName}").Get<List<T>>();
            if (value != null && value.Any())
            {
                return value;
            }
            var environmentName = !string.IsNullOrWhiteSpace(EnvHelper.CurrentEnvironment) ? EnvHelper.CurrentEnvironment : EnvHelper.EnvDevelopmentName;
            value = configuration.GetSection($"{key}:{environmentName}").Get<List<T>>();
            if (value != null && value.Any())
            {
                return value;
            }
            value = configuration.GetSection($"{key}").Get<List<T>>();
            return value;
        }
    }
}
