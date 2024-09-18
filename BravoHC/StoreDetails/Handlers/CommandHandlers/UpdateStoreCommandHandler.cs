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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStoreHistoryRepository _storeHistoryRepository;

        public UpdateStoreCommandHandler(
            IStoreRepository storeRepository,
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository,
            IStoreHistoryRepository storeHistoryRepository)
        {
            _storeRepository = storeRepository;
            _headCountRepository = headCountRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _storeHistoryRepository = storeHistoryRepository;
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


                var store = await _storeRepository.GetAsync(d => d.Id == request.Id);
                if (store == null)
                {
                    throw new BadRequestException($"Store with ID {request.Id} does not exist.");
                }

                int oldHeadCountNumber = store.HeadCountNumber;
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
                            HCNumber = i
                        };

                        await _headCountRepository.AddAsync(headCount);
                    }
                    await _headCountRepository.CommitAsync();
                }

                // Eğer HeadCountNumber değiştiyse, StoreHistory kaydı ekleyelim
                if (oldHeadCountNumber != request.HeadCountNumber)
                {
                    var storeHistory = new StoreHistory
                    {
                        StoreId = store.Id,
                        OldHeadCountNumber = oldHeadCountNumber,
                        NewHeadCountNumber = request.HeadCountNumber,
                        ModifiedDate = DateTime.UtcNow.AddHours(4)
                    };

                    await _storeHistoryRepository.AddAsync(storeHistory);
                    await _storeHistoryRepository.CommitAsync();
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
