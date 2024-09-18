using BakuTargetDetails.Commands.Request;
using BakuTargetDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BakuTargetDetails.Handlers.CommandHandlers;

public class CreateBakuTargetCommandHandler : IRequestHandler<CreateBakuTargetCommandRequest, CreateBakuTargetCommandResponse>
{

    private readonly IBakuTargetRepository _repository;

    public CreateBakuTargetCommandHandler(IBakuTargetRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateBakuTargetCommandResponse> Handle(CreateBakuTargetCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.Name == request.Name))
        {
            return new CreateBakuTargetCommandResponse
            {
                IsSuccess = false,
            };
        }
        var bakuTarget = new BakuTarget();
        bakuTarget.SetDetail(request.Name);

        await _repository.AddAsync(bakuTarget);
        await _repository.CommitAsync();
        return new CreateBakuTargetCommandResponse
        {
            IsSuccess = true,
        };
    }
}