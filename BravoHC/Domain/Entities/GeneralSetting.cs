using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class GeneralSetting : BaseEntity
    {
        // Nested class for common report settings
        public class ReportSettings
        {
            public List<TimeSpan> SendingTimes { get; set; } = new();
            public int SendingFrequency { get; set; }                  
            public List<string> Receivers { get; set; } = new();        
            public List<string> ReceiversCC { get; set; } = new();      
            public List<int> AvailableCreatedDays { get; set; } = new(); 
        }
        public ReportSettings EndOfMonthReportSettings { get; set; } = new();
        public ReportSettings ExpenseReportSettings { get; set; } = new();
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
