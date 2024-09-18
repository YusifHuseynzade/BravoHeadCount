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
        private readonly ISectionRepository _sectionRepository;
        private readonly IProjectSectionsRepository _projectSectionsRepository;

        public CreateProjectCommandHandler(
            IProjectRepository projectRepository,
            ISectionRepository sectionRepository,
            IProjectSectionsRepository projectSectionsRepository)
        {
            _projectRepository = projectRepository;
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

            // Yeni bir proje oluştur ve detayları ayarla
            var project = new Project
            {
                ProjectCode = request.ProjectCode,
                ProjectName = request.ProjectName,
                IsStore = request.IsStore,
                IsHeadOffice = request.IsHeadOffice,
                IsActive = request.IsActive,
                Format = request.Format,
                FunctionalArea = request.FunctionalArea,
                OperationDirector = request.Director,
                DirectorEmail = request.DirectorEmail,
                AreaManager = request.AreaManager,
                AreaManagerEmail = request.AreaManagerEmail,
                StoreManagerEmail = request.StoreManagerEmail,
                Recruiter = request.Recruiter,
                RecruiterEmail = request.RecruiterEmail
            };

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
