namespace Elect.Web.SiteMap.Services
{
    public class SiteMapHelper
    {
        /// <summary>
        ///     Generate SiteMap content result for assembly project, use execute assembly as web
        ///     project assembly.
        /// </summary>
        /// <param name="iUrlHelper"></param>
        /// <returns></returns>
        public static ContentResult GetSiteMapContentResult(IUrlHelper iUrlHelper)
        {
            var siteMapContentResult = GetSiteMapContentResult(Assembly.GetEntryAssembly(), iUrlHelper);
            return siteMapContentResult;
        }
        /// <summary>
        ///     Generate SiteMap content result for assembly project. 
        /// </summary>
        /// <param name="asm">       
        ///     assembly of web project you want to generate SiteMap
        /// </param>
        /// <param name="iUrlHelper"></param>
        /// <returns></returns>
        public static ContentResult GetSiteMapContentResult(Assembly asm, IUrlHelper iUrlHelper)
        {
            var actionList = GetListSiteMapItemAction(Assembly.GetEntryAssembly());
            var siteMapItems = actionList
                .Select(
                    x => new SiteMapItem(iUrlHelper.AbsoluteAction(x.Action.Name, x.Controller.Name.Replace("Controller", string.Empty)),
                        null,
                        x.Frequency,
                        x.Priority)
                )
                .ToArray();
            var siteMapContentResult = new SiteMapGenerator().GenerateContentResult(siteMapItems);
            return siteMapContentResult;
        }
        public static List<SiteMapItemActionModel> GetListSiteMapItemAction(Assembly asm)
        {
            var listAction = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => m.GetCustomAttributes(typeof(SiteMapItemAttribute), true).Any())
                .Select(x => new SiteMapItemActionModel
                {
                    Controller = x.DeclaringType,
                    Action = x,
                    Frequency = (x.GetCustomAttributes(typeof(SiteMapItemAttribute), false).LastOrDefault() as SiteMapItemAttribute)?.Frequency ?? SiteMapItemFrequency.Never,
                    Priority = (x.GetCustomAttributes(typeof(SiteMapItemAttribute), false).LastOrDefault() as SiteMapItemAttribute)?.Priority ?? 0
                })
                .ToList();
            return listAction;
        }
    }
}
