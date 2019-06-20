#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> MemoryStreamHelper.cs </Name>
//         <Created> 02/04/2018 8:43:27 PM </Created>
//         <Key> 10abe4bf-fb21-402c-90fb-b66a8de8953c </Key>
//     </File>
//     <Summary>
//         MemoryStreamHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.CheckUtils;
using System.IO;

namespace Elect.Data.IO.StreamUtils
{
    public class MemoryStreamHelper
    {
        public static void Save(MemoryStream stream, string path)
        {
            CheckHelper.CheckNullOrWhiteSpace(path, nameof(path));

            path = PathHelper.GetFullPath(path);

            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.Position = 0;
                stream.CopyTo(file);
            }
        }
    }
}