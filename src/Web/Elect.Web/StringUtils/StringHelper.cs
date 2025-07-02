namespace Elect.Web.StringUtils
{
    public class StringHelper : Core.StringUtils.StringHelper
    {
        public static string GetFriendlySlug(string value, int maxLength = 150)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            value = Normalize(value).ToLowerInvariant();
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
        public static string EncodeBase64Url(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            string base64Encode = WebEncoders.Base64UrlEncode(bytes);
            return base64Encode;
        }
        public static string DecodeBase64Url(string value)
        {
            byte[] bytes = WebEncoders.Base64UrlDecode(value);
            string base64Decode = Encoding.UTF8.GetString(bytes);
            return base64Decode;
        }
        public static string RemoveHtmlTag(string value)
        {
            return Regex.Replace(value, "<.*?>", string.Empty);
        }
    }
}
