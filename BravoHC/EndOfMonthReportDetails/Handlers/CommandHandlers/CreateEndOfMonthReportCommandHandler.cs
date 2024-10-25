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

namespace EndOfMonthReportDetails.Handlers.CommandHandlers
{
    public class CreateEndOfMonthReportCommandHandler : IRequestHandler<CreateEndOfMonthReportCommandRequest, CreateEndOfMonthReportCommandResponse>
    {
        private readonly IEndOfMonthReportRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateEndOfMonthReportCommandHandler(
            IEndOfMonthReportRepository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateEndOfMonthReportCommandResponse> Handle(CreateEndOfMonthReportCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateEndOfMonthReportCommandResponse();

            try
            {
                // Kullanıcı bilgilerini çekiyoruz.
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

                // Yeni EndOfMonthReport oluşturuluyor.
                var report = new EndOfMonthReport
                {
                    ProjectId = request.ProjectId,
                    Name = request.Name,
                    EncashmentAmount = request.EncashmentAmount,
                    DepositAmount = request.DepositAmount,
                    TotalAmount = request.EncashmentAmount + request.DepositAmount,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = fullName
                };

                // Veritabanına ekleme işlemi.
                await _repository.AddAsync(report);
                await _repository.CommitAsync();

                response.IsSuccess = true;
                response.ErrorMessage = "End of month report created successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
