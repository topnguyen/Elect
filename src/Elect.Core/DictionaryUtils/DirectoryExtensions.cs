#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DirectoryExtensions.cs </Name>
//         <Created> 21/03/2018 6:52:23 PM </Created>
//         <Key> 193b1f84-32a6-4ec7-82f4-b2a3db5cb0c9 </Key>
//     </File>
//     <Summary>
//         DirectoryExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Collections.Generic;

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