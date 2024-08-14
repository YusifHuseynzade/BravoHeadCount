using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HeadCountDetails.Handlers.CommandHandlers
{
    public class BulkUpdateHeadCountCommandHandler : IRequestHandler<BulkUpdateHeadCountCommandRequest, BulkUpdateHeadCountCommandResponse>
    {
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubSectionRepository _subSectionRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BulkUpdateHeadCountCommandHandler(
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository,
            IPositionRepository positionRepository,
            IEmployeeRepository employeeRepository)
        {
            _headCountRepository = headCountRepository;
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
            _positionRepository = positionRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<BulkUpdateHeadCountCommandResponse> Handle(BulkUpdateHeadCountCommandRequest request, CancellationToken cancellationToken)
        {
            var updateResults = new List<UpdateHeadCountResult>();

            foreach (var headCountModel in request.HeadCounts)
            {
                try
                {
                    // Validasyon işlemleri
                    if (headCountModel.Id <= 0)
                        throw new BadRequestException("Id is required and must be greater than 0.");

                    if (headCountModel.ProjectId <= 0)
                        throw new BadRequestException("ProjectId is required and must be greater than 0.");

                    if (headCountModel.FunctionalAreaId <= 0)
                        throw new BadRequestException("FunctionalAreaId is required and must be greater than 0.");

                    if (headCountModel.HCNumber < 0)
                        throw new BadRequestException("HCNumber is required and must not be negative.");

                    // Veritabanı kontrolü
                    var headCount = await _headCountRepository.GetAsync(d => d.Id == headCountModel.Id);
                    if (headCount == null)
                        throw new BadRequestException($"HeadCount with ID {headCountModel.Id} does not exist.");

                    var projectExists = await _projectRepository.IsExistAsync(d => d.Id == headCountModel.ProjectId);
                    if (!projectExists)
                        throw new BadRequestException($"Project with ID {headCountModel.ProjectId} does not exist.");

                    var functionalAreaExists = await _functionalAreaRepository.IsExistAsync(d => d.Id == headCountModel.FunctionalAreaId);
                    if (!functionalAreaExists)
                        throw new BadRequestException($"FunctionalArea with ID {headCountModel.FunctionalAreaId} does not exist.");

                    if (headCountModel.SectionId.HasValue)
                    {
                        var sectionExists = await _sectionRepository.IsExistAsync(d => d.Id == headCountModel.SectionId);
                        if (!sectionExists)
                            throw new BadRequestException($"Section with ID {headCountModel.SectionId.Value} does not exist.");
                    }

                    if (headCountModel.PositionId.HasValue)
                    {
                        var positionExists = await _positionRepository.IsExistAsync(d => d.Id == headCountModel.PositionId);
                        if (!positionExists)
                            throw new BadRequestException($"Position with ID {headCountModel.PositionId.Value} does not exist.");
                    }

                    if (headCountModel.EmployeeId.HasValue)
                    {
                        var employeeExists = await _employeeRepository.IsExistAsync(d => d.Id == headCountModel.EmployeeId);
                        if (!employeeExists)
                            throw new BadRequestException($"Employee with ID {headCountModel.EmployeeId.Value} does not exist.");
                    }

                    int? parentHeadCountId = null;
                    if (headCountModel.ParentId.HasValue)
                    {
                        var parentHeadCount = await _headCountRepository.GetAsync(d => d.EmployeeId == headCountModel.ParentId.Value);
                        if (parentHeadCount == null)
                            throw new BadRequestException($"Parent HeadCount with Employee ID {headCountModel.ParentId.Value} does not exist.");
                        parentHeadCountId = parentHeadCount.Id;
                    }

                    headCount.ProjectId = headCountModel.ProjectId;
                    headCount.FunctionalAreaId = headCountModel.FunctionalAreaId;
                    headCount.SectionId = headCountModel.SectionId;
                    headCount.SubSectionId = headCountModel.SubSectionId;
                    headCount.PositionId = headCountModel.PositionId;
                    headCount.EmployeeId = headCountModel.EmployeeId;
                    headCount.HCNumber = headCountModel.HCNumber;
                    headCount.ParentId = parentHeadCountId;
                    headCount.IsVacant = headCountModel.IsVacant;
                    headCount.RecruiterComment = headCountModel.RecruiterComment;

                    await _headCountRepository.UpdateAsync(headCount);

                    updateResults.Add(new UpdateHeadCountResult
                    {
                        Id = headCount.Id,
                        IsSuccess = true,
                        Message = "HeadCount updated successfully."
                    });
                }
                catch (BadRequestException valEx)
                {
                    updateResults.Add(new UpdateHeadCountResult
                    {
                        Id = headCountModel.Id,
                        IsSuccess = false,
                        Message = $"BadRequest: {valEx.Message}"
                    });
                }
                catch (Exception ex)
                {
                    updateResults.Add(new UpdateHeadCountResult
                    {
                        Id = headCountModel.Id,
                        IsSuccess = false,
                        Message = $"Error occurred while updating headcount: {ex.Message}"
                    });
                }
            }

            await _headCountRepository.CommitAsync();

            return new BulkUpdateHeadCountCommandResponse
            {
                IsSuccess = updateResults.All(r => r.IsSuccess),
                Results = updateResults
            };
        }
    }
}
