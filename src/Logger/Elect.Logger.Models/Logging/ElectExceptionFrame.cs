using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Elect.Core.ObjUtils;

namespace Elect.Logger.Models.Logging
{
    public class ElectExceptionFrame : ElectDisposableModel
    {
        /// <summary>
        ///     Gets or sets the absolute path.
        /// </summary>
        /// <value>
        /// The absolute path.
        /// </value>
        public string AbsolutePath { get; set; }

        public int ColumnNo { get; set; }

        public string Filename { get; set; }

        public string Function { get; set; }

        /// <summary>
        ///     Signifies whether this frame is related to the execution of the relevant code in this
        ///     stacktrace. For example, the frames that might power the framework’s webserver of your
        ///     app are probably not relevant, however calls to the framework’s library once you start
        ///     handling code likely are.
        /// </summary>
        /// <value> <c>true</c> unless the StackFrame is part of the System namespace. </value>
        public bool InApp { get; set; }

        public int LineNo { get; set; }

        public string Module { get; set; }

        public string Source { get; set; }

        public ElectExceptionFrame(StackFrame frame)
        {
            if (frame == null)
            {
                return;
            }

            int lineNo = frame.GetFileLineNumber();

            if (lineNo == 0)
            {
                //The pdb files aren't currently available
                lineNo = frame.GetILOffset();
            }

            var method = frame.GetMethod();
            if (method != null)
            {
                Module = (method.DeclaringType != null) ? method.DeclaringType.FullName : null;
                Function = method.Name;
                Source = method.ToString();
            }

            AbsolutePath = frame.GetFileName();

            if (!string.IsNullOrWhiteSpace(AbsolutePath))
            {
                Filename = Path.GetFileName(AbsolutePath);
            }

            LineNo = lineNo;

            ColumnNo = frame.GetFileColumnNumber();

            InApp = !IsSystemModuleName(Module);

            DemangleAsyncFunctionName();

            DemangleAnonymousFunction();
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Module != null)
            {
                sb.Append(Module);
                sb.Append('.');
            }

            if (Function != null)
            {
                sb.Append(Function);
                sb.Append("()");
            }

            if (Filename != null)
            {
                sb.Append(" in ");
                sb.Append(Filename);
            }

            if (LineNo > -1)
            {
                sb.Append(":line ");
                sb.Append(LineNo);
            }

            return sb.ToString();
        }

        private static bool IsSystemModuleName(string moduleName)
        {
            return !string.IsNullOrEmpty(moduleName) &&
                   (moduleName.StartsWith("System.", System.StringComparison.Ordinal) ||
                    moduleName.StartsWith("Microsoft.", System.StringComparison.Ordinal));
        }

        /// <summary>
        ///     Clean up function and module names produced from `async` state machine calls.
        /// </summary>
        /// <para>
        /// When the Microsoft cs.exe compiler compiles some modern C# features,
        /// such as async/await calls, it can create synthetic function names that
        /// do not match the function names in the original source code. Here we
        /// reverse some of these transformations, so that the function and module
        /// names that appears in the Sentry UI will match the function and module
        /// names in the original source-code.
        /// </para>
        private void DemangleAsyncFunctionName()
        {
            if (Module == null || Function != "MoveNext")
            {
                return;
            }

            //  Search for the function name in angle brackets followed by d__<digits>.
            //
            // Change:
            //   RemotePrinterService+<UpdateNotification>d__24 in MoveNext at line 457:13
            // to:
            //   RemotePrinterService in UpdateNotification at line 457:13

            var mangled = @"^(.*)\+<(\w*)>d__\d*$";
            var match = Regex.Match(Module, mangled);
            if (match.Success && match.Groups.Count == 3)
            {
                Module = match.Groups[1].Value;
                Function = match.Groups[2].Value;
            }
        }

        /// <summary>
        ///     Clean up function names for anonymous lambda calls.
        /// </summary>
        private void DemangleAnonymousFunction()
        {
            if (Function == null)
            {
                return;
            }

            // Search for the function name in angle brackets followed by b__<digits/letters>.
            //
            // Change:
            //   <BeginInvokeAsynchronousActionMethod>b__36
            // to:
            //   BeginInvokeAsynchronousActionMethod { <lambda> }

            var mangled = @"^<(\w*)>b__\w+$";
            var match = Regex.Match(Function, mangled);
            if (match.Success && match.Groups.Count == 2)
            {
                Function = match.Groups[1].Value + " { <lambda> }";
            }
        }
    }
}