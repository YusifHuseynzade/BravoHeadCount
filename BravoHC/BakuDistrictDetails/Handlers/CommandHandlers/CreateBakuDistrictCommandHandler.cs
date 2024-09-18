using BakuDistrictDetails.Commands.Request;
using BakuDistrictDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BakuDistrictDetails.Handlers.CommandHandlers;

public class CreateBakuDistrictCommandHandler : IRequestHandler<CreateBakuDistrictCommandRequest, CreateBakuDistrictCommandResponse>
{

    private readonly IBakuDistrictRepository _repository;

    public CreateBakuDistrictCommandHandler(IBakuDistrictRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateBakuDistrictCommandResponse> Handle(CreateBakuDistrictCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.Name == request.Name))
        {
            return new CreateBakuDistrictCommandResponse
            {
                IsSuccess = false,
            };
        }
        var bakuDistrict = new BakuDistrict();
        bakuDistrict.SetDetail(request.Name);

        await _repository.AddAsync(bakuDistrict);
        await _repository.CommitAsync();
        return new CreateBakuDistrictCommandResponse
        {
            IsSuccess = true,
        };
    }
}