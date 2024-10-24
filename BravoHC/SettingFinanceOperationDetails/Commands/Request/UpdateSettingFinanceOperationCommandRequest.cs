using MediatR;
using SettingFinanceOperationDetails.Commands.Response;

namespace SettingFinanceOperationDetails.Commands.Request;

public class UpdateSettingFinanceOperationCommandRequest : IRequest<UpdateSettingFinanceOperationCommandResponse>
{
    public int Id { get; set; }
}

