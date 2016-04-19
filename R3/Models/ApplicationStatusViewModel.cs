using System;

namespace R3.Models
{
    public class ApplicationStatusViewModel
    {
        public DateTime LastDateTaken { get; set; }
        public int NumberOfYes { get; set; }
        public int NumberOfNo { get; set; }
        public int NumberOfMaybe { get; set; }
        public int NumberOfUnclear { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public int NumberOfHiddenRecords { get; set; }
    }
}
