using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using ExpensesReportDetails.Commands.Request;
using ExpensesReportDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Handlers.CommandHandlers
{
    public class UpdateExpensesReportCommandHandler : IRequestHandler<UpdateExpensesReportCommandRequest, UpdateExpensesReportCommandResponse>
    {
        private readonly IExpensesReportRepository _expensesReportRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<FileSettings> _settings;

        public UpdateExpensesReportCommandHandler(
            IExpensesReportRepository expensesReportRepository,
            IAttachmentRepository attachmentRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<FileSettings> settings)
        {
            _expensesReportRepository = expensesReportRepository;
            _attachmentRepository = attachmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _settings = settings;
        }

        public async Task<UpdateExpensesReportCommandResponse> Handle(UpdateExpensesReportCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateExpensesReportCommandResponse();

            try
            {
                var expensesReport = await _expensesReportRepository.GetAsync(x => x.Id == request.Id, "Attachments");

                if (expensesReport == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Expenses report not found.";
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

                expensesReport.SetDetails(
                    request.Name,
                    request.Date,
                    request.UtilityElectricity,
                    request.UtilityWater,
                    request.RepairExpenses,
                    request.TransportationExpenses,
                    request.CleaningExpenses,
                    request.StationeryExpenses,
                    request.PrintingExpenses,
                    request.OperationExpenses,
                    request.Other,
                    request.TotalExpenses,
                    request.Comment,
                    request.BalanceEndMonth);

                expensesReport.ModifiedDate = DateTime.UtcNow;
                expensesReport.ModifiedBy = fullName;

                if (request.DeleteAttachmentFileIds.Any())
                {
                    foreach (var fileId in request.DeleteAttachmentFileIds)
                    {
                        var attachment = expensesReport.Attachments.FirstOrDefault(a => a.Id == fileId);
                        if (attachment != null)
                        {
                            _attachmentRepository.Remove(attachment);
                        }
                    }
                }

                if (request.NewAttachmentFiles != null && request.NewAttachmentFiles.Any())
                {
                    foreach (var file in request.NewAttachmentFiles)
                    {
                        var filePath = await file.SaveAsync(_settings.Value.Path, _settings.Value.ExpensesReport);

                        var attachment = new Attachment
                        {
                            FileUrl = filePath,
                            ExpensesReportId = expensesReport.Id,
                            UploadedDate = DateTime.UtcNow,
                            UploadedBy = fullName
                        };

                        expensesReport.Attachments.Add(attachment);
                        await _attachmentRepository.AddAsync(attachment);
                    }
                }

                await _expensesReportRepository.UpdateAsync(expensesReport);
                await _expensesReportRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Expenses report updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while updating the expenses report: {ex.Message}";
            }

            return response;
        }
    }
}
