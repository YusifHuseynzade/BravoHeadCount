using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectDetails.Handlers.CommandHandlers
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommandRequest, UpdateProjectCommandResponse>
    {
        private readonly IProjectRepository _repository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;

        public UpdateProjectCommandHandler(IProjectRepository repository, IFunctionalAreaRepository functionalAreaRepository)
        {
            _repository = repository;
            _functionalAreaRepository = functionalAreaRepository;
        }

        public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetAsync(x => x.Id == request.Id);

            // Əgər department tapılmazsa false qaytar
            if (project == null)
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false
                };
            }

            // Yeni adı istifadə olunan adla müqayisə etmək və eyni adlı department olmamasına əmin olmaq
            if (await _repository.IsExistAsync(d => d.ProjectCode == request.ProjectCode && d.Id != request.Id))
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false
                };
            }

            // Əgər tələbədə göndərilən FunctionalAreaId varsa, onun mövcudluğunu yoxlayın
            if (request.FunctionalAreaId != 0 && !await _functionalAreaRepository.IsExistAsync(p => p.Id == request.FunctionalAreaId))
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false
                };
            }

            if (project != null)
            {
                project.SetDetails(request.ProjectCode, request.ProjectName, request.IsStore, request.IsHeadOffice);
                project.FunctionalAreaId = request.FunctionalAreaId;
                await _repository.UpdateAsync(project);
                await _repository.CommitAsync();

                return new UpdateProjectCommandResponse
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false,
                };
            }
        }
    }
}
