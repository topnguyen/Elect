using System;
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

        public string Size { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastUpdatedAt { get; set; }

        public string ViewDetailUrl { get; set; }
    }
}