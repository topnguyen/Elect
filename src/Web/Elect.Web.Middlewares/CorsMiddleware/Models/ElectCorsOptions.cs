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

using System;
using Elect.Core.Interfaces;
using System.Collections.Generic;
using Elect.Web.Models;
using EnumsNET;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Elect.Web.Middlewares.CorsMiddleware.Models
{
    public class ElectCorsOptions : IElectOptions
    {
        /// <summary>
        ///     Default is "Elect". 
        /// </summary>
        public string PolicyName { get; set; } = nameof(Elect);

        /// <summary>
        ///     Default is "*". If contains "*" then allow all. 
        /// </summary>
        public List<string> AllowOrigins { get; set; } = new List<string> {"*"};

        /// <summary>
        ///     Default is "*". If contains "*" then allow all. 
        /// </summary>
        public List<string> AllowHeaders { get; set; } = new List<string> {"*"};

        /// <summary>
        ///     Default is "GET, POST, PUT", "DELETE". If contains "*"  then allow all. 
        /// </summary>
        public List<string> AllowMethods { get; set; } = new List<string>
        {
            HttpMethod.GET.AsString(EnumFormat.DisplayName),
            HttpMethod.POST.AsString(EnumFormat.DisplayName),
            HttpMethod.PUT.AsString(EnumFormat.DisplayName),
            HttpMethod.DELETE.AsString(EnumFormat.DisplayName)
        };

        public bool IsAllowCredentials { get; set; } = true;
        
        /// <summary>
        ///     Enable auto add same domain origins
        /// </summary>
        public bool IsAllowOriginsSubDomains { get; set; } = true;
        
        /// <summary>
        ///     Additional Config Builder for Policy if you want to add your customize after Elect add Config Policy Builder.
        /// </summary>
        public Action<CorsPolicyBuilder> ExtendPolicyBuilder { get; set; }

        /// <summary>
        ///     Additional Config Options for Policy if you want to add your customize after Elect add Config Policy Options.
        /// </summary>
        public Action<CorsOptions> ExtendPolicyOptions { get; set; }
    }
}