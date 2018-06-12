using System;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Logging
{
    public interface IElectLog
    {
        Func<LogModel, LogModel> BeforeLog { get; set; }

        LogModel Capture(string message, LogType type = LogType.Error, HttpContext httpContent = null);

        LogModel Capture(Exception exception, LogType type = LogType.Error, HttpContext httpContent = null);

        LogModel Capture(object obj, LogType type = LogType.Error, HttpContext httpContent = null);
    }
}