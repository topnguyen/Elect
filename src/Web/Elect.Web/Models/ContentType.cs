﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ContentType.cs </Name>
//         <Created> 21/03/2018 2:04:52 PM </Created>
//         <Key> 361fb777-1371-4e95-90da-ea965c2393d9 </Key>
//     </File>
//     <Summary>
//         ContentType.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Web.Models
{
    /// <summary>
    ///     A list of internet media types, which are a standard identifier used on the Internet to
    ///     indicate the type of data that a file contains. Web browsers use them to determine how to
    ///     display, output or handle files and search engines use them to classify data files on the web.
    /// </summary>
    public static class ContentType
    {
        /// <summary>
        ///     Atom feeds. 
        /// </summary>
        public const string Atom = "application/atom+xml";

        /// <summary>
        ///     HTML; Defined in RFC 2854. 
        /// </summary>
        public const string Html = "text/html";

        /// <summary>
        ///     Form URL Encoded. 
        /// </summary>
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        /// <summary>
        ///     GIF image; Defined in RFC 2045 and RFC 2046. 
        /// </summary>
        public const string Gif = "image/gif";

        /// <summary>
        ///     JPEG JFIF image; Defined in RFC 2045 and RFC 2046. 
        /// </summary>
        public const string Jpg = "image/jpeg";

        /// <summary>
        ///     Binary JavaScript Object Notation BSON. 
        /// </summary>
        public const string Bson = "application/bson";

        /// <summary>
        ///     JavaScript Object Notation JSON; Defined in RFC 4627. 
        /// </summary>
        public const string Json = "application/json";

        /// <summary>
        ///     Multi-part form data; Defined in RFC 2388. 
        /// </summary>
        public const string MultipartFormData = "multipart/form-data";

        /// <summary>
        ///     Portable Network Graphics; Registered,[8] Defined in RFC 2083. 
        /// </summary>
        public const string Png = "image/png";

        /// <summary>
        ///     Textual data; Defined in RFC 2046 and RFC 3676. 
        /// </summary>
        public const string Text = "text/plain";

        /// <summary>
        ///     Extensible Markup Language; Defined in RFC 3023 
        /// </summary>
        public const string Xml = "application/xml";

        /// <summary>
        ///     Compressed ZIP. 
        /// </summary>
        public const string Zip = "application/zip";
    }
}