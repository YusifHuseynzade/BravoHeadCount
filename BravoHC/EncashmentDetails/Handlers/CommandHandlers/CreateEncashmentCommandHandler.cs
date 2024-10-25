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
    public class CreateEncashmentCommandHandler : IRequestHandler<CreateEncashmentCommandRequest, CreateEncashmentCommandResponse>
    {
        private readonly IEncashmentRepository _encashmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IHostEnvironment _env;
        private readonly IOptions<FileSettings> _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateEncashmentCommandHandler(
            IEncashmentRepository encashmentRepository,
            IAttachmentRepository attachmentRepository,
            IHostEnvironment env,
            IOptions<FileSettings> settings,
            IHttpContextAccessor httpContextAccessor)
        {
            _encashmentRepository = encashmentRepository;
            _attachmentRepository = attachmentRepository;
            _env = env;
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateEncashmentCommandResponse> Handle(CreateEncashmentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateEncashmentCommandResponse();

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
                    response.ErrorMessage = "User information is not available.";
                    return response;
                }

                // UserId'yi integer'a dönüştür
                if (!int.TryParse(userIdString, out int userId))
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Invalid user ID.";
                    return response;
                }

                // Yeni Encashment nesnesi oluştur
                var encashment = new Encashment
                {
                    Name = request.Name,
                    ProjectId = request.ProjectId,
                    AmountFromSales = request.AmountFromSales,
                    AmountFoundOnSite = request.AmountFoundOnSite,
                    SafeSurplus = request.SafeSurplus,
                    TotalAmount = request.AmountFromSales + request.AmountFoundOnSite + request.SafeSurplus,
                    CreatedDate = DateTime.UtcNow,  // UTC kullanımı
                    CreatedBy = fullName,  // Kullanıcının tam adını atıyoruz
                    BranchId = request.BranchId,
                    SealNumber = request.SealNumber,
                    Attachments = new List<Attachment>()
                };

                // Encashment'ı veritabanına ekle
                await _encashmentRepository.AddAsync(encashment);
                await _encashmentRepository.CommitAsync();

                // Dosyaları yükle ve ek olarak ekle
                foreach (var file in request.AttachmentFiles)
                {
                    string filePath = await file.SaveAsync(_settings.Value.Path, _settings.Value.Encashment);

                    var attachment = new Attachment
                    {
                        FileUrl = filePath,
                        EncashmentId = encashment.Id,
                        UploadedDate = DateTime.UtcNow,  // UTC kullan
                        UploadedBy = fullName  // UploadedBy'ye kullanıcı adı atanıyor
                    };

                    encashment.Attachments.Add(attachment);
                    await _attachmentRepository.AddAsync(attachment);
                }

                // Değişiklikleri kaydet
                await _attachmentRepository.CommitAsync();

                response.IsSuccess = true;
                response.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"An error occurred while creating encashment: {ex.ToString()}";
            }

            return response;
        }
    }
}
