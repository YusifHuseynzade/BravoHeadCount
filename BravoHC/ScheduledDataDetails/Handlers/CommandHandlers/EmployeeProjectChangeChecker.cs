using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataBackgroundService
{
    public class EmployeeProjectChangeChecker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<EmployeeProjectChangeChecker> _logger;

        public EmployeeProjectChangeChecker(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<EmployeeProjectChangeChecker> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EmployeeProjectChangeChecker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAndCopyScheduledData(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in EmployeeProjectChangeChecker: {ex}");
                }

                await Task.Delay(TimeSpan.FromHours(6), stoppingToken); // Runs every 6 hour
            }
        }

        private async Task CheckAndCopyScheduledData(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var employeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
            var scheduledDataRepository = scope.ServiceProvider.GetRequiredService<IScheduledDataRepository>();

            var employees = await employeeRepository.GetAllAsync();

            var currentMonthStart = DateTime.SpecifyKind(
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1),
                DateTimeKind.Utc
            );

            var currentDate = DateTime.UtcNow.Date; // Always use UTC

            foreach (var employee in employees)
            {
                var scheduledDatas = await scheduledDataRepository.GetAllAsyncForCron(sd =>
                    sd.EmployeeId == employee.Id &&
                    sd.Date >= currentMonthStart &&
                    sd.Date < currentDate &&
                    sd.ProjectId != employee.ProjectId, cancellationToken);

                if (scheduledDatas.Any())
                {
                    _logger.LogInformation($"Employee {employee.FullName} has changed projects.");

                    foreach (var scheduledData in scheduledDatas)
                    {
                        var existingData = await scheduledDataRepository.GetAllAsyncForCron(sd =>
                            sd.EmployeeId == employee.Id &&
                            sd.ProjectId == employee.ProjectId && // Yeni proje
                            sd.Date == scheduledData.Date, cancellationToken);

                        if (existingData.Any())
                        {
                            _logger.LogInformation(
                                $"Skipped copying schedule data for employee {employee.FullName} on {scheduledData.Date:yyyy-MM-dd} to project {employee.ProjectId} (already exists)."
                            );
                            continue;
                        }

                        var newScheduledData = new ScheduledData
                        {
                            EmployeeId = employee.Id,
                            ProjectId = employee.ProjectId,
                            Date = DateTime.SpecifyKind(scheduledData.Date, DateTimeKind.Utc),
                            PlanId = scheduledData.PlanId,
                            FactId = scheduledData.FactId
                        };

                        await scheduledDataRepository.AddAsync(newScheduledData);
                    }

                    await scheduledDataRepository.CommitAsync();
                    _logger.LogInformation(
                        $"Scheduled data copied for employee {employee.FullName} to project {employee.ProjectId}."
                    );
                }
            }
        }
    }
}
