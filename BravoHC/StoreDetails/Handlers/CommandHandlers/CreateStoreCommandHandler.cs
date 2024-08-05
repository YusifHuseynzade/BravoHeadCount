using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Commands.Request;
using StoreDetails.Commands.Response;

namespace StoreDetails.Handlers.CommandHandlers
{
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommandRequest, CreateStoreCommandResponse>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly IFormatRepository _formatRepository;

        public CreateStoreCommandHandler(
            IStoreRepository storeRepository,
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            IFormatRepository formatRepository)
        {
            _storeRepository = storeRepository;
            _headCountRepository = headCountRepository;
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _formatRepository = formatRepository;
        }

        public async Task<CreateStoreCommandResponse> Handle(CreateStoreCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Validasyon
                if (request.ProjectId == 0)
                    throw new BadRequestException("ProjectId is required and must not be zero.");

                if (request.FunctionalAreaId == 0)
                    throw new BadRequestException("FunctionalAreaId is required and must not be zero.");

                if (request.FormatId == 0)
                    throw new BadRequestException("FormatId is required and must not be zero.");

                if (request.HeadCountNumber == 0)
                    throw new BadRequestException("HeadCountNumber is required and must not be zero.");

                // Veritabanı kontrolü
                var projectExists = await _projectRepository.IsExistAsync(d => d.Id == request.ProjectId);
                if (!projectExists)
                    throw new BadRequestException($"Project with ID {request.ProjectId} does not exist.");

                var functionalAreaExists = await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId);
                if (!functionalAreaExists)
                    throw new BadRequestException($"FunctionalArea with ID {request.FunctionalAreaId} does not exist.");

                var formatExists = await _formatRepository.IsExistAsync(d => d.Id == request.FormatId);
                if (!formatExists)
                    throw new BadRequestException($"Format with ID {request.FormatId} does not exist.");

                // Yeni store oluşturma
                var store = new Store
                {
                    ProjectId = request.ProjectId,
                    FunctionalAreaId = request.FunctionalAreaId,
                    FormatId = request.FormatId,
                    HeadCountNumber = request.HeadCountNumber,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                await _storeRepository.AddAsync(store);
                await _storeRepository.CommitAsync();

                // Headcount kayıtlarını oluşturma
                for (int i = 1; i <= request.HeadCountNumber; i++)
                {
                    var headCount = new HeadCount
                    {
                        ProjectId = request.ProjectId,
                        FunctionalAreaId = request.FunctionalAreaId,
                        HCNumber = i
                    };

                    await _headCountRepository.AddAsync(headCount);
                    await _headCountRepository.CommitAsync();
                }

                return new CreateStoreCommandResponse
                {
                    IsSuccess = true,
                    Message = "Store and headcount records created successfully."
                };
            }
            catch (ValidationException valEx)
            {
                return new CreateStoreCommandResponse
                {
                    IsSuccess = false,
                    Message = $"BadRequest: {valEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new CreateStoreCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while creating store: {ex.Message}"
                };
            }
        }
    }
}
