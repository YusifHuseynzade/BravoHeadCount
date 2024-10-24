using Domain.IRepositories;
using MediatR;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Commands.Response;

namespace MoneyOrderDetails.Handlers.CommandHandlers;

public class UpdateMoneyOrderCommandHandler : IRequestHandler<UpdateMoneyOrderCommandRequest, UpdateMoneyOrderCommandResponse>
{
    private readonly IMoneyOrderRepository _moneyOrderRepository;

    public UpdateMoneyOrderCommandHandler(IMoneyOrderRepository moneyOrderRepository)
    {
        _moneyOrderRepository = moneyOrderRepository;
    }

    public async Task<UpdateMoneyOrderCommandResponse> Handle(UpdateMoneyOrderCommandRequest request, CancellationToken cancellationToken)
    {

    }

}
