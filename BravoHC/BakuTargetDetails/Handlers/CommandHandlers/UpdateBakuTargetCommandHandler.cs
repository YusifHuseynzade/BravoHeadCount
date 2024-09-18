using BakuTargetDetails.Commands.Request;
using BakuTargetDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuTargetDetails.Handlers.CommandHandlers;

public class UpdateBakuTargetCommandHandler : IRequestHandler<UpdateBakuTargetCommandRequest, UpdateBakuTargetCommandResponse>
{

    private readonly IBakuTargetRepository _repository;

    public UpdateBakuTargetCommandHandler(IBakuTargetRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateBakuTargetCommandResponse> Handle(UpdateBakuTargetCommandRequest request, CancellationToken cancellationToken)
    {
        var bakuTarget = await _repository.GetAsync(x => x.Id == request.Id);

        if (bakuTarget != null)
        {
            bakuTarget.SetDetail(request.Name);
            await _repository.UpdateAsync(bakuTarget);

            return new UpdateBakuTargetCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateBakuTargetCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}
