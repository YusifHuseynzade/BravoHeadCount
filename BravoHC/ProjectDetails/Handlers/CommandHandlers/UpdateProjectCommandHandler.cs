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
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IProjectSectionsRepository _projectSectionsRepository;

        public UpdateProjectCommandHandler(
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

        public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == request.Id);

            // Eğer proje bulunamazsa
            if (project == null)
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false,
                    Message = "Project not found."
                };
            }

            // Aynı ProjectCode'a sahip başka bir proje olup olmadığını kontrol et
            if (await _projectRepository.IsExistAsync(d => d.ProjectCode == request.ProjectCode && d.Id != request.Id))
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false,
                    Message = "Project code already exists."
                };
            }

            // FunctionalArea'nın var olup olmadığını kontrol et
            if (request.FunctionalAreaId != 0 && !await _functionalAreaRepository.IsExistAsync(p => p.Id == request.FunctionalAreaId))
            {
                return new UpdateProjectCommandResponse
                {
                    IsSuccess = false,
                    Message = "Functional area not found."
                };
            }

            // Proje bilgilerini güncelle
            project.SetDetails(request.ProjectCode, request.ProjectName, request.IsStore, request.IsHeadOffice);
            project.FunctionalAreaId = request.FunctionalAreaId;

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

            // Projeyi güncelle ve veritabanına kaydet
            await _projectRepository.UpdateAsync(project);
            await _projectRepository.CommitAsync();

            return new UpdateProjectCommandResponse
            {
                IsSuccess = true,
                Message = "Project updated successfully."
            };
        }
    }
}
