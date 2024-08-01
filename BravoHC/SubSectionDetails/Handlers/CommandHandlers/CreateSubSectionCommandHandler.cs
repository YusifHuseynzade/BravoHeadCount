using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using SubSectionDetails.Commands.Request;
using SubSectionDetails.Commands.Response;

namespace SubSectionDetails.Handlers.CommandHandlers;

public class CreateSubSectionCommandHandler : IRequestHandler<CreateSubSectionCommandRequest, CreateSubSectionCommandResponse>
{
    private readonly ISubSectionRepository _repository;
    private readonly ISectionRepository _sectionRepository;

    public CreateSubSectionCommandHandler(ISubSectionRepository repository, ISectionRepository sectionRepository)
    {
        _repository = repository;
        _sectionRepository = sectionRepository;
    }
    public async Task<CreateSubSectionCommandResponse> Handle(CreateSubSectionCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(d => d.Name == request.Name))
        {
            return new CreateSubSectionCommandResponse
            {
                IsSuccess = false,
            };
        }
        if (request.SectionId != 0 && !await _sectionRepository.IsExistAsync(d => d.Id == request.SectionId))
        {
            return new CreateSubSectionCommandResponse
            {
                IsSuccess = false,
            };
        }

        var subSection = new SubSection
        {
            SectionId = request.SectionId
        };

        subSection.SetDetail(request.Name);

        await _repository.AddAsync(subSection);
        await _repository.CommitAsync();

        return new CreateSubSectionCommandResponse
        {
            IsSuccess = true
        };
    }
}
