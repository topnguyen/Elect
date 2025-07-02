namespace Elect.Web.HttpDetection.Models
{
    public class DeviceModel : ElectDisposableModel
    {
        private static readonly object Lock = new object();
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceType Type { get; set; }
        public bool IsCrawler { get; set; }
        // Marker
        public string MarkerName { get; set; }
        public string MarkerVersion { get; set; }
        // OS
        public string OsName { get; set; }
        public string OsVersion { get; set; }
        // Engine
        public string EngineName { get; set; }
        public string EngineVersion { get; set; }
        // Browser
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        // Location
        public string IpAddress { get; set; }
        // City
        public string CityName { get; set; }
        // Country
        public string CountryName { get; set; }
        public string CountryIsoCode { get; set; }
        // Continent
        public string ContinentName { get; set; }
        public string ContinentCode { get; set; }
        // Time Zone
        public string TimeZone { get; set; }
        public string PostalCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? AccuracyRadius { get; set; }
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
            MarkerName = HttpRequestHelper.GetMarkerName(request);
            MarkerVersion = HttpRequestHelper.GetMarkerVersion(request);
            // OS
            OsName = HttpRequestHelper.GetOsName(request);
            OsVersion = HttpRequestHelper.GetOsVersion(request);
            // Engine
            EngineName = HttpRequestHelper.GetEngineName(request);
            EngineVersion = HttpRequestHelper.GetEngineVersion(request);
            // Browser
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
            var ipAddress = string.IsNullOrWhiteSpace(IpAddress) ? StringHelper.Generate(16) : IpAddress;
            var identityDevice = $"{OsName}|{OsVersion}_{EngineName}|{EngineVersion}_{BrowserName}|{BrowserVersion}_{ipAddress}";
            var deviceHash = SecurityHelper.EncryptSha256(identityDevice);
            return deviceHash;
        }
        private void UpdateLocation(HttpRequest request)
        {
            IpAddress = request.GetIpAddress();
            var geoDbAbsolutePath = Path.Combine(Bootstrapper.Instance.WorkingFolder, ElectHttpDetectionConstants.DbName);
            if (!File.Exists(geoDbAbsolutePath))
            {
                throw new FileNotFoundException($"{geoDbAbsolutePath} not found", geoDbAbsolutePath);
            }
            lock (Lock)
            {
                using (var reader = new DatabaseReader(geoDbAbsolutePath))
                {
                    if (!reader.TryCity(IpAddress, out var city))
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
                    // Country
                    CountryName = city.Country.Names.TryGetValue("en", out var countryName) ? countryName : city.Country.Name;
                    CountryIsoCode = city.Country.IsoCode;
                    // Continent
                    ContinentName = city.Continent.Names.TryGetValue("en", out var continentName) ? continentName : city.Continent.Name;
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
}
