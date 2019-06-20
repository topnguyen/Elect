#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HttpMethod.cs </Name>
//         <Created> 02/04/2018 8:58:36 AM </Created>
//         <Key> c6e8a164-176e-428f-80ce-3e83eb24aab9 </Key>
//     </File>
//     <Summary>
//         HttpMethod.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace Elect.Web.Models
{
    public enum HttpMethod
    {
        [Display(Name = "GET")]
        GET,

        [Display(Name = "POST")]
        POST,

        [Display(Name = "PUT")]
        PUT,

        [Display(Name = "PATCH")]
        PATCH,

        [Display(Name = "DELETE")]
        DELETE,

        [Display(Name = "OPTIONS")]
        OPTIONS,

        [Display(Name = "HEAD")]
        HEAD,

        [Display(Name = "TRADE")]
        TRADE,

        [Display(Name = "CONNECT")]
        CONNECT
    }
}