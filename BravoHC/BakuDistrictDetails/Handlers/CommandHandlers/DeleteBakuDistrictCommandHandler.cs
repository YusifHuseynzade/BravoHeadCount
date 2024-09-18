using BakuDistrictDetails.Commands.Request;
using BakuDistrictDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuDistrictDetails.Handlers.CommandHandlers;

public class DeleteBakuDistrictCommandHandler : IRequestHandler<DeleteBakuDistrictCommandRequest, DeleteBakuDistrictCommandResponse>
{
    private readonly IBakuDistrictRepository _repository;

    public DeleteBakuDistrictCommandHandler(IBakuDistrictRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteBakuDistrictCommandResponse> Handle(DeleteBakuDistrictCommandRequest request, CancellationToken cancellationToken)
    {
        var bakuDistrict = await _repository.GetAsync(x => x.Id == request.Id);

        if (bakuDistrict == null)
        {
            return new DeleteBakuDistrictCommandResponse { IsSuccess = false };
        }

        _repository.Remove(bakuDistrict);
        await _repository.CommitAsync();

        return new DeleteBakuDistrictCommandResponse
        {
            IsSuccess = true
        };
    }
}
