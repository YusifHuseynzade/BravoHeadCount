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
        private readonly IBakuDistrictRepository _bakuDistrictRepository;
        private readonly IBakuMetroRepository _bakuMetroRepository;
        private readonly IBakuTargetRepository _bakuTargetRepository;

        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IStoreRepository storeRepository,
            IResidentalAreaRepository residentalAreaRepository,
            IProjectRepository projectRepository,
            IPositionRepository positionRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository,
            IBakuDistrictRepository bakuDistrictRepository,
            IBakuMetroRepository bakuMetroRepository,
            IBakuTargetRepository bakuTargetRepository)
        {
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
            _residentalAreaRepository = residentalAreaRepository;
            _projectRepository = projectRepository;
            _positionRepository = positionRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
            _bakuDistrictRepository = bakuDistrictRepository;
            _bakuMetroRepository = bakuMetroRepository;
            _bakuTargetRepository = bakuTargetRepository;
        }

        public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Validasyon
                if (request.Id <= 0)
                    throw new BadRequestException("Id is required and must be greater than 0.");

                if (string.IsNullOrWhiteSpace(request.FIN))
                    throw new BadRequestException("FIN is required.");

                if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                    throw new BadRequestException("PhoneNumber is required.");

                // Veritabanı kontrolü
                var employee = await _employeeRepository.GetAsync(d => d.Id == request.Id);
                if (employee == null)
                    throw new BadRequestException($"Employee with ID {request.Id} does not exist.");

                if (request.ResidentalAreaId.HasValue)
                {
                    var residentalAreaExists = await _residentalAreaRepository.IsExistAsync(d => d.Id == request.ResidentalAreaId);
                    if (!residentalAreaExists)
                        throw new BadRequestException($"ResidentalArea with ID {request.ResidentalAreaId} does not exist.");
                }

                if (request.BakuDistrictId.HasValue)
                {
                    var bakuDistrictExists = await _bakuDistrictRepository.IsExistAsync(d => d.Id == request.BakuDistrictId);
                    if (!bakuDistrictExists)
                        throw new BadRequestException($"BakuDistrict with ID {request.BakuDistrictId} does not exist.");
                }

                if (request.BakuMetroId.HasValue)
                {
                    var bakuMetroExists = await _bakuMetroRepository.IsExistAsync(d => d.Id == request.BakuMetroId);
                    if (!bakuMetroExists)
                        throw new BadRequestException($"BakuMetro with ID {request.BakuMetroId} does not exist.");
                }

                if (request.BakuTargetId.HasValue)
                {
                    var bakuTargetExists = await _bakuTargetRepository.IsExistAsync(d => d.Id == request.BakuTargetId);
                    if (!bakuTargetExists)
                        throw new BadRequestException($"BakuTarget with ID {request.BakuTargetId} does not exist.");
                }

                if (request.SubSectionId.HasValue)
                {
                    var subSectionExists = await _subSectionRepository.IsExistAsync(d => d.Id == request.SubSectionId);
                    if (!subSectionExists)
                        throw new BadRequestException($"SubSection with ID {request.SubSectionId.Value} does not exist.");
                }

                // Employee güncelleme
                employee.FullName = request.FullName ?? employee.FullName;
                employee.Badge = request.Badge ?? employee.Badge;
                employee.FIN = request.FIN;
                employee.PhoneNumber = request.PhoneNumber;
                employee.ResidentalAreaId = request.ResidentalAreaId;
                employee.BakuDistrictId = request.BakuDistrictId;
                employee.BakuMetroId = request.BakuMetroId;
                employee.BakuTargetId = request.BakuTargetId;
                employee.RecruiterComment = request.RecruiterComment ?? employee.RecruiterComment;
                employee.ProjectId = request.ProjectId;
                employee.PositionId = request.PositionId;
                employee.SectionId = request.SectionId;
                employee.SubSectionId = request.SubSectionId;
                employee.StartedDate = request.StartedDate ?? employee.StartedDate;
                employee.ContractEndDate = request.ContractEndDate ?? employee.ContractEndDate;

                await _employeeRepository.UpdateAsync(employee);
                await _employeeRepository.CommitAsync();

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
