using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using EncashmentDetails.Commands.Request;
using EncashmentDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EncashmentDetails.Handlers.CommandHandlers
{
    public class UpdateEncashmentCommandHandler : IRequestHandler<UpdateEncashmentCommandRequest, UpdateEncashmentCommandResponse>
    {
        private readonly IEncashmentRepository _encashmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IOptions<FileSettings> _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEncashmentHistoryRepository _historyRepository;

        public UpdateEncashmentCommandHandler(
            IEncashmentRepository encashmentRepository,
            IAttachmentRepository attachmentRepository,
            IOptions<FileSettings> settings,
            IHttpContextAccessor httpContextAccessor,
            IEncashmentHistoryRepository historyRepository)
        {
            _encashmentRepository = encashmentRepository;
            _attachmentRepository = attachmentRepository;
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
            _historyRepository = historyRepository;
        }

        public async Task<UpdateEncashmentCommandResponse> Handle(UpdateEncashmentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateEncashmentCommandResponse();

            try
            {
                // Kullanıcı bilgilerini HttpContext üzerinden al
                var fullName = _httpContextAccessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == "FullName")?.Value;

                var userIdString = _httpContextAccessor.HttpContext?.User?.Claims
                   .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(userIdString))
                {
                    response.IsSuccess = false;
                    response.Message = "User information is not available.";
                    return response;
                }

                // UserId'yi integer'a dönüştür
                if (!int.TryParse(userIdString, out int userId))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid user ID.";
                    return response;
                }

                // Güncellenecek Encashment nesnesini al
                var encashment = _encashmentRepository.Get(e => e.Id == request.Id, "Attachments");
                if (encashment == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Encashment with ID {request.Id} not found.";
                    return response;
                }


                // Encashment'ı güncelle
                encashment.SetDetails(
                    request.Name,
                    request.AmountFromSales,
                    request.AmountFoundOnSite,
                    request.SafeSurplus,
                    DateTime.UtcNow,  // Güncellenme tarihi
                    request.SealNumber,
                    fullName // Güncellemeyi yapan kullanıcı
                );

                // Yeni dosyaları ekle
                foreach (var file in request.NewAttachmentFiles)
                {
                    string filePath = await file.SaveAsync(_settings.Value.Path, _settings.Value.Encashment);

                    var newAttachment = new Attachment
                    {
                        FileUrl = filePath,
                        EncashmentId = encashment.Id,
                        UploadedDate = DateTime.UtcNow,  // UTC kullanımı
                        UploadedBy = fullName
                    };

                    encashment.Attachments.Add(newAttachment);
                    await _attachmentRepository.AddAsync(newAttachment);
                }

                // Silinecek dosyaları kaldır
                foreach (var attachmentId in request.DeleteAttachmentFileId)
                {
                    var attachmentToDelete = encashment.Attachments.FirstOrDefault(a => a.Id == attachmentId);
                    if (attachmentToDelete != null)
                    {
                        _attachmentRepository.Remove(attachmentToDelete);
                        var fileName = attachmentToDelete.FileUrl.Split('/').Last();
                        IFormFileExtensions.Delete(_settings.Value.Path, _settings.Value.Encashment, fileName);
                    }
                }


                // Değişiklikleri kaydet
                await _encashmentRepository.UpdateAsync(encashment);
                await _encashmentRepository.CommitAsync();

                // Create a history entry before updating
                var history = new EncashmentHistory
                {
                    EncashmentId = encashment.Id,
                    Name = encashment.Name,
                    AmountFromSales = encashment.AmountFromSales,
                    AmountFoundOnSite = encashment.AmountFoundOnSite,
                    SafeSurplus = encashment.SafeSurplus,
                    TotalAmount = encashment.TotalAmount,
                    SealNumber = encashment.SealNumber,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = fullName
                };
                await _historyRepository.AddAsync(history);
                await _historyRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Encashment updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while updating encashment: {ex.Message}";
            }

            return response;
        }
    }
}
