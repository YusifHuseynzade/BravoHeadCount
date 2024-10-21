using Common.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Handlers.CommandHandlers
{
    public class ScheduledDataFactUpdaterService : BackgroundService
    {
        private readonly ILogger<ScheduledDataFactUpdaterService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ScheduledDataFactUpdaterService(
            ILogger<ScheduledDataFactUpdaterService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scheduledDataRepository = scope.ServiceProvider.GetRequiredService<IScheduledDataRepository>();
                    var planRepository = scope.ServiceProvider.GetRequiredService<IPlanRepository>();

                    try
                    {
                        _logger.LogInformation("ScheduledDataFactUpdaterService çalışıyor: {time}", DateTimeOffset.Now);

                        // Fact değeri boş olan ScheduledData kayıtlarını getir
                        var incompleteScheduledData = await scheduledDataRepository.GetAllWithEmptyFactAsync();
                        _logger.LogInformation("Güncellenmesi gereken kayıt sayısı: {Count}", incompleteScheduledData.Count);

                        // İlgili planları tek tek getir: Bayram, Day Off, Xəstəlik vərəqi, Məzuniyyət
                        var holidayPlan = await planRepository.GetByValueAsync("Bayram");
                        var dayOffPlan = await planRepository.GetByValueAsync("Day Off");
                        var sickLeavePlan = await planRepository.GetByValueAsync("Xəstəlik vərəqi");
                        var vacationPlan = await planRepository.GetByValueAsync("Məzuniyyət");

                        foreach (var data in incompleteScheduledData)
                        {
                            // Plan Id'sine göre Fact alanına ilgili değeri ata
                            if (data.PlanId == holidayPlan?.Id)
                            {
                                data.Fact = holidayPlan.Value;
                            }
                            else if (data.PlanId == dayOffPlan?.Id)
                            {
                                data.Fact = dayOffPlan.Value;
                            }
                            else if (data.PlanId == sickLeavePlan?.Id)
                            {
                                data.Fact = sickLeavePlan.Value;
                            }
                            else if (data.PlanId == vacationPlan?.Id)
                            {
                                data.Fact = vacationPlan.Value;
                            }
                            else
                            {
                                // Eğer plan uygun değilse veya boşsa, Fact alanına '8' ata
                                data.Fact = "8";
                            }

                            await scheduledDataRepository.UpdateAsync(data);
                        }

                        // Değişiklikleri kaydet
                        await scheduledDataRepository.CommitAsync();
                        _logger.LogInformation("ScheduledDataFactUpdaterService başarıyla tamamlandı.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "ScheduledDataFactUpdaterService çalışırken bir hata oluştu.");
                    }
                }

                // 2 dakika bekle
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }
    }
}
