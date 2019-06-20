#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ITempDataDictionaryExtensions.cs </Name>
//         <Created> 19/04/2018 1:08:26 AM </Created>
//         <Key> 9f4cab38-a4fa-402d-a659-80a6f61caf18 </Key>
//     </File>
//     <Summary>
//         ITempDataDictionaryExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Mvc.ViewFeatures;

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