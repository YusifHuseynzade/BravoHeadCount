using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataDetailsHandlers.CommandHandlers
{
    public class UpdateScheduledDataCommandHandler : IRequestHandler<UpdateScheduledDataCommandRequest, UpdateScheduledDataCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly IPlanRepository _planRepository;

        public UpdateScheduledDataCommandHandler(IEmployeeRepository employeeRepository,
                                                 IScheduledDataRepository scheduledDataRepository,
                                                 IPlanRepository planRepository)
        {
            _employeeRepository = employeeRepository;
            _scheduledDataRepository = scheduledDataRepository;
            _planRepository = planRepository;
        }

        public async Task<UpdateScheduledDataCommandResponse> Handle(UpdateScheduledDataCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Çalışan bilgilerini al
                var employee = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId);
                if (employee == null)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Employee not found."
                    };
                }

                // "Məzuniyyət" ve "Day Off" planlarının ID'sini al
                var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                var dayOffPlan = await _planRepository.GetByValueAsync("Day Off");

                if (vacationPlan == null || dayOffPlan == null)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = "'Məzuniyyət' or 'Day Off' plan not found."
                    };
                }

                // İstek içerisindeki scheduled data ID'lerini alın
                var scheduledDataIds = request.WeeklyUpdates.Select(x => x.ScheduledDataId).ToList();

                // Veritabanından ilgili scheduled data kayıtlarını alın
                var scheduledDataList = await _scheduledDataRepository.GetAllAsync(sd => scheduledDataIds.Contains(sd.Id) && sd.EmployeeId == request.EmployeeId);

                if (!scheduledDataList.Any())
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = "No scheduled data found for the given employee and IDs."
                    };
                }

                // Referans tarihi al ve haftanın başlangıcını ve bitişini belirle
                var referenceDate = scheduledDataList.First().Date;
                var currentWeekStart = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1);
                var referenceWeekStart = referenceDate.AddDays(-(int)referenceDate.DayOfWeek + 1);
                var referenceWeekEnd = referenceWeekStart.AddDays(6);

                // Geçmiş haftalara güncelleme yapılmamalı
                if (referenceWeekEnd < currentWeekStart)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Cannot update past weeks."
                    };
                }

                // Haftalık güncellemenin 7 gün olup olmadığını kontrol edin
                var fullWeekData = scheduledDataList.Where(sd => sd.Date >= referenceWeekStart && sd.Date <= referenceWeekEnd).ToList();
                if (fullWeekData.Count != 7)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = "All 7 days of the week must be filled for each update week."
                    };
                }

                // Güncellemeleri yap ve "Məzuniyyət" planı kontrolü
                foreach (var updateDto in request.WeeklyUpdates)
                {
                    var scheduledData = scheduledDataList.FirstOrDefault(sd => sd.Id == updateDto.ScheduledDataId);
                    if (scheduledData != null)
                    {
                        // Eğer plan "Məzuniyyət" ise güncellenmemelidir
                        if (scheduledData.PlanId == vacationPlan.Id)
                        {
                            continue;
                        }

                        scheduledData.PlanId = updateDto.PlanId;
                        scheduledData.Fact = updateDto.Fact;
                    }
                }

                // Haftalık güncellemede en az bir "Day Off" kontrolü
                if (!fullWeekData.Any(sd => sd.PlanId == dayOffPlan.Id))
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = "Weekly updates must include at least one 'Day Off' for the employee."
                    };
                }

                // Güncellemeleri veritabanında uygulayın
                foreach (var scheduledData in scheduledDataList)
                {
                    await _scheduledDataRepository.UpdateAsync(scheduledData);
                }

                await _scheduledDataRepository.CommitAsync();

                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Scheduled data updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An error occurred while updating scheduled data: {ex.Message}"
                };
            }
        }
    }
}
