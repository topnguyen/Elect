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
