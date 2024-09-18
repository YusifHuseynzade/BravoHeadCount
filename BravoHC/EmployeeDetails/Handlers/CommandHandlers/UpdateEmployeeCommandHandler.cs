using Core.Helpers;
using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommandRequest, UpdateEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IResidentalAreaRepository _residentalAreaRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubSectionRepository _subSectionRepository;

        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IStoreRepository storeRepository,
            IResidentalAreaRepository residentalAreaRepository,
            IProjectRepository projectRepository,
            IPositionRepository positionRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository)
        {
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
            _residentalAreaRepository = residentalAreaRepository;
            _projectRepository = projectRepository;
            _positionRepository = positionRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
        }

        public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Validasyon
                if (request.Id <= 0)
                    throw new BadRequestException("Id is required and must be greater than 0.");

                if (string.IsNullOrWhiteSpace(request.FullName))
                    throw new BadRequestException("FullName is required.");

                if (string.IsNullOrWhiteSpace(request.Badge))
                    throw new BadRequestException("Badge is required.");


                if (string.IsNullOrWhiteSpace(request.FIN))
                    throw new BadRequestException("FIN is required.");

                if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                    throw new BadRequestException("PhoneNumber is required.");

                if (request.ResidentalAreaId <= 0)
                    throw new BadRequestException("ResidentalAreaId is required and must be greater than 0.");

                if (request.FunctionalAreaId <= 0)
                    throw new BadRequestException("FunctionalAreaId is required and must be greater than 0.");

                if (request.ProjectId <= 0)
                    throw new BadRequestException("ProjectId is required and must be greater than 0.");

                if (request.PositionId <= 0)
                    throw new BadRequestException("PositionId is required and must be greater than 0.");

                if (request.SectionId <= 0)
                    throw new BadRequestException("SectionId is required and must be greater than 0.");

                // Veritabanı kontrolü
                var employeeExists = await _employeeRepository.IsExistAsync(d => d.Id == request.Id);
                if (!employeeExists)
                    throw new BadRequestException($"Employee with ID {request.Id} does not exist.");

                var residentalAreaExists = await _residentalAreaRepository.IsExistAsync(d => d.Id == request.ResidentalAreaId);
                if (!residentalAreaExists)
                    throw new BadRequestException($"ResidentalArea with ID {request.ResidentalAreaId} does not exist.");


                var projectExists = await _projectRepository.IsExistAsync(d => d.Id == request.ProjectId);
                if (!projectExists)
                    throw new BadRequestException($"Project with ID {request.ProjectId} does not exist.");

                var positionExists = await _positionRepository.IsExistAsync(d => d.Id == request.PositionId);
                if (!positionExists)
                    throw new BadRequestException($"Position with ID {request.PositionId} does not exist.");

                var sectionExists = await _sectionRepository.IsExistAsync(d => d.Id == request.SectionId);
                if (!sectionExists)
                    throw new BadRequestException($"Section with ID {request.SectionId} does not exist.");

                if (request.SubSectionId.HasValue)
                {
                    var subSectionExists = await _subSectionRepository.IsExistAsync(d => d.Id == request.SubSectionId);
                    if (!subSectionExists)
                        throw new BadRequestException($"SubSection with ID {request.SubSectionId} does not exist.");
                }

                // Employee güncelleme
                var employee = await _employeeRepository.GetAsync(d => d.Id == request.Id);
                if (employee == null)
                {
                    throw new BadRequestException($"Employee with ID {request.Id} does not exist.");
                }

                employee.FullName = request.FullName;
                employee.Badge = request.Badge;
                employee.FIN = request.FIN;
                employee.PhoneNumber = request.PhoneNumber;
                employee.ResidentalAreaId = request.ResidentalAreaId;
                employee.ProjectId = request.ProjectId;
                employee.PositionId = request.PositionId;
                employee.SectionId = request.SectionId;
                employee.SubSectionId = request.SubSectionId;
                employee.StartedDate = (DateTime)request.StartedDate;
                employee.ContractEndDate = request.ContractEndDate;


                await _employeeRepository.UpdateAsync(employee);

                return new UpdateEmployeeCommandResponse
                {
                    IsSuccess = true,
                    Message = "Employee updated successfully."
                };
            }
            catch (ValidationException valEx)
            {
                return new UpdateEmployeeCommandResponse
                {
                    IsSuccess = false,
                    Message = $"BadRequest: {valEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new UpdateEmployeeCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while updating employee: {ex.Message}"
                };
            }
        }
    }
}
