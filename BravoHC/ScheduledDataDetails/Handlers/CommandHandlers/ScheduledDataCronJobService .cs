using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Handlers.CommandHandlers
{
    public class ScheduledDataCronJobService : BackgroundService
    {
        private readonly ILogger<ScheduledDataCronJobService> _logger;
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly IVacationScheduleRepository _vacationScheduleRepository;
        private readonly ISickLeaveRepository _sickLeaveRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ScheduledDataCronJobService(
            ILogger<ScheduledDataCronJobService> logger,
            IScheduledDataRepository scheduledDataRepository,
            IVacationScheduleRepository vacationScheduleRepository,
            ISickLeaveRepository sickLeaveRepository,
            IPlanRepository planRepository,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _scheduledDataRepository = scheduledDataRepository;
            _vacationScheduleRepository = vacationScheduleRepository;
            _sickLeaveRepository = sickLeaveRepository;
            _planRepository = planRepository;
            _employeeRepository = employeeRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("ScheduledDataCronJobService is running at: {time}", DateTimeOffset.Now);

                    // "Məzuniyyət" ve "Xəstəlik vərəqi" planlarını al
                    var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                    var sickLeavePlan = await _planRepository.GetByValueAsync("Xəstəlik vərəqi");

                    if (vacationPlan == null || sickLeavePlan == null)
                    {
                        _logger.LogError("'Məzuniyyət' veya 'Xəstəlik vərəqi' planı bulunamadı.");
                        return;
                    }

                    // Tüm çalışanları getir
                    var employees = await _employeeRepository.GetAllAsync();
                    foreach (var employee in employees)
                    {
                        // Çalışanın tatil ve hastalık bilgilerini al
                        var vacationSchedule = await _vacationScheduleRepository.GetByEmployeeIdAsync(employee.Id);
                        var sickLeave = await _sickLeaveRepository.GetByEmployeeIdAsync(employee.Id);

                        // Tatil tarih aralığını kontrol et ve ScheduledData oluştur veya güncelle
                        if (vacationSchedule != null)
                        {
                            var vacationStartDate = vacationSchedule.StartDate;
                            var vacationEndDate = vacationSchedule.EndDate;

                            for (var date = vacationStartDate.Date; date <= vacationEndDate.Date; date = date.AddDays(1))
                            {
                                // ScheduledData kaydını kontrol et
                                var scheduledData = await _scheduledDataRepository.GetByEmployeeAndDateAsync(employee.Id, date);

                                if (scheduledData == null)
                                {
                                    // Eğer ScheduledData kaydı yoksa oluştur
                                    var newScheduledData = new ScheduledData
                                    {
                                        EmployeeId = employee.Id,
                                        ProjectId = employee.ProjectId,
                                        Date = date,
                                        PlanId = vacationPlan.Id
                                    };

                                    await _scheduledDataRepository.AddAsync(newScheduledData);
                                }
                                else if (scheduledData.PlanId == null || scheduledData.PlanId != vacationPlan.Id)
                                {
                                    // Eğer ScheduledData kaydı varsa ve plan atanmadıysa veya yanlışsa, doğru planı ata
                                    scheduledData.PlanId = vacationPlan.Id;
                                    await _scheduledDataRepository.UpdateAsync(scheduledData);
                                }
                            }
                        }

                        // Hastalık izni tarih aralığını kontrol et ve ScheduledData oluştur veya güncelle
                        if (sickLeave != null)
                        {
                            var sickLeaveStartDate = sickLeave.StartDate;
                            var sickLeaveEndDate = sickLeave.EndDate;

                            for (var date = sickLeaveStartDate.Date; date <= sickLeaveEndDate.Date; date = date.AddDays(1))
                            {
                                // ScheduledData kaydını kontrol et
                                var scheduledData = await _scheduledDataRepository.GetByEmployeeAndDateAsync(employee.Id, date);

                                if (scheduledData == null)
                                {
                                    // Eğer ScheduledData kaydı yoksa oluştur
                                    var newScheduledData = new ScheduledData
                                    {
                                        EmployeeId = employee.Id,
                                        ProjectId = employee.ProjectId,
                                        Date = date,
                                        PlanId = sickLeavePlan.Id
                                    };

                                    await _scheduledDataRepository.AddAsync(newScheduledData);
                                }
                                else if (scheduledData.PlanId == null || scheduledData.PlanId != sickLeavePlan.Id)
                                {
                                    // Eğer ScheduledData kaydı varsa ve plan atanmadıysa veya yanlışsa, doğru planı ata
                                    scheduledData.PlanId = sickLeavePlan.Id;
                                    await _scheduledDataRepository.UpdateAsync(scheduledData);
                                }
                            }
                        }
                    }

                    // Değişiklikleri kaydet
                    await _scheduledDataRepository.CommitAsync();

                    _logger.LogInformation("ScheduledDataCronJobService successfully completed.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while running the ScheduledDataCronJobService.");
                }

                // 2 dakika bekle
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }
    }
}
