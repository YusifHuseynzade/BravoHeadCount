using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectDetails.Handlers.CommandHandlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommandRequest, CreateProjectCommandResponse>
    {
        private readonly IProjectRepository _repository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;

        public CreateProjectCommandHandler(IProjectRepository repository, IFunctionalAreaRepository functionalAreaRepository)
        {
            _repository = repository;
            _functionalAreaRepository = functionalAreaRepository;
        }

        public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
        {
            // Check if the project already exists
            if (await _repository.IsExistAsync(p => p.ProjectCode == request.ProjectCode))
            {
                return new CreateProjectCommandResponse
                {
                    IsSuccess = false,
                };
            }

            if (request.FunctionalAreaId != 0 && !await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId))
            {
                return new CreateProjectCommandResponse
                {
                    IsSuccess = false,
                };
            }

            // Create a new project and set its details
            var project = new Project
            {
                FunctionalAreaId = request.FunctionalAreaId
            };
            project.SetDetails(request.ProjectCode, request.ProjectName, request.IsStore, request.IsHeadOffice);

            // Add the new project to the repository
            await _repository.AddAsync(project);
            await _repository.CommitAsync();

            return new CreateProjectCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
