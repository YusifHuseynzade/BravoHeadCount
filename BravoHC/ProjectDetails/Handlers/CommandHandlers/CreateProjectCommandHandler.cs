using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectDetails.Handlers.CommandHandlers
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommandRequest, CreateProjectCommandResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IProjectSectionsRepository _projectSectionsRepository;

        public CreateProjectCommandHandler(
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            ISectionRepository sectionRepository,
            IProjectSectionsRepository projectSectionsRepository)
        {
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _sectionRepository = sectionRepository;
            _projectSectionsRepository = projectSectionsRepository;
        }

        public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
        {
            // Projenin daha önce var olup olmadığını kontrol et
            if (await _projectRepository.IsExistAsync(p => p.ProjectCode == request.ProjectCode))
            {
                return new CreateProjectCommandResponse
                {
                    IsSuccess = false,
                    Message = "Project already exists."
                };
            }

            // FunctionalArea'nın var olup olmadığını kontrol et
            if (request.FunctionalAreaId != 0 && !await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId))
            {
                return new CreateProjectCommandResponse
                {
                    IsSuccess = false,
                    Message = "Functional area not found."
                };
            }

            // Yeni bir proje oluştur ve detayları ayarla
            var project = new Project
            {
                FunctionalAreaId = request.FunctionalAreaId
            };
            project.SetDetails(request.ProjectCode, request.ProjectName, request.IsStore, request.IsHeadOffice);

            // Projeyi veritabanına ekle
            await _projectRepository.AddAsync(project);
            await _projectRepository.CommitAsync();

            // Eğer SectionId'ler varsa, ProjectSections ilişkisini ekle
            if (request.SectionIds != null && request.SectionIds.Any())
            {
                foreach (var sectionId in request.SectionIds)
                {
                    var sectionExists = await _sectionRepository.IsExistAsync(s => s.Id == sectionId);
                    if (sectionExists)
                    {
                        var projectSection = new ProjectSections
                        {
                            ProjectId = project.Id,
                            SectionId = sectionId
                        };

                        await _projectSectionsRepository.AddAsync(projectSection);
                    }
                }
                await _projectSectionsRepository.CommitAsync();
            }

            return new CreateProjectCommandResponse
            {
                IsSuccess = true,
                Message = "Project created successfully."
            };
        }
    }
}
