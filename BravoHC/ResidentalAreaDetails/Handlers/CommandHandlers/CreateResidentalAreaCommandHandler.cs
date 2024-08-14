using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Commands.Response;

namespace ResidentalAreaDetails.Handlers.CommandHandlers;

public class CreateResidentalAreaCommandHandler : IRequestHandler<CreateResidentalAreaCommandRequest, CreateResidentalAreaCommandResponse>
{

    private readonly IResidentalAreaRepository _repository;

    public CreateResidentalAreaCommandHandler(IResidentalAreaRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResidentalAreaCommandResponse> Handle(CreateResidentalAreaCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.Name == request.Name))
        {
            return new CreateResidentalAreaCommandResponse
            {
                IsSuccess = false,
            };
        }
        var residentalArea = new ResidentalArea();
        residentalArea.SetDetail(request.Name);

        await _repository.AddAsync(residentalArea);
        await _repository.CommitAsync();
        return new CreateResidentalAreaCommandResponse
        {
            IsSuccess = true,
        };
    }
}