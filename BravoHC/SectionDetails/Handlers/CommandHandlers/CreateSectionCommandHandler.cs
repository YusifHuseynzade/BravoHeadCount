using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using SectionDetails.Commands.Request;
using SectionDetails.Commands.Response;

namespace SectionDetails.Handlers.CommandHandlers;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommandRequest, CreateSectionCommandResponse>
{
    private readonly ISectionRepository _repository;
    private readonly IProjectRepository _projectRepository;

    public CreateSectionCommandHandler(ISectionRepository repository, IProjectRepository projectRepository)
    {
        _repository = repository;
        _projectRepository = projectRepository;
    }
    public async Task<CreateSectionCommandResponse> Handle(CreateSectionCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(d => d.Name == request.Name && d.ProjectId == request.ProjectId))
        {
            return new CreateSectionCommandResponse
            {
                IsSuccess = false,
            };
        }

        var section = new Section
        {
            ProjectId = request.ProjectId
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
