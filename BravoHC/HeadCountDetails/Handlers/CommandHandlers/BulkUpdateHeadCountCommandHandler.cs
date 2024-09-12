using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeadCountDetails.Handlers.CommandHandlers
{
    public class BulkUpdateHeadCountCommandHandler : IRequestHandler<BulkUpdateHeadCountCommandRequest, BulkUpdateHeadCountCommandResponse>
    {
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IHeadCountBackgroundColorRepository _colorRepository;

        public BulkUpdateHeadCountCommandHandler(
            IHeadCountRepository headCountRepository, IStoreRepository storeRepository, IHeadCountBackgroundColorRepository colorRepository
           )
        {
            _headCountRepository = headCountRepository;
            _storeRepository = storeRepository;
            _colorRepository = colorRepository;
        }

        public async Task<BulkUpdateHeadCountCommandResponse> Handle(BulkUpdateHeadCountCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Headcount'ları güncelle
                int updatedCount = await UpdateHeadCountAsync(request);

                // Eğer hala eksik headcount varsa, yeni headcount'lar oluştur
                int remainingCount = request.Count - updatedCount;
                int createdCount = remainingCount > 0 ? await CreateHeadCountAsync(request, remainingCount) : 0;

                return new BulkUpdateHeadCountCommandResponse
                {
                    IsSuccess = true,
                    Message = $"{updatedCount} kayıt güncellendi, {createdCount} yeni kayıt eklendi.",
                    UpdatedCount = updatedCount,
                    CreatedCount = createdCount
                };
            }
            catch (Exception ex)
            {
                return new BulkUpdateHeadCountCommandResponse
                {
                    IsSuccess = false,
                    Message = $"HeadCount güncellenirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Headcount'ları güncelleme fonksiyonu
        private async Task<int> UpdateHeadCountAsync(BulkUpdateHeadCountCommandRequest request)
        {
            // Var olan headcount'ları HCNumber'a göre küçükten büyüğe sırala
            var existingHeadCounts = (await _headCountRepository.GetAllAsync(hc =>
                hc.ProjectId == request.ProjectId && hc.FunctionalAreaId == request.FunctionalAreaId))
                .OrderBy(hc => hc.HCNumber).ToList();

            int updatedCount = 0;

            foreach (var headCount in existingHeadCounts)
            {
                if (headCount.SectionId == null && headCount.PositionId == null)
                {
                    headCount.SectionId = request.SectionId;
                    headCount.SubSectionId = request.SubSectionId;
                    headCount.PositionId = request.PositionId;
                    headCount.IsVacant = true;

                    await _headCountRepository.UpdateAsync(headCount);
                    updatedCount++;

                    // Eğer gerekli sayıda güncelleme yapıldıysa dur
                    if (updatedCount == request.Count)
                        break;
                }
            }

            return updatedCount;
        }

        // Yeni headcount oluşturma fonksiyonu
        private async Task<int> CreateHeadCountAsync(BulkUpdateHeadCountCommandRequest request, int remainingCount)
        {
            // Var olan headcount'ları HCNumber'a göre sırala
            var existingHeadCounts = (await _headCountRepository.GetAllAsync(hc => hc.ProjectId == request.ProjectId))
                .OrderBy(hc => hc.HCNumber).ToList();

            var store = await _storeRepository.GetByProjectIdAsync(request.ProjectId);  // Fetch the store by project ID

            int maxHCNumber = existingHeadCounts.Any() ? existingHeadCounts.Max(hc => hc.HCNumber) : 0;
            int createdCount = 0;

            for (int i = 0; i < remainingCount; i++)
            {
                var newHeadCount = new HeadCount
                {
                    ProjectId = request.ProjectId,
                    FunctionalAreaId = request.FunctionalAreaId,
                    SectionId = request.SectionId,
                    SubSectionId = request.SubSectionId,
                    PositionId = request.PositionId,
                    IsVacant = true,
                    HCNumber = maxHCNumber + i + 1 // Increment HCNumber
                };

                // Compare with the store's HeadCountNumber
                if (newHeadCount.HCNumber > store.HeadCountNumber)
                {
                    // Set the background color to yellow (make sure you have a yellow color ID)
                    newHeadCount.ColorId = await _colorRepository.GetYellowColorIdAsync(); // Implement GetYellowColorIdAsync() to return the ID of yellow color
                }

                await _headCountRepository.AddAsync(newHeadCount);
                createdCount++;
            }

            await _headCountRepository.CommitAsync();
            return createdCount;
        }
    }
}
