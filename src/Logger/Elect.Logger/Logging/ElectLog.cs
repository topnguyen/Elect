using System;
using System.Collections.Generic;
using Elect.Core.ObjUtils;
using Elect.Logger.Logging.Models;
using Elect.Logger.Models.Logging;
using Elect.Logger.Utils;
using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Elect.Logger.Logging
{
    public class ElectLog : ElectMessageQueue<LogModel>, IElectLog
    {
        public Func<LogModel, LogModel> BeforeLog { get; set; }

        private ElectLogOptions _options;
        
        public ElectLog(IOptions<ElectLogOptions> configuration)
        {
            _options = configuration.Value;
        }

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
                Console.WriteLine("Write Log " + logModel.Id + " in " + _options.JsonFilePath);
            }
            
            // TODO write to JSON file
//            var store = new DataStore("data.json");
        }
    }
}