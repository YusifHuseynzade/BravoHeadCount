using BGSStockRequestDetails.Commands.Request;
using BGSStockRequestDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace BGSStockRequestDetails.Handlers.CommandHandlers;

internal class DeleteBGSStockRequestCommandHandler : IRequestHandler<DeleteBGSStockRequestCommandRequest, DeleteBGSStockRequestCommandResponse>
{
    private readonly IBGSStockRequestRepository _repository;

    public DeleteBGSStockRequestCommandHandler(IBGSStockRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteBGSStockRequestCommandResponse> Handle(DeleteBGSStockRequestCommandRequest request, CancellationToken cancellationToken)
    {
        var BGSStockRequest = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (BGSStockRequest == null)
        {
            return new DeleteBGSStockRequestCommandResponse { IsSuccess = false };
        }

        _repository.Remove(BGSStockRequest);
        await _repository.CommitAsync();

        return new DeleteBGSStockRequestCommandResponse
        {
            IsSuccess = true
        };
    }
}
