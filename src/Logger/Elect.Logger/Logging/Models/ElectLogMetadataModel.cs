using System;

namespace Elect.Logger.Logging.Models
{
    public class ElectLogMetadataModel
    {
        public string FileName { get; set; }

        public string FileSize { get; set; }

        public long TotalLog { get; set; }
        
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }
    }
}