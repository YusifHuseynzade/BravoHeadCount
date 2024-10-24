using Domain.IRepositories;
using EncashmentDetails.Commands.Request;
using EncashmentDetails.Commands.Response;
using MediatR;

namespace EncashmentDetails.Handlers.CommandHandlers;

internal class DeleteEncashmentCommandHandler : IRequestHandler<DeleteEncashmentCommandRequest, DeleteEncashmentCommandResponse>
{
    private readonly IEncashmentRepository _repository;

    public DeleteEncashmentCommandHandler(IEncashmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteEncashmentCommandResponse> Handle(DeleteEncashmentCommandRequest request, CancellationToken cancellationToken)
    {
        var Encashment = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Encashment == null)
        {
            return new DeleteEncashmentCommandResponse { IsSuccess = false };
        }

        _repository.Remove(Encashment);
        await _repository.CommitAsync();

        return new DeleteEncashmentCommandResponse
        {
            IsSuccess = true
        };
    }
}
