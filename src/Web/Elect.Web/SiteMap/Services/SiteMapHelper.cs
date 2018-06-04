#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SiteMapHelper.cs </Name>
//         <Created> 21/03/2018 3:07:54 PM </Created>
//         <Key> c78e2930-7a0f-47dd-b679-4f5f6d9d13de </Key>
//     </File>
//     <Summary>
//         SiteMapHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.IUrlHelperUtils;
using Elect.Web.SiteMap.Attributes;
using Elect.Web.SiteMap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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