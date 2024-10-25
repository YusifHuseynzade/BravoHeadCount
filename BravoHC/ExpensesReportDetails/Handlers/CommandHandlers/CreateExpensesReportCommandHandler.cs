using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using ExpensesReportDetails.Commands.Request;
using ExpensesReportDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ExpensesReportDetails.Handlers.CommandHandlers
{
    public class CreateExpensesReportCommandHandler : IRequestHandler<CreateExpensesReportCommandRequest, CreateExpensesReportCommandResponse>
    {
        private readonly IExpensesReportRepository _repository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<FileSettings> _settings;
        private readonly IHostEnvironment _env;

        public CreateExpensesReportCommandHandler(
            IExpensesReportRepository repository,
            IAttachmentRepository attachmentRepository,
            IHttpContextAccessor httpContextAccessor,
            IOptions<FileSettings> settings,
            IHostEnvironment env)
        {
            _repository = repository;
            _attachmentRepository = attachmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _settings = settings;
            _env = env;
        }

        public async Task<CreateExpensesReportCommandResponse> Handle(CreateExpensesReportCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateExpensesReportCommandResponse();

            try
            {
                // Kullanıcı bilgilerini almak
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

                // Yeni ExpensesReport nesnesi oluşturma
                var expensesReport = new ExpensesReport
                {
                    Name = request.Name,
                    ProjectId = request.ProjectId,
                    Date = request.Date.ToUniversalTime(),
                    UtilityElectricity = request.UtilityElectricity,
                    UtilityWater = request.UtilityWater,
                    RepairExpenses = request.RepairExpenses,
                    TransportationExpenses = request.TransportationExpenses,
                    CleaningExpenses = request.CleaningExpenses,
                    StationeryExpenses = request.StationeryExpenses,
                    PrintingExpenses = request.PrintingExpenses,
                    OperationExpenses = request.OperationExpenses,
                    BalanceEndMonth = request.BalanceEndMonth,
                    Other = request.Other,
                    TotalExpenses = request.TotalExpenses,
                    Comment = request.Comment,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = fullName,
                    Attachments = new List<Attachment>()
                };

                // Veritabanına ekleme
                await _repository.AddAsync(expensesReport);
                await _repository.CommitAsync();

                // Dosyaları yükleme ve ek olarak ekleme
                foreach (var file in request.AttachmentFiles)
                {
                    string filePath = await file.SaveAsync(_settings.Value.Path, _settings.Value.ExpensesReport);

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

                await _attachmentRepository.CommitAsync();

                response.IsSuccess = true;
                response.ErrorMessage = "Expenses report successfully created.";
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
