using Domain.IRepositories;
using FormatDetails.Commands.Request;
using FormatDetails.Commands.Response;
using MediatR;

namespace FormatDetails.Handlers.CommandHandlers;

public class UpdateFormatCommandHandler : IRequestHandler<UpdateFormatCommandRequest, UpdateFormatCommandResponse>
{

    private readonly IFormatRepository _repository;

    public UpdateFormatCommandHandler(IFormatRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateFormatCommandResponse> Handle(UpdateFormatCommandRequest request, CancellationToken cancellationToken)
    {
        var format = await _repository.GetAsync(x => x.Id == request.Id);

        if (format != null)
        {
            format.SetDetail(request.Name);


            await _repository.UpdateAsync(format);

            return new UpdateFormatCommandResponse
            {
                IsSuccess = true
            };
        }
        else
        {
            return new UpdateFormatCommandResponse
            {
                IsSuccess = false,
            };
        }
    }
}
