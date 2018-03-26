#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HttpRequestHelper.cs </Name>
//         <Created> 21/03/2018 8:35:12 PM </Created>
//         <Key> 61ca0fd2-deba-4af6-8d54-07ce8d993e65 </Key>
//     </File>
//     <Summary>
//         HttpRequestHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.LinqUtils;
using Elect.Web.HttpDetection.Models;
using Elect.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.RegularExpressions;

namespace Elect.Web.HttpDetection
{
    public class HttpRequestHelper
    {
        // Marker
        public static string GetMarkerFullInfo(HttpRequest request)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iEnd = agent.IndexOf('(');

            if (iEnd < 0)
            {
                return null;
            }

            string markerFullInfo = agent.Substring(0, iEnd)?.Trim();

            return markerFullInfo;
        }

        public static string GetMarkerName(HttpRequest request)
        {
            string markerName = GetMarkerFullInfo(request)?.Split('/').FirstOrDefault()?.Trim();

            return markerName;
        }

        public static string GetMarkerVersion(HttpRequest request)
        {
            string markerVersion = GetMarkerFullInfo(request)?.Split('/').LastOrDefault()?.Trim();

            return markerVersion;
        }

        // OS
        public static string GetOsFullInfo(HttpRequest request)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iStart = agent.IndexOf('(') + 1;

            int iEnd = agent.IndexOf(')') - iStart;

            if (iEnd < 0)
            {
                return null;
            }

            string osFullInfo = agent.Substring(iStart, iEnd)?.Trim();

            return osFullInfo;
        }

        public static string GetOsName(HttpRequest request)
        {
            string osName = GetOsFullInfo(request)?.Split(';').FirstOrDefault()?.Trim();

            return osName;
        }

        public static string GetOsVersion(HttpRequest request)
        {
            var info = GetOsFullInfo(request)?.Split(';');

            string osVersion = null;

            if (info != null && info.Length > 0)
            {
                int i = 1;

                while (i <= info.Length && (osVersion == null || osVersion.ToLowerInvariant() == "u"))
                {
                    osVersion = info[i];

                    i++;
                }
            }

            return osVersion;
        }

        // Engine
        public static string GetEngineFullInfo(HttpRequest request)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iStart = agent.IndexOf(')') + 1;

            string engineFullInfo = agent.Substring(iStart)?.Trim();

            if (string.IsNullOrWhiteSpace(engineFullInfo))
            {
                return null;
            }

            int iEnd = engineFullInfo.IndexOf(' ');

            if (iEnd < 0)
            {
                return null;
            }

            engineFullInfo = engineFullInfo.Substring(0, iEnd);

            return engineFullInfo;
        }

        public static string GetEngineName(HttpRequest request)
        {
            string engineName = GetEngineFullInfo(request)?.Split('/').FirstOrDefault()?.Trim();

            // Standardize engine name
            const string webKitStandardName = "WebKit";

            engineName = engineName?.EndsWith(webKitStandardName) == true ? webKitStandardName : engineName;

            return engineName;
        }

        public static string GetEngineVersion(HttpRequest request)
        {
            string engineName = GetEngineFullInfo(request)?.Split('/').LastOrDefault()?.Trim();

            return engineName;
        }

        // Browser

        /// <summary>
        ///     Get browser info in user agent 
        /// </summary>
        /// <param name="request">      </param>
        /// <param name="isStandardize"> will exclude "version", "mobile" from browser info </param>
        /// <returns></returns>
        public static string GetBrowserFullInfo(HttpRequest request, bool isStandardize = true)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iStart = agent.LastIndexOf(')') + 1;

            string browserFullInfo = agent.Substring(iStart)?.Trim();

            // Filters
            if (!string.IsNullOrWhiteSpace(browserFullInfo))
            {
                var listExclude = new[] { "version", "mobile" };

                var browserInfo = browserFullInfo.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                if (isStandardize)
                {
                    browserInfo = browserInfo.RemoveWhere(x => listExclude.Any(y => x.ToLowerInvariant().Contains(y))).ToList();
                }

                browserFullInfo = string.Join(", ", browserInfo);
            }

            return browserFullInfo;
        }

        public static string GetBrowserName(HttpRequest request)
        {
            string browserName = GetBrowserFullInfo(request)?.Split(',').FirstOrDefault()?.Split('/').FirstOrDefault()?.Trim();

            return browserName;
        }

        public static string GetBrowserVersion(HttpRequest request)
        {
            string browserFullInfo = GetBrowserFullInfo(request, false);

            // Filters
            if (string.IsNullOrWhiteSpace(browserFullInfo))
            {
                return null;
            }

            var browserInfo = browserFullInfo.Split(' ').ToList();

            var browserVersion = browserInfo.FirstOrDefault(x => x.ToLowerInvariant().Contains("version"))?.Split('/').LastOrDefault()?.Trim().Trim(',');

            if (string.IsNullOrWhiteSpace(browserVersion))
            {
                browserVersion = GetBrowserFullInfo(request)?.Split(',').FirstOrDefault()?.Split('/').LastOrDefault()?.Trim();
            }

            return browserVersion;
        }

        public static string GetUserAgent(HttpRequest request)
        {
            var agent = request?.Headers[HeaderKey.UserAgent].ToString();

            return agent;
        }

        public static bool IsCrawlerRequest(HttpRequest request)
        {
            var agent = GetUserAgent(request)?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(agent))
            {
                return false;
            }

            if (Regex.IsMatch(agent, ElectHttpDetectionConstants.CrawlerAgentsRegex, RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}