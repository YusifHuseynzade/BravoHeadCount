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

                                // Eğer ScheduledData kaydı yoksa, o tarihe ait tatil bilgisi mevcut mu kontrol et
                                if (scheduledData == null)
                                {
                                    // Eğer o tarihe ait scheduledData yoksa, oluşturma
                                    continue;
                                }

                                // Eğer ScheduledData kaydı varsa ve plan atanmadıysa veya yanlışsa, doğru planı ata
                                if (scheduledData.PlanId == null || scheduledData.PlanId != vacationPlan.Id)
                                {
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

                                // Eğer ScheduledData kaydı yoksa, o tarihe ait hastalık izni bilgisi mevcut mu kontrol et
                                if (scheduledData == null)
                                {
                                    // Eğer o tarihe ait scheduledData yoksa, oluşturma
                                    continue;
                                }

                                // Eğer ScheduledData kaydı varsa ve plan atanmadıysa veya yanlışsa, doğru planı ata
                                if (scheduledData.PlanId == null || scheduledData.PlanId != sickLeavePlan.Id)
                                {
                                    scheduledData.PlanId = sickLeavePlan.Id;
                                    await _scheduledDataRepository.UpdateAsync(scheduledData);
                                }
                            }
                        }

                        // Uygun olmayan ScheduledData kayıtlarını kontrol et ve PlanId'yi null yap
                        await ClearInvalidScheduledData(employee.Id, vacationSchedule, sickLeave, vacationPlan, sickLeavePlan);
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
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }


        private async Task ClearInvalidScheduledData(int employeeId, VacationSchedule vacationSchedule, SickLeave sickLeave, Plan vacationPlan, Plan sickLeavePlan)
        {
            // Çalışanın tüm ScheduledData kayıtlarını al
            var allScheduledData = await _scheduledDataRepository.GetByEmployeeIdAsync(employeeId);

            // Tatil tarih aralığını kontrol et
            if (vacationSchedule != null)
            {
                foreach (var scheduledData in allScheduledData)
                {
                    // Eğer mevcut tarih tatil aralığına uymuyorsa ve mevcut plan tatil planıysa, PlanId'yi null yap
                    if ((scheduledData.Date < vacationSchedule.StartDate || scheduledData.Date > vacationSchedule.EndDate)
                        && scheduledData.PlanId == vacationPlan.Id)
                    {
                        scheduledData.PlanId = null; // Geçersiz olan PlanId'yi null yap
                        await _scheduledDataRepository.UpdateAsync(scheduledData);
                    }
                }
            }

            // Hastalık izni tarih aralığını kontrol et
            if (sickLeave != null)
            {
                foreach (var scheduledData in allScheduledData)
                {
                    // Eğer mevcut tarih hastalık izni aralığına uymuyorsa ve mevcut plan hastalık planıysa, PlanId'yi null yap
                    if ((scheduledData.Date < sickLeave.StartDate || scheduledData.Date > sickLeave.EndDate)
                        && scheduledData.PlanId == sickLeavePlan.Id)
                    {
                        scheduledData.PlanId = null; // Geçersiz olan PlanId'yi null yap
                        await _scheduledDataRepository.UpdateAsync(scheduledData);
                    }
                }
            }
        }


    }
}
