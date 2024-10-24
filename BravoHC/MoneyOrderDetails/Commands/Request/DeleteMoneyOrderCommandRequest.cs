using MediatR;
using MoneyOrderDetails.Commands.Response;

namespace MoneyOrderDetails.Commands.Request;

public class DeleteMoneyOrderCommandRequest : IRequest<DeleteMoneyOrderCommandResponse>
{
    public int Id { get; set; }
}
