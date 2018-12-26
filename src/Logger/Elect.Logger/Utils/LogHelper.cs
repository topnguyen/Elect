using System;
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
            var requestPath = context.Request.Path.Value.Trim('/');

            var summaryUrl = options.Url.Trim('/');

            var contentResult = requestPath == summaryUrl
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
                    StatusCode = StatusCodes.Status200OK,
                    Content = "{}"
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
            // Get Log File

            var requestPath = context.Request.Path.Value.Trim('/');

            var lastPath = requestPath.Split('/').LastOrDefault();

            var storageFolder = Path.GetDirectoryName(Path.GetFullPath(options.JsonFilePath));

            if (string.IsNullOrWhiteSpace(storageFolder))
            {
                return new ContentResult
                {
                    ContentType = ContentType.Json,
                    StatusCode = StatusCodes.Status200OK,
                    Content = "{}"
                };
            }

            var logFilePath = Directory.GetFiles(storageFolder).FirstOrDefault(x => Path.GetFileName(x) == lastPath);

            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                return new ContentResult
                {
                    ContentType = ContentType.Json,
                    StatusCode = StatusCodes.Status200OK,
                    Content = "{}"
                };
            }

            // Skip
            int skip = 0;
            if (context.Request.Query.TryGetValue("skip", out var skipStr))
            {
                if (int.TryParse(skipStr, out var skipInt))
                {
                    skip = skipInt;
                }
            }

            // Take
            int take = 1000;
            if (context.Request.Query.TryGetValue("take", out var takeStr))
            {
                if (int.TryParse(takeStr, out var takeInt))
                {
                    take = takeInt;
                }
            }

            // Full Info or not
            bool isFullInfo = false;
            if (context.Request.Query.TryGetValue("full_info", out var fullInfoStr))
            {
                if (bool.TryParse(fullInfoStr, out var fullInfoBool))
                {
                    isFullInfo = fullInfoBool;
                }
            }

            // Message Filter
            context.Request.Query.TryGetValue("message", out var message);

            // Log Type Filter
            LogType? logType = null;
            if (context.Request.Query.TryGetValue("type", out var type))
            {
                if (Enum.TryParse(type, out LogType logTypeEnum))
                {
                    logType = logTypeEnum;
                }
            }

            using (var file = new DataStore(logFilePath))
            {
                var meta = file.GetCollection<ElectLogMetadataModel>("metadata").AsQueryable().FirstOrDefault();

                if (meta == null)
                {
                    return new ContentResult
                    {
                        ContentType = ContentType.Json,
                        StatusCode = StatusCodes.Status200OK,
                        Content = "{}"
                    };
                }

                var logs = file.GetCollection<LogModel>("logs").AsQueryable().Skip(skip).Take(take);

                // Filter by Type

                if (logType != null)
                {
                    logs = logs.Where(x => x.Type == logType);
                }

                // Filter by Message

                if (!string.IsNullOrWhiteSpace(message))
                {
                    logs = logs.Where(x =>
                        !string.IsNullOrWhiteSpace(x.Message) && x.Message.Contains(message) ||
                        message.Contains(x.Message));
                }

                if (options.BeforeLogResponse != null)
                {
                    logs = options.BeforeLogResponse(context, logs);
                }

                var resultLogs = logs.ToList(10000);

                if (!isFullInfo)
                {
                    foreach (var logModel in resultLogs)
                    {
                        logModel.HttpContext = null;
                        logModel.Runtime = null;
                        logModel.EnvironmentModel = null;
                        logModel.Sdk = null;
                    }
                }
                
                var content = new
                {
                    meta,
                    logs = resultLogs
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