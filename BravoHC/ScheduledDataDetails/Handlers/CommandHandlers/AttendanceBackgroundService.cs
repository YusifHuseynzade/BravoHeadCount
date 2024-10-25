using Core.Helpers;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Handlers.CommandHandlers
{
    public class AttendanceBackgroundService : BackgroundService
    {
        private readonly ILogger<AttendanceBackgroundService> _logger;
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFactRepository _factRepository; // Fact repository eklendi
        private readonly string _hikvisionConnectionString;

        public AttendanceBackgroundService(
            ILogger<AttendanceBackgroundService> logger,
            IScheduledDataRepository scheduledDataRepository,
            IEmployeeRepository employeeRepository,
            IFactRepository factRepository, // Fact repository inject edildi
            IOptions<HikVisionSettings> hikVisionSettings)
        {
            _logger = logger;
            _scheduledDataRepository = scheduledDataRepository;
            _employeeRepository = employeeRepository;
            _factRepository = factRepository;
            _hikvisionConnectionString = hikVisionSettings.Value.HikVisionConnectionString;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Attendance background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessAttendanceData();
                    _logger.LogInformation("Attendance data processed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing attendance data: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task ProcessAttendanceData()
        {
            var attendanceData = await FetchHikvisionData();
            var groupedData = attendanceData.GroupBy(x => new { x.EmployeeId, x.AccessDate });

            foreach (var group in groupedData)
            {
                var badge = group.Key.EmployeeId;
                var accessDate = group.Key.AccessDate;

                // Badge ile Employee bulma
                var employee = await _employeeRepository.GetByBadgeAsync(badge);
                if (employee == null)
                {
                    _logger.LogWarning($"No employee found for badge: {badge}");
                    continue;
                }

                var employeeId = employee.Id;
                var attendanceTime = CalculateAttendanceTime(group.ToList());

                // AttendanceTime verisini Fact tablosuna kaydetme
                var fact = new Fact { Value = attendanceTime };
                await _factRepository.AddAsync(fact);
                await _factRepository.CommitAsync();

                // Yeni eklenen Fact kaydının ID'sini alma
                var factId = fact.Id;

                // ScheduledData güncellemesi
                var scheduledData = await _scheduledDataRepository.GetByEmployeeAndDateAsync(employeeId, accessDate);
                if (scheduledData != null)
                {
                    scheduledData.FactId = factId; // Fact ID'sini ekleme
                    await _scheduledDataRepository.UpdateAsync(scheduledData);
                }
                else
                {
                    _logger.LogWarning($"No scheduled data found for EmployeeId: {employeeId} on Date: {accessDate:yyyy-MM-dd}");
                }
            }

            await _scheduledDataRepository.CommitAsync();
        }

        private async Task<List<AttendanceRecord>> FetchHikvisionData()
        {
            var data = new List<AttendanceRecord>();

            using (var conn = new SqlConnection(_hikvisionConnectionString))
            {
                await conn.OpenAsync();
                var query = @"
                    SELECT EmployeeID, AccessDateAndTime, AccessDate, DeviceName, PersonName
                    FROM [HikVision].[dbo].[EmployeeAttendance]
                    WHERE AccessDate >= DATEADD(DAY, -1, GETDATE())
                ";

                using (var command = new SqlCommand(query, conn))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        data.Add(new AttendanceRecord
                        {
                            EmployeeId = reader["EmployeeID"].ToString(),
                            AccessDateTime = DateTime.Parse(reader["AccessDateAndTime"].ToString()),
                            AccessDate = DateTime.Parse(reader["AccessDate"].ToString()),
                            DeviceName = reader["DeviceName"].ToString(),
                            PersonName = reader["PersonName"].ToString()
                        });
                    }
                }
            }

            return data;
        }

        private string CalculateAttendanceTime(List<AttendanceRecord> records)
        {
            DateTime? entryTime = records
                .Where(r => r.DeviceName.Contains("Enter"))
                .Min(r => r.AccessDateTime);

            DateTime? exitTime = records
                .Where(r => r.DeviceName.Contains("Exit"))
                .Max(r => r.AccessDateTime);

            if (entryTime.HasValue && exitTime.HasValue)
            {
                var duration = exitTime.Value - entryTime.Value;
                return $"{entryTime.Value:HH:mm} - {exitTime.Value:HH:mm} ({duration.TotalHours:F2} hrs)";
            }

            return "No valid entry/exit data";
        }
    }
}
