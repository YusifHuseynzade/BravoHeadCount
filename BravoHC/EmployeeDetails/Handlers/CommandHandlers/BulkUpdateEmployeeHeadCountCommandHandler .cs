using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class BulkUpdateEmployeeHeadCountCommandHandler : IRequestHandler<BulkUpdateEmployeeHeadCountCommandRequest, BulkUpdateEmployeeHeadCountCommandResponse>
    {
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BulkUpdateEmployeeHeadCountCommandHandler(IHeadCountRepository headCountRepository, IEmployeeRepository employeeRepository)
        {
            _headCountRepository = headCountRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<BulkUpdateEmployeeHeadCountCommandResponse> Handle(BulkUpdateEmployeeHeadCountCommandRequest request, CancellationToken cancellationToken)
        {
            var failedEmployeeIds = new List<int>();
            int updatedCount = 0;

            try
            {
                // Her employee için headcount güncelle
                foreach (var employeeId in request.EmployeeIds)
                {
                    // İlgili employee'yi al
                    var employee = await _employeeRepository.GetAsync(e => e.Id == employeeId);
                    if (employee == null)
                    {
                        failedEmployeeIds.Add(employeeId);
                        continue;
                    }

                    // Employee'nin daha önce headcount'a eklenip eklenmediğini kontrol et
                    var existingHeadCount = await _headCountRepository.FirstOrDefaultAsync(hc => hc.EmployeeId == employeeId);
                    if (existingHeadCount != null)
                    {
                        // Employee zaten bir headcount'a eklenmişse işlem başarısız oldu
                        failedEmployeeIds.Add(employeeId);
                        continue;
                    }

                    // HeadCount'ta uygun kaydı bul ve HCNumber'a göre sırala (vacant olan)
                    var availableHeadCounts = await _headCountRepository.GetAllAsync(hc =>
                        hc.ProjectId == employee.ProjectId &&
                        hc.FunctionalAreaId == employee.FunctionalAreaId &&
                        hc.SectionId == employee.SectionId &&
                        hc.PositionId == employee.PositionId &&
                        hc.SubSectionId == employee.SubSectionId &&
                        hc.IsVacant == true);  // Yalnızca boş (vacant) olan headcount'lar

                    // Headcount'ları HCNumber'a göre küçükten büyüğe sırala
                    var sortedHeadCounts = availableHeadCounts.OrderBy(hc => hc.HCNumber).ToList();

                    if (sortedHeadCounts.Any())
                    {
                        // İlk uygun headcount'u al ve employee'yi ekle
                        var headCount = sortedHeadCounts.First();
                        headCount.EmployeeId = employeeId;
                        headCount.IsVacant = false;

                        await _headCountRepository.UpdateAsync(headCount);
                        updatedCount++;
                    }
                    else
                    {
                        // Eğer uygun bir headcount yoksa, bu employee için işlem başarısız oldu
                        failedEmployeeIds.Add(employeeId);
                    }
                }

                // Commit işlemi
                await _headCountRepository.CommitAsync();

                return new BulkUpdateEmployeeHeadCountCommandResponse
                {
                    IsSuccess = true,
                    Message = $"{updatedCount} headcount güncellendi. {failedEmployeeIds.Count} employee eşleşmedi.",
                    UpdatedCount = updatedCount,
                    FailedEmployeeIds = failedEmployeeIds
                };
            }
            catch (Exception ex)
            {
                return new BulkUpdateEmployeeHeadCountCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Headcount güncellenirken hata oluştu: {ex.Message}",
                    FailedEmployeeIds = failedEmployeeIds
                };
            }
        }
    }
}
