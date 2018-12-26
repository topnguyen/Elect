using System.IO;
using System.Linq;
using Elect.Core.LinqUtils;
using Elect.Core.ObjUtils;
using Elect.Logger.Logging.Models;
using Elect.Logger.Models.Logging;
using Elect.Web.HttpUtils;
using Elect.Web.Models;
using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Logger.Utils
{
    public class LogHelper
    {
        #region Content Result

        public static ContentResult GetLogContentResult(HttpContext context, ElectLogOptions options)
        {
            ContentResult contentResult;

            var requestPath = context.Request.Path.Value.Trim('/');

            var summaryUrl = options.Url.Trim('/');

            contentResult = requestPath == summaryUrl
                ? GetSummaryContentResult(context, options)
                : GetDetailContentResult(context, options);

            return contentResult;
        }

        private static ContentResult GetSummaryContentResult(HttpContext context, ElectLogOptions options)
        {
            var storageFolder = Path.GetDirectoryName(Path.GetFullPath(options.JsonFilePath));

            if (string.IsNullOrWhiteSpace(storageFolder))
            {
                return new ContentResult
                {
                    ContentType = ContentType.Json,
                    StatusCode = StatusCodes.Status204NoContent,
                    Content = string.Empty
                };
            }

            var logFilesPath = Directory.GetFiles(storageFolder);

            var summaryModel = new ElectLogSummaryModel {TotalFile = logFilesPath.Length};

            var domain = context.Request.GetDomain().Trim('/');

            var summaryUrl = options.Url.Trim('/');

            foreach (var logFilePath in logFilesPath)
            {
                using (var file = new DataStore(logFilePath))
                {
                    var meta = file.GetCollection<ElectLogMetadataModel>("metadata").AsQueryable().FirstOrDefault();

                    if (meta == null)
                    {
                        continue;
                    }

                    var data = file.GetCollection("logs").AsQueryable().ToList(10000);

                    summaryModel.TotalLog += data.Count;

                    summaryModel.Files.Add(new ElectLogFileSummaryModel
                    {
                        TotalLog = data.Count,
                        FileName = Path.GetFileName(logFilePath),
                        CreatedAt = meta.CreatedTime,
                        LastUpdatedAt = meta.LastUpdatedTime,
                        ViewDetailUrl = Path.Combine(domain, summaryUrl, Path.GetFileName(logFilePath))
                    });
                }
            }

            string content = summaryModel.ToJsonString();

            ContentResult contentResult = new ContentResult
            {
                ContentType = ContentType.Json,
                StatusCode = StatusCodes.Status200OK,
                Content = content
            };
            return contentResult;
        }

        private static ContentResult GetDetailContentResult(HttpContext context, ElectLogOptions options)
        {
            var storageFolder = Path.GetDirectoryName(Path.GetFullPath(options.JsonFilePath));

            if (string.IsNullOrWhiteSpace(storageFolder))
            {
                return new ContentResult
                {
                    ContentType = ContentType.Json,
                    StatusCode = StatusCodes.Status204NoContent,
                    Content = string.Empty
                };
            }

            var requestPath = context.Request.Path.Value.Trim('/');

            var lastPath = requestPath.Split('/').LastOrDefault();

            var logFilePath = Directory.GetFiles(storageFolder).FirstOrDefault(x => Path.GetFileName(x) == lastPath);

            using (var file = new DataStore(logFilePath))
            {
                var meta = file.GetCollection<ElectLogMetadataModel>("metadata").AsQueryable().FirstOrDefault();

                if (meta == null)
                {
                    return new ContentResult
                    {
                        ContentType = ContentType.Json,
                        StatusCode = StatusCodes.Status204NoContent,
                        Content = string.Empty
                    };
                }

                var logs = file.GetCollection("logs").AsQueryable().ToList(10000);

                var content = new
                {
                    meta,
                    logs
                }.ToJsonString();

                ContentResult contentResult = new ContentResult
                {
                    ContentType = ContentType.Json,
                    StatusCode = StatusCodes.Status200OK,
                    Content = content
                };
                return contentResult;
            }
        }

        #endregion

        #region Access

        public static bool IsAccessLog(HttpContext httpContext, ElectLogOptions options)
        {
            var isRequestForSummaryLog = httpContext.Request.IsRequestFor(options.Url);

            if (isRequestForSummaryLog)
            {
                return true;
            }

            var isRequestForDetailLog = httpContext.Request.Path.StartsWithSegments(options.Url);

            return isRequestForDetailLog;
        }

        public static bool IsCanAccessLog(HttpContext httpContext, string accessKey)
        {
            // Null access key is allow anonymous
            if (string.IsNullOrWhiteSpace(accessKey))
            {
                return true;
            }

            string requestKey = httpContext.Request.Query[ElectLogConstants.AccessKeyName];

            requestKey = string.IsNullOrWhiteSpace(requestKey)
                ? httpContext.Request.Cookies[ElectLogConstants.CookieAccessKeyName]
                : requestKey;

            // Case sensitive compare
            var isCanAccess = accessKey == requestKey;

            return isCanAccess;
        }

        #endregion
    }
}