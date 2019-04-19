using System;
using System.Collections.Generic;
using System.Linq;
using Elect.Core.ObjUtils;

namespace Elect.Logger.Models.Logging
{
    public class RuntimeModel : ElectDisposableModel
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
        
        protected override void DisposeUnmanagedResources()
        {
            if (ReferencePackages?.Any() != true)
            {
                return;
            }
            
            foreach (var sdkModel in ReferencePackages)
            {
                sdkModel.Dispose();
            }
        }
    }
}