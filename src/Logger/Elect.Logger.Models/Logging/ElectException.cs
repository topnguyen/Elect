using System;
using System.Text;

namespace Elect.Logger.Models.Logging
{
    public class ElectException
    {
        private readonly string _message;

        public string Module { get; set; }

        public ElectStacktrace Stacktrace { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public ElectException(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            _message = exception.Message;

            Module = exception.Source;

            Type = exception.GetType().FullName;

            Value = exception.Message;

            Stacktrace = new ElectStacktrace(exception);

            if (Stacktrace.Frames == null || Stacktrace.Frames.Length == 0)
            {
                Stacktrace = null;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Type != null)
            {
                sb.Append(Type);
            }

            if (_message != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append(": ");
                }

                sb.Append(_message);

                sb.AppendLine();
            }

            if (Stacktrace != null)
            {
                sb.Append(Stacktrace);
            }

            return sb.ToString().TrimEnd();
        }
    }
}