using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Elect.Logger.Models.Logging.Utils
{
    internal static class EnvironmentHelper
    {
        public static EnvironmentModel Get()
        {
            EnvironmentModel environmentInfo = new EnvironmentModel();

            // OS Info

            environmentInfo.OsName = GetOsPlatform();

            environmentInfo.OsVersion = Environment.OSVersion.VersionString;

            environmentInfo.OsDescription = RuntimeInformation.OSDescription;

            environmentInfo.OsArchitecture = RuntimeInformation.OSArchitecture.ToString();

            // OS Time Zone

            environmentInfo.OsTimeZoneId = TimeZoneInfo.Local.Id;

            environmentInfo.OsTimeZone = TimeZoneInfo.Local.BaseUtcOffset;

            environmentInfo.OsTimeZoneDisplay = TimeZoneInfo.Local.DisplayName;

            // OS Boot Time
            environmentInfo.OsBootTime =
                new DateTimeOffset(DateTime.UtcNow - TimeSpan.FromTicks(Stopwatch.GetTimestamp()),
                    TimeSpan.Zero);

            // Framework Info

            environmentInfo.FrameworkName = ".NET Core";

            environmentInfo.FrameworkDescription = RuntimeInformation.FrameworkDescription;

            // Framework Version

            var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;

            var assemblyPath = assembly.CodeBase.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);

            var netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");

            if (!(netCoreAppIndex <= 0 && netCoreAppIndex >= assemblyPath.Length - 2))
            {
                environmentInfo.FrameworkVersion = assemblyPath[netCoreAppIndex + 1];
            }

            return environmentInfo;
        }

        /// <summary>
        ///     Get OS platform
        /// </summary>
        /// <returns></returns>
        public static string GetOsPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows.ToString();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX.ToString();
            }

            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? OSPlatform.Linux.ToString()
                : "Others";
        }
    }
}