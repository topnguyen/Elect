using System;
using System.Collections.Generic;
using System.Reflection;
using Elect.Core.ObjUtils;
using Elect.Logger.Models.Logging.Utils;
using Newtonsoft.Json;

namespace Elect.Logger.Models.Logging
{
    public class LogModel
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTimeOffset CreatedTime { get; }

        public LogType Type { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public RuntimeModel Runtime { get; set; }

        public EnvironmentModel EnvironmentModel { get; set; }

        public SdkModel Sdk { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

        public LogModel()
        {
        }

        /// <summary>
        ///     New Instance of Log Model
        /// </summary>
        /// <param name="obj">Canbe an Exception or any object (will be serialize to Json String and store in Message property)</param>
        public LogModel(object obj)
        {
            if (obj is Exception exception)
            {
                Exception = exception;
            }
            else
            {
                Message = obj.ToJsonString();
            }

            CreatedTime = DateTimeOffset.UtcNow;

            Type = LogType.Error;

            Runtime = RuntimeHelper.Get();

            EnvironmentModel = EnvironmentHelper.Get();

            Sdk = SdkHelper.Get(Assembly.GetCallingAssembly().GetName());
        }
    }
}