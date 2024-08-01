using Domain.IRepositories;
using MediatR;
using SubSectionDetails.Commands.Request;
using SubSectionDetails.Commands.Response;

namespace SubSectionDetails.Handlers.CommandHandlers;

public class UpdateSubSectionCommandHandler : IRequestHandler<UpdateSubSectionCommandRequest, UpdateSubSectionCommandResponse>
{

    private readonly ISubSectionRepository _repository;
    private readonly ISectionRepository _sectionRepository;


    public UpdateSubSectionCommandHandler(ISubSectionRepository repository, ISectionRepository sectionRepository)
    {
        _repository = repository;
        _sectionRepository = sectionRepository;
    }

    public async Task<UpdateSubSectionCommandResponse> Handle(UpdateSubSectionCommandRequest request, CancellationToken cancellationToken)
    {
        var existingSubSection = await _repository.GetAsync(d => d.Id == request.Id);

        // Əgər SubDivision tapılmazsa false qaytar
        if (existingSubSection == null)
        {
            return new UpdateSubSectionCommandResponse
            {
                IsSuccess = false
            };
        }

        // Yeni adı istifadə olunan adla müqayisə etmək və eyni adlı SubDivision olmamasına əmin olmaq
        if (await _repository.IsExistAsync(d => d.Name == request.Name && d.Id != request.Id))
        {
            return new UpdateSubSectionCommandResponse
            {
                IsSuccess = false
            };
        }
        // Əgər tələbədə göndərilən DivisionId varsa, onun mövcudluğunu yoxlayın
        if (request.SectionId != 0 && !await _sectionRepository.IsExistAsync(p => p.Id == request.SectionId))
        {
            return new UpdateSubSectionCommandResponse
            {
                IsSuccess = false
            };
        }

        existingSubSection.SetDetail(request.Name);
        existingSubSection.SectionId = request.SectionId;
        // Update edilmiş SubDivisioni yadda saxlamaq
        await _repository.UpdateAsync(existingSubSection);
        await _repository.CommitAsync();

        return new UpdateSubSectionCommandResponse
        {
            IsSuccess = true
        };
    }
}
