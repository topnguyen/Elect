#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableAttributeExtensions.cs </Name>
//         <Created> 23/03/2018 4:47:46 PM </Created>
//         <Key> 2f1e740a-e7ee-4086-87a2-0d11b5e67e0a </Key>
//     </File>
//     <Summary>
//         DataTableAttributeExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Attributes;
using Elect.Web.DataTable.Utils.TypeUtils;

namespace Elect.Web.DataTable.Utils.DataTableAttributeUtils
{
    public static class DataTableAttributeExtensions
    {
        public static string ToDisplayName(this DataTableAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.DisplayName) || (attribute.DisplayNameResourceType == null /*&& DataTableGlobalConfig.SharedResourceType == null*/))
            {
                return attribute.DisplayName;
            }

            var value = TypeHelper.GetResourceLookup<string>(attribute.DisplayNameResourceType, attribute.DisplayName);

            if (string.IsNullOrWhiteSpace(value))
            {
                return attribute.DisplayName;
            }

            return value;
        }
    }
}