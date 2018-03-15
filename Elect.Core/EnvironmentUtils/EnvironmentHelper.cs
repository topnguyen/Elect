using System;

namespace Elect.Core.EnvironmentUtils
{
    public static class EnvironmentHelper
    {
        public const string AspNetCoreEnvironmentVariable = "ASPNETCORE_ENVIRONMENT";

        public static string CurrentEnvironment => Environment.GetEnvironmentVariable(AspNetCoreEnvironmentVariable);

        public static readonly string MachineName = Environment.MachineName;

        public static bool IsDevelopment()
        {
            return Is("Development");
        }

        public static bool IsStaging()
        {
            return Is("Staging");
        }

        public static bool IsProduction()
        {
            return Is("Production");
        }

        public static bool Is(string environment)
        {
            return string.Equals(CurrentEnvironment, environment, StringComparison.OrdinalIgnoreCase);
        }
    }
}