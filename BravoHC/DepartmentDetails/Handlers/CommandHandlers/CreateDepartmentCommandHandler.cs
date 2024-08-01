using DepartmentDetails.Commands.Request;
using DepartmentDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace DepartmentDetails.Handlers.CommandHandlers;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommandRequest, CreateDepartmentCommandResponse>
{
    private readonly IDepartmentRepository _repository;
    private readonly IFunctionalAreaRepository _functionalAreaRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository repository, IFunctionalAreaRepository functionalAreaRepository)
    {
        _repository = repository;
        _functionalAreaRepository = functionalAreaRepository;
    }

    public async Task<CreateDepartmentCommandResponse> Handle(CreateDepartmentCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(d => d.Name == request.Name))
        {
            return new CreateDepartmentCommandResponse
            {
                IsSuccess = false,
            };
        }

		if (request.FunctionalAreaId != 0 && !await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId))
		{
			return new CreateDepartmentCommandResponse
			{
				IsSuccess = false,
			};
		}


		var department = new Department
        {
            FunctionalAreaId = request.FunctionalAreaId
        };

        department.SetDetail(request.Name);

        await _repository.AddAsync(department);
        await _repository.CommitAsync();

        return new CreateDepartmentCommandResponse
        {
            IsSuccess = true
        };
    }


}