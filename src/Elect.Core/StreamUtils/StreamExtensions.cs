namespace Elect.Core.StreamUtils
{
    public static class StreamExtensions
    {
        public static byte[] ToBytes(this System.IO.Stream stream)
        {
            return StreamHelper.ConvertToBytes(stream);
        }
    }
}
