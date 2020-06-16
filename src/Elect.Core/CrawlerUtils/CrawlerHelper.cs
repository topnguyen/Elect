using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Io.Network;
using Elect.Core.CrawlerUtils.Models;

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
            List<Task<MetadataModel>> tasks = new List<Task<MetadataModel>>();

            urls = urls.Distinct().ToArray();

            foreach (var url in urls)
            {
                Task<MetadataModel> task = GetMetadataAsync(url);

                tasks.Add(task);
            }

            var metadataModels = await Task.WhenAll(tasks).ConfigureAwait(true);

            return metadataModels.ToList();
        }

        public static async Task<MetadataModel> GetMetadataAsync(string url)
        {
            var metadataModel = new MetadataModel
            {
                Url = url
            };

            try
            {
                var httpClient = new HttpClient();
                
                httpClient
                    .DefaultRequestHeaders
                    .Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                    
                httpClient
                    .DefaultRequestHeaders
                    .Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                      
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

                var document = await browsingContext.OpenAsync(url);

                if (document == null)
                {
                    return metadataModel;
                }

                var metaTags = document.GetElementsByTagName("meta");

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
                }

                metadataModel.Title = string.IsNullOrWhiteSpace(metadataModel.Title)
                    ? document.Title
                    : metadataModel.Title;

                metadataModel.Url = string.IsNullOrWhiteSpace(metadataModel.Url)
                    ? document.Url
                    : metadataModel.Url;
            }
            catch (Exception e)
            {
                // Ignore

#if DEBUG

                Console.WriteLine(e);

#endif
            }

            return metadataModel;
        }
    }
}