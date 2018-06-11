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

        public DateTimeOffset CreatedTime { get; } = DateTimeOffset.UtcNow;

        public LogType Type { get; set; } = LogType.Error;

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public RuntimeModel Runtime { get; set; } = RuntimeHelper.Get();

        public EnvironmentModel EnvironmentModel { get; set; } = EnvironmentHelper.Get();

        public SdkModel Sdk { get; set; } = SdkHelper.Get(Assembly.GetCallingAssembly().GetName());

        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

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
        }
    }
}