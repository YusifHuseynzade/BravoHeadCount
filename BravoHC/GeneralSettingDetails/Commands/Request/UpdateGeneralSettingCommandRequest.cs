using Domain.Entities;
using GeneralSettingDetails.Commands.Response;
using MediatR;

namespace GeneralSettingDetails.Commands.Request;

public class UpdateGeneralSettingCommandRequest : IRequest<UpdateGeneralSettingCommandResponse>
{
    // GeneralSetting Id
    public int Id { get; set; }

    // End of Month Report Settings
    public List<TimeSpan> EndOfMonthSendingTimes { get; set; } = new();
    public int EndOfMonthSendingFrequency { get; set; }
    public List<string> EndOfMonthReceivers { get; set; } = new();
    public List<string> EndOfMonthReceiversCC { get; set; } = new();
    public List<int> EndOfMonthAvailableCreatedDays { get; set; } = new();

    // Expense Report Settings
    public List<TimeSpan> ExpenseReportSendingTimes { get; set; } = new();
    public int ExpenseReportSendingFrequency { get; set; }
    public List<string> ExpenseReportReceivers { get; set; } = new();
    public List<string> ExpenseReportReceiversCC { get; set; } = new();
    public List<int> ExpenseReportAvailableCreatedDays { get; set; } = new();

}

