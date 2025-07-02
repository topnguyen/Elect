namespace Elect.Web.SiteMap.Services
{
    /// <summary>
    ///     Generate Image site map (see more
    ///     https://support.google.com/webmasters/answer/178636?hl=en) List up to 1,000 images for
    ///     each page
    /// </summary>
    public class SiteMapImageGenerator : ISiteMapGenerator<SiteMapImageItemModel>
    {
        public static readonly XNamespace Image = @"http://www.google.com/schemas/sitemap-image/1.1";
        public static readonly XNamespace Xmlns = @"http://www.sitemaps.org/schemas/sitemap/0.9";
        public virtual ContentResult GenerateContentResult(params SiteMapImageItemModel[] items)
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
        public virtual string GenerateXmlString(params SiteMapImageItemModel[] items)
        {
            if (items?.Any() != true)
            {
                throw new ArgumentNullException($"{nameof(items)} is null");
            }
            var siteMap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(Xmlns + "urlset",
                    new XAttribute("xmlns", Xmlns),
                    new XAttribute(XNamespace.Xmlns + "image", Image),
                    from item in items
                    select CreateItemElement(item)
                )
            );
            var xml = siteMap.ToString(Encoding.UTF8);
            SiteMapValidator.CheckDocumentSize(xml);
            return xml;
        }
        private static string CreateItemElement(SiteMapImageItemModel item)
        {
            var itemElement = new XElement(Xmlns + "url", new XElement(Xmlns + "loc", item.Url.ToLowerInvariant()));
            foreach (var siteMapImageDetail in item.Images)
            {
                var imageElement = new XElement(Image + "image");
                // all other elements are optional
                imageElement.Add(new XElement(Image + "loc", siteMapImageDetail.ImagePath.ToLowerInvariant()));
                if (!string.IsNullOrWhiteSpace(siteMapImageDetail.Caption))
                {
                    imageElement.Add(new XElement(Image + "caption", siteMapImageDetail.Caption));
                }
                if (!string.IsNullOrWhiteSpace(siteMapImageDetail.GeoLocation))
                {
                    imageElement.Add(new XElement(Image + "geo_location", siteMapImageDetail.GeoLocation));
                }
                if (!string.IsNullOrWhiteSpace(siteMapImageDetail.Title))
                {
                    imageElement.Add(new XElement(Image + "title", siteMapImageDetail.Title));
                }
                if (!string.IsNullOrWhiteSpace(siteMapImageDetail.License))
                {
                    imageElement.Add(new XElement(Image + "license", siteMapImageDetail.License));
                }
                itemElement.Add(imageElement);
            }
            var document = new XDocument(itemElement);
            var xml = document.ToString(Encoding.UTF8);
            SiteMapValidator.CheckDocumentSize(xml);
            return xml;
        }
    }
}
