using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using SettingFinanceOperationDetails.Commands.Request;
using SettingFinanceOperationDetails.Commands.Response;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SettingFinanceOperationDetails.Handlers.CommandHandlers;

public class CreateSettingFinanceOperationCommandHandler : IRequestHandler<CreateSettingFinanceOperationCommandRequest, CreateSettingFinanceOperationCommandResponse>
{
    private readonly ISettingFinanceOperationRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateSettingFinanceOperationCommandHandler(
        ISettingFinanceOperationRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CreateSettingFinanceOperationCommandResponse> Handle(CreateSettingFinanceOperationCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CreateSettingFinanceOperationCommandResponse();

        try
        {
            // Retrieve user information from HttpContext
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

            // Create new SettingFinanceOperation entity
            var newSetting = new SettingFinanceOperation
            {
                Name = request.Name,
                EncashmentDays = request.EncashmentDays,
                DateEncashment = request.DateEncashment.ToUniversalTime(),
                IsActiveEncashment = request.IsActiveEncashment,
                FrequencyEncashment = request.FrequencyEncashment,
                EncashmentRecipient = request.EncashmentRecipient,
                ProjectId = request.ProjectId,
                MoneyOrderDays = request.MoneyOrderDays,
                DateMoneyOrder = request.DateMoneyOrder.ToUniversalTime(),
                IsActiveMoneyOrder = request.IsActiveMoneyOrder,
                FrequencyMoneyOrder = request.FrequencyMoneyOrder,
                MoneyOrderRecipient = request.MoneyOrderRecipient,
                BranchId = request.BranchId,
                CreatedBy = fullName,
                CreatedDate = DateTime.UtcNow
            };

            // Save the new entity to the database
            await _repository.AddAsync(newSetting);
            await _repository.CommitAsync();

            response.IsSuccess = true;
            response.Message = "SettingFinanceOperation created successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }
}
