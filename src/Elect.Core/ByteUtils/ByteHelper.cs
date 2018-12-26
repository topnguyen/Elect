#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ByteHelper.cs </Name>
//         <Created> 26/12/2018 10:60:43 AM </Created>
//         <Key> 1cb85387-0071-4676-9a6a-ab7f06e6f1a8 </Key>
//     </File>
//     <Summary>
//         ByteHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;

namespace Elect.Core.ByteUtils
{
    public static class ByteHelper
    {
        public static string ConvertToToBase64(byte[] bytes)
        {
            // This wrapper to easier find out the convert method for bytes

            string base64 = Convert.ToBase64String(bytes);

            return base64;
        }
    }
}