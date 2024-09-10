using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeadCountDetails.Handlers.CommandHandlers
{
    public class UpdateHeadCountCommandHandler : IRequestHandler<UpdateHeadCountCommandRequest, UpdateHeadCountCommandResponse>
    {
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubSectionRepository _subSectionRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHeadCountBackgroundColorRepository _headCountBackgroundColorRepository;

        public UpdateHeadCountCommandHandler(
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository,
            IPositionRepository positionRepository,
            IEmployeeRepository employeeRepository,
            IHeadCountBackgroundColorRepository headCountBackgroundColorRepository)
        {
            _headCountRepository = headCountRepository;
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
            _positionRepository = positionRepository;
            _employeeRepository = employeeRepository;
            _headCountBackgroundColorRepository = headCountBackgroundColorRepository;
        }

        public async Task<UpdateHeadCountCommandResponse> Handle(UpdateHeadCountCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Validasyon
                if (request.Id <= 0)
                    throw new BadRequestException("Id is required and must be greater than 0.");
                if (request.ColorId <= 0)
                    throw new BadRequestException("Id is required and must be greater than 0.");

                if (request.ProjectId <= 0)
                    throw new BadRequestException("ProjectId is required and must be greater than 0.");

                if (request.FunctionalAreaId <= 0)
                    throw new BadRequestException("FunctionalAreaId is required and must be greater than 0.");

                if (request.HCNumber < 0)
                    throw new BadRequestException("HCNumber is required and must not be negative.");

                // Veritabanı kontrolü
                var headCountExists = await _headCountRepository.IsExistAsync(d => d.Id == request.Id);
                if (!headCountExists)
                    throw new BadRequestException($"HeadCount with ID {request.Id} does not exist.");

                var projectExists = await _projectRepository.IsExistAsync(d => d.Id == request.ProjectId);
                if (!projectExists)
                    throw new BadRequestException($"Project with ID {request.ProjectId} does not exist.");

                var functionalAreaExists = await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId);
                if (!functionalAreaExists)
                    throw new BadRequestException($"FunctionalArea with ID {request.FunctionalAreaId} does not exist.");

                if (request.SectionId.HasValue)
                {
                    var sectionExists = await _sectionRepository.IsExistAsync(d => d.Id == request.SectionId);
                    if (!sectionExists)
                        throw new BadRequestException($"Section with ID {request.SectionId.Value} does not exist.");
                }

                if (request.PositionId.HasValue)
                {
                    var positionExists = await _positionRepository.IsExistAsync(d => d.Id == request.PositionId);
                    if (!positionExists)
                        throw new BadRequestException($"Position with ID {request.PositionId.Value} does not exist.");
                }

                if (request.EmployeeId.HasValue)
                {
                    var employeeExists = await _employeeRepository.IsExistAsync(d => d.Id == request.EmployeeId);
                    if (!employeeExists)
                        throw new BadRequestException($"Employee with ID {request.EmployeeId.Value} does not exist.");
                }

                if (request.ColorId.HasValue)
                {
                    var colorExists = await _headCountBackgroundColorRepository.IsExistAsync(d => d.Id == request.ColorId);
                    if (!colorExists)
                        throw new BadRequestException($"Employee with ID {request.ColorId.Value} does not exist.");
                }

                int? parentHeadCountId = null;
                if (request.ParentId.HasValue)
                {
                    var parentHeadCount = await _headCountRepository.GetAsync(d => d.EmployeeId == request.ParentId.Value);
                    if (parentHeadCount == null)
                        throw new BadRequestException($"Parent HeadCount with Employee ID {request.ParentId.Value} does not exist.");
                    parentHeadCountId = parentHeadCount.Id;
                }

            
                var headCount = await _headCountRepository.GetAsync(d => d.Id == request.Id);
                if (headCount == null)
                {
                    throw new BadRequestException($"HeadCount with ID {request.Id} does not exist.");
                }

                headCount.ProjectId = request.ProjectId;
                headCount.FunctionalAreaId = request.FunctionalAreaId;
                headCount.SectionId = request.SectionId;
                headCount.SubSectionId = request.SubSectionId;
                headCount.PositionId = request.PositionId;
                headCount.EmployeeId = request.EmployeeId;
                headCount.HCNumber = request.HCNumber;
                headCount.ParentId = parentHeadCountId;
                headCount.IsVacant = request.IsVacant;
                headCount.RecruiterComment = request.RecruiterComment;
                headCount.ColorId = request.ColorId;

                await _headCountRepository.UpdateAsync(headCount);
                await _headCountRepository.CommitAsync();

                return new UpdateHeadCountCommandResponse
                {
                    IsSuccess = true,
                    Message = "HeadCount updated successfully."
                };
            }
            catch (BadRequestException valEx)
            {
                return new UpdateHeadCountCommandResponse
                {
                    IsSuccess = false,
                    Message = $"BadRequest: {valEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new UpdateHeadCountCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while updating headcount: {ex.Message}"
                };
            }
        }
    }
}
