#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StreamExtensions.cs </Name>
//         <Created> 26/12/2018 10:60:43 AM </Created>
//         <Key> c1e1778c-6bac-4621-9edb-89e340d53d13 </Key>
//     </File>
//     <Summary>
//         StreamExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

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