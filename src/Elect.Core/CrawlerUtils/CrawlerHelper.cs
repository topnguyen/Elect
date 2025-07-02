namespace Elect.Core.CrawlerUtils
{
    public class CrawlerHelper
    {
        /// <summary>
        ///     Get metadata tags of the destination websites
        /// </summary>
        /// <returns></returns>
        public static async Task<List<MetadataModel>> GetListMetadataAsync(params string[] urls)
        {
            urls = urls.Distinct().ToArray();
            var metadataConcurrentBag = new ConcurrentBag<MetadataModel>();
            await urls.ParallelForEachAsync(async url =>
            {
                var metaData = await GetMetadataByUrlAsync(url);
                metadataConcurrentBag.Add(metaData);
            }, maxDegreeOfParallelism: 100);
            var metadataModels = metadataConcurrentBag.ToList();
            return metadataModels;
        }
        public static async Task<MetadataModel> GetMetadataByUrlAsync(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                // Re-procedure as Native browser
                httpClient
                    .DefaultRequestHeaders
                    .Add("user-agent",
                        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                httpClient
                    .DefaultRequestHeaders
                    .Add("accept",
                        "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpClient
                    .DefaultRequestHeaders
                    .Add("upgrade-insecure-requests", "1");
                var requester = new HttpClientRequester(httpClient);
                var browsingConfig = Configuration
                    .Default
                    .WithRequester(requester)
                    .WithDefaultCookies()
                    .WithDefaultLoader();
                using var browsingContext = BrowsingContext.New(browsingConfig);
                var htmlDocument = await browsingContext.OpenAsync(url);
                return GetMetadata(htmlDocument);
            }
            catch (Exception e)
            {
                // Ignore
#if DEBUG
                Console.WriteLine(e);
#endif
                return new MetadataModel
                {
                    Url = url
                };
            }
        }
        public static async Task<MetadataModel> GetMetadataByHtmlAsync(string html)
        {
            var htmlParser = new HtmlParser();
            var htmlDocument = await htmlParser.ParseDocumentAsync(html);
            return GetMetadata(htmlDocument);
        }
        private static MetadataModel GetMetadata(IDocument htmlDocument)
        {
            var metadataModel = new MetadataModel();
            if (htmlDocument == null)
            {
                return metadataModel;
            }
            metadataModel.OriginalUrl = htmlDocument.Url;
            var metaTags = htmlDocument.GetElementsByTagName("meta");
            foreach (var metaTag in metaTags)
            {
                var metaTagModel = new MetaTagModel
                {
                    Html = metaTag.OuterHtml,
                    Attributes = metaTag.Attributes.ToDictionary(x => x.Name, x => x.Value)
                };
                metadataModel.MetaTags.Add(metaTagModel);
                // Check Property Value
                var propertyValue =
                    metaTag
                        .Attributes
                        .FirstOrDefault(x => x.Name.ToLowerInvariant() == "property")
                        ?.Value;
                // Check Name Value
                var nameValue =
                    metaTag
                        .Attributes
                        .FirstOrDefault(x => x.Name.ToLowerInvariant() == "name")
                        ?.Value;
                if (string.IsNullOrWhiteSpace(propertyValue) && string.IsNullOrWhiteSpace(nameValue))
                {
                    continue;
                }
                var contentValue = metaTag
                    .Attributes
                    .FirstOrDefault(x => x.Name.ToLowerInvariant() == "content")
                    ?.Value;
                switch (propertyValue ?? nameValue)
                {
                    case "og:url":
                    {
                        metadataModel.Url = contentValue;
                        break;
                    }
                    case "og:title":
                    {
                        metadataModel.Title = contentValue;
                        break;
                    }
                    case "og:description":
                    {
                        metadataModel.Description = contentValue;
                        break;
                    }
                    case "og:image":
                    {
                        metadataModel.Image = contentValue;
                        break;
                    }
                    case "og:video":
                    {
                        metadataModel.Image = contentValue;
                        break;
                    }
                    case "og:type":
                    {
                        metadataModel.Type = contentValue;
                        break;
                    }
                    case "og:locale":
                    {
                        metadataModel.Locale = contentValue;
                        break;
                    }
                    case "og:site_name":
                    {
                        metadataModel.SiteName = contentValue;
                        break;
                    }
                    case "author":
                    {
                        metadataModel.Author = contentValue;
                        break;
                    }
                }
                if (propertyValue?.Contains("published_time") == true)
                {
                    if (DateTimeOffset.TryParse(contentValue, out var publishedTime))
                    {
                        metadataModel.PublishedTime = publishedTime;
                    }   
                }
                if (propertyValue?.Contains("tag") != true)
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(contentValue))
                {
                    continue;
                }
                metadataModel.Tags ??= new List<string>();
                metadataModel.Tags.Add(contentValue);
            }
            metadataModel.Title = string.IsNullOrWhiteSpace(metadataModel.Title)
                ? htmlDocument.Title
                : metadataModel.Title;
            metadataModel.Url = string.IsNullOrWhiteSpace(metadataModel.Url)
                ? htmlDocument.Url
                : metadataModel.Url;
            return metadataModel;
        }
    }
}
