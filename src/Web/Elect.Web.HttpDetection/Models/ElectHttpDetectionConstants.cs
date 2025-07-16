namespace Elect.Web.HttpDetection.Models
{
    public class ElectHttpDetectionConstants
    {
        public static readonly string TabletAgentsRegex = "(tablet|ipad|playbook|hp-tablet|kindle|silk)|(android(?!.*mobile))";
        public static readonly string CrawlerAgentsRegex = "bot|slurp|spider";
        public static readonly string MobileAgentsRegex = "Mobile|iP(hone|od|ad)|Android|BlackBerry|IEMobile|Kindle|NetFront|Silk-Accelerated|(hpw|web)OS|Fennec|Minimo|Opera M(obi|ini)|Blazer|Dolfin|Dolphin|Skyfire|Zune";
        public const string DbName = "GeoCity.mmdb";
    }
}
