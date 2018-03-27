#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IConfigurationExtensions.cs </Name>
//         <Created> 15/03/2018 4:50:32 PM </Created>
//         <Key> d89474ba-2bc3-4b8f-8943-081bc07a0b97 </Key>
//     </File>
//     <Summary>
//         IConfigurationExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.Configuration;

namespace Elect.Core.ConfigUtils
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
        public static T GetValueByEnv<T>(this IConfiguration configuration, string key)
        {
            return IConfigurationHelper.GetValueByEnv<T>(configuration, key);
        }
    }
}