using System;
using System.Collections.Generic;
using System.IO;
using Elect.Core.Interfaces;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Logging.Models
{
    public class ElectLogOptions : IElectOptions
    {
        /// <summary>
        ///     Relative Path of Json File, default is Logs/{yyyy-MM-dd}.json
        /// </summary>
        /// <remarks>Path.GetFullPath(JsonFilePath) to get full path of json files</remarks>
        public string JsonFilePath { get; set; } = "Logs" + Path.DirectorySeparatorChar + "{yyyy-MM-dd}.json";

        public uint BatchSize { get; set; } = 20;

        public TimeSpan Threshold { get; set; } = TimeSpan.FromSeconds(2);

        public bool IsEnableLogToConsole { get; set; } = true;

        public bool IsEnableLogToFile { get; set; } = true;

        /// <summary>
        ///     Api Document Endpoint, default is "/developers/logs". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string Url { get; set; } = "/developers/logs";

        /// <summary>
        ///     Access Key via uri param "key", default is "" - allow anonymous. 
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;

        /// <summary>
        ///     Un-authorize message when user access api document with not correct key. Default is
        ///     "You don't have permission to view API Document, please contact your administrator."
        /// </summary>
        public string UnAuthorizeMessage { get; set; } =
            "You don't have permission to view Develop Logs, please contact your administrator.";

        /// <summary>
        ///    By default when user access Log Dashboard Url. <br />
        ///    We support filter log by query strings "skip" (int), "take" (int), "message" (string), "type" (string) <br />
        ///    Then you can handle more by add Func BeforeLogResponse. 
        /// </summary>
        /// <remarks>The query string "full_info" (bool) auto apply before response after this Func</remarks>
        public Func<HttpContext, IEnumerable<LogModel>, List<LogModel>> BeforeLogResponse { get; set; }
    }
}