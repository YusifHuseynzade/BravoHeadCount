using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BackgroundServices
{
    public class GeneralSettingsCronJobService : BackgroundService
    {
        private readonly IGeneralSettingRepository _generalSettingRepository;
        private readonly IEndOfMonthReportRepository _endOfMonthReportRepository;
        private readonly IExpensesReportRepository _expensesReportRepository;
        private readonly ILogger<GeneralSettingsCronJobService> _logger;

        public GeneralSettingsCronJobService(
            IGeneralSettingRepository generalSettingRepository,
            IEndOfMonthReportRepository endOfMonthReportRepository,
            IExpensesReportRepository expensesReportRepository,
            ILogger<GeneralSettingsCronJobService> logger)
        {
            _generalSettingRepository = generalSettingRepository;
            _endOfMonthReportRepository = endOfMonthReportRepository;
            _expensesReportRepository = expensesReportRepository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var settings = await _generalSettingRepository.GetSettingsAsync();

                    // End of Month Report işlemi
                    await HandleEndOfMonthReport(settings);

                    // Expense Report işlemi
                    await HandleExpenseReport(settings);

                    // 1 dakika bekle ve tekrar çalıştır
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Cron Job'da hata oluştu: {ex.Message}");
                }
            }
        }

        // End of Month Report işleyici
        private async Task HandleEndOfMonthReport(GeneralSetting settings)
        {
            if (IsDayMatchingFrequency(settings.ModifiedDate, settings.EndOfMonthReportSettings.SendingFrequency))
            {
                await ProcessReport(
                    settings.EndOfMonthReportSettings.SendingTimes,
                    "End of Month Report",
                    async () => await _endOfMonthReportRepository.GetAllAsync()
                );
            }
            else
            {
                _logger.LogInformation($"Bugün End of Month frekansa uymuyor: {DateTime.UtcNow.Day}");
            }
        }

        // Expense Report işleyici
        private async Task HandleExpenseReport(GeneralSetting settings)
        {
            if (IsDayMatchingFrequency(settings.ModifiedDate, settings.ExpenseReportSettings.SendingFrequency))
            {
                await ProcessReport(
                    settings.ExpenseReportSettings.SendingTimes,
                    "Expense Report",
                    async () => await _expensesReportRepository.GetAllAsync()
                );
            }
            else
            {
                _logger.LogInformation($"Bugün Expense Report frekansa uymuyor: {DateTime.UtcNow.Day}");
            }
        }

        // Rapor işlem metodu
        private async Task ProcessReport<T>(
            List<TimeSpan> sendingTimes,
            string reportType,
            Func<Task<List<T>>> fetchReports)
        {
            foreach (var sendingTime in sendingTimes)
            {
                if (IsTimeToRun(sendingTime))
                {
                    _logger.LogInformation($"{reportType} tetiklendi: {DateTime.UtcNow}.");

                    try
                    {
                        var reports = await fetchReports();
                        _logger.LogInformation($"{reportType} için {reports.Count} rapor bulundu.");

                        foreach (var report in reports)
                        {
                            Console.WriteLine($"{reportType}: {report}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Raporlar alınırken hata oluştu: {ex.Message}");
                    }
                }
            }
        }

        // Frekans kontrol metodu (ModifiedDate'e dayalı)
        private bool IsDayMatchingFrequency(DateTime? modifiedDate, int frequency)
        {
            if (modifiedDate == null) return false;

            var daysSinceModified = (DateTime.UtcNow.Date - modifiedDate.Value.Date).Days;
            return daysSinceModified % frequency == 0;
        }

        // Saat kontrol metodu
        private bool IsTimeToRun(TimeSpan sendingTime)
        {
            var currentTime = DateTime.UtcNow.AddHours(4).TimeOfDay;
            var difference = Math.Abs((currentTime - sendingTime).TotalMinutes);

            _logger.LogInformation($"Kontrol: Şu anki saat {currentTime}, Gönderim saati {sendingTime}, Fark: {difference} dakika.");

            // Belirtilen saatle fark 0.5 dakikadan azsa raporu çalıştır
            return difference < 0.5;
        }
    }
}
