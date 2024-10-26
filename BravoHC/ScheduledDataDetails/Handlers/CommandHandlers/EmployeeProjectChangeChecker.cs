using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataBackgroundService
{
    public class EmployeeProjectChangeChecker : BackgroundService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly ILogger<EmployeeProjectChangeChecker> _logger;

        public EmployeeProjectChangeChecker(
            IEmployeeRepository employeeRepository,
            IScheduledDataRepository scheduledDataRepository,
            ILogger<EmployeeProjectChangeChecker> logger)
        {
            _employeeRepository = employeeRepository;
            _scheduledDataRepository = scheduledDataRepository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EmployeeProjectChangeChecker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAndCopyScheduledData();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in EmployeeProjectChangeChecker: {ex}");
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken); // Runs every 2 minutes
            }
        }

        private async Task CheckAndCopyScheduledData()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var currentMonthStart = DateTime.SpecifyKind(
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1),
                DateTimeKind.Utc
            );

            var currentDate = DateTime.UtcNow.Date; // Always use UTC

            foreach (var employee in employees)
            {
                var scheduledDatas = await _scheduledDataRepository.GetAllAsync(sd =>
                    sd.EmployeeId == employee.Id &&
                    sd.Date >= currentMonthStart &&
                    sd.Date < currentDate &&
                    sd.ProjectId != employee.ProjectId); // Find mismatched project assignments

                if (scheduledDatas.Any())
                {
                    _logger.LogInformation($"Employee {employee.FullName} has changed projects.");

                    foreach (var scheduledData in scheduledDatas)
                    {
                        // Kontrol: Yeni proje için aynı tarihte zaten veri varsa ekleme yapma
                        var existingData = await _scheduledDataRepository.GetAllAsync(sd =>
                            sd.EmployeeId == employee.Id &&
                            sd.ProjectId == employee.ProjectId && // Yeni proje
                            sd.Date == scheduledData.Date); // Aynı tarih

                        if (existingData.Any())
                        {
                            _logger.LogInformation(
                                $"Skipped copying schedule data for employee {employee.FullName} on {scheduledData.Date:yyyy-MM-dd} to project {employee.ProjectId} (already exists)."
                            );
                            continue; // Bu tarih için veri zaten varsa geç
                        }

                        var newScheduledData = new ScheduledData
                        {
                            EmployeeId = employee.Id,
                            ProjectId = employee.ProjectId, // Assign to new project
                            Date = DateTime.SpecifyKind(scheduledData.Date, DateTimeKind.Utc), // Ensure Date is UTC
                            PlanId = scheduledData.PlanId,
                            FactId = scheduledData.FactId
                        };

                        await _scheduledDataRepository.AddAsync(newScheduledData);
                    }

                    await _scheduledDataRepository.CommitAsync();
                    _logger.LogInformation(
                        $"Scheduled data copied for employee {employee.FullName} to project {employee.ProjectId}."
                    );
                }
            }
        }
    }
}
