using Domain.IRepositories;
using FormatDetails.Commands.Request;
using FormatDetails.Commands.Response;
using MediatR;

namespace FormatDetails.Handlers.CommandHandlers;

public class DeleteFormatCommandHandler : IRequestHandler<DeleteFormatCommandRequest, DeleteFormatCommandResponse>
{
    private readonly IFormatRepository _repository;

    public DeleteFormatCommandHandler(IFormatRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteFormatCommandResponse> Handle(DeleteFormatCommandRequest request, CancellationToken cancellationToken)
    {
        var format = await _repository.GetAsync(x => x.Id == request.Id);

        if (format == null)
        {
            return new DeleteFormatCommandResponse { IsSuccess = false };
        }

        _repository.Remove(format);
        await _repository.CommitAsync();

        return new DeleteFormatCommandResponse
        {
            IsSuccess = true
        };
    }
}
