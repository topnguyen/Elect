#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringExtensions.cs </Name>
//         <Created> 21/03/2018 4:11:29 PM </Created>
//         <Key> 1692d0ca-1e38-4ced-8454-1489393358ea </Key>
//     </File>
//     <Summary>
//         StringExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Web.StringUtils
{
    public static class StringExtensions
    {
        public static string ToFriendlySlug(this string value, int maxLength = 150)
        {
            return StringHelper.GetFriendlySlug(value, maxLength);
        }
        
        public static string RemoveHtmlTag(string value)
        {
            return StringHelper.RemoveHtmlTag(value);
        }
    }
}