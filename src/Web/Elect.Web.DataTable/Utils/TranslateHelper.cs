#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2019 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TranslateHelper.cs </Name>
//         <Created> 14/01/2019 7:39:46 AM </Created>
//         <Key> 2f1e740a-e7ee-4086-87a2-0d11b5e67e0b </Key>
//     </File>
//     <Summary>
//         TranslateHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using Elect.Web.DataTable.Models.Options;
using Elect.Web.DataTable.Utils.TypeUtils;

namespace Elect.Web.DataTable.Utils
{
    internal static class TranslateHelper
    {
        /// <summary>
        ///     Get Translate string for the <see cref="key"/> by lookup on <see cref=" ElectDataTableOptions.SharedResourceType"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static string GetTranslate(string key)
        {
            return GetTranslate(key, ElectDataTableOptions.Instance.SharedResourceType);
        }

        /// <summary>
        ///     Get Translate string for the <see cref="key"/> by lookup on <paramref name="resourceType"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        internal static string GetTranslate(string key, Type resourceType)
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