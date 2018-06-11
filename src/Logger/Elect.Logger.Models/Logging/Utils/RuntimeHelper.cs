using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Elect.Logger.Models.Logging.Utils
{
    internal static class RuntimeHelper
    {
        public static RuntimeModel Get()
        {
            var runtimeInfo = new RuntimeModel();

            // App Runtime Info

            AssemblyName assemblyName = Assembly.GetEntryAssembly()?.GetName();

            if (assemblyName != null)
            {
                runtimeInfo.AppName = assemblyName.Name;
                runtimeInfo.AppVersion = assemblyName.Version.ToString();
            }

            // Reference Packages Info

            runtimeInfo.ReferencePackages = GetReferencePackges();

            // Process Info

            using (var currentProcess = Process.GetCurrentProcess())
            {
                runtimeInfo.ProcessStartTime = currentProcess.StartTime.ToUniversalTime();
            }

            runtimeInfo.ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString();

            // Machine Info

            runtimeInfo.MachineName = Environment.MachineName;

            runtimeInfo.MachineUserName = Environment.UserName;

            return runtimeInfo;
        }

        public static List<SdkModel> GetReferencePackges()
        {
            var assemblies =
                AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(q => !q.IsDynamic)
                    .Select(a => a.GetName())
                    .OrderBy(a => a.Name);

            var dictionary = new Dictionary<string, string>();

            foreach (var assembly in assemblies)
            {
                if (dictionary.ContainsKey(assembly.Name))
                {
                    continue;
                }

                dictionary.Add(assembly.Name, assembly.Version.ToString());
            }

            var packages = dictionary.Select(x => new SdkModel
            {
                Name = x.Key,
                Version = x.Value
            }).ToList();

            return packages;
        }
    }
}