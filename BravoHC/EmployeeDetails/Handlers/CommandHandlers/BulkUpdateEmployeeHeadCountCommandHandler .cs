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

            // Genel bir try-catch bloğu eklemek yerine spesifik işlemleri yönetmek için her employee işleminde try-catch kullanacağız.
            foreach (var employeeId in request.EmployeeIds)
            {
                try
                {
                    var employee = await _employeeRepository.GetAsync(e => e.Id == employeeId);
                    if (employee == null)
                    {
                        // Eğer employee bulunamazsa bu employee'yi atla ve bir sonrakine geç
                        failedEmployeeIds.Add(employeeId);
                        continue;
                    }

                    // Store'u alırken try-catch kullanacağız.
                    try
                    {
                        // Employee'nin ProjectId'sine göre store bilgisini al
                        var store = await _storeRepository.GetByProjectIdAsync(employee.ProjectId);

                        // Eğer store bulunamazsa, işlemi atla ve diğer employee'ye geç
                        if (store == null)
                        {
                            // Store bulunamadı, bu employee'yi atlayarak bir sonrakine geç
                            Console.WriteLine($"Store bulunamadı, employeeId: {employeeId} atlanıyor.");
                            failedEmployeeIds.Add(employeeId);
                            continue;
                        }

                        // Employee'ye uygun vacant olan headcount'ları al
                        var availableHeadCounts = await _headCountRepository.GetAllAsync(hc =>
                            hc.ProjectId == employee.ProjectId &&
                            hc.SectionId == employee.SectionId &&
                            hc.PositionId == employee.PositionId &&
                            hc.SubSectionId == employee.SubSectionId &&
                            hc.IsVacant == true);

                        var sortedHeadCounts = availableHeadCounts.OrderBy(hc => hc.HCNumber).ToList();

                        if (sortedHeadCounts.Any())
                        {
                            // Mevcut headcount'u güncelle
                            var headCount = sortedHeadCounts.First();
                            headCount.EmployeeId = employeeId;
                            headCount.IsVacant = false;

                            await _headCountRepository.UpdateAsync(headCount);
                            updatedCount++;
                        }
                        else
                        {
                            // Yeni headcount oluştur
                            createdCount += await CreateNewHeadCountAsync(employee, store);
                        }
                    }
                    catch (Exception storeEx)
                    {
                        // Eğer store ile ilgili bir sorun olursa, log yaz ve employee'yi atla
                        Console.WriteLine($"Store bulunurken hata oluştu, employeeId: {employeeId}. Hata: {storeEx.Message}");
                        failedEmployeeIds.Add(employeeId);
                        continue;
                    }
                }
                catch (Exception empEx)
                {
                    // Eğer employee ile ilgili bir sorun olursa, log yaz ve employee'yi atla
                    Console.WriteLine($"Employee bulunurken hata oluştu, employeeId: {employeeId}. Hata: {empEx.Message}");
                    failedEmployeeIds.Add(employeeId);
                    continue;
                }
            }

            // İşlem sonunda CommitAsync çağırarak tüm işlemleri kaydet
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
            // Mevcut HCNumber'ları al
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
                HCNumber = maxHCNumber + 1 // Yeni HCNumber belirle
            };

            // Eğer yeni HCNumber store'daki HeadCountNumber'dan büyükse, rengi sarı yap
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
