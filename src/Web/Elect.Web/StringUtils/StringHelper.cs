#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringHelper.cs </Name>
//         <Created> 21/03/2018 4:11:44 PM </Created>
//         <Key> 25f935ca-8d97-4b64-9f08-2d3449b47188 </Key>
//     </File>
//     <Summary>
//         StringHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Text;

namespace Elect.Web.StringUtils
{
    public class StringHelper
    {
        public static string GetFriendlySlug(string value, int maxLength = 150)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            value = Core.StringUtils.StringHelper.Normalize(value).ToLowerInvariant();

            var length = value.Length;

            var previousDash = false;

            var stringBuilder = new StringBuilder(length);

            for (var i = 0; i < length; ++i)
            {
                var c = value[i];

                if (c >= 'a' && c <= 'z' || c >= '0' && c <= '9' || c >= 'A' && c <= 'Z')
                {
                    stringBuilder.Append(c);

                    previousDash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!previousDash && stringBuilder.Length > 0)
                    {
                        stringBuilder.Append('-');

                        previousDash = true;
                    }
                }
                else if (c >= 128)
                {
                    var previousLength = stringBuilder.Length;

                    stringBuilder.Append(c);

                    if (previousLength != stringBuilder.Length)
                    {
                        previousDash = false;
                    }
                }

                if (stringBuilder.Length >= maxLength)
                {
                    break;
                }
            }

            if (previousDash || stringBuilder.Length > maxLength)
            {
                return stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
            }

            return stringBuilder.ToString();
        }
    }
}