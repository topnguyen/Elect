using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Elect.Logger.Models.Logging
{
    public class ElectStacktrace
    {
        public ElectExceptionFrame[] Frames { get; set; }

        public ElectStacktrace(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            var trace = new StackTrace(exception, true);

            var frames = trace.GetFrames();

            if (frames == null)
            {
                return;
            }

            // Elect expects the frames to be sent in reversed order
            Frames = frames.Reverse().Select(f => new ElectExceptionFrame(f)).ToArray();
        }

        public override string ToString()
        {
            if (Frames == null || !Frames.Any())
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            // Have to reverse the frames before presenting them 
            // since they are stored in reversed order.

            foreach (var frame in Frames.Reverse())
            {
                sb.Append("   at ");
                sb.Append(frame);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}