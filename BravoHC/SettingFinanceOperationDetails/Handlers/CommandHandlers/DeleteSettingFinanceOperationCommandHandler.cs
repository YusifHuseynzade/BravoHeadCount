using Domain.IRepositories;
using MediatR;
using SettingFinanceOperationDetails.Commands.Request;
using SettingFinanceOperationDetails.Commands.Response;

namespace SettingFinanceOperationDetails.Handlers.CommandHandlers;

internal class DeleteSettingFinanceOperationCommandHandler : IRequestHandler<DeleteSettingFinanceOperationCommandRequest, DeleteSettingFinanceOperationCommandResponse>
{
    private readonly ISettingFinanceOperationRepository _repository;

    public DeleteSettingFinanceOperationCommandHandler(ISettingFinanceOperationRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteSettingFinanceOperationCommandResponse> Handle(DeleteSettingFinanceOperationCommandRequest request, CancellationToken cancellationToken)
    {
        var SettingFinanceOperation = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (SettingFinanceOperation == null)
        {
            return new DeleteSettingFinanceOperationCommandResponse { IsSuccess = false };
        }

        _repository.Remove(SettingFinanceOperation);
        await _repository.CommitAsync();

        return new DeleteSettingFinanceOperationCommandResponse
        {
            IsSuccess = true
        };
    }
}
