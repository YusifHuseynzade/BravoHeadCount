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
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommandRequest, UpdateProjectCommandResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectHistoryRepository _projectHistoryRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IProjectSectionsRepository _projectSectionsRepository;

        public UpdateProjectCommandHandler(
            IProjectRepository projectRepository,
            IProjectHistoryRepository projectHistoryRepository,
            ISectionRepository sectionRepository,
            IProjectSectionsRepository projectSectionsRepository)
        {
            _projectRepository = projectRepository;
            _projectHistoryRepository = projectHistoryRepository;
            _sectionRepository = sectionRepository;
            _projectSectionsRepository = projectSectionsRepository;
        }

        public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == request.Id);

            if (project == null)
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false,
                    Message = "Project not found."
                };
            }

            // Eski proje bilgilerini sakla
            var oldProject = new Project
            {
                IsActive = project.IsActive,
                Format = project.Format,
                FunctionalArea = project.FunctionalArea,
                OperationDirector = project.OperationDirector,
                DirectorEmail = project.DirectorEmail,
                AreaManager = project.AreaManager,
                AreaManagerEmail = project.AreaManagerEmail,
                StoreManagerEmail = project.StoreManagerEmail,
                Recruiter = project.Recruiter,
                RecruiterEmail = project.RecruiterEmail
            };

            // Proje bilgilerini güncelle
            project.SetDetails(
                request.ProjectCode,
                request.ProjectName,
                request.IsStore,
                request.IsHeadOffice,
                request.IsActive,
                request.Format,
                request.FunctionalArea,
                request.Director,
                request.DirectorEmail,
                request.AreaManager,
                request.AreaManagerEmail,
                request.StoreManagerEmail,
                request.Recruiter,
                request.RecruiterEmail
            );

            // Mevcut ProjectSections ilişkilerini kaldır
            var existingProjectSections = await _projectSectionsRepository.GetAllAsync(ps => ps.ProjectId == project.Id);
            foreach (var projectSection in existingProjectSections)
            {
                _projectSectionsRepository.Remove(projectSection);
            }
            await _projectSectionsRepository.CommitAsync();

            // Yeni SectionIds ile ProjectSections ilişkisini güncelle
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

            // Proje güncellemesi
            await _projectRepository.UpdateAsync(project);
            await _projectRepository.CommitAsync();

            // Eğer belirli alanlar değiştiyse ProjectHistory kaydı oluştur
            if (HasProjectChanged(oldProject, project))
            {
                var projectHistory = new ProjectHistory
                {
                    ProjectId = project.Id,
                    OldIsActive = oldProject.IsActive,
                    NewIsActive = project.IsActive,
                    OldFormat = oldProject.Format,
                    NewFormat = project.Format,
                    OldFunctionalArea = oldProject.FunctionalArea,
                    NewFunctionalArea = project.FunctionalArea,
                    OldDirector = oldProject.OperationDirector,
                    NewDirector = project.OperationDirector,
                    OldDirectorEmail = oldProject.DirectorEmail,
                    NewDirectorEmail = project.DirectorEmail,
                    OldAreaManager = oldProject.AreaManager,
                    NewAreaManager = project.AreaManager,
                    OldAreaManagerEmail = oldProject.AreaManagerEmail,
                    NewAreaManagerEmail = project.AreaManagerEmail,
                    OldStoreManagerEmail = oldProject.StoreManagerEmail,
                    NewStoreManagerEmail = project.StoreManagerEmail,
                    OldRecruiter = oldProject.Recruiter,
                    NewRecruiter = project.Recruiter,
                    OldRecruiterEmail = oldProject.RecruiterEmail,
                    NewRecruiterEmail = project.RecruiterEmail,
                    ModifiedDate = DateTime.UtcNow,
                };

                await _projectHistoryRepository.AddAsync(projectHistory);
                await _projectHistoryRepository.CommitAsync();
            }

            return new UpdateProjectCommandResponse
            {
                IsSuccess = true,
                Message = "Project updated successfully."
            };
        }

        // Projedeki önemli değişiklikleri kontrol eden bir metot
        private bool HasProjectChanged(Project oldProject, Project newProject)
        {
            return oldProject.IsActive != newProject.IsActive ||
                   oldProject.Format != newProject.Format ||
                   oldProject.FunctionalArea != newProject.FunctionalArea ||
                   oldProject.OperationDirector != newProject.OperationDirector ||
                   oldProject.DirectorEmail != newProject.DirectorEmail ||
                   oldProject.AreaManager != newProject.AreaManager ||
                   oldProject.AreaManagerEmail != newProject.AreaManagerEmail ||
                   oldProject.StoreManagerEmail != newProject.StoreManagerEmail ||
                   oldProject.Recruiter != newProject.Recruiter ||
                   oldProject.RecruiterEmail != newProject.RecruiterEmail;
        }
    }
}
