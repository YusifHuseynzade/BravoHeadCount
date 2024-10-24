using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExpensesReport : BaseEntity
    {
        public string? Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public DateTime Date { get; set; }
        public float UtilityElectricity { get; set; }
        public float UtilityWater { get; set; }
        public float RepairExpenses { get; set; }
        public float TransportationExpenses { get; set; }
        public float CleaningExpenses { get; set; }
        public float StationeryExpenses { get; set; }
        public float PrintingExpenses { get; set; }
        public float OperationExpenses { get; set; }
        public float Other { get; set; }
        public float TotalExpenses { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<Attachment> Attachments { get; set; } = new();
        public void SetDetails(string name, string marketCodeAndName, DateTime date, float utilityElectricity,
                          float utilityWater, float repairExpenses, float transportationExpenses,
                          float cleaningExpenses, float stationeryExpenses, float printingExpenses,
                          float operationExpenses, float other, float totalExpenses, string comment)
        {
            Name = name;
            Date = date;
            UtilityElectricity = utilityElectricity;
            UtilityWater = utilityWater;
            RepairExpenses = repairExpenses;
            TransportationExpenses = transportationExpenses;
            CleaningExpenses = cleaningExpenses;
            StationeryExpenses = stationeryExpenses;
            PrintingExpenses = printingExpenses;
            OperationExpenses = operationExpenses;
            Other = other;
            TotalExpenses = totalExpenses;
            Comment = comment;
        }
    }
}
