namespace Elect.Core.ByteUtils
{
    public static class ByteExtensions
    {
        public static string ToBase64(this byte[] bytes)
        {
            return ByteHelper.ConvertToToBase64(bytes);
        }
    }
}
