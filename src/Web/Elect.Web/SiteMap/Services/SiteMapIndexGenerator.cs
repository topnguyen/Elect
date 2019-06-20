#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SiteMapIndexGenerator.cs </Name>
//         <Created> 21/03/2018 3:39:35 PM </Created>
//         <Key> 34d122a8-259a-4f0f-b99d-e792d33d4b88 </Key>
//     </File>
//     <Summary>
//         SiteMapIndexGenerator.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.XmlUtils;
using Elect.Web.Models;
using Elect.Web.SiteMap.Interfaces;
using Elect.Web.SiteMap.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Elect.Web.SiteMap.Services
{
    /// <summary>
    ///     Generate Site map index (see more http://www.sitemaps.org/protocol.html) 
    /// </summary>
    public class SiteMapIndexGenerator : ISiteMapGenerator<SiteMapIndexItemModel>
    {
        private static readonly XNamespace Xmlns = @"http://www.sitemaps.org/schemas/sitemap/0.9";

        private static readonly XNamespace Xsi = @"http://www.w3.org/2001/XMLSchema-instance";

        public virtual ContentResult GenerateContentResult(params SiteMapIndexItemModel[] items)
        {
            string siteMapContent = GenerateXmlString(items);

            ContentResult contentResult = new ContentResult
            {
                ContentType = ContentType.Xml,
                StatusCode = 200,
                Content = siteMapContent
            };
            return contentResult;
        }

        public virtual string GenerateXmlString(params SiteMapIndexItemModel[] items)
        {
            if (items?.Any() != true)
            {
                throw new ArgumentNullException($"{nameof(items)} is null");
            }

            var siteMap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(Xmlns + "sitemapindex",
                    new XAttribute("xmlns", Xmlns),
                    new XAttribute(XNamespace.Xmlns + "xsi", Xsi),
                    new XAttribute(Xsi + "schemaLocation",
                        @"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd"),
                    from item in items
                    select CreateItemElement(item)
                )
            );

            var xml = siteMap.ToString(Encoding.UTF8);
            SiteMapValidator.CheckDocumentSize(xml);
            return xml;
        }

        private static XElement CreateItemElement(SiteMapIndexItemModel item)
        {
            var itemElement = new XElement(Xmlns + "sitemap", new XElement(Xmlns + "loc", item.Url.ToLowerInvariant()));

            // all other elements are optional
            if (item.LastModified.HasValue)
            {
                itemElement.Add(new XElement(Xmlns + "lastmod", item.LastModified.Value.ToString("yyyy-MM-ddTHH:mm:sszzz")));
            }

            return itemElement;
        }
    }
}