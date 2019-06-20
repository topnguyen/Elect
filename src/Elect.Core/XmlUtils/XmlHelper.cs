#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> XmlHelper.cs </Name>
//         <Created> 16/03/2018 11:00:55 AM </Created>
//         <Key> 37db8d44-add5-4f3c-9bd6-c543570662f5 </Key>
//     </File>
//     <Summary>
//         XmlHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

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