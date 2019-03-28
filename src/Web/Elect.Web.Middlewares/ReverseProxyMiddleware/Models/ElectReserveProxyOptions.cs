#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectReserveProxyOptions.cs </Name>
//         <Created> 28/03/2019 10:53:00 AM </Created>
//         <Key> cc62d82a-8159-4655-a342-f73615a7a8d8 </Key>
//     </File>
//     <Summary>
//         ElectReserveProxyOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using Elect.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Elect.Web.Middlewares.ReverseProxyMiddleware.Models
{
    public class ElectReserveProxyOptions : IElectOptions
    {
        public string ServiceRootUrl { get; set; }
        
        /// <summary>
        ///     Func execute before ReserveProxy, return false if you want to stop ReserveProxy and response the HttpContext to client.
        /// </summary>
        public Func<HttpContext, bool> BeforeReserveProxy { get; set; }
        
        
        /// <summary>
        ///     Action execute after ReserveProxy rewrited
        /// </summary>
        public Action<HttpContext> AfterReserveProxy { get; set; }
    }
}