#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> XDocumentExtensions.cs </Name>
//         <Created> 16/03/2018 8:38:48 AM </Created>
//         <Key> 289fa25c-9bfd-4a4c-9904-1e90189cc0ab </Key>
//     </File>
//     <Summary>
//         XDocumentExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.StringUtils;
using System.IO;
using System.Text;
using System.Xml.Linq;

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