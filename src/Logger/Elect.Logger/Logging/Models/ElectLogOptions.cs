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

        
        /// <summary>
        ///     Batch size for log message queue
        /// </summary>
        /// <remarks>Default is 20</remarks>
        public uint BatchSize { get; set; } = 20;

        /// <summary>
        ///     Threshold for log message queue
        /// </summary>
        /// <remarks>Default is 5 seconds</remarks>
        public TimeSpan Threshold { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        ///     Log will write immediately to the Console after the Capture feature executed.
        /// </summary>
        public bool IsEnableLogToConsole { get; set; } = true;

        /// <summary>
        ///     Log will push to a queue then write to the File after the Capture feature executed.
        /// </summary>
        public bool IsEnableLogToFile { get; set; } = true;

        /// <summary>
        ///     Limit log information,
        ///     if false will force null for <see cref="LogModel"/>
        ///     <see cref="LogModel.Runtime"/>, <see cref="LogModel.Sdk"/>,
        ///     <see cref="LogModel.EnvironmentModel"/>, <see cref="LogModel.HttpContext"/>.
        /// </summary>
        /// <remarks>Default is false</remarks>
        public bool IsLogFullInfo { get; set; }

        /// <summary>
        ///     Api Document Endpoint, default is "/developers/logs". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string Url { get; set; } = "/developers/logs";

        /// <summary>
        ///     Access Key via uri param "key" 
        /// </summary>
        /// <remarks>Default is "" - allow anonymous</remarks>
        public string AccessKey { get; set; } = string.Empty;

        /// <summary>
        ///     Un-authorize message when user access Log Dashboard with not correct key
        /// </summary>
        /// <remarks>Default is "You don't have permission to view Log Dashboard, please contact your administrator."</remarks>
        public string UnAuthorizeMessage { get; set; } = "You don't have permission to view Log Dashboard, please contact your administrator.";

        /// <summary>
        ///    By default when user access Log Dashboard Url. <br />
        ///    Support filter log detail by query strings "skip" (int), "take" (int), "type" (string), "exception_place" (string), "message" (string) <br />
        ///    Then you can handle more by add Func BeforeLogResponse. 
        /// </summary>
        /// <remarks>The query string "full_info" (bool) auto apply before response after this Func</remarks>
        public Func<HttpContext, IEnumerable<LogModel>, IEnumerable<LogModel>> BeforeLogResponse { get; set; }
    }
}