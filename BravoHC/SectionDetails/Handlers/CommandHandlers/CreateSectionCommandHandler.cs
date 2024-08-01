using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using SectionDetails.Commands.Request;
using SectionDetails.Commands.Response;

namespace SectionDetails.Handlers.CommandHandlers;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommandRequest, CreateSectionCommandResponse>
{
    private readonly ISectionRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateSectionCommandHandler(ISectionRepository repository, IDepartmentRepository departmentRepository)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
    }
    public async Task<CreateSectionCommandResponse> Handle(CreateSectionCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(d => d.Name == request.Name && d.DepartmentId == request.DepartmentId))
        {
            return new CreateSectionCommandResponse
            {
                IsSuccess = false,
            };
        }

        var section = new Section
        {
            DepartmentId = request.DepartmentId
        };

        section.SetDetail(request.Name);

        await _repository.AddAsync(section);
        await _repository.CommitAsync();

        return new CreateSectionCommandResponse
        {
            IsSuccess = true
        };
    }
}
