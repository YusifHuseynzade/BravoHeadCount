using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;

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
                OperationDirectorMail = project.OperationDirectorMail,
                AreaManager = project.AreaManager,
                AreaManagerBadge = request.AreaManagerBadge,
                AreaManagerMail = project.AreaManagerMail,
                StoreManagerMail = project.StoreManagerMail,
                Recruiter = project.Recruiter,
                RecruiterMail = project.RecruiterMail,
                StoreOpeningDate = project.StoreOpeningDate,  
                StoreClosedDate = project.StoreClosedDate     
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
                request.OperationDirector,
                request.OperationDirectorMail,
                request.AreaManager,
                request.AreaManagerMail,
                request.StoreManagerMail,
                request.Recruiter,
                request.RecruiterMail
            );

            // StoreOpeningDate ve StoreClosedDate güncelleniyor
            project.StoreOpeningDate = request.StoreOpeningDate ?? project.StoreOpeningDate;
            project.StoreClosedDate = request.StoreClosedDate ?? project.StoreClosedDate;

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
                    OldDirectorEmail = oldProject.OperationDirectorMail,
                    NewDirectorEmail = project.OperationDirectorMail,
                    OldAreaManager = oldProject.AreaManager,
                    NewAreaManager = project.AreaManager,
                    OldAreaManagerEmail = oldProject.AreaManagerMail,
                    NewAreaManagerEmail = project.AreaManagerMail,
                    OldStoreManagerEmail = oldProject.StoreManagerMail,
                    NewStoreManagerEmail = project.StoreManagerMail,
                    OldRecruiter = oldProject.Recruiter,
                    NewRecruiter = project.Recruiter,
                    OldRecruiterEmail = oldProject.RecruiterMail,
                    NewRecruiterEmail = project.RecruiterMail,
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
                   oldProject.OperationDirectorMail != newProject.OperationDirectorMail ||
                   oldProject.AreaManager != newProject.AreaManager ||
                   oldProject.AreaManagerMail != newProject.AreaManagerMail ||
                   oldProject.StoreManagerMail != newProject.StoreManagerMail ||
                   oldProject.Recruiter != newProject.Recruiter ||
                   oldProject.RecruiterMail != newProject.RecruiterMail;
        }
    }
}
