using System;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Logging
{
    public interface IElectLog
    {
        /// <summary>
        ///     Modify log info or do some logic before Elect write log.
        /// </summary>
        Func<LogModel, LogModel> BeforeLog { get; set; }

        /// <summary>
        ///     Modify log info or do some logic after Elect write log.
        /// </summary>
        Func<LogModel, LogModel> AfterLog { get; set; }
        
        LogModel Capture(string message, LogType type = LogType.Error, HttpContext httpContent = null);

        LogModel Capture(Exception exception, LogType type = LogType.Error, HttpContext httpContent = null);

        LogModel Capture(object obj, LogType type = LogType.Error, HttpContext httpContent = null);
    }
}