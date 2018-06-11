using System;

namespace Elect.Logger.Models.Logging
{
    public class EnvironmentModel
    {
        // Operating System
        
        public string OsName { get; set; }

        public string OsVersion { get; set; }

        public string OsDescription { get; set; }

        public string OsArchitecture { get; set; }

        public string OsTimeZoneId { get; set; }
        
        public TimeSpan OsTimeZone { get; set; }
        
        public string OsTimeZoneDisplay { get; set; }

        public DateTimeOffset OsBootTime { get; set; }
        
        // Framework
        
        public string FrameworkName { get; set; }

        public string FrameworkVersion { get; set; }

        public string FrameworkDescription { get; set; }
    }
}