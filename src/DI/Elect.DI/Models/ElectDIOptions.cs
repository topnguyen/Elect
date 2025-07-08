namespace Elect.DI.Models
{
    public class ElectDIOptions : IElectOptions
    {
        /// <summary>
        ///     List assembly folder to scan, default is application base path. 
        /// </summary>
        public List<string> ListAssemblyFolderPath { get; set; } = new List<string> { PlatformServices.Default.Application.ApplicationBasePath };
        /// <summary>
        ///     List assembly name to scan, default is application name. 
        /// </summary>
        /// <remarks>Default is root assembly name, e.g: Elect.DI.dll => Scan Elect.dll and Elect.*.dll </remarks>
        public List<string> ListAssemblyName { get; set; } = new List<string> { PlatformServices.Default.Application.ApplicationName };
    }
}
