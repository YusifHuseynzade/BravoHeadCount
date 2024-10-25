using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Queries.Response;

public class GetByIdExpensesReportQueryResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DateTime Date { get; set; }
    public float UtilityElectricity { get; set; }
    public float UtilityWater { get; set; }
    public float RepairExpenses { get; set; }
    public float TransportationExpenses { get; set; }
    public float CleaningExpenses { get; set; }
    public float StationeryExpenses { get; set; }
    public float PrintingExpenses { get; set; }
    public float OperationExpenses { get; set; }
    public float BalanceEndMonth { get; set; }
    public float Other { get; set; }
    public float TotalExpenses { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public List<string> AttachmentUrls { get; set; } = new();
}