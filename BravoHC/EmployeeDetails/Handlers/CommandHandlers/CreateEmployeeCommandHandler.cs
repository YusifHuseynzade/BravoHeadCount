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
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubSectionRepository _subSectionRepository;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IStoreRepository storeRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            IProjectRepository projectRepository,
            IPositionRepository positionRepository,
            ISectionRepository sectionRepository,
            ISubSectionRepository subSectionRepository)
        {
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _projectRepository = projectRepository;
            _positionRepository = positionRepository;
            _sectionRepository = sectionRepository;
            _subSectionRepository = subSectionRepository;
        }

        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(request.FullName))
                    throw new BadRequestException("FullName is required.");

                if (string.IsNullOrWhiteSpace(request.Badge))
                    throw new BadRequestException("Badge is required.");

                if (request.StoreId <= 0)
                    throw new BadRequestException("StoreId is required and must be greater than 0.");

                if (request.FunctionalAreaId <= 0)
                    throw new BadRequestException("FunctionalAreaId is required and must be greater than 0.");

                if (request.ProjectId <= 0)
                    throw new BadRequestException("ProjectId is required and must be greater than 0.");

                if (request.PositionId <= 0)
                    throw new BadRequestException("PositionId is required and must be greater than 0.");

                if (request.SectionId <= 0)
                    throw new BadRequestException("SectionId is required and must be greater than 0.");

                // Veritabanı kontrolü
                var storeExists = await _storeRepository.IsExistAsync(d => d.Id == request.StoreId);
                if (!storeExists)
                    throw new BadRequestException($"Store with ID {request.StoreId} does not exist.");

                var functionalAreaExists = await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId);
                if (!functionalAreaExists)
                    throw new BadRequestException($"FunctionalArea with ID {request.FunctionalAreaId} does not exist.");

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
                        throw new BadRequestException($"SubSection with ID {request.SubSectionId.Value} does not exist.");
                }

                var employee = new Employee
                {
                    FullName = request.FullName,
                    Badge = request.Badge,
                    Director = request.Director,
                    Recruiter = request.Recruiter,
                    AreaManager = request.AreaManager,
                    StoreManager = request.StoreManager,
                    StoreId = request.StoreId,
                    FunctionalAreaId = request.FunctionalAreaId,
                    ProjectId = request.ProjectId,
                    PositionId = request.PositionId,
                    SectionId = request.SectionId,
                    SubSectionId = request.SubSectionId
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
