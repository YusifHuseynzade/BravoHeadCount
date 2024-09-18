using BakuDistrictDetails.Commands.Request;
using BakuDistrictDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuDistrictDetails.Handlers.CommandHandlers;

public class UpdateBakuDistrictCommandHandler : IRequestHandler<UpdateBakuDistrictCommandRequest, UpdateBakuDistrictCommandResponse>
{

    private readonly IBakuDistrictRepository _repository;

    public UpdateBakuDistrictCommandHandler(IBakuDistrictRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateBakuDistrictCommandResponse> Handle(UpdateBakuDistrictCommandRequest request, CancellationToken cancellationToken)
    {
        var bakuDistrict = await _repository.GetAsync(x => x.Id == request.Id);

        if (bakuDistrict != null)
        {
            bakuDistrict.SetDetail(request.Name);
            await _repository.UpdateAsync(bakuDistrict);

            return new UpdateBakuDistrictCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateBakuDistrictCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}
