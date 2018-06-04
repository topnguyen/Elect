#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TypeHelper.cs </Name>
//         <Created> 24/03/2018 2:17:00 PM </Created>
//         <Key> 890ad843-06f4-4848-a699-113176d8af51 </Key>
//     </File>
//     <Summary>
//         TypeHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Models.Options;
using System;
using System.Reflection;

namespace Elect.Web.DataTable.Utils.TypeUtils
{
    internal class TypeHelper
    {
        internal static T GetResourceLookup<T>(Type resourceType, string resourceName)
        {
            resourceType = resourceType ?? ElectDataTableOptions.Instance.SharedResourceType;

            if (resourceType == null || resourceName == null) return default;

            var property = resourceType.GetProperty(resourceName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);

            if (property == null) return default;

            return (T)property.GetValue(null, null);
        }
    }
}