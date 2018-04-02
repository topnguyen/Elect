#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> PagedMetaModel_T_.cs </Name>
//         <Created> 02/04/2018 1:24:32 AM </Created>
//         <Key> cdcced57-88a2-4e45-a815-e3bcbde4d8a5 </Key>
//     </File>
//     <Summary>
//         PagedMetaModel_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Elect.Web.IUrlHelperUtils;
using Elect.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;

namespace Elect.Web.Api.Models
{
    public class PagedMetaModel<T> : PagedResponseModel<T> where T : class, new()
    {
        private readonly PagedRequestModel _pagedRequestModel;

        private readonly PagedResponseModel<T> _pagedResponseModel;

        private readonly HttpMethod _method;

        private readonly string _endpoint;

        [JsonProperty(Order = 1)]
        public LinkModel Meta { get; set; }

        [JsonProperty(Order = 2)]
        public LinkModel First { get; set; }

        [JsonProperty(Order = 3)]
        public LinkModel Previous { get; set; }

        [JsonProperty(Order = 4)]
        public LinkModel Next { get; set; }

        [JsonProperty(Order = 5)]
        public LinkModel Last { get; set; }

        public PagedMetaModel()
        {
        }

        public PagedMetaModel(PagedResponseModel<T> pagedResponseModel) : this()
        {
            _pagedResponseModel = pagedResponseModel;

            Total = pagedResponseModel.Total;

            Items = pagedResponseModel.Items;

            AdditionalData = pagedResponseModel.AdditionalData;
        }

        public PagedMetaModel(IUrlHelper urlHelper, PagedRequestModel pagedRequestModel, PagedResponseModel<T> pagedResponseModel, HttpMethod method = HttpMethod.GET) : this(pagedResponseModel)
        {
            _pagedRequestModel = pagedRequestModel;

            _method = method;

            _endpoint = urlHelper.ActionContext.HttpContext.Request.Path.Value;

            _endpoint = urlHelper.AbsoluteContent(_endpoint);

            Meta = GetMetaLink();

            First = GetFirstLink();

            Previous = GetPreviousLink();

            Next = GetNextLink();

            Last = GetLastLink();
        }

        private LinkModel GetMetaLink()
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(_pagedRequestModel);

            var link = new LinkModel
            {
                Method = _method,

                Href = GetUrlWithQueries(_endpoint, routeValueDictionary)
            };

            return link;
        }

        private LinkModel GetFirstLink()
        {
            var pagedRequestModel = _pagedRequestModel.Clone();

            pagedRequestModel.Skip = 0;

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(pagedRequestModel);

            var link = new LinkModel
            {
                Method = _method,

                Href = GetUrlWithQueries(_endpoint, routeValueDictionary)
            };

            return link;
        }

        private LinkModel GetPreviousLink()
        {
            if (_pagedRequestModel.Skip == 0 || _pagedResponseModel.Total <= _pagedRequestModel.Skip)
            {
                return null;
            }

            var skipToPrevious = Math.Max(_pagedRequestModel.Skip - _pagedRequestModel.Take, 0);

            if (skipToPrevious <= 0)
            {
                return GetFirstLink();
            }

            var pagedRequestModel = _pagedRequestModel.Clone();

            pagedRequestModel.Skip = skipToPrevious;

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(pagedRequestModel);

            var link = new LinkModel
            {
                Method = _method,

                Href = GetUrlWithQueries(_endpoint, routeValueDictionary)
            };

            return link;
        }

        private LinkModel GetNextLink()
        {
            var skipToNext = _pagedRequestModel.Skip + _pagedRequestModel.Take;

            if (skipToNext >= _pagedResponseModel.Total)
            {
                return null;
            }

            var pagedRequestModel = _pagedRequestModel.Clone();

            pagedRequestModel.Skip = skipToNext;

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(pagedRequestModel);

            var link = new LinkModel
            {
                Method = _method,

                Href = GetUrlWithQueries(_endpoint, routeValueDictionary)
            };

            return link;
        }

        private LinkModel GetLastLink()
        {
            if (_pagedResponseModel.Total <= _pagedRequestModel.Take)
            {
                return null;
            }

            var skipToNext = _pagedRequestModel.Skip + _pagedRequestModel.Take;

            if (skipToNext >= _pagedResponseModel.Total)
            {
                return null;
            }

            var skipToLast = (int)(Math.Ceiling((_pagedResponseModel.Total - (double)_pagedRequestModel.Take) / _pagedRequestModel.Take) * _pagedRequestModel.Take);

            var pagedRequestModel = _pagedRequestModel.Clone();

            pagedRequestModel.Skip = skipToLast;

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(pagedRequestModel);

            var link = new LinkModel
            {
                Method = _method,

                Href = GetUrlWithQueries(_endpoint, routeValueDictionary)
            };

            return link;
        }

        private static string GetUrlWithQueries(string url, RouteValueDictionary routeValueDictionary)
        {
            url = url.ToLowerInvariant();

            foreach (var routeData in routeValueDictionary)
            {
                var hrefKey = "{" + routeData.Key.ToLowerInvariant() + "}";

                if (url.Contains(hrefKey))
                {
                    url = url.Replace(hrefKey, routeData.Value?.ToString() ?? string.Empty);
                }
                else
                {
                    var hrefValue = routeData.Value?.ToString();

                    if (string.IsNullOrWhiteSpace(hrefValue))
                    {
                        continue;
                    }

                    var query = $"{routeData.Key}={hrefValue}";

                    url = AddQueryString(url, query);
                }
            }

            return url;
        }

        private static string AddQueryString(string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }
    }
}