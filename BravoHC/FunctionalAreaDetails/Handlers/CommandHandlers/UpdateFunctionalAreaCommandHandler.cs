using Domain.IRepositories;
using FunctionalAreaDetails.Commands.Request;
using FunctionalAreaDetails.Commands.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.CommandHandlers;

public class UpdateFunctionalAreaCommandHandler : IRequestHandler<UpdateFunctionalAreaCommandRequest, UpdateFunctionalAreaCommandResponse>
{

    private readonly IFunctionalAreaRepository _repository;

    public UpdateFunctionalAreaCommandHandler(IFunctionalAreaRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateFunctionalAreaCommandResponse> Handle(UpdateFunctionalAreaCommandRequest request, CancellationToken cancellationToken)
    {
        var functionalArea = await _repository.GetAsync(x => x.Id == request.Id);

        if (functionalArea != null)
        {
            functionalArea.SetDetail(request.Name);


            await _repository.UpdateAsync(functionalArea);

            return new UpdateFunctionalAreaCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateFunctionalAreaCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}
