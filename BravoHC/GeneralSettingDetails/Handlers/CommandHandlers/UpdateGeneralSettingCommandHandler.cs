using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using GeneralSettingDetails.Commands.Request;
using GeneralSettingDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralSettingDetails.Handlers.CommandHandlers
{
    public class UpdateGeneralSettingCommandHandler : IRequestHandler<UpdateGeneralSettingCommandRequest, UpdateGeneralSettingCommandResponse>
    {
        private readonly IGeneralSettingRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateGeneralSettingCommandHandler(
            IGeneralSettingRepository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateGeneralSettingCommandResponse> Handle(UpdateGeneralSettingCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateGeneralSettingCommandResponse();

            try
            {
                // Retrieve existing general setting
                var existingSetting = await _repository.GetSettingsAsync();
                if (existingSetting == null)
                {
                    response.IsSuccess = false;
                    response.Message = "General setting not found.";
                    return response;
                }

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

                // Update End of Month Report Settings
                existingSetting.EndOfMonthReportSettings.SendingTimes = request.EndOfMonthSendingTimes;
                existingSetting.EndOfMonthReportSettings.SendingFrequency = request.EndOfMonthSendingFrequency;
                existingSetting.EndOfMonthReportSettings.Receivers = request.EndOfMonthReceivers;
                existingSetting.EndOfMonthReportSettings.ReceiversCC = request.EndOfMonthReceiversCC;
                existingSetting.EndOfMonthReportSettings.AvailableCreatedDays = request.EndOfMonthAvailableCreatedDays;

                // Update Expense Report Settings
                existingSetting.ExpenseReportSettings.SendingTimes = request.ExpenseReportSendingTimes;
                existingSetting.ExpenseReportSettings.SendingFrequency = request.ExpenseReportSendingFrequency;
                existingSetting.ExpenseReportSettings.Receivers = request.ExpenseReportReceivers;
                existingSetting.ExpenseReportSettings.ReceiversCC = request.ExpenseReportReceiversCC;
                existingSetting.ExpenseReportSettings.AvailableCreatedDays = request.ExpenseReportAvailableCreatedDays;

                // Update tracking fields
                existingSetting.ModifiedBy = fullName;
                existingSetting.ModifiedDate = DateTime.UtcNow;

                // Save changes to the database
                await _repository.UpdateAsync(existingSetting);
                await _repository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "General setting updated successfully.";
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
