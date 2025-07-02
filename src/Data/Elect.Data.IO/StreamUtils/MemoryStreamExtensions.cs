namespace Elect.Data.IO.StreamUtils
{
    public static class MemoryStreamExtensions
    {
        public static void Save(this MemoryStream stream, string path)
        {
            MemoryStreamHelper.Save(stream, path);
        }
    }
}
