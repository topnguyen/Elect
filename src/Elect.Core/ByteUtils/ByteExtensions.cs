#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ByteExtensions.cs </Name>
//         <Created> 26/12/2018 10:60:43 AM </Created>
//         <Key> f766e729-8f52-4393-bf4f-277f8495c021 </Key>
//     </File>
//     <Summary>
//         ByteExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

namespace Elect.Core.ByteUtils
{
    public static class ByteExtensions
    {
        public static string ToBase64(this byte[] bytes)
        {
            return ByteHelper.ConvertToToBase64(bytes);
        }
    }
}