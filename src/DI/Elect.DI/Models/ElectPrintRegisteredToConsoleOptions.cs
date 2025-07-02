namespace Elect.DI.Models
{
    public class ElectPrintRegisteredToConsoleOptions : IElectOptions
    {
        /// <summary>
        ///     List assembly name to scan, default is application name. 
        /// </summary>
        public List<string> ListAssemblyName { get; set; } = new List<string> { PlatformServices.Default.Application.ApplicationName };
        /// <summary>
        ///     Print with minimal display format, default is true. 
        /// </summary>
        public bool IsMinimalDisplay { get; set; } = true;
        /// <summary>
        ///     Primary Text Color 
        /// </summary>
        public ConsoleColor PrimaryColor { get; set; } = ConsoleColor.Cyan;
        /// <summary>
        ///     Secondary Text Color 
        /// </summary>
        public ConsoleColor SecondaryColor { get; set; } = ConsoleColor.DarkGray;
        /// <summary>
        ///     Sort Ascending By 
        /// </summary>
        public ElectDIPrintSortBy SortAscBy { get; set; } = ElectDIPrintSortBy.Service;
    }
}
