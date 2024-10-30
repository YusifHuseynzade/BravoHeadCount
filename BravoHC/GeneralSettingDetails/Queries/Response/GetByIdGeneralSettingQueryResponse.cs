using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralSettingDetails.Queries.Response;

public class GetByIdGeneralSettingQueryResponse
{
    public int Id { get; set; }

    // End of Month Report Settings
    public List<TimeSpan> EndOfMonthSendingTimes { get; set; }
    public int EndOfMonthSendingFrequency { get; set; }
    public List<string> EndOfMonthReceivers { get; set; }
    public List<string> EndOfMonthReceiversCC { get; set; }
    public List<int> EndOfMonthAvailableCreatedDays { get; set; }

    // Expense Report Settings
    public List<TimeSpan> ExpenseReportSendingTimes { get; set; }
    public int ExpenseReportSendingFrequency { get; set; }
    public List<string> ExpenseReportReceivers { get; set; }
    public List<string> ExpenseReportReceiversCC { get; set; }
    public List<int> ExpenseReportAvailableCreatedDays { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}