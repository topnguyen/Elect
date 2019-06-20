#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ITempDataDictionaryHelper.cs </Name>
//         <Created> 19/04/2018 1:08:58 AM </Created>
//         <Key> ddd4e9cd-7eed-4763-98ec-9837a2f6ab9c </Key>
//     </File>
//     <Summary>
//         ITempDataDictionaryHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

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