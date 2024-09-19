using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class UpdateEmployeeLocationCommandHandler : IRequestHandler<UpdateEmployeeLocationCommandRequest, UpdateEmployeeLocationCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IResidentalAreaRepository _residentalAreaRepository;
        private readonly IBakuDistrictRepository _bakuDistrictRepository;
        private readonly IBakuMetroRepository _bakuMetroRepository;
        private readonly IBakuTargetRepository _bakuTargetRepository;

        public UpdateEmployeeLocationCommandHandler(
            IEmployeeRepository employeeRepository,
            IResidentalAreaRepository residentalAreaRepository,
            IBakuDistrictRepository bakuDistrictRepository,
            IBakuMetroRepository bakuMetroRepository,
            IBakuTargetRepository bakuTargetRepository)
        {
            _employeeRepository = employeeRepository;
            _residentalAreaRepository = residentalAreaRepository;
            _bakuDistrictRepository = bakuDistrictRepository;
            _bakuMetroRepository = bakuMetroRepository;
            _bakuTargetRepository = bakuTargetRepository;
        }

        public async Task<UpdateEmployeeLocationCommandResponse> Handle(UpdateEmployeeLocationCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.EmployeeId <= 0)
                    throw new BadRequestException("EmployeeId is required and must be greater than 0.");

                var employee = await _employeeRepository.GetAsync(d => d.Id == request.EmployeeId);
                if (employee == null)
                    throw new BadRequestException($"Employee with ID {request.EmployeeId} does not exist.");

                if (request.ResidentalAreaId.HasValue)
                {
                    var residentalAreaExists = await _residentalAreaRepository.IsExistAsync(d => d.Id == request.ResidentalAreaId.Value);
                    if (!residentalAreaExists)
                        throw new BadRequestException($"ResidentalArea with ID {request.ResidentalAreaId.Value} does not exist.");
                    employee.ResidentalAreaId = request.ResidentalAreaId;
                }

                if (request.BakuDistrictId.HasValue)
                {
                    var bakuDistrictExists = await _bakuDistrictRepository.IsExistAsync(d => d.Id == request.BakuDistrictId.Value);
                    if (!bakuDistrictExists)
                        throw new BadRequestException($"BakuDistrict with ID {request.BakuDistrictId.Value} does not exist.");
                    employee.BakuDistrictId = request.BakuDistrictId;
                }

                if (request.BakuMetroId.HasValue)
                {
                    var bakuMetroExists = await _bakuMetroRepository.IsExistAsync(d => d.Id == request.BakuMetroId.Value);
                    if (!bakuMetroExists)
                        throw new BadRequestException($"BakuMetro with ID {request.BakuMetroId.Value} does not exist.");
                    employee.BakuMetroId = request.BakuMetroId;
                }

                if (request.BakuTargetId.HasValue)
                {
                    var bakuTargetExists = await _bakuTargetRepository.IsExistAsync(d => d.Id == request.BakuTargetId.Value);
                    if (!bakuTargetExists)
                        throw new BadRequestException($"BakuTarget with ID {request.BakuTargetId.Value} does not exist.");
                    employee.BakuTargetId = request.BakuTargetId;
                }

                await _employeeRepository.UpdateAsync(employee);
                await _employeeRepository.CommitAsync();

                return new UpdateEmployeeLocationCommandResponse
                {
                    IsSuccess = true,
                    Message = "Employee location updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new UpdateEmployeeLocationCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while updating employee location: {ex.Message}"
                };
            }
        }
    }
}
