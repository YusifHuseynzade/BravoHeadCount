using ExpensesReportDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ExpensesReportDetails.Commands.Request;

public class UpdateExpensesReportCommandRequest : IRequest<UpdateExpensesReportCommandResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProjectId { get; set; }
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
    public List<IFormFile>? NewAttachmentFiles { get; set; } = new();  // Yeni dosyalar
    public List<int>? DeleteAttachmentFileIds { get; set; } = new();  // Silinecek dosyalar
}

