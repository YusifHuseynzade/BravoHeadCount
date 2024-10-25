using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TrolleyTypeDetails.Commands.Request;
using TrolleyTypeDetails.Commands.Response;

namespace TrolleyTypeDetails.Handlers.CommandHandlers
{
    public class CreateTrolleyTypeCommandHandler : IRequestHandler<CreateTrolleyTypeCommandRequest, CreateTrolleyTypeCommandResponse>
    {
        private readonly ITrolleyTypeRepository _trolleyTypeRepository;
        private readonly IOptions<FileSettings> _fileSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTrolleyTypeCommandHandler(
            ITrolleyTypeRepository trolleyTypeRepository,
            IOptions<FileSettings> fileSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _trolleyTypeRepository = trolleyTypeRepository;
            _fileSettings = fileSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateTrolleyTypeCommandResponse> Handle(CreateTrolleyTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateTrolleyTypeCommandResponse();

            try
            {
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
                // Dosya yolunu hazırlıyoruz
                var imagePath = await request.Image.SaveAsync(
                    _fileSettings.Value.Path,
                    _fileSettings.Value.TrolleyType
                );

                // Yeni TrolleyType nesnesini oluşturuyoruz
                var trolleyType = new TrolleyType
                {
                    Name = request.Name,
                    Image = imagePath,
                    CreatedBy = fullName,
                    CreatedDate = DateTime.UtcNow,

                };

                // Veritabanına ekliyoruz
                await _trolleyTypeRepository.AddAsync(trolleyType);
                await _trolleyTypeRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Trolley type created successfully.";
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
