using DepartmentDetails.Commands.Request;
using DepartmentDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace DepartmentDetails.Commands.Update;


public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommandRequest, UpdateDepartmentCommandResponse>
{

	private readonly IDepartmentRepository _repository;
	private readonly IFunctionalAreaRepository _functionalAreaRepository;

	public UpdateDepartmentCommandHandler(IDepartmentRepository repository, IFunctionalAreaRepository functionalAreaRepository = null)
	{
		_repository = repository;
		_functionalAreaRepository = functionalAreaRepository;
	}

	public async Task<UpdateDepartmentCommandResponse> Handle(UpdateDepartmentCommandRequest request, CancellationToken cancellationToken)
	{
		var existingDepartment = await _repository.GetAsync(d => d.Id == request.Id);

		// Əgər department tapılmazsa false qaytar
		if (existingDepartment == null)
		{
			return new UpdateDepartmentCommandResponse
			{
				IsSuccess = false
			};
		}

		// Yeni adı istifadə olunan adla müqayisə etmək və eyni adlı department olmamasına əmin olmaq
		if (await _repository.IsExistAsync(d => d.Name == request.Name && d.Id != request.Id))
		{
			return new UpdateDepartmentCommandResponse
			{
				IsSuccess = false
			};
		}

		// Əgər tələbədə göndərilən FunctionalAreaId varsa, onun mövcudluğunu yoxlayın
		if (request.FunctionalAreaId != 0 && !await _functionalAreaRepository.IsExistAsync(p => p.Id == request.FunctionalAreaId))
		{
			return new UpdateDepartmentCommandResponse
			{
				IsSuccess = false
			};
		}

		existingDepartment.SetDetail(request.Name);
		existingDepartment.FunctionalAreaId = request.FunctionalAreaId;
		// Update edilmiş departmenti yadda saxlamaq
		await _repository.UpdateAsync(existingDepartment);
		await _repository.CommitAsync();

		return new UpdateDepartmentCommandResponse
		{
			IsSuccess = true
		};
	}
}
