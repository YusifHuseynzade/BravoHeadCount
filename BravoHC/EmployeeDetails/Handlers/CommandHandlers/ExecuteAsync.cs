using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class EmployeeHeadCountService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHeadCountRepository _headCountRepository;
        private readonly IHeadCountBackgroundColorRepository _colorRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IHeadCountHistoryRepository _headCountHistoryRepository;

        public EmployeeHeadCountService(
            IServiceProvider services,
            IEmployeeRepository employeeRepository,
            IHeadCountRepository headCountRepository,
            IHeadCountBackgroundColorRepository colorRepository,
            IStoreRepository storeRepository,
            IHeadCountHistoryRepository headCountHistoryRepository)
        {
            _services = services;
            _employeeRepository = employeeRepository;
            _headCountRepository = headCountRepository;
            _colorRepository = colorRepository;
            _storeRepository = storeRepository;
            _headCountHistoryRepository = headCountHistoryRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // 2 dakikada bir işlem çalıştır
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var employees = await _employeeRepository.GetAllAsync();

                        foreach (var employee in employees)
                        {
                            // Mevcut headcount kontrolü
                            var oldHeadCount = await _headCountRepository.GetAsync(hc => hc.EmployeeId == employee.Id);
                            var oldProjectId = oldHeadCount?.ProjectId;

                            // Eğer headcount varsa ve employee'nin projesi değiştiyse, HeadCountHistory kaydı oluştur
                            if (oldHeadCount != null && oldProjectId != employee.ProjectId)
                            {
                                await AddHeadCountHistoryAsync(employee.Id, oldProjectId.Value, employee.ProjectId);
                            }

                            if (oldHeadCount == null || oldHeadCount.ProjectId != employee.ProjectId)
                            {
                                // Employee'nin mevcut projede headcount'ı yok veya yanlış projede, düzelt
                                await RemoveEmployeeFromOldHeadCountAsync(employee.Id);
                                await AddEmployeeToNewHeadCountAsync(employee);
                            }
                            // Headcount rengi kontrol et ve güncelle
                            await UpdateHeadCountColorBasedOnHistoryAsync(employee);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                }
            }
        }

        private async Task UpdateHeadCountColorBasedOnHistoryAsync(Employee employee)
        {
            // Employee'nin HeadCountHistory kaydını al
            var headCountHistory = await _headCountHistoryRepository.GetLatestHistoryByEmployeeIdAsync(employee.Id);

            if (headCountHistory != null)
            {
                var daysSinceTransfer = (DateTime.UtcNow - headCountHistory.ChangeDate).TotalDays;

                // Employee'nin mevcut headcount'unu bul
                var currentHeadCount = await _headCountRepository.GetAsync(hc => hc.EmployeeId == employee.Id);
                if (currentHeadCount == null) return; // Eğer headcount yoksa işlem yapma

                // Employee'nin bulunduğu store bilgisini al
                var store = await _storeRepository.GetByProjectIdAsync(currentHeadCount.ProjectId);

                if (daysSinceTransfer < 3)
                {
                    // 3 günden az zaman geçtiyse headcount rengi mavi olmalı
                    currentHeadCount.ColorId = await _colorRepository.GetBlueColorIdAsync();
                }
                else
                {
                    // 3 günden fazla geçtiyse, HCNumber'a göre store'daki HeadCountNumber ile kıyaslama yaparak renk ayarla
                    if (currentHeadCount.HCNumber > store.HeadCountNumber)
                    {
                        currentHeadCount.ColorId = await _colorRepository.GetYellowColorIdAsync(); // Örnek olarak kırmızı
                    }
                    else
                    {
                        currentHeadCount.ColorId = await _colorRepository.GetWhiteColorIdAsync(); // Sarı renk
                    }
                }

                // Headcount güncelle
                await _headCountRepository.UpdateAsync(currentHeadCount);
            }
        }


        private async Task RemoveEmployeeFromOldHeadCountAsync(int employeeId)
        {
            var headCount = await _headCountRepository.GetAsync(hc => hc.EmployeeId == employeeId);
            if (headCount != null)
            {
                headCount.EmployeeId = null;
                headCount.IsVacant = true;
                await _headCountRepository.UpdateAsync(headCount);
            }
        }

        private async Task AddEmployeeToNewHeadCountAsync(Employee employee)
        {
            var availableHeadCounts = await _headCountRepository.GetAllAsync(hc =>
                hc.ProjectId == employee.ProjectId &&
                hc.SectionId == employee.SectionId &&
                hc.PositionId == employee.PositionId &&
                hc.SubSectionId == employee.SubSectionId &&
                hc.IsVacant == true);

            var sortedHeadCounts = availableHeadCounts.OrderBy(hc => hc.HCNumber).ToList();

            if (sortedHeadCounts.Any())
            {
                // Mevcut bir headcount'u güncelle
                var headCount = sortedHeadCounts.First();
                headCount.EmployeeId = employee.Id;
                headCount.IsVacant = false;

                await _headCountRepository.UpdateAsync(headCount);
            }
            else
            {
                // Employee'nin ProjectId'sine göre store bilgisini al
                var store = await _storeRepository.GetByProjectIdAsync(employee.ProjectId);

                // Yeni headcount oluştur
                await CreateNewHeadCountAsync(employee, store);
            }

            await _headCountRepository.CommitAsync();
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

        private async Task AddHeadCountHistoryAsync(int employeeId, int fromProjectId, int toProjectId)
        {
            var headCountHistory = new HeadCountHistory
            {
                EmployeeId = employeeId,
                FromProjectId = fromProjectId,
                ToProjectId = toProjectId,
                ChangeDate = DateTime.UtcNow.AddHours(4)
            };

            await _headCountHistoryRepository.AddAsync(headCountHistory);
            await _headCountHistoryRepository.CommitAsync();
        }
    }
}
