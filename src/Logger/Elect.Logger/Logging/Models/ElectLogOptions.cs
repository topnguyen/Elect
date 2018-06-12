using System;
using System.IO;
using Elect.Core.Interfaces;

namespace Elect.Logger.Logging.Models
{
    public class ElectLogOptions : IElectOptions
    {
        /// <summary>
        ///     Relative Path of Json File, default is Logs.json
        /// </summary>
        public string JsonFilePath { get; set; } = "Logs" + Path.DirectorySeparatorChar + "{yyyy-MM-dd}.json";
    }
}