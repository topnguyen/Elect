namespace Elect.Core.ByteUtils
{
    public static class ByteHelper
    {
        public static string ConvertToToBase64(byte[] bytes)
        {
            // This wrapper to easier find out the convert method for bytes
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
