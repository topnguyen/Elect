﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HangfireHelper.cs </Name>
//         <Created> 02/04/2018 6:58:51 PM </Created>
//         <Key> 5404fc37-fb71-429c-a838-8835b2727efe </Key>
//     </File>
//     <Summary>
//         HangfireHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Job.Hangfire.Models;
using Microsoft.AspNetCore.Http;

namespace Elect.Job.Hangfire.Utils
{
    public class HangfireHelper
    {
        internal const string AccessKeyName = "key";
        internal const string CookieAccessKeyName = "Elect_Hangfire_AccessKey";

        public static bool IsCanAccessHangfireDashboard(HttpContext httpContext, ElectHangfireOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AccessKey))
            {
                return true;
            }

            string requestKey = httpContext.Request.Query[AccessKeyName];

            requestKey = string.IsNullOrWhiteSpace(requestKey) ? httpContext.Request.Cookies[CookieAccessKeyName] : requestKey;

            var isCanAccess = options.AccessKey == requestKey;

            return isCanAccess;
        }
    }
}