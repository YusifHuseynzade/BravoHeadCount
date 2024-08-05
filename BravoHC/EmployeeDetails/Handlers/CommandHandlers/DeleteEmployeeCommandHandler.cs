using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Handlers.CommandHandlers;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommandRequest, DeleteEmployeeCommandResponse>
{
    private readonly IEmployeeRepository _repository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetAsync(x => x.Id == request.Id);

        if (employee == null)
        {
            return new DeleteEmployeeCommandResponse { IsSuccess = false };
        }

        _repository.Remove(employee);
        await _repository.CommitAsync();

        return new DeleteEmployeeCommandResponse
        {
            IsSuccess = true
        };
    }
}
