using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Commands.Request;
using StoreDetails.Commands.Response;
using System;
using System.Linq;
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
        private readonly IHeadCountBackgroundColorRepository _colorRepository;

        public UpdateStoreCommandHandler(
            IStoreRepository storeRepository,
            IHeadCountRepository headCountRepository,
            IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository,
            IStoreHistoryRepository storeHistoryRepository,
            IHeadCountBackgroundColorRepository colorRepository)
        {
            _storeRepository = storeRepository;
            _headCountRepository = headCountRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _storeHistoryRepository = storeHistoryRepository;
            _colorRepository = colorRepository;
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

                // Headcount table'daki mevcut HCNumber'ları kontrol et
                var headCountsForProject = await _headCountRepository.GetAllAsync(hc => hc.ProjectId == request.ProjectId);
                int maxHCNumberInTable = headCountsForProject.Any() ? headCountsForProject.Max(hc => hc.HCNumber) : 0;

                var yellowColorId = await _colorRepository.GetYellowColorIdAsync();
                var whiteColorId = await _colorRepository.GetWhiteColorIdAsync();

                // Eğer headcount tablosundaki HCNumber sayısı, store'daki HeadCountNumber'dan büyükse (Azaltma Durumu)
                if (request.HeadCountNumber < maxHCNumberInTable)
                {
                    // Fazla headcount'ları sarıya çevir (request.HeadCountNumber'dan büyük olanlar)
                    var headCountsToTurnYellow = headCountsForProject
                        .Where(hc => hc.HCNumber > request.HeadCountNumber)
                        .ToList();

                    foreach (var headCount in headCountsToTurnYellow)
                    {
                        headCount.ColorId = yellowColorId; // Renk sarıya dönüyor
                        await _headCountRepository.UpdateAsync(headCount);
                    }

                    await _headCountRepository.CommitAsync();
                }

                // Eğer headcount tablosundaki HCNumber sayısı, store'daki HeadCountNumber'dan küçükse
                if (request.HeadCountNumber > maxHCNumberInTable)
                {
                    // Mevcut sarı olanları beyaza çeviriyoruz
                    var yellowHeadCounts = headCountsForProject
                        .Where(hc => hc.ColorId == yellowColorId && hc.HCNumber <= maxHCNumberInTable)
                        .ToList();

                    foreach (var yellowHeadCount in yellowHeadCounts)
                    {
                        yellowHeadCount.ColorId = whiteColorId;
                        await _headCountRepository.UpdateAsync(yellowHeadCount);
                    }

                    await _headCountRepository.CommitAsync();

                    // Eksik olan headcount'ları ekleyelim
                    for (int i = maxHCNumberInTable + 1; i <= request.HeadCountNumber; i++)
                    {
                        var headCount = new HeadCount
                        {
                            ProjectId = request.ProjectId,
                            HCNumber = i,
                            IsVacant = true // Yeni eklenen Headcount'lar boş olarak oluşturuluyor
                        };

                        await _headCountRepository.AddAsync(headCount);
                    }
                    await _headCountRepository.CommitAsync();
                }
                else
                {
                    // Eğer HCNumber zaten store'daki HeadCountNumber kadar veya fazlaysa, sadece sarı olanları beyaza çevir
                    var yellowHeadCounts = headCountsForProject
                        .Where(hc => hc.ColorId == yellowColorId && hc.HCNumber <= request.HeadCountNumber)
                        .ToList();

                    foreach (var yellowHeadCount in yellowHeadCounts)
                    {
                        yellowHeadCount.ColorId = whiteColorId;
                        await _headCountRepository.UpdateAsync(yellowHeadCount);
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
