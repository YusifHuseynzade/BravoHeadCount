using BakuMetroDetails.Commands.Request;
using BakuMetroDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuMetroDetails.Handlers.CommandHandlers;

public class UpdateBakuMetroCommandHandler : IRequestHandler<UpdateBakuMetroCommandRequest, UpdateBakuMetroCommandResponse>
{

    private readonly IBakuMetroRepository _repository;

    public UpdateBakuMetroCommandHandler(IBakuMetroRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateBakuMetroCommandResponse> Handle(UpdateBakuMetroCommandRequest request, CancellationToken cancellationToken)
    {
        var bakuMetro = await _repository.GetAsync(x => x.Id == request.Id);

        if (bakuMetro != null)
        {
            bakuMetro.SetDetail(request.Name);
            await _repository.UpdateAsync(bakuMetro);

            return new UpdateBakuMetroCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateBakuMetroCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}
