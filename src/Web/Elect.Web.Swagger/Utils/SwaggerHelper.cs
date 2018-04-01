#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SwaggerHelper.cs </Name>
//         <Created> 01/04/2018 11:36:49 PM </Created>
//         <Key> edf63670-33e9-4fa8-8b45-74610d05f4ca </Key>
//     </File>
//     <Summary>
//         SwaggerHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO;
using Elect.Web.HttpUtils;
using Elect.Web.Models;
using Elect.Web.Swagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Elect.Web.Swagger.Utils
{
    internal class SwaggerHelper
    {
        internal const string AssetsUrl = "/developers/assets";

        internal const string AccessKeyName = "key";

        internal const string AssetFolderName = "apidoc";

        internal static readonly string IndexFileFullPath = $"{AssetFolderName}/index.html";

        internal static readonly string JsonViewerFileFullPath = $"{AssetFolderName}/json-viewer.html";

        private static string IndexFileContent { get; set; }

        private static string JsonViewerFileContent { get; set; }

        #region Html Content

        internal static ContentResult GetApiDocHtml()
        {
            if (string.IsNullOrWhiteSpace(IndexFileContent))
            {
                string indexFilePath = PathHelper.GetFullPath(IndexFileFullPath);

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

        internal static ContentResult GetApiJsonViewerHtml()
        {
            if (string.IsNullOrWhiteSpace(JsonViewerFileContent))
            {
                var jsonViewerFilePath = PathHelper.GetFullPath(JsonViewerFileFullPath);

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

        internal static void UpdateApiDocFileContent(string title, string swaggerEndpoint, string authTokenKeyPrefix, string jsonViewerUrl)
        {
            UpdateFileContent(new Dictionary<string, string>
            {
                {"@AssetPath", AssetsUrl},
                {"@ApiDocumentHtmlTitle", title},
                {"@SwaggerEndpoint", swaggerEndpoint},
                {"@AuthTokenKeyPrefix", authTokenKeyPrefix},
                {"@JsonViewerUrl", jsonViewerUrl }
            }, IndexFileFullPath);
        }

        internal static void UpdateApiJsonViewerFileContent(string title)
        {
            UpdateFileContent(new Dictionary<string, string>
            {
                {"@AssetPath", AssetsUrl},
                {"@ApiDocumentHtmlTitle", title}
            }, JsonViewerFileFullPath);
        }

        internal static void UpdateFileContent(Dictionary<string, string> replaceDictionary, string filePath)
        {
            string fileFullPath = PathHelper.GetFullPath(filePath);

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
        internal static bool IsCanAccessSwagger(HttpContext httpContext, string accessKey)
        {
            // Null access key is allow anonymous
            if (string.IsNullOrWhiteSpace(accessKey))
            {
                return true;
            }

            string paramKeyValue = httpContext.Request.Query[AccessKeyName];

            // Case sensitive compare
            var isCanAccess = accessKey == paramKeyValue;

            return isCanAccess;
        }

        /// <summary>
        ///     Check request access to UI, Json Viewer or Swagger Pure Doc 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="options">    </param>
        /// <returns></returns>
        internal static bool IsAccessSwagger(HttpContext httpContext, ElectSwaggerOptions options)
        {
            return IsAccessUI(httpContext, options) || IsAccessJsonViewer(httpContext, options) || IsAccessSwaggerEndpoint(httpContext, options);
        }

        internal static bool IsAccessUI(HttpContext httpContext, ElectSwaggerOptions options)
        {
            var pathQuery = httpContext.Request.Path.Value?.Trim('/').ToLower() ?? string.Empty;

            pathQuery = pathQuery.ToLowerInvariant();

            var documentApiBaseUrl = options.SwaggerRoutePrefix ?? string.Empty;

            documentApiBaseUrl = documentApiBaseUrl.ToLowerInvariant();

            var isSwaggerUi = pathQuery == documentApiBaseUrl || pathQuery == $"{documentApiBaseUrl}/index.html";

            return isSwaggerUi || httpContext.Request.IsRequestFor(options.Url);
        }

        internal static bool IsAccessJsonViewer(HttpContext httpContext, ElectSwaggerOptions options)
        {
            return httpContext.Request.IsRequestFor(options.JsonViewerUrl);
        }

        internal static bool IsAccessSwaggerEndpoint(HttpContext httpContext, ElectSwaggerOptions options)
        {
            return httpContext.Request.IsRequestFor(GetSwaggerEndpoint(options, false));
        }

        internal static string GetSwaggerEndpoint(ElectSwaggerOptions options, bool isIncludeAccessKey = true)
        {
            string swaggerEndpoint = $"/{options.SwaggerRoutePrefix}/{options.Version}/{options.SwaggerName}";

            if (isIncludeAccessKey && !string.IsNullOrWhiteSpace(options.AccessKey))
            {
                swaggerEndpoint += $"?{AccessKeyName}={options.AccessKey}";
            }

            return swaggerEndpoint;
        }

        #endregion Request Helper
    }
}