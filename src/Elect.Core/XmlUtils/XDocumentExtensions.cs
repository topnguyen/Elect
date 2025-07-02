namespace Elect.Core.XmlUtils
{
    public static class XDocumentExtensions
    {
        public static string ToString(this XDocument document, Encoding encoding)
        {
            var stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriterWithEncoding(stringBuilder, encoding))
            {
                document.Save(stringWriter);
            }
            return stringBuilder.ToString();
        }
    }
}
