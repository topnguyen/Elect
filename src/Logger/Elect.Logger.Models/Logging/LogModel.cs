using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using Elect.Core.ObjUtils;
using Elect.Logger.Models.Logging.Utils;
using Elect.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Elect.Logger.Models.Logging
{
    [Serializable]
    public class LogModel
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTimeOffset CreatedTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LogType Type { get; set; }

        public string Message { get; set; }

        public List<ElectException> Exceptions { get; set; }

        public string ExceptionPlace { get; set; }

        public RuntimeModel Runtime { get; set; }

        public EnvironmentModel EnvironmentModel { get; set; }

        public SdkModel Sdk { get; set; }

        public HttpContextModel HttpContext { get; set; }

        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

        public LogModel()
        {
        }

        /// <summary>
        ///     New Instance of Log Model
        /// </summary>
        /// <param name="obj">
        ///     Can be an Exception or any object (will be serialize to Json String and store in Message property)
        /// </param>
        /// <param name="httpContext">HttpContext of current request if have</param>
        public LogModel(object obj, HttpContext httpContext = null)
        {
            if (obj is Exception exception)
            {
                Initial(exception);
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

            if (httpContext != null)
            {
                HttpContext = new HttpContextModel(httpContext)
                {
                    Id = Id.ToString("N")
                };
            }
        }

        private void Initial(Exception exception)
        {
            Message = exception.Message;

            if (exception.TargetSite != null)
            {
                ExceptionPlace =
                    $"{(exception.TargetSite.ReflectedType == null ? "<dynamic type>" : exception.TargetSite.ReflectedType.FullName)} in {exception.TargetSite.Name}";
            }

            Exceptions = new List<ElectException>();

            for (var currentException = exception;
                currentException != null;
                currentException = currentException.InnerException)
            {
                var electException = new ElectException(currentException)
                {
                    Module = currentException.Source,
                    Type = currentException.GetType().Name,
                    Value = currentException.Message
                };

                Exceptions.Add(electException);
            }

            // ReflectionTypeLoadException doesn't contain much useful info in itself, and needs special handling

            if (exception is ReflectionTypeLoadException reflectionTypeLoadException)
            {
                foreach (var loaderException in reflectionTypeLoadException.LoaderExceptions)
                {
                    var sentryException = new ElectException(loaderException);

                    Exceptions.Add(sentryException);
                }
            }
        }

        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}