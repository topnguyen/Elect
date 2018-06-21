using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elect.Core.ConcurrentUtils.Models;
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
        /// <summary>
        ///     Modify log info or do some logic before Elect write log.
        /// </summary>
        public Func<LogModel, LogModel> BeforeLog { get; set; }

        /// <summary>
        ///     Modify log info or do some logic after Elect write log.
        /// </summary>
        public Func<LogModel, LogModel> AfterLog { get; set; }

        private readonly ElectLogOptions _options;

        public ElectLog(IOptions<ElectLogOptions> configuration) : base(configuration.Value.BatchSize, configuration.Value.Threshold)
        {
            _options = configuration.Value;
        }
        
        #region Capture

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

            Push(log);

            return log;
        }

        #endregion

        protected override void Write(ICollection<LogModel> events)
        {
            foreach (var @event in events)
            {
                var log = @event;

                // Before
                if (BeforeLog != null)
                {
                    log = BeforeLog(log);
                }

                if (log == null)
                {
                    continue;
                }

                var jsonFilePath = GetJsonFilePath(_options, log);

                // To File
                if (_options.IsEnableLogToFile)
                {
                    using (var store = new DataStore(jsonFilePath))
                    {
                        WriteMetadata(store);

                        WriteLog(store, log);
                    }
                }

                // To Console
                if (_options.IsEnableLogToConsole)
                {
                    WriteConsole(log);
                }

                // After
                AfterLog?.Invoke(log);
            }
        }

        #region Json File Path Helper

        private static string GetJsonFilePath(ElectLogOptions options, LogModel log)
        {
            var jsonFilePath = Path.GetFullPath(options.JsonFilePath);

            // Replace {Type}
            jsonFilePath = GetFilePathByType(jsonFilePath, log);

            // Repalce {<DateTimeFormat>}
            var utcNow = DateTimeOffset.UtcNow;
            jsonFilePath = GetFilePathByDateTime(jsonFilePath, utcNow);

            // Directory Handle
            CreateNotExistDirectory(jsonFilePath);

            return jsonFilePath;
        }

        private static string GetFilePathByType(string jsonFilePath, LogModel log)
        {
            jsonFilePath = jsonFilePath.Replace("{Type}", log.Type.ToString());
            return jsonFilePath;
        }

        private static string GetFilePathByDateTime(string jsonFilePath, DateTimeOffset dateTime)
        {
            while (true)
            {
                var iStartParam = jsonFilePath.IndexOf("{", StringComparison.Ordinal);
                var iEndParam = jsonFilePath.IndexOf("}", StringComparison.Ordinal);

                // Don't have any params
                if (iStartParam <= 0 || iEndParam <= 0 || iStartParam >= iEndParam)
                {
                    return jsonFilePath;
                }

                var length = iEndParam - iStartParam + 1; // Include last char

                var param = jsonFilePath.Substring(iStartParam, length);

                var value = dateTime.ToString(param).Trim('{', '}');

                jsonFilePath = jsonFilePath.Replace(param, value);
            }
        }

        private static void CreateNotExistDirectory(string jsonFilePath)
        {
            var jsonFolderPath = Path.GetDirectoryName(jsonFilePath);

            if (jsonFolderPath == null)
            {
                return;
            }

            if (!Directory.Exists(jsonFolderPath))
            {
                Directory.CreateDirectory(jsonFolderPath);
            }
        }

        #endregion

        #region Write helper

        private static void WriteMetadata(IDataStore store)
        {
            var metadatas = store.GetCollection<ElectLogMetadataModel>("metadata");
            var metadata = metadatas.AsQueryable().FirstOrDefault();
            if (metadata == null)
            {
                metadata = new ElectLogMetadataModel();
                metadata.CreatedTime = metadata.LastUpdatedTime = DateTimeOffset.UtcNow;
                metadatas.InsertOne(metadata);
            }
            else
            {
                metadata.LastUpdatedTime = DateTimeOffset.UtcNow;
                metadatas.UpdateOne(x => true, metadata);
            }
        }

        private static void WriteLog(IDataStore store, LogModel log)
        {
            var logs = store.GetCollection<LogModel>("logs");

            logs.InsertOne(log);
        }

        private static void WriteConsole(LogModel log)
        {
            string prefixText;

            ConsoleColor color = ConsoleColor.Red;

            switch (log.Type)
            {
                case LogType.Debug:
                {
                    color = ConsoleColor.Yellow;
                    prefixText = "[D]";
                    break;
                }
                case LogType.Info:
                {
                    color = ConsoleColor.Cyan;
                    prefixText = "[I]";
                    break;
                }
                case LogType.Warning:
                {
                    color = ConsoleColor.DarkYellow;
                    prefixText = "[W]";
                    break;
                }
                case LogType.Error:
                {
                    color = ConsoleColor.Red;
                    prefixText = "[E]";
                    break;
                }
                case LogType.Fatal:
                {
                    color = ConsoleColor.Magenta;
                    prefixText = "[F]";
                    break;
                }
                default:
                {
                    var logType = log.Type.ToString();

                    logType = logType.Length > 4 ? logType.Substring(0, 4) : logType;

                    prefixText = $"[{logType}]";
                    break;
                }
            }

            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(prefixText);
            Console.ResetColor();

            if (!string.IsNullOrWhiteSpace(log.ExceptionPlace))
            {
                Console.WriteLine($" {log.ExceptionPlace}.");
            }

            string logMessage;

            if (log.Exceptions?.Any() == true)
            {
                var jsonSetting = Core.Constants.Formatting.JsonSerializerSettings;

                jsonSetting.Formatting = Formatting.Indented;

                logMessage = JsonConvert.SerializeObject(log.Exceptions, jsonSetting);
            }
            else
            {
                logMessage = log.Message;
            }

            Console.WriteLine(logMessage);
        }

        #endregion
    }
}