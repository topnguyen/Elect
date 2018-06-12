using System;
using System.Collections.Generic;
using Elect.Core.ObjUtils;
using Elect.Logger.Models.Logging;
using Elect.Logger.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Elect.Logger.Logging
{
    public class ElectLog : ElectMessageQueue<LogModel>
    {
        private static ElectLog _instance;

        public static ElectLog Instance => _instance = _instance ?? new ElectLog();

        public Func<LogModel, LogModel> BeforeLog { get; set; }

        public LogModel Capture(string message, LogType type = LogType.Error, HttpContext httpContent = null)
        {
            var log = new LogModel(message, httpContent) {Type = type};

            return Capture(log);
        }

        public LogModel Capture(Exception exception, LogType type = LogType.Error, HttpContext httpContent = null)
        {
            var log = new LogModel(exception, httpContent) {Type = type};

            return Capture(log);
        }

        public LogModel Capture(object obj, LogType type = LogType.Error, HttpContext httpContent = null)
        {
            var log = new LogModel(obj, httpContent) {Type = type};

            return Capture(log);
        }

        private LogModel Capture(LogModel log)
        {
            // Convert to Json string for Filter purpose
            var logJsonStr = log.ToJsonString();

            // Filter Credit Card
            logJsonStr = CreditCardHelper.Filter(logJsonStr);

            // Update log by filtered info
            log = JsonConvert.DeserializeObject<LogModel>(logJsonStr);

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