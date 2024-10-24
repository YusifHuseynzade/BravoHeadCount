using MediatR;
using SettingFinanceOperationDetails.Commands.Response;

namespace SettingFinanceOperationDetails.Commands.Request;

public class DeleteSettingFinanceOperationCommandRequest : IRequest<DeleteSettingFinanceOperationCommandResponse>
{
    public int Id { get; set; }
}
