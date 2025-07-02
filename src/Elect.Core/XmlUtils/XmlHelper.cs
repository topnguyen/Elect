namespace Elect.Core.XmlUtils
{
    public class XmlHelper
    {
        /// <summary>
        ///     To serialize string XML to object 
        /// </summary>
        public static T FromXmlString<T>(string xmlString)
        {
            using (var sr = new StringReader(xmlString))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(sr);
            }
        }
        public static string ToXmlString<T>(T obj, bool isRemoveNameSpace = true)
        {
            var json = JsonConvert.SerializeObject(obj);
            XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
            using (var stringWriter = new StringWriter())
            {
                XmlWriter writer;
                if (isRemoveNameSpace)
                {
                    var settings = new XmlWriterSettings
                    {
                        Indent = false,
                        OmitXmlDeclaration = true
                    };
                    writer = XmlWriter.Create(stringWriter, settings);
                }
                else
                {
                    writer = XmlWriter.Create(stringWriter);
                }
                doc.WriteTo(writer);
                writer.Flush();
                var xmlString = stringWriter.GetStringBuilder().ToString();
                return xmlString;
            }
        }
    }
}
