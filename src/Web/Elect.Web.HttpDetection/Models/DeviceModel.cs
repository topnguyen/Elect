#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> cs </Name>
//         <Created> 21/03/2018 8:33:26 PM </Created>
//         <Key> 39cdce2c-fb72-49f8-bc2f-1b2236866f93 </Key>
//     </File>
//     <Summary>
//         cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.SecurityUtils;
using Elect.Core.StringUtils;
using Elect.Data.IO;
using Elect.Web.HttpUtils;
using Elect.Web.Models;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
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

            IsCrawler = HttpRequestHelper.IsCrawlerRequest(request);

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

            // Location by GeoCity Database
            UpdateLocation(request);

            // Others
            UserAgent = HttpRequestHelper.GetUserAgent(request);

            DeviceHash = GetDeviceHash();
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

        private string GetDeviceHash()
        {
            string ipAddress = string.IsNullOrWhiteSpace(IpAddress) ? StringHelper.Generate(16) : IpAddress;

            string identityDevice = $"{OsName}|{OsVersion}_{EngineName}|{EngineVersion}_{BrowserName}|{BrowserVersion}_{ipAddress}";

            var deviceHash = SecurityHelper.EncryptSha256(identityDevice);

            return deviceHash;
        }

        private void UpdateLocation(HttpRequest request)
        {
            string geoDbRelativePath = Path.Combine(nameof(HttpUtils), nameof(HttpDetection), "GeoCity.mmdb");

            string geoDbAbsolutePath = PathHelper.GetFullPath(geoDbRelativePath);

            if (!File.Exists(geoDbAbsolutePath))
            {
                // Try to get folder in executed assembly
                geoDbAbsolutePath = PathHelper.GetFullPath(geoDbRelativePath);
            }

            if (!File.Exists(geoDbAbsolutePath))
            {
                return;
            }

            using (var reader = new DatabaseReader(geoDbAbsolutePath))
            {
                var ipAddress = request.GetIpAddress();

                if (!reader.TryCity(ipAddress, out var city))
                {
                    return;
                }

                if (city == null)
                {
                    return;
                }

                IpAddress = city.Traits.IPAddress;

                // City
                CityName = city.City.Names.TryGetValue("en", out var cityName) ? cityName : city.City.Name;
                CityGeoNameId = city.City.GeoNameId;

                // Country
                CountryName = city.Country.Names.TryGetValue("en", out var countryName) ? countryName : city.Country.Name;
                CountryGeoNameId = city.Country.GeoNameId;
                CountryIsoCode = city.Country.IsoCode;

                // Continent
                ContinentName = city.Continent.Names.TryGetValue("en", out var continentName) ? continentName : city.Continent.Name;
                ContinentGeoNameId = city.Continent.GeoNameId;
                ContinentCode = city.Continent.Code;

                // Location
                Latitude = city.Location.Latitude;
                Longitude = city.Location.Longitude;
                AccuracyRadius = city.Location.AccuracyRadius;

                PostalCode = city.Postal.Code;

                // Time Zone
                TimeZone = city.Location.TimeZone;
            }
        }
    }
}