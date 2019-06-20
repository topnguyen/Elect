#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectHttpDetectionConstants.cs </Name>
//         <Created> 21/03/2018 8:48:05 PM </Created>
//         <Key> 84547ce5-be02-45c5-b13e-8484d16fcf74 </Key>
//     </File>
//     <Summary>
//         ElectHttpDetectionConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Web.HttpDetection.Models
{
    public class ElectHttpDetectionConstants
    {
        public static readonly string TabletAgentsRegex = "/(tablet|ipad|playbook|hp-tablet|kindle|silk)|(android(?!.*mobile))/";
        public static readonly string CrawlerAgentsRegex = "/bot|slurp|spider/";
        public static readonly string MobileAgentsRegex = "/Mobile|iP(hone|od|ad)|Android|BlackBerry|IEMobile|Kindle|NetFront|Silk-Accelerated|(hpw|web)OS|Fennec|Minimo|Opera M(obi|ini)|Blazer|Dolfin|Dolphin|Skyfire|Zune/";

        public const string DbName = "GeoCity.mmdb";
    }
}