using System;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Logging
{
    public class ElectLog
    {
        public Func<LogModel, LogModel> BeforeLog { get; set; }

        public Func<LogModel, LogModel> AfterLog { get; set; }

        public LogModel Capture(object obj, HttpContext httpContent)
        {
            var log = new LogModel(obj, httpContent);

            if (BeforeLog != null)
            {
                log = BeforeLog(log);
            }
            
            // TODO Capture Here
            
            if (AfterLog != null)
            {
                log = AfterLog(log);
            }

            return log;
        }
    }
}