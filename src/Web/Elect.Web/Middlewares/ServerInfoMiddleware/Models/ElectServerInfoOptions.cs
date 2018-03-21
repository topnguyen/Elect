#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectServerInfoOptions.cs </Name>
//         <Created> 21/03/2018 7:18:49 PM </Created>
//         <Key> 131eb714-2c97-4eaf-8376-f94e7087793e </Key>
//     </File>
//     <Summary>
//         ElectServerInfoOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Interfaces;

namespace Elect.Web.Middlewares.ServerInfoMiddleware.Models
{
    public class ElectServerInfoOptions : IElectOptions
    {
        /// <summary>
        ///     Default is "cloudflare-nginx". 
        /// </summary>
        public string ServerName { get; set; } = "cloudflare-nginx";

        /// <summary>
        ///     Power by a technology, default is "PHP/5.6.30". 
        /// </summary>
        public string PoweredBy { get; set; } = "PHP/5.6.30";

        /// <summary>
        ///     Author Full Name 
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        ///     Author of Website URL 
        /// </summary>
        public string AuthorWebsite { get; set; }

        /// <summary>
        ///     Author Email 
        /// </summary>
        public string AuthorEmail { get; set; }
    }
}