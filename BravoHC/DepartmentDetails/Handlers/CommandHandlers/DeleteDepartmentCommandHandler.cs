
using DepartmentDetails.Commands.Request;
using DepartmentDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace DepartmentDetails.Handlers.CommandHandlers;


public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommandRequest, DeleteDepartmentCommandResponse>
{
	private readonly IDepartmentRepository _repository;

	public DeleteDepartmentCommandHandler(IDepartmentRepository repository)
	{
		_repository = repository;
	}

	public async Task<DeleteDepartmentCommandResponse> Handle(DeleteDepartmentCommandRequest request, CancellationToken cancellationToken)
	{
		var Department = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

		if (Department == null)
		{
			return new DeleteDepartmentCommandResponse { IsSuccess = false };
		}

		_repository.Remove(Department);
		await _repository.CommitAsync();

		return new DeleteDepartmentCommandResponse
		{
			IsSuccess = true
		};
	}
}

