namespace Elect.Web.SiteMap.Models
{
    public class SiteMapItemActionModel: ElectDisposableModel
    {
        public MethodInfo Action { get; set; }
        public Type Controller { get; set; }
        public double Priority { get; set; }
        public SiteMapItemFrequency Frequency { get; set; }
    }
}
