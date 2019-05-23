using System;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Logging
{
    public interface IElectLog
    {
        /// <summary>
        ///     Capture/Log the message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="type">Log Type</param>
        /// <param name="httpContext">Http context when capture the message</param>
        /// <param name="jsonFilePath"> Override file path of Config, can be absolute or relative file path</param>
        /// <returns></returns>
        LogModel Capture(string message, LogType type = LogType.Info, HttpContext httpContext = null, string jsonFilePath = null);

        /// <summary>
        ///     Capture/Log the exception
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="type">Log Type</param>
        /// <param name="httpContext">Http context when capture the exception</param>
        /// <param name="jsonFilePath"> Override file path of Config, can be absolute or relative file path</param>
        /// <returns></returns>
        LogModel Capture(Exception exception, LogType type = LogType.Error, HttpContext httpContext = null, string jsonFilePath = null);

        /// <summary>
        ///     Capture/Log the object data
        /// </summary>
        /// <param name="obj">Data</param>
        /// <param name="type">Log Type</param>
        /// <param name="httpContext">Http context when capture the data</param>
        /// <param name="jsonFilePath"> Override file path of Config, can be absolute or relative file path</param>
        /// <returns></returns>
        LogModel Capture(object obj, LogType type = LogType.Info, HttpContext httpContext = null, string jsonFilePath = null);

        /// <summary>
        ///     Capture/log info by log model
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        LogModel Capture(LogModel log);

    }
}