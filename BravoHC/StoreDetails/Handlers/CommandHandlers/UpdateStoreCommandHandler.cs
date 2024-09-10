using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Commands.Request;
using StoreDetails.Commands.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreDetails.Handlers.CommandHandlers
{
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommandRequest, UpdateStoreCommandResponse>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly IFormatRepository _formatRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateStoreCommandHandler(
            IStoreRepository storeRepository,
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            IFunctionalAreaRepository functionalAreaRepository,
            IFormatRepository formatRepository,
            IEmployeeRepository employeeRepository)
        {
            _storeRepository = storeRepository;
            _headCountRepository = headCountRepository;
            _projectRepository = projectRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _formatRepository = formatRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<UpdateStoreCommandResponse> Handle(UpdateStoreCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Veritabanı kontrolü
                var storeExists = await _storeRepository.IsExistAsync(d => d.Id == request.Id);
                if (!storeExists)
                    throw new BadRequestException($"Store with ID {request.Id} does not exist.");

                var projectExists = await _projectRepository.IsExistAsync(d => d.Id == request.ProjectId);
                if (!projectExists)
                    throw new BadRequestException($"Project with ID {request.ProjectId} does not exist.");

                var functionalAreaExists = await _functionalAreaRepository.IsExistAsync(d => d.Id == request.FunctionalAreaId);
                if (!functionalAreaExists)
                    throw new BadRequestException($"FunctionalArea with ID {request.FunctionalAreaId} does not exist.");

                var formatExists = await _formatRepository.IsExistAsync(d => d.Id == request.FormatId);
                if (!formatExists)
                    throw new BadRequestException($"Format with ID {request.FormatId} does not exist.");

                // Yöneticilerin varlığını kontrol et
                if (request.DirectorId.HasValue)
                {
                    var directorExists = await _employeeRepository.IsExistAsync(d => d.Id == request.DirectorId.Value);
                    if (!directorExists)
                        throw new BadRequestException($"Director with ID {request.DirectorId.Value} does not exist.");
                }

                if (request.AreaManagerId.HasValue)
                {
                    var areaManagerExists = await _employeeRepository.IsExistAsync(d => d.Id == request.AreaManagerId.Value);
                    if (!areaManagerExists)
                        throw new BadRequestException($"AreaManager with ID {request.AreaManagerId.Value} does not exist.");
                }

                if (request.StoreManagerId.HasValue)
                {
                    var storeManagerExists = await _employeeRepository.IsExistAsync(d => d.Id == request.StoreManagerId.Value);
                    if (!storeManagerExists)
                        throw new BadRequestException($"StoreManager with ID {request.StoreManagerId.Value} does not exist.");
                }

                if (request.RecruiterId.HasValue)
                {
                    var recruiterExists = await _employeeRepository.IsExistAsync(d => d.Id == request.RecruiterId.Value);
                    if (!recruiterExists)
                        throw new BadRequestException($"Recruiter with ID {request.RecruiterId.Value} does not exist.");
                }

                var store = await _storeRepository.GetAsync(d => d.Id == request.Id);
                if (store == null)
                {
                    throw new BadRequestException($"Store with ID {request.Id} does not exist.");
                }

                int oldHeadCountNumber = store.HeadCountNumber;
                store.DirectorId = request.DirectorId;
                store.AreaManagerId = request.AreaManagerId;
                store.StoreManagerId = request.StoreManagerId;
                store.RecruiterId = request.RecruiterId;
                store.ProjectId = request.ProjectId;
                store.FunctionalAreaId = request.FunctionalAreaId;
                store.FormatId = request.FormatId;
                store.HeadCountNumber = request.HeadCountNumber;
                store.ModifiedDate = DateTime.UtcNow;

                await _storeRepository.UpdateAsync(store);
                await _storeRepository.CommitAsync();

                // Headcount kayıtlarını güncelleme veya ekleme
                if (request.HeadCountNumber > oldHeadCountNumber)
                {
                    for (int i = oldHeadCountNumber + 1; i <= request.HeadCountNumber; i++)
                    {
                        var headCount = new HeadCount
                        {
                            ProjectId = request.ProjectId,
                            FunctionalAreaId = request.FunctionalAreaId,
                            HCNumber = i
                        };

                        await _headCountRepository.AddAsync(headCount);
                    }
                    await _headCountRepository.CommitAsync();
                }

                return new UpdateStoreCommandResponse
                {
                    IsSuccess = true,
                    Message = "Store updated successfully."
                };
            }
            catch (BadRequestException valEx)
            {
                return new UpdateStoreCommandResponse
                {
                    IsSuccess = false,
                    Message = $"BadRequest: {valEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new UpdateStoreCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while updating store: {ex.Message}"
                };
            }
        }
    }
}
