﻿using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryDetails.Handlers.CommandHandlers
{
    public class SummaryCronJobService : BackgroundService
    {
        private readonly ILogger<SummaryCronJobService> _logger;
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly ISummaryRepository _summaryRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMonthRepository _monthRepository;

        public SummaryCronJobService(
            ILogger<SummaryCronJobService> logger,
            IScheduledDataRepository scheduledDataRepository,
            ISummaryRepository summaryRepository,
            IPlanRepository planRepository,
            IEmployeeRepository employeeRepository,
            IMonthRepository monthRepository)
        {
            _logger = logger;
            _scheduledDataRepository = scheduledDataRepository;
            _summaryRepository = summaryRepository;
            _planRepository = planRepository;
            _employeeRepository = employeeRepository;
            _monthRepository = monthRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("SummaryCronJobService is running at: {time}", DateTimeOffset.Now);

                    // Plan ID'leri al
                    var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                    var sickLeavePlan = await _planRepository.GetByValueAsync("Xəstəlik vərəqi");
                    var dayOffPlan = await _planRepository.GetByValueAsync("Day Off");
                    var holidayPlan = await _planRepository.GetByValueAsync("Bayram");

                    if (vacationPlan == null || sickLeavePlan == null || dayOffPlan == null || holidayPlan == null)
                    {
                        _logger.LogError("Required plans ('Məzuniyyət', 'Xəstəlik vərəqi', 'Day Off', 'Bayram') not found.");
                        return;
                    }

                    // Tüm çalışanları al
                    var employees = await _employeeRepository.GetAllAsync();
                    foreach (var employee in employees)
                    {
                        // Çalışanın ScheduledData verilerini al (bu ay ve yıl için)
                        var currentDate = DateTime.UtcNow;
                        var month = await _monthRepository.GetByNumberAsync(currentDate.Month);
                        if (month == null) continue;

                        var scheduledData = await _scheduledDataRepository.GetByEmployeeAndMonthAsync(employee.Id, currentDate.Year, month.Id);

                        // Verileri say
                        var workdaysCount = scheduledData.Count(sd =>
                            sd.PlanId != vacationPlan.Id &&
                            sd.PlanId != sickLeavePlan.Id &&
                            sd.PlanId != dayOffPlan.Id &&
                            sd.PlanId != holidayPlan.Id);

                        var vacationDaysCount = scheduledData.Count(sd => sd.PlanId == vacationPlan.Id);
                        var sickDaysCount = scheduledData.Count(sd => sd.PlanId == sickLeavePlan.Id);
                        var dayOffCount = scheduledData.Count(sd => sd.PlanId == dayOffPlan.Id);
                        var absentDaysCount = scheduledData.Count(sd => sd.PlanId == holidayPlan.Id);

                        // Mevcut Summary kaydı var mı?
                        var summary = await _summaryRepository.GetByEmployeeAndMonthAsync(employee.Id, currentDate.Year, month.Id);
                        if (summary == null)
                        {
                            // Eğer summary kaydı yoksa, yeni bir kayıt oluştur
                            summary = new Summary
                            {
                                EmployeeId = employee.Id,
                                MonthId = month.Id,
                                Year = currentDate.Year,
                                WorkdaysCount = workdaysCount,
                                VacationDaysCount = vacationDaysCount,
                                SickDaysCount = sickDaysCount,
                                DayOffCount = dayOffCount,
                                AbsentDaysCount = absentDaysCount
                            };

                            await _summaryRepository.AddAsync(summary);
                        }
                        else
                        {
                            // Eğer summary kaydı varsa, güncelle
                            summary.WorkdaysCount = workdaysCount;
                            summary.VacationDaysCount = vacationDaysCount;
                            summary.SickDaysCount = sickDaysCount;
                            summary.DayOffCount = dayOffCount;
                            summary.AbsentDaysCount = absentDaysCount;

                            await _summaryRepository.UpdateAsync(summary);
                        }
                    }

                    // Değişiklikleri kaydet
                    await _summaryRepository.CommitAsync();

                    _logger.LogInformation("SummaryCronJobService successfully completed.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while running the SummaryCronJobService.");
                }

                // 24 saatte bir çalış
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}