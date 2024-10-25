using ExpensesReportDetails.Commands.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ExpensesReportDetails.Commands.Request
{
    public class CreateExpensesReportCommandRequest : IRequest<CreateExpensesReportCommandResponse>
    {
        public string? Name { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public float UtilityElectricity { get; set; }
        public float UtilityWater { get; set; }
        public float RepairExpenses { get; set; }
        public float TransportationExpenses { get; set; }
        public float CleaningExpenses { get; set; }
        public float StationeryExpenses { get; set; }
        public float PrintingExpenses { get; set; }
        public float OperationExpenses { get; set; }
        public float BalanceEndMonth { get; set; }
        public float Other { get; set; }
        public float TotalExpenses { get; set; }
        public string? Comment { get; set; }
        public List<IFormFile>? AttachmentFiles { get; set; } = new(); // Yüklenecek dosyalar
    }
}
