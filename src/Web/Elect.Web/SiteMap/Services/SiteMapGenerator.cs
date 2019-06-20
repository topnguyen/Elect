#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SiteMapGenerator.cs </Name>
//         <Created> 21/03/2018 2:43:44 PM </Created>
//         <Key> 43ab28d9-cb0a-4816-8c39-82db61ac7302 </Key>
//     </File>
//     <Summary>
//         SiteMapGenerator.cs is a part of Elect
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Elect.Web.SiteMap.Services
{
    /// <summary>
    ///     Generates site map XML. 
    /// </summary>
    public class SiteMapGenerator : ISiteMapGenerator<SiteMapItem>
    {
        private static readonly XNamespace Xmlns = @"http://www.sitemaps.org/schemas/sitemap/0.9";

        private static readonly XNamespace Xsi = @"http://www.w3.org/2001/XMLSchema-instance";

        public virtual ContentResult GenerateContentResult(params SiteMapItem[] items)
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

        public virtual string GenerateXmlString(params SiteMapItem[] items)
        {
            if (items.Any() != true)
            {
                throw new ArgumentNullException($"{nameof(items)} is empty.");
            }

            var siteMapCount = (int)Math.Ceiling(items.Length / (double)SiteMapValidator.MaximumSiteMapIndexCount);

            SiteMapValidator.CheckSiteMapCount(siteMapCount);

            var siteMapXml =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(Xmlns + "urlset",
                        new XAttribute("xmlns", Xmlns),
                        new XAttribute(XNamespace.Xmlns + "xsi", Xsi),
                        new XAttribute(Xsi + "schemaLocation",
                            @"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                        from item in items
                        select CreateItemElement(item)
                    )
                );

            var xml = siteMapXml.ToString(Encoding.UTF8);

            SiteMapValidator.CheckDocumentSize(xml);

            return xml;
        }

        private static XElement CreateItemElement(SiteMapItem item)
        {
            var itemElement = new XElement(Xmlns + "url", new XElement(Xmlns + "loc", item.Url.ToLowerInvariant()));

            // all other elements are optional
            if (item.LastModified.HasValue)
            {
                itemElement.Add(new XElement(Xmlns + "lastmod", item.LastModified.Value.ToString("yyyy-MM-ddTHH:mm:sszzz")));
            }

            if (item.Frequency.HasValue)
            {
                itemElement.Add(new XElement(Xmlns + "changefreq", item.Frequency.Value.ToString().ToLower()));
            }

            if (item.Priority.HasValue)
            {
                itemElement.Add(new XElement(Xmlns + "priority", item.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
            }

            return itemElement;
        }
    }
}