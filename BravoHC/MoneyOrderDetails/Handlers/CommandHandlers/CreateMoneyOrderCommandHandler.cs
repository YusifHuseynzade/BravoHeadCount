using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Commands.Response;
using System.Security.Claims;

namespace MoneyOrderDetails.Handlers.CommandHandlers;

public class CreateMoneyOrderCommandHandler : IRequestHandler<CreateMoneyOrderCommandRequest, CreateMoneyOrderCommandResponse>
{
    private readonly IMoneyOrderRepository _moneyOrderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateMoneyOrderCommandHandler(
        IMoneyOrderRepository moneyOrderRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _moneyOrderRepository = moneyOrderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateMoneyOrderCommandResponse> Handle(CreateMoneyOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateMoneyOrderCommandResponse();

        try
        {
            // Kullanıcı bilgilerini al
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

            // TotalAmount hesapla
            int totalAmount =
                request.HundredAZN * 100 +
                (int)(request.FiftyAZN * 50) +
                (int)(request.TwentyAZN * 20) +
                (int)(request.TenAZN * 10) +
                (int)(request.FiveAZN * 5) +
                (int)(request.OneAZN * 1) +
                (int)(request.FiftyQapik * 0.5f) +
                (int)(request.TwentyQapik * 0.2f) +
                (int)(request.TenQapik * 0.1f) +
                (int)(request.FiveQapik * 0.05f) +
                (int)(request.ThreeQapik * 0.03f) +
                (int)(request.OneQapik * 0.01f);

            // Yeni MoneyOrder nesnesini oluştur
            var moneyOrder = new MoneyOrder
            {
                ProjectId = request.ProjectId,
                BranchId = request.BranchId,
                HundredAZN = request.HundredAZN,
                FiftyAZN = request.FiftyAZN,
                TwentyAZN = request.TwentyAZN,
                TenAZN = request.TenAZN,
                FiveAZN = request.FiveAZN,
                OneAZN = request.OneAZN,
                FiftyQapik = request.FiftyQapik,
                TwentyQapik = request.TwentyQapik,
                TenQapik = request.TenQapik,
                FiveQapik = request.FiveQapik,
                ThreeQapik = request.ThreeQapik,
                OneQapik = request.OneQapik,
                TotalQuantity = request.TotalQuantity,
                TotalAmount = totalAmount,
                Name = request.Name,
                SealNumber = request.SealNumber,
                CreatedBy = fullName,
                CreatedDate = DateTime.UtcNow
            };

            // Veritabanına ekle
            await _moneyOrderRepository.AddAsync(moneyOrder);
            await _moneyOrderRepository.CommitAsync();

            response.IsSuccess = true;
            response.ErrorMessage = "Money order created successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"An error occurred: {ex.Message}";
        }

        return response;
    }
}
