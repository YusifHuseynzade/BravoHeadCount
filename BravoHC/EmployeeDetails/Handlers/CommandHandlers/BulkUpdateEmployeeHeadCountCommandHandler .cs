using Domain.Entities;
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
        private readonly IStoreRepository _storeRepository;
        private readonly IHeadCountBackgroundColorRepository _colorRepository;

        public BulkUpdateEmployeeHeadCountCommandHandler(
            IHeadCountRepository headCountRepository,
            IEmployeeRepository employeeRepository,
            IStoreRepository storeRepository,
            IHeadCountBackgroundColorRepository colorRepository)
        {
            _headCountRepository = headCountRepository;
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
            _colorRepository = colorRepository;
        }

        public async Task<BulkUpdateEmployeeHeadCountCommandResponse> Handle(BulkUpdateEmployeeHeadCountCommandRequest request, CancellationToken cancellationToken)
        {
            var failedEmployeeIds = new List<int>();
            int updatedCount = 0;
            int createdCount = 0;
            var assignedEmployeeIds = new HashSet<int>();

            foreach (var employeeId in request.EmployeeIds)
            {
                try
                {
                    if (assignedEmployeeIds.Contains(employeeId))
                    {
                        continue;
                    }

                    var employee = await _employeeRepository.GetAsync(e => e.Id == employeeId);
                    if (employee == null)
                    {
                        failedEmployeeIds.Add(employeeId);
                        continue;
                    }

                    // Aynı proje için employee'nin daha önce headcount'a atanıp atanmadığını kontrol et
                    var alreadyAssignedHeadCount = await _headCountRepository.GetAsync(hc =>
                        hc.EmployeeId == employeeId && hc.ProjectId == employee.ProjectId);

                    if (alreadyAssignedHeadCount != null)
                    {
                        // Eğer employee zaten aynı projede bir headcount'a atanmışsa, failed listesine ekle ve devam et
                        Console.WriteLine($"EmployeeId {employeeId}, ProjeId {employee.ProjectId} için zaten atanmış.");
                        failedEmployeeIds.Add(employeeId);
                        continue;
                    }

                    try
                    {
                        // Store bilgisini al
                        var store = await _storeRepository.GetByProjectIdAsync(employee.ProjectId);

                        if (store == null)
                        {
                            failedEmployeeIds.Add(employeeId);
                            continue;
                        }

                        // Mevcut headcount'ları kontrol et
                        var existingHeadCounts = await _headCountRepository.GetAllAsync(hc =>
                            hc.ProjectId == employee.ProjectId &&
                            hc.IsVacant == true &&
                            hc.EmployeeId == null);

                        var sortedHeadCounts = existingHeadCounts.OrderBy(hc => hc.HCNumber).ToList();

                        // Headcount'taki eksik bilgileri doldur
                        foreach (var headCount in sortedHeadCounts)
                        {
                            if (headCount.SectionId == null)
                            {
                                headCount.SectionId = employee.SectionId; // Employee'nin section bilgisini ata
                            }

                            if (headCount.PositionId == null)
                            {
                                headCount.PositionId = employee.PositionId; // Employee'nin position bilgisini ata
                            }

                            if (headCount.SubSectionId == null)
                            {
                                headCount.SubSectionId = employee.SubSectionId; // Employee'nin sub-section bilgisini ata
                            }

                            headCount.EmployeeId = employeeId;
                            headCount.IsVacant = false;

                            await _headCountRepository.UpdateAsync(headCount);
                            updatedCount++;
                            assignedEmployeeIds.Add(employeeId);
                            break; // Employee'yi bir kere headcount'a atadıktan sonra çık
                        }

                        // Eğer uygun headcount yoksa yeni oluştur
                        if (!assignedEmployeeIds.Contains(employeeId))
                        {
                            createdCount += await CreateNewHeadCountAsync(employee, store);
                            assignedEmployeeIds.Add(employeeId);
                        }
                    }
                    catch (Exception storeEx)
                    {
                        Console.WriteLine($"Store bulunurken hata oluştu, employeeId: {employeeId}. Hata: {storeEx.Message}");
                        failedEmployeeIds.Add(employeeId);
                    }
                }
                catch (Exception empEx)
                {
                    Console.WriteLine($"Employee bulunurken hata oluştu, employeeId: {employeeId}. Hata: {empEx.Message}");
                    failedEmployeeIds.Add(employeeId);
                }
            }

            await _headCountRepository.CommitAsync();

            return new BulkUpdateEmployeeHeadCountCommandResponse
            {
                IsSuccess = true,
                Message = $"{updatedCount} headcount güncellendi, {createdCount} yeni headcount oluşturuldu. {failedEmployeeIds.Count} employee için işlem gerçekleştirilemedi.",
                UpdatedCount = updatedCount,
                CreatedCount = createdCount,
                FailedEmployeeIds = failedEmployeeIds
            };
        }

        private async Task<int> CreateNewHeadCountAsync(Employee employee, Store store)
        {
            var existingHeadCounts = await _headCountRepository.GetAllAsync(hc => hc.ProjectId == employee.ProjectId);
            int maxHCNumber = existingHeadCounts.Any() ? existingHeadCounts.Max(hc => hc.HCNumber) : 0;

            var newHeadCount = new HeadCount
            {
                ProjectId = employee.ProjectId,
                SectionId = employee.SectionId,
                SubSectionId = employee.SubSectionId,
                PositionId = employee.PositionId,
                EmployeeId = employee.Id,
                IsVacant = false,
                HCNumber = maxHCNumber + 1
            };

            if (newHeadCount.HCNumber > store.HeadCountNumber)
            {
                newHeadCount.ColorId = await _colorRepository.GetYellowColorIdAsync();
            }

            await _headCountRepository.AddAsync(newHeadCount);
            await _headCountRepository.CommitAsync();
            return 1;
        }

    }
}
