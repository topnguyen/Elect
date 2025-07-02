namespace Elect.Web.SiteMap.Interfaces
{
    public interface ISiteMapGenerator<in T> where T : class, ISiteMapItem
    {
        string GenerateXmlString(params T[] items);
        ContentResult GenerateContentResult(params T[] items);
    }
}
