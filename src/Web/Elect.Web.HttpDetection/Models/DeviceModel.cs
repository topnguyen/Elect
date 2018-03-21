#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DeviceModel.cs </Name>
//         <Created> 21/03/2018 8:33:26 PM </Created>
//         <Key> 39cdce2c-fb72-49f8-bc2f-1b2236866f93 </Key>
//     </File>
//     <Summary>
//         DeviceModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.SecurityUtils;
using Elect.Core.StringUtils;
using Elect.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Text.RegularExpressions;

namespace Elect.Web.HttpDetection.Models
{
    public class DeviceModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceType Type { get; set; }

        public bool IsCrawler { get; set; }

        // Marker
        public string MarkerFullInfo { get; set; }

        public string MarkerName { get; set; }

        public string MarkerVersion { get; set; }

        // OS
        public string OsFullInfo { get; set; }

        public string OsName { get; set; }

        public string OsVersion { get; set; }

        // Engine
        public string EngineFullInfo { get; set; }

        public string EngineName { get; set; }

        public string EngineVersion { get; set; }

        // Browser
        public string BrowserFullInfo { get; set; }

        public string BrowserName { get; set; }

        public string BrowserVersion { get; set; }

        // Location

        public string IpAddress { get; set; }

        public string CityName { get; set; }

        public int? CityGeoNameId { get; set; }

        public string CountryName { get; set; }

        public int? CountryGeoNameId { get; set; }

        public string CountryIsoCode { get; set; }

        public string ContinentName { get; set; }

        public int? ContinentGeoNameId { get; set; }

        public string ContinentCode { get; set; }

        public string TimeZone { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? AccuracyRadius { get; set; }

        public string PostalCode { get; set; }

        // Others

        public string UserAgent { get; set; }

        public string DeviceHash { get; set; }

        public DeviceModel()
        {
        }

        public DeviceModel(HttpRequest request)
        {
            Type = GetDeviceType(request);
            IsCrawler = GetIsCrawlerRequest(request);

            // Marker
            MarkerFullInfo = HttpRequestHelper.GetMarkerFullInfo(request);
            MarkerName = HttpRequestHelper.GetMarkerName(request);
            MarkerVersion = HttpRequestHelper.GetMarkerVersion(request);

            // OS
            OsFullInfo = HttpRequestHelper.GetOsFullInfo(request);
            OsName = HttpRequestHelper.GetOsName(request);
            OsVersion = HttpRequestHelper.GetOsVersion(request);

            // Engine
            EngineFullInfo = HttpRequestHelper.GetEngineFullInfo(request);
            EngineName = HttpRequestHelper.GetEngineName(request);
            EngineVersion = HttpRequestHelper.GetEngineVersion(request);

            // Browser
            BrowserFullInfo = HttpRequestHelper.GetBrowserFullInfo(request);
            BrowserName = HttpRequestHelper.GetBrowserName(request);
            BrowserVersion = HttpRequestHelper.GetBrowserVersion(request);

            // Location
            HttpRequestHelper.GetDeviceInformation(request, this);

            // Others
            UserAgent = HttpRequestHelper.GetUserAgent(request);
            DeviceHash = GetDeviceHash(this);
        }

        private static DeviceType GetDeviceType(HttpRequest request)
        {
            var agent = HttpRequestHelper.GetUserAgent(request)?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(agent))
            {
                return DeviceType.Unknown;
            }

            if (Regex.IsMatch(agent, ElectHttpDetectionConstants.TabletAgentsRegex, RegexOptions.IgnoreCase))
                return DeviceType.Tablet;

            if (Regex.IsMatch(agent, ElectHttpDetectionConstants.MobileAgentsRegex, RegexOptions.IgnoreCase))
                return DeviceType.Mobile;

            // mobile opera mini special case
            if (request.Headers.Any(header => header.Value.Any(value => value.Contains("operamini"))))
                return DeviceType.Mobile;

            // mobile user agent prof detection
            if (request.Headers.ContainsKey("x-wap-profile") || request.Headers.ContainsKey("profile"))
                return DeviceType.Mobile;

            // mobile accept-header base detection
            if (request.Headers[HeaderKey.Accept].Any(accept => accept.ToLowerInvariant() == "wap"))
                return DeviceType.Mobile;

            return DeviceType.Desktop;
        }

        private static bool GetIsCrawlerRequest(HttpRequest request)
        {
            var agent = HttpRequestHelper.GetUserAgent(request)?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(agent))
            {
                return false;
            }

            if (Regex.IsMatch(agent, ElectHttpDetectionConstants.CrawlerAgentsRegex, RegexOptions.IgnoreCase))
                return true;

            return false;
        }

        private static string GetDeviceHash(DeviceModel deviceModel)
        {
            string ipAddress = string.IsNullOrWhiteSpace(deviceModel.IpAddress) ? StringHelper.Generate(16) : deviceModel.IpAddress;

            string identityDevice = $"{deviceModel.OsName}|{deviceModel.OsVersion}_{deviceModel.EngineName}|{deviceModel.EngineVersion}_{deviceModel.BrowserName}|{deviceModel.BrowserVersion}_{ipAddress}";

            var deviceHash = SecurityHelper.EncryptSha256(identityDevice);

            return deviceHash;
        }
    }
}