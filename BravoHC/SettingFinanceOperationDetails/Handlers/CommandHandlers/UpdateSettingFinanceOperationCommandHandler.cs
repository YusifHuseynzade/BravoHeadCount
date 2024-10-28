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

public class UpdateSettingFinanceOperationCommandHandler : IRequestHandler<UpdateSettingFinanceOperationCommandRequest, UpdateSettingFinanceOperationCommandResponse>
{
    private readonly ISettingFinanceOperationRepository _settingFinanceOperationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateSettingFinanceOperationCommandHandler(
        ISettingFinanceOperationRepository settingFinanceOperationRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _settingFinanceOperationRepository = settingFinanceOperationRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UpdateSettingFinanceOperationCommandResponse> Handle(UpdateSettingFinanceOperationCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateSettingFinanceOperationCommandResponse();

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

            // Retrieve the existing SettingFinanceOperation
            var setting = await _settingFinanceOperationRepository.GetAsync(x => x.Id == request.Id);

            if (setting == null)
            {
                response.IsSuccess = false;
                response.Message = "SettingFinanceOperation not found.";
                return response;
            }

            // Update the SettingFinanceOperation details
            setting.SetDetails(
                request.Name,
                request.EncashmentDays,
                request.DateEncashment.ToUniversalTime(),
                request.IsActiveEncashment,
                fullName,
                request.MoneyOrderDays,
                request.DateMoneyOrder.ToUniversalTime(),
                request.IsActiveMoneyOrder,
                request.FrequencyEncashment,
                request.FrequencyMoneyOrder,
                fullName
            );

            setting.ProjectId = request.ProjectId;
            setting.BranchId = request.BranchId;
            setting.ModifiedDate = DateTime.UtcNow;

            // Save the updated entity
            await _settingFinanceOperationRepository.UpdateAsync(setting);
            await _settingFinanceOperationRepository.CommitAsync();

            response.IsSuccess = true;
            response.Message = "SettingFinanceOperation updated successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }
}
