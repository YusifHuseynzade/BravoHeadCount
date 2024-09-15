using Domain.IRepositories;
using MediatR;
using SectionDetails.Commands.Request;
using SectionDetails.Commands.Response;

namespace SectionDetails.Handlers.CommandHandlers;

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommandRequest, UpdateSectionCommandResponse>
{

    private readonly ISectionRepository _repository;
    private readonly IProjectRepository _departmentRepository;

    public UpdateSectionCommandHandler(ISectionRepository repository, IProjectRepository departmentRepository = null)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
    }

    public async Task<UpdateSectionCommandResponse> Handle(UpdateSectionCommandRequest request, CancellationToken cancellationToken)
    {
        var existingSection = await _repository.GetAsync(d => d.Id == request.Id);

        // Əgər Project tapılmazsa false qaytar
        if (existingSection == null)
        {
            return new UpdateSectionCommandResponse
            {
                IsSuccess = false
            };
        }

        // Yeni adı istifadə olunan adla müqayisə etmək və eyni adlı Project olmamasına əmin olmaq
        if (await _repository.IsExistAsync(d => d.Name == request.Name && d.Id != request.Id))
        {
            return new UpdateSectionCommandResponse
            {
                IsSuccess = false
            };
        }

        existingSection.SetDetail(request.Name);
        // Update edilmiş Projecti yadda saxlamaq
        await _repository.UpdateAsync(existingSection);
        await _repository.CommitAsync();

        return new UpdateSectionCommandResponse
        {
            IsSuccess = true
        };
    }
}
