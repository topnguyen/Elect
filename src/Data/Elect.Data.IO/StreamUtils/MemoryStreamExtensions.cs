#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> MemoryStreamExtensions.cs </Name>
//         <Created> 02/04/2018 8:42:39 PM </Created>
//         <Key> f52d3424-bcab-4e90-a169-3ce5a72e7a19 </Key>
//     </File>
//     <Summary>
//         MemoryStreamExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.IO;

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