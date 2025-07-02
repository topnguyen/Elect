namespace Elect.Web.Api.Models
{
    public class PagedMetaModel<TRequest, TResponse> : PagedResponseModel<TResponse> where TResponse : class, new() where TRequest : PagedRequestModel
    {
        private readonly TRequest _pagedRequestModel;
        private readonly PagedResponseModel<TResponse> _pagedResponseModel;
        private readonly HttpMethod _method;
        private readonly string _endpoint;
        [JsonProperty(Order = 1)]
        public virtual LinkModel Meta { get; set; }
        [JsonProperty(Order = 2)]
        public virtual LinkModel First { get; set; }
        [JsonProperty(Order = 3)]
        public virtual LinkModel Previous { get; set; }
        [JsonProperty(Order = 4)]
        public virtual LinkModel Next { get; set; }
        [JsonProperty(Order = 5)]
        public virtual LinkModel Last { get; set; }
        public PagedMetaModel()
        {
        }
        public PagedMetaModel(PagedResponseModel<TResponse> pagedResponseModel) : this()
        {
            _pagedResponseModel = pagedResponseModel;
            Total = pagedResponseModel.Total;
            Items = pagedResponseModel.Items;
            AdditionalData = pagedResponseModel.AdditionalData;
        }
        public PagedMetaModel(IUrlHelper urlHelper, TRequest pagedRequestModel, PagedResponseModel<TResponse> pagedResponseModel, HttpMethod method = HttpMethod.GET) : this(pagedResponseModel)
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
        protected LinkModel GetMetaLink()
        {
            var routeValueDictionary = new RouteValueDictionary(_pagedRequestModel);
            var link = new LinkModel
            {
                Method = _method,
                Url = GetUrlWithQueries(_endpoint, routeValueDictionary),
                Data = routeValueDictionary
            };
            return link;
        }
        protected LinkModel GetFirstLink()
        {
            var pagedRequestModel = _pagedRequestModel.Clone();
            pagedRequestModel.Skip = 0;
            var routeValueDictionary = new RouteValueDictionary(pagedRequestModel);
            var link = new LinkModel
            {
                Method = _method,
                Url = GetUrlWithQueries(_endpoint, routeValueDictionary),
                Data = routeValueDictionary
            };
            return link;
        }
        protected LinkModel GetPreviousLink()
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
            var routeValueDictionary = new RouteValueDictionary(pagedRequestModel);
            var link = new LinkModel
            {
                Method = _method,
                Url = GetUrlWithQueries(_endpoint, routeValueDictionary),
                Data = routeValueDictionary
            };
            return link;
        }
        protected LinkModel GetNextLink()
        {
            var skipToNext = _pagedRequestModel.Skip + _pagedRequestModel.Take;
            if (skipToNext >= _pagedResponseModel.Total)
            {
                return null;
            }
            var pagedRequestModel = _pagedRequestModel.Clone();
            pagedRequestModel.Skip = skipToNext;
            var routeValueDictionary = new RouteValueDictionary(pagedRequestModel);
            var link = new LinkModel
            {
                Method = _method,
                Url = GetUrlWithQueries(_endpoint, routeValueDictionary),
                Data = routeValueDictionary
            };
            return link;
        }
        protected LinkModel GetLastLink()
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
            var routeValueDictionary = new RouteValueDictionary(pagedRequestModel);
            var link = new LinkModel
            {
                Method = _method,
                Url = GetUrlWithQueries(_endpoint, routeValueDictionary),
                Data = routeValueDictionary
            };
            return link;
        }
        protected string GetUrlWithQueries(string url, RouteValueDictionary routeValueDictionary)
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
                    var query = $"{routeData.Key.ToLowerInvariant()}={hrefValue}";
                    url = AddQueryString(url, query);
                }
            }
            return url;
        }
        protected string AddQueryString(string url, string query)
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
