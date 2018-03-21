#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectCorsOptions.cs </Name>
//         <Created> 21/03/2018 6:29:05 PM </Created>
//         <Key> cc62d82a-8159-4655-a342-f73615a7a8d7 </Key>
//     </File>
//     <Summary>
//         ElectCorsOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Collections.Generic;

namespace Elect.Web.Middlewares.CorsMiddleware.Models
{
    public class ElectCorsOptions
    {
        /// <summary>
        ///     Default is "Default". 
        /// </summary>
        public string PolicyName { get; set; } = "Default";

        /// <summary>
        ///     Default is "*". 
        /// </summary>
        public List<string> AccessControlAllowOrigins { get; set; } = new List<string> { "*" };

        /// <summary>
        ///     Default is "Authorization, Content-Type". 
        /// </summary>
        public List<string> AccessControlAllowHeaders { get; set; } = new List<string> { "Authorization", "Content-Type" };

        /// <summary>
        ///     Default is "GET, POST, PUT, DELETE, OPTIONS, HEAD". 
        /// </summary>
        public List<string> AccessControlAllowMethods { get; set; } = new List<string> { "GET", "POST", "PUT", "DELETE", "OPTIONS", "HEAD" };
    }
}