using System.Reflection;

namespace Elect.Logger.Models.Logging.Utils
{
    internal static class SdkHelper
    {
        public static SdkModel Get(AssemblyName assemblyName)
        {
            SdkModel sdkInfo = new SdkModel
            {
                Name = assemblyName.Name,
                Version = assemblyName.Version.ToString()
            };

            return sdkInfo;
        }
    }
}