using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using EndOfMonthReportDetails.Commands.Request;
using EndOfMonthReportDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EndOfMonthReportDetails.Handlers.CommandHandlers;

public class UpdateEndOfMonthReportCommandHandler : IRequestHandler<UpdateEndOfMonthReportCommandRequest, UpdateEndOfMonthReportCommandResponse>
{
    private readonly IEndOfMonthReportRepository _endOfMonthReportRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEndOfMonthReportHistoryRepository _historyRepository;

    public UpdateEndOfMonthReportCommandHandler(
        IEndOfMonthReportRepository endOfMonthReportRepository,
        IHttpContextAccessor httpContextAccessor,
        IEndOfMonthReportHistoryRepository historyRepository)
    {
        _endOfMonthReportRepository = endOfMonthReportRepository;
        _httpContextAccessor = httpContextAccessor;
        _historyRepository = historyRepository;
    }

    public async Task<UpdateEndOfMonthReportCommandResponse> Handle(UpdateEndOfMonthReportCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateEndOfMonthReportCommandResponse();

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

            // Güncellenecek raporu bul
            var report = await _endOfMonthReportRepository.GetAsync(x => x.Id == request.Id);

            if (report == null)
            {
                response.IsSuccess = false;
                response.Message = "End of month report not found.";
                return response;
            }


            // Raporun detaylarını güncelle
            report.SetDetails(
                request.EncashmentAmount,
                request.DepositAmount,
                request.EncashmentAmount + request.DepositAmount,
                request.Name,
                fullName
            );

            report.ModifiedBy = fullName;
            report.ModifiedDate = DateTime.UtcNow;

            // Veritabanında değişiklikleri kaydet
            await _endOfMonthReportRepository.UpdateAsync(report);
            await _endOfMonthReportRepository.CommitAsync();

            // Log history only after a successful update
            var history = new EndOfMonthReportHistory
            {
                EndOfMonthReportId = report.Id,
                EncashmentAmount = report.EncashmentAmount,
                DepositAmount = report.DepositAmount,
                TotalAmount = report.TotalAmount,
                Name = report.Name,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = fullName
            };

            await _historyRepository.AddAsync(history);
            await _historyRepository.CommitAsync();

            response.IsSuccess = true;
            response.Message = "End of month report updated successfully.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"An error occurred: {ex.Message}";
        }

        return response;
    }
}
