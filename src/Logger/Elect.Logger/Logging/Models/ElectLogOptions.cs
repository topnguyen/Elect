using Elect.Core.Interfaces;

namespace Elect.Logger.Logging.Models
{
    public class ElectLogOptions : IElectOptions
    {
        /// <summary>
        ///     Relative Path of Json File
        /// </summary>
        public string JsonFilePath { get; set; } = "ElectLog.json";
    }
}