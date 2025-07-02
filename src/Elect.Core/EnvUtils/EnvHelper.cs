namespace Elect.Core.EnvUtils
{
    public static class EnvHelper
    {
        public const string AspNetCoreEnvironmentVariable = "ASPNETCORE_ENVIRONMENT";
        public const string EnvDevelopmentName = "Development";
        public const string EnvStagingName = "Staging";
        public const string EnvProductionName = "Production";
        private static string _currentEnvironment;
        public static string CurrentEnvironment
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_currentEnvironment))
                {
                    return _currentEnvironment;
                }
                _currentEnvironment = Environment.GetEnvironmentVariable(AspNetCoreEnvironmentVariable);
                if (string.IsNullOrWhiteSpace(_currentEnvironment))
                {
                    _currentEnvironment = EnvDevelopmentName;
                }
                return _currentEnvironment;
            }
        }
        public static readonly string MachineName = Environment.MachineName;
        public static bool IsDevelopment()
        {
            return Is(EnvDevelopmentName);
        }
        public static bool IsStaging()
        {
            return Is(EnvStagingName);
        }
        public static bool IsProduction()
        {
            return Is(EnvProductionName);
        }
        public static bool Is(string environment)
        {
            return string.Equals(CurrentEnvironment, environment, StringComparison.OrdinalIgnoreCase);
        }
    }
}
