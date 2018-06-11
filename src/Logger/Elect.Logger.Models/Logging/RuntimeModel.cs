using System;
using System.Collections.Generic;

namespace Elect.Logger.Models.Logging
{
    public class RuntimeModel
    {
        // App

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        // Packages

        public List<SdkModel> ReferencePackages { get; set; }
        
        // Process Info
        
        public DateTimeOffset ProcessStartTime { get; set; }

        public string ProcessArchitecture { get; set; }

        // User Machine Info

        public string MachineName { get; set; }

        public string MachineUserName { get; set; }
    }
}