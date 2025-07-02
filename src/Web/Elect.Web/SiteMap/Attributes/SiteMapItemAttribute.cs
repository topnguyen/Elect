namespace Elect.Web.SiteMap.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SiteMapItemAttribute : Attribute
    {
        public SiteMapItemAttribute(SiteMapItemFrequency frequency, double priority)
        {
            Frequency = frequency;
            Priority = priority;
        }
        public double Priority { get; set; }
        public SiteMapItemFrequency Frequency { get; set; }
    }
}
