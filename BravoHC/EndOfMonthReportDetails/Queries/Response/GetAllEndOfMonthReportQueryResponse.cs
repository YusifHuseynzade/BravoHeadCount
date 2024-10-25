using System;

namespace EndOfMonthReportDetails.Queries.Response;

public class GetAllEndOfMonthReportQueryResponse
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }  // Project bilgisi için
    public float EncashmentAmount { get; set; }
    public float DepositAmount { get; set; }
    public float TotalAmount { get; set; }
    public string? Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
