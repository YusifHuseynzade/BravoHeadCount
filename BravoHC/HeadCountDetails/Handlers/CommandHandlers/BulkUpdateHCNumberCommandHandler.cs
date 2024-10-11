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
    public class BulkUpdateHCNumberCommandHandler : IRequestHandler<BulkUpdateHCNumberCommandRequest, BulkUpdateHCNumberCommandResponse>
    {
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IHeadCountBackgroundColorRepository _colorRepository;

        public BulkUpdateHCNumberCommandHandler(IHeadCountRepository headCountRepository, IStoreRepository storeRepository, IHeadCountBackgroundColorRepository colorRepository)
        {
            _headCountRepository = headCountRepository;
            _storeRepository = storeRepository;
            _colorRepository = colorRepository;
        }

        public async Task<BulkUpdateHCNumberCommandResponse> Handle(BulkUpdateHCNumberCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Store bilgilerini ProjectId'ye göre al
                var store = await _storeRepository.GetByProjectIdAsync(request.ProjectId);

                // Bu HeadCount'un ProjectId'sini kullanarak, o proje içindeki tüm HeadCount'ları alalım
                var headCounts = (await _headCountRepository.GetAllAsync(h => h.ProjectId == request.ProjectId)).OrderBy(h => h.HCNumber).ToList();

                // Her bir güncelleme isteğini işle
                foreach (var updateRequest in request.UpdatedHeadCounts)
                {
                    // İlk olarak güncellenen headcount'u bulalım
                    var targetHeadCount = await _headCountRepository.GetAsync(h => h.Id == updateRequest.Id && h.ProjectId == request.ProjectId);

                    if (targetHeadCount == null)
                    {
                        return new BulkUpdateHCNumberCommandResponse
                        {
                            IsSuccess = false,
                            Message = $"Target headcount with Id {updateRequest.Id} not found."
                        };
                    }

                    // Yeni HCNumber değeri
                    var newHCNumber = updateRequest.HCNumber;

                    if (newHCNumber <= store.HeadCountNumber)
                    {
                        // Mevcut rengin sarı olup olmadığını kontrol et
                        var yellowColorId = await _colorRepository.GetYellowColorIdAsync();
                        if (targetHeadCount.ColorId == yellowColorId)
                        {
                            // Beyaz rengin ID'sini al
                            var whiteColorId = await _colorRepository.GetWhiteColorIdAsync();

                            // ColorId'yi beyaz olarak ayarla
                            targetHeadCount.ColorId = whiteColorId;
                        }
                    }
                    else
                    {
                        // Eğer HCNumber store.HeadCountNumber'dan büyükse, color'u sarı yap
                        var yellowColorId = await _colorRepository.GetYellowColorIdAsync(); // Implement GetYellowColorIdAsync() method

                        // ColorId'yi sarı olarak ayarla
                        targetHeadCount.ColorId = yellowColorId;
                    }

                    // Hedef headcount'u listeden çıkarıp, yeni HCNumber sırasına ekleyelim
                    headCounts.Remove(targetHeadCount);
                    headCounts.Insert(newHCNumber - 1, targetHeadCount);

                    // Listede bulunan tüm HeadCount'ların HCNumber değerlerini küçükten büyüğe sıralayalım
                    for (int i = 0; i < headCounts.Count; i++)
                    {
                        // HCNumber değerini güncelle
                        headCounts[i].HCNumber = i + 1;

                        // Eğer HCNumber store'daki HeadCountNumber'dan büyükse sarı renge ayarla
                        if (headCounts[i].HCNumber > store.HeadCountNumber)
                        {
                            // Sarı rengin ID'sini al
                            var yellowColorId = await _colorRepository.GetYellowColorIdAsync();

                            // ColorId'yi sarı renge ayarla
                            headCounts[i].ColorId = yellowColorId;
                        }
                        // Eğer HCNumber store'daki HeadCountNumber'dan küçük veya eşitse beyaz renge ayarla
                        else
                        {
                            // Eğer mevcut renk sarı ise beyaza ayarla, aksi halde değişmeden bırak
                            var yellowColorId = await _colorRepository.GetYellowColorIdAsync();
                            if (headCounts[i].ColorId == yellowColorId)
                            {
                                // Beyaz rengin ID'sini al
                                var whiteColorId = await _colorRepository.GetWhiteColorIdAsync();

                                // ColorId'yi beyaz renge ayarla
                                headCounts[i].ColorId = whiteColorId;
                            }
                        }

                        // HeadCount'u güncelle
                        await _headCountRepository.UpdateAsync(headCounts[i]);
                    }
                }

                // Veritabanı değişikliklerini commit edelim
                await _headCountRepository.CommitAsync();

                return new BulkUpdateHCNumberCommandResponse
                {
                    IsSuccess = true,
                    Message = "HeadCount order updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new BulkUpdateHCNumberCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error updating HeadCount order: {ex.Message}"
                };
            }
        }
    }
}
