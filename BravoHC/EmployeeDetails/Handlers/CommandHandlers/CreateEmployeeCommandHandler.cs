using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommandRequest, CreateEmployeeCommandResponse>
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

        public CreateEmployeeCommandHandler(
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

        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Zorunlu alanların kontrolü
                if (string.IsNullOrWhiteSpace(request.FullName))
                    throw new BadRequestException("FullName is required.");

                if (string.IsNullOrWhiteSpace(request.Badge))
                    throw new BadRequestException("Badge is required.");

                if (string.IsNullOrWhiteSpace(request.FIN))
                    throw new BadRequestException("FIN is required.");

                if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                    throw new BadRequestException("PhoneNumber is required.");

                if (request.ProjectId <= 0)
                    throw new BadRequestException("ProjectId is required and must be greater than 0.");

                if (request.PositionId <= 0)
                    throw new BadRequestException("PositionId is required and must be greater than 0.");

                if (request.SectionId <= 0)
                    throw new BadRequestException("SectionId is required and must be greater than 0.");

                // Veritabanı kontrolü
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

                var employee = new Employee
                {
                    FullName = request.FullName,
                    Badge = request.Badge,
                    FIN = request.FIN,
                    PhoneNumber = request.PhoneNumber,
                    ResidentalAreaId = request.ResidentalAreaId,
                    BakuDistrictId = request.BakuDistrictId,
                    BakuMetroId = request.BakuMetroId,
                    BakuTargetId = request.BakuTargetId,
                    RecruiterComment = request.RecruiterComment,
                    ProjectId = request.ProjectId,
                    PositionId = request.PositionId,
                    SectionId = request.SectionId,
                    SubSectionId = request.SubSectionId,
                    StartedDate = request.StartedDate,
                    ContractEndDate = request.ContractEndDate,
                };

                await _employeeRepository.AddAsync(employee);
                await _employeeRepository.CommitAsync();

                return new CreateEmployeeCommandResponse
                {
                    IsSuccess = true,
                    Message = "Employee created successfully."
                };
            }
            catch (ValidationException valEx)
            {
                return new CreateEmployeeCommandResponse
                {
                    IsSuccess = false,
                    Message = $"BadRequest: {valEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new CreateEmployeeCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while creating employee: {ex.Message}"
                };
            }
        }
    }
}
