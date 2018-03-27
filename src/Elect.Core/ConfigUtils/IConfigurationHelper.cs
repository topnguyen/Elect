#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IConfigurationHelper.cs </Name>
//         <Created> 15/03/2018 4:52:13 PM </Created>
//         <Key> 797dc235-5d3e-448a-a227-3e01dd14dd46 </Key>
//     </File>
//     <Summary>
//         IConfigurationHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Core.CheckUtils;
using Elect.Core.EnvUtils;
using Microsoft.Extensions.Configuration;

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

            if (value != null)
            {
                return value;
            }

            var environmentName = !string.IsNullOrWhiteSpace(EnvHelper.CurrentEnvironment) ? EnvHelper.CurrentEnvironment : EnvHelper.EnvDevelopmentName;

            value = configuration.GetValue<T>($"{key}:{environmentName}");

            if (value != null)
            {
                return value;
            }

            value = configuration.GetValue<T>($"{key}");

            return value;
        }
    }
}