using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Commands.Response;
using System.Security.Claims;

namespace MoneyOrderDetails.Handlers.CommandHandlers;

public class UpdateMoneyOrderCommandHandler : IRequestHandler<UpdateMoneyOrderCommandRequest, UpdateMoneyOrderCommandResponse>
{
    private readonly IMoneyOrderRepository _moneyOrderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateMoneyOrderCommandHandler(
        IMoneyOrderRepository moneyOrderRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _moneyOrderRepository = moneyOrderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateMoneyOrderCommandResponse> Handle(UpdateMoneyOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateMoneyOrderCommandResponse();

        try
        {
            // Mevcut MoneyOrder nesnesini bul
            var moneyOrder = await _moneyOrderRepository.GetAsync(
                x => x.Id == request.Id,
                nameof(MoneyOrder.Project),
                nameof(MoneyOrder.Branch)
            );

            if (moneyOrder == null)
            {
                response.IsSuccess = false;
                response.Message = "Money order not found.";
                return response;
            }

            // Kullanıcı bilgilerini al
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

            if (!int.TryParse(userIdString, out int userId))
            {
                response.IsSuccess = false;
                response.Message = "Invalid user ID.";
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

            // Güncelleme işlemi
            moneyOrder.SetDetails(
                request.HundredAZN,
                request.FiftyAZN,
                request.TwentyAZN,
                request.TenAZN,
                request.FiveAZN,
                request.OneAZN,
                request.FiftyQapik,
                request.TwentyQapik,
                request.TenQapik,
                request.FiveQapik,
                request.ThreeQapik,
                request.OneQapik,
                request.TotalQuantity,
                request.Name,
                fullName
            );

            moneyOrder.TotalAmount = totalAmount;
            moneyOrder.ModifiedDate = DateTime.UtcNow;

            // Veritabanında değişiklikleri kaydet
            await _moneyOrderRepository.UpdateAsync(moneyOrder);
            await _moneyOrderRepository.CommitAsync();

            response.IsSuccess = true;
            response.Message = "Money order updated successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }
}
