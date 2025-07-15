namespace Elect.Core.XmlUtils
{
    public class XmlHelper
    {
        /// <summary>
        ///     To serialize string XML to object 
        /// </summary>
        public static T FromXmlString<T>(string xmlString)
        {
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null
            };
            
            using (var sr = new StringReader(xmlString))
            using (var reader = XmlReader.Create(sr, settings))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(reader);
            }
        }
        public static string ToXmlString<T>(T obj, bool isRemoveNameSpace = true)
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var ns = new System.Xml.Serialization.XmlSerializerNamespaces();
            if (isRemoveNameSpace)
            {
                ns.Add("", "");
            }
            using (var stringWriter = new System.IO.StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj, ns);
                return stringWriter.ToString();
            }
        }
    }
}
