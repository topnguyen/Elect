using System;
using System.Collections.Generic;
using System.Reflection;
using Elect.Core.ObjUtils;
using Elect.Logger.Models.Logging.Utils;

namespace Elect.Logger.Models.Logging
{
    public class LogModel
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTimeOffset CreatedTime { get; set; }

        public LogType Type { get; set; }

        public string Message { get; set; }

        public List<ElectException> Exceptions { get; set; }

        /// <summary>
        ///     Function call which was the primary perpetrator of this event.
        /// </summary>
        public string ExceptionPlace { get; set; }

        public RuntimeModel Runtime { get; set; }

        public EnvironmentModel EnvironmentModel { get; set; }

        public SdkModel Sdk { get; set; }

        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

        public LogModel()
        {
        }

        /// <summary>
        ///     New Instance of Log Model
        /// </summary>
        /// <param name="obj">
        ///     Canbe an Exception or any object (will be serialize to Json String and store in Message property)
        /// </param>
        public LogModel(object obj)
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
        }

        private void Initial(Exception exception)
        {
            Message = exception.Message;

            if (exception.TargetSite != null)
            {
                ExceptionPlace =
                    $"{((exception.TargetSite.ReflectedType == null) ? "<dynamic type>" : exception.TargetSite.ReflectedType.FullName)} in {exception.TargetSite.Name}";
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
    }
}