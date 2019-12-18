using System.Collections.Generic;

namespace Elect.Logger.Logging.Models
{
    public class ElectLogSummaryModel
    {
        public int TotalFile { get; set; }

        public int TotalLog { get; set; }

        public List<ElectLogFileSummaryModel> Files { get; set; } = new List<ElectLogFileSummaryModel>();
    }

    public class ElectLogFileSummaryModel
    {
        public string FileName { get; set; }

        public int TotalLog { get; set; }

        public string FileSize { get; set; }
        
        public string CreatedAt { get; set; }

        public string LastUpdatedAt { get; set; }

        public string ViewDetailUrl { get; set; }
        
        public string DeleteUrl { get; set; }
    }
}