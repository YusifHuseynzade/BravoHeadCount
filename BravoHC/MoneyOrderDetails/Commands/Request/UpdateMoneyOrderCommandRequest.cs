using MediatR;
using MoneyOrderDetails.Commands.Response;

namespace MoneyOrderDetails.Commands.Request;

public class UpdateMoneyOrderCommandRequest : IRequest<UpdateMoneyOrderCommandResponse>
{
    public int Id { get; set; }
}

