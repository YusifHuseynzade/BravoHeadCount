using Domain.IRepositories;
using MediatR;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Commands.Response;

namespace MoneyOrderDetails.Handlers.CommandHandlers;


public class CreateMoneyOrderCommandHandler : IRequestHandler<CreateMoneyOrderCommandRequest, CreateMoneyOrderCommandResponse>
{
    private readonly IMoneyOrderRepository _repository;

    public CreateMoneyOrderCommandHandler(IMoneyOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateMoneyOrderCommandResponse> Handle(CreateMoneyOrderCommandRequest request, CancellationToken cancellationToken)
    {

    }
}


