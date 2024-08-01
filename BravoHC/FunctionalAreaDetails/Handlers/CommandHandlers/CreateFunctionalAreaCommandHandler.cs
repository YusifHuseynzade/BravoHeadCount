using Domain.Entities;
using Domain.IRepositories;
using FunctionalAreaDetails.Commands.Request;
using FunctionalAreaDetails.Commands.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.CommandHandlers;

public class CreateFunctionalAreaCommandHandler : IRequestHandler<CreateFunctionalAreaCommandRequest, CreateFunctionalAreaCommandResponse>
{

    private readonly IFunctionalAreaRepository _repository;

    public CreateFunctionalAreaCommandHandler(IFunctionalAreaRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateFunctionalAreaCommandResponse> Handle(CreateFunctionalAreaCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.Name == request.Name))
        {
            return new CreateFunctionalAreaCommandResponse
            {
                IsSuccess = false,
            };
        }
        var funtioanlArea = new FunctionalArea();
        funtioanlArea.SetDetail(request.Name);

        await _repository.AddAsync(funtioanlArea);
        await _repository.CommitAsync();
        return new CreateFunctionalAreaCommandResponse
        {
            IsSuccess = true,
        };
    }
}