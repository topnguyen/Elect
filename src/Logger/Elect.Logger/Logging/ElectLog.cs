using System;
using System.Collections.Generic;
using Elect.Logger.Models.Logging;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Logging
{
    public class ElectLog : ElectMessageQueue<LogModel>
    {
        public Func<LogModel, LogModel> BeforeLog { get; set; }

        public LogModel Capture(string message, HttpContext httpContent = null)
        {
            var log = new LogModel(message, httpContent);

            return Capture(log);
        }

        public LogModel Capture(Exception exception, HttpContext httpContent = null)
        {
            var log = new LogModel(exception, httpContent);

            return Capture(log);
        }

        public LogModel Capture(object obj, HttpContext httpContent = null)
        {
            var log = new LogModel(obj, httpContent);

            return Capture(log);
        }

        private LogModel Capture(LogModel log)
        {
            if (BeforeLog != null)
            {
                log = BeforeLog(log);
            }

            if (log != null)
            {
                Push(log);
            }

            return log;
        }

        protected override void Write(ICollection<LogModel> events)
        {
            foreach (var logModel in events)
            {
                Console.WriteLine("Write Log " + logModel.Id);
            }

//            throw new NotImplementedException();
        }
    }
}