using MediatR;
using SettingFinanceOperationDetails.Commands.Response;

namespace SettingFinanceOperationDetails.Commands.Request;

public class CreateSettingFinanceOperationCommandRequest : IRequest<CreateSettingFinanceOperationCommandResponse>
{
    public string Name { get; set; }
    public string EncashmentDays { get; set; }
    public DateTime DateEncashment { get; set; }
    public bool IsActiveEncashment { get; set; }
    public int FrequencyEncashment { get; set; }
    public List<string> EncashmentRecipient { get; set; }
    public int ProjectId { get; set; }
    public string MoneyOrderDays { get; set; }
    public DateTime DateMoneyOrder { get; set; }
    public bool IsActiveMoneyOrder { get; set; }
    public int FrequencyMoneyOrder { get; set; }
    public List<string> MoneyOrderRecipient { get; set; }
    public int BranchId { get; set; }
}
