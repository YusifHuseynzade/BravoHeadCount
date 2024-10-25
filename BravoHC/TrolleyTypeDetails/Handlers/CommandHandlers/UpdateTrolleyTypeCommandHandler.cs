using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrolleyTypeDetails.Commands.Request;
using TrolleyTypeDetails.Commands.Response;
using Core.Helpers;
using Microsoft.Extensions.Options;

namespace TrolleyTypeDetails.Handlers.CommandHandlers
{
    public class UpdateTrolleyTypeCommandHandler : IRequestHandler<UpdateTrolleyTypeCommandRequest, UpdateTrolleyTypeCommandResponse>
    {
        private readonly ITrolleyTypeRepository _trolleyTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<FileSettings> _fileSettings;

        public UpdateTrolleyTypeCommandHandler(
            ITrolleyTypeRepository trolleyTypeRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<FileSettings> fileSettings)
        {
            _trolleyTypeRepository = trolleyTypeRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileSettings = fileSettings;
        }

        public async Task<UpdateTrolleyTypeCommandResponse> Handle(UpdateTrolleyTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateTrolleyTypeCommandResponse();

            try
            {
                // Mevcut TrolleyType nesnesini bul
                var trolleyType = await _trolleyTypeRepository.GetAsync(
                    x => x.Id == request.Id, nameof(TrolleyType.Trolleys));

                if (trolleyType == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Trolley type not found.";
                    return response;
                }

                // Kullanıcı bilgilerini al
                var fullName = _httpContextAccessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == "FullName")?.Value;

                if (string.IsNullOrEmpty(fullName))
                {
                    response.IsSuccess = false;
                    response.Message = "User information is not available.";
                    return response;
                }

                // TrolleyType bilgilerini güncelle
                if (!string.IsNullOrEmpty(request.Name))
                    trolleyType.Name = request.Name;

                // Yeni görsel dosyası yüklenecekse
                if (request.NewImage != null)
                {
                    // Eski görseli sil
                    if (!string.IsNullOrEmpty(trolleyType.Image))
                    {
                        IFormFileExtensions.Delete(_fileSettings.Value.Path, _fileSettings.Value.TrolleyType, trolleyType.Image);
                    }

                    // Yeni görseli kaydet
                    var newImagePath = await request.NewImage.SaveAsync(_fileSettings.Value.Path, _fileSettings.Value.TrolleyType);
                    trolleyType.Image = newImagePath;
                }

                trolleyType.ModifiedDate = DateTime.UtcNow;
                trolleyType.ModifiedBy = fullName;

                // Veritabanında güncelle
                await _trolleyTypeRepository.UpdateAsync(trolleyType);
                await _trolleyTypeRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Trolley type updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
