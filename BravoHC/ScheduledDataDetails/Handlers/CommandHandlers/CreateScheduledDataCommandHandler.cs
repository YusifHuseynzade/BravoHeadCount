using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetails.Commands.Response;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataDetailsHandlers.CommandHandlers
{
    public class CreateScheduledDataCommandHandler : IRequestHandler<CreateScheduledDataCommandRequest, CreateScheduledDataCommandResponse>
    {
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVacationScheduleRepository _vacationScheduleRepository;
        private readonly IPlanRepository _planRepository;

        public CreateScheduledDataCommandHandler(IScheduledDataRepository scheduledDataRepository,
                                                 IProjectRepository projectRepository,
                                                 IEmployeeRepository employeeRepository,
                                                 IVacationScheduleRepository vacationScheduleRepository,
                                                 IPlanRepository planRepository)
        {
            _scheduledDataRepository = scheduledDataRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _vacationScheduleRepository = vacationScheduleRepository;
            _planRepository = planRepository;
        }

        public async Task<CreateScheduledDataCommandResponse> Handle(CreateScheduledDataCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Veritabanındaki en son tarihli ScheduledData'yı al
                var lastScheduledData = await _scheduledDataRepository.GetLastScheduledDataAsync();
                DateTime nextMonday;
                DateTime nextSunday;

                if (lastScheduledData != null)
                {
                    // Eğer veri varsa, en son tarihten sonraki haftayı hesapla
                    nextMonday = lastScheduledData.Date.AddDays(1);
                    nextSunday = nextMonday.AddDays(6);
                }
                else
                {
                    // Eğer veri yoksa, geçerli tarihten bir sonraki haftanın Pazartesi ve Pazar tarihlerini belirle
                    var currentDate = DateTime.UtcNow;
                    nextMonday = currentDate.AddDays(8 - (int)currentDate.DayOfWeek);
                    nextSunday = nextMonday.AddDays(6);
                }

                // Bu tarih aralığı için daha önce veri olup olmadığını kontrol et
                var existingData = await _scheduledDataRepository.GetByDateRangeAsync(nextMonday, nextSunday);
                if (existingData.Any())
                {
                    return new CreateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Scheduled data already exists for the specified date range."
                    };
                }

                // "Məzuniyyət" planını al
                var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                if (vacationPlan == null)
                {
                    return new CreateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "'Məzuniyyət' planı bulunamadı."
                    };
                }

                // Aktif projeleri al
                var projects = await _projectRepository.GetAllAsync(p => p.IsActive == true);
                foreach (var project in projects)
                {
                    // Her projeye bağlı çalışanları al
                    var employees = await _employeeRepository.GetAllAsync(e => e.ProjectId == project.Id);
                    foreach (var employee in employees)
                    {
                        // Çalışanın tatil bilgilerini al
                        var vacationSchedule = await _vacationScheduleRepository.GetByEmployeeIdAsync(employee.Id);

                        // Haftalık tarih aralığını belirle (Pazartesi - Pazar)
                        for (var date = nextMonday; date <= nextSunday; date = date.AddDays(1))
                        {
                            int? planId = null;

                            // Tatil kontrolü: Tatil varsa planId'yi ayarla
                            if (vacationSchedule != null && vacationSchedule.StartDate <= date && vacationSchedule.EndDate >= date)
                            {
                                planId = vacationPlan.Id;
                            }

                            var scheduledData = new ScheduledData
                            {
                                EmployeeId = employee.Id, // Çalışanın ID'si
                                ProjectId = project.Id,   // Projenin ID'si
                                Date = date.Date,         // Tarih (Günlük olarak)
                                PlanId = planId,          // Tatil varsa planId "Məzuniyyət" olacak
                            };

                            await _scheduledDataRepository.AddAsync(scheduledData);
                        }
                    }
                }

                // Tüm scheduledData verilerini kaydet
                await _scheduledDataRepository.CommitAsync();

                return new CreateScheduledDataCommandResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                // Inner exception'ı yakalayarak hata hakkında daha fazla bilgi edin
                return new CreateScheduledDataCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred while creating scheduled data: {ex.Message} - {ex.InnerException?.Message}"
                };
            }
        }
    }
}
