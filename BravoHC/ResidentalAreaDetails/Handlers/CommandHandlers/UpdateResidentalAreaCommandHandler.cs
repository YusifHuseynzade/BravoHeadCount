using Domain.IRepositories;
using MediatR;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Commands.Response;

namespace ResidentalAreaDetails.Handlers.CommandHandlers;

public class UpdateResidentalAreaCommandHandler : IRequestHandler<UpdateResidentalAreaCommandRequest, UpdateResidentalAreaCommandResponse>
{

    private readonly IResidentalAreaRepository _repository;

    public UpdateResidentalAreaCommandHandler(IResidentalAreaRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateResidentalAreaCommandResponse> Handle(UpdateResidentalAreaCommandRequest request, CancellationToken cancellationToken)
    {
        var residentalArea = await _repository.GetAsync(x => x.Id == request.Id);

        if (residentalArea != null)
        {
            residentalArea.SetDetail(request.Name);
            await _repository.UpdateAsync(residentalArea);

            return new UpdateResidentalAreaCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateResidentalAreaCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}
