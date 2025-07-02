namespace Elect.Web.Swagger.Utils
{
    internal class SwaggerHelper
    {
        private static string IndexFileContent { get; set; }
        private static string JsonViewerFileContent { get; set; }
        #region Html Content
        public static ContentResult GetApiDocHtml()
        {
            if (string.IsNullOrWhiteSpace(IndexFileContent))
            {
                string indexFilePath = Path.Combine(Bootstrapper.Instance.WorkingFolder, ElectSwaggerConstants.IndexFileName);
                IndexFileContent = File.ReadAllText(indexFilePath);
            }
            ContentResult contentResult = new ContentResult
            {
                ContentType = ContentType.Html,
                StatusCode = StatusCodes.Status200OK,
                Content = IndexFileContent
            };
            return contentResult;
        }
        public static ContentResult GetApiJsonViewerHtml()
        {
            if (string.IsNullOrWhiteSpace(JsonViewerFileContent))
            {
                var jsonViewerFilePath = Path.Combine(Bootstrapper.Instance.WorkingFolder, ElectSwaggerConstants.JsonViewerFileName);
                JsonViewerFileContent = File.ReadAllText(jsonViewerFilePath);
            }
            ContentResult contentResult = new ContentResult
            {
                ContentType = ContentType.Html,
                StatusCode = StatusCodes.Status200OK,
                Content = JsonViewerFileContent
            };
            return contentResult;
        }
        #endregion Html Content
        #region File Content
        public static void UpdateApiDocFileContent(string title, string swaggerEndpoint, string authTokenKeyPrefix,
            string jsonViewerUrl)
        {
            UpdateFileContent(new Dictionary<string, string>
            {
                {"@AssetPath", ElectSwaggerConstants.AssetsUrl},
                {"@ApiDocumentHtmlTitle", title},
                {"@SwaggerEndpoint", swaggerEndpoint},
                {"@AuthTokenKeyPrefix", authTokenKeyPrefix},
                {"@JsonViewerUrl", jsonViewerUrl}
            }, ElectSwaggerConstants.IndexFileName);
        }
        internal static void UpdateApiJsonViewerFileContent(string title)
        {
            UpdateFileContent(new Dictionary<string, string>
            {
                {"@AssetPath", ElectSwaggerConstants.AssetsUrl},
                {"@ApiDocumentHtmlTitle", title}
            }, ElectSwaggerConstants.JsonViewerFileName);
        }
        public static void UpdateFileContent(Dictionary<string, string> replaceDictionary, string filePath)
        {
            string fileFullPath = Path.Combine(Bootstrapper.Instance.WorkingFolder, filePath);
            var viewerFileContent = File.ReadAllText(fileFullPath);
            foreach (var key in replaceDictionary.Keys)
            {
                viewerFileContent = viewerFileContent.Replace(key, replaceDictionary[key]);
            }
            File.WriteAllText(fileFullPath, viewerFileContent, Encoding.UTF8);
        }
        #endregion File Content
        #region Request Helper
        /// <summary>
        ///     Case sensitive compare for key access 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="accessKey">  </param>
        /// <returns></returns>
        public static bool IsCanAccessSwagger(HttpContext httpContext, string accessKey)
        {
            // Null access key is allow anonymous
            if (string.IsNullOrWhiteSpace(accessKey))
            {
                return true;
            }
            string requestKey = httpContext.Request.Query[ElectSwaggerConstants.AccessKeyName];
            requestKey = string.IsNullOrWhiteSpace(requestKey)
                ? httpContext.Request.Cookies[ElectSwaggerConstants.CookieAccessKeyName]
                : requestKey;
            // Case sensitive compare
            var isCanAccess = accessKey == requestKey;
            return isCanAccess;
        }
        /// <summary>
        ///     Check request access to UI, Json Viewer or Swagger Pure Doc 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="options">    </param>
        /// <returns></returns>
        public static bool IsAccessSwagger(HttpContext httpContext, ElectSwaggerOptions options)
        {
            return IsAccessUI(httpContext, options) || IsAccessJsonViewer(httpContext, options) ||
                   IsAccessSwaggerEndpoint(httpContext, options);
        }
        public static bool IsAccessUI(HttpContext httpContext, ElectSwaggerOptions options)
        {
            var pathQuery = httpContext.Request.Path.Value?.Trim('/').ToLower() ?? string.Empty;
            pathQuery = pathQuery.ToLowerInvariant();
            var documentApiBaseUrl = options.SwaggerRoutePrefix ?? string.Empty;
            documentApiBaseUrl = documentApiBaseUrl.ToLowerInvariant();
            var isSwaggerUi = pathQuery == documentApiBaseUrl || pathQuery == $"{documentApiBaseUrl}/index.html";
            return isSwaggerUi || httpContext.Request.IsRequestFor(options.Url);
        }
        public static bool IsAccessJsonViewer(HttpContext httpContext, ElectSwaggerOptions options)
        {
            return httpContext.Request.IsRequestFor(options.JsonViewerUrl);
        }
        public static bool IsAccessSwaggerEndpoint(HttpContext httpContext, ElectSwaggerOptions options)
        {
            return httpContext.Request.IsRequestFor(GetSwaggerEndpoint(options, false));
        }
        public static string GetSwaggerEndpoint(ElectSwaggerOptions options, bool isIncludeAccessKey = true)
        {
            string swaggerEndpoint = $"/{options.SwaggerRoutePrefix}/{options.Version}/{options.SwaggerName}";
            if (isIncludeAccessKey && !string.IsNullOrWhiteSpace(options.AccessKey))
            {
                swaggerEndpoint += $"?{ElectSwaggerConstants.AccessKeyName}={options.AccessKey}";
            }
            return swaggerEndpoint;
        }
        #endregion Request Helper
    }
}
