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
        private readonly ISickLeaveRepository _sickLeaveRepository;

        public UpdateScheduledDataCommandHandler(IEmployeeRepository employeeRepository,
                                                 IScheduledDataRepository scheduledDataRepository,
                                                 IPlanRepository planRepository,
                                                 ISickLeaveRepository sickLeaveRepository)
        {
            _employeeRepository = employeeRepository;
            _scheduledDataRepository = scheduledDataRepository;
            _planRepository = planRepository;
            _sickLeaveRepository = sickLeaveRepository;
        }

        public async Task<UpdateScheduledDataCommandResponse> Handle(UpdateScheduledDataCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var employeeUpdate in request.EmployeesWeeklyUpdates)
                {
                    // Çalışan bilgilerini al
                    var employee = await _employeeRepository.GetAsync(e => e.Id == employeeUpdate.EmployeeId);
                    if (employee == null)
                    {
                        return new UpdateScheduledDataCommandResponse
                        {
                            IsSuccess = false,
                            Message = $"Employee with ID {employeeUpdate.EmployeeId} not found."
                        };
                    }

                    // "Məzuniyyət", "Day Off" ve "Xəstəlik vərəqi" planlarının ID'sini al
                    var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                    var dayOffPlan = await _planRepository.GetByValueAsync("Day Off");
                    var sickLeavePlan = await _planRepository.GetByValueAsync("Xəstəlik vərəqi");

                    if (vacationPlan == null || dayOffPlan == null || sickLeavePlan == null)
                    {
                        return new UpdateScheduledDataCommandResponse
                        {
                            IsSuccess = false,
                            Message = "'Məzuniyyət', 'Day Off', or 'Xəstəlik vərəqi' plan not found."
                        };
                    }

                    // İstek içerisindeki scheduled data ID'lerini alın
                    var scheduledDataIds = employeeUpdate.WeeklyUpdates.Select(x => x.ScheduledDataId).ToList();

                    // Veritabanından ilgili scheduled data kayıtlarını alın
                    var scheduledDataList = await _scheduledDataRepository.GetAllAsync(sd => scheduledDataIds.Contains(sd.Id) && sd.EmployeeId == employeeUpdate.EmployeeId);

                    if (!scheduledDataList.Any())
                    {
                        return new UpdateScheduledDataCommandResponse
                        {
                            IsSuccess = false,
                            Message = $"No scheduled data found for the given employee ID {employeeUpdate.EmployeeId}."
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
                            Message = $"Cannot update past weeks for employee ID {employeeUpdate.EmployeeId}."
                        };
                    }

                    // Haftalık güncellemenin 7 gün olup olmadığını kontrol edin
                    var fullWeekData = scheduledDataList.Where(sd => sd.Date >= referenceWeekStart && sd.Date <= referenceWeekEnd).ToList();
                    if (fullWeekData.Count != 7)
                    {
                        return new UpdateScheduledDataCommandResponse
                        {
                            IsSuccess = false,
                            Message = $"All 7 days of the week must be filled for each update week for employee ID {employeeUpdate.EmployeeId}."
                        };
                    }

                    // Çalışanın mevcut hafta içinde tatil veya hastalık durumu olup olmadığını kontrol et
                    var sickLeave = await _sickLeaveRepository.GetByEmployeeIdAsync(employee.Id);

                    // Güncellemeleri yap ve "Məzuniyyət" ve "Xəstəlik vərəqi" planları kontrolü
                    foreach (var updateDto in employeeUpdate.WeeklyUpdates)
                    {
                        var scheduledData = scheduledDataList.FirstOrDefault(sd => sd.Id == updateDto.ScheduledDataId);
                        if (scheduledData != null)
                        {
                            // Eğer plan "Məzuniyyət" veya "Xəstəlik vərəqi" ise güncellenmemelidir
                            if (scheduledData.PlanId == vacationPlan.Id || scheduledData.PlanId == sickLeavePlan.Id)
                            {
                                continue;
                            }

                            // Eğer hastalık durumu mevcut ise ve tarihler uyuşuyorsa, "Xəstəlik vərəqi" planını atayalım
                            if (sickLeave != null && sickLeave.StartDate <= scheduledData.Date && sickLeave.EndDate >= scheduledData.Date)
                            {
                                scheduledData.PlanId = sickLeavePlan.Id;
                            }
                            else
                            {
                                scheduledData.PlanId = updateDto.PlanId;

                                // Fact alanını kontrol et
                                if (!string.IsNullOrEmpty(updateDto.Fact) && updateDto.Fact != "8")
                                {
                                    return new UpdateScheduledDataCommandResponse
                                    {
                                        IsSuccess = false,
                                        Message = $"Invalid Fact value. Only '8' is allowed for employee ID {employeeUpdate.EmployeeId}."
                                    };
                                }

                                // Eğer Fact "8" veya boşsa kaydet
                                scheduledData.Fact = updateDto.Fact;
                            }
                        }
                    }

                    // Haftalık güncellemede en az bir "Day Off" kontrolü
                    if (!fullWeekData.Any(sd => sd.PlanId == dayOffPlan.Id))
                    {
                        return new UpdateScheduledDataCommandResponse
                        {
                            IsSuccess = false,
                            Message = $"Weekly updates must include at least one 'Day Off' for employee ID {employeeUpdate.EmployeeId}."
                        };
                    }

                    // Güncellemeleri veritabanında uygulayın
                    foreach (var scheduledData in scheduledDataList)
                    {
                        await _scheduledDataRepository.UpdateAsync(scheduledData);
                    }
                }

                await _scheduledDataRepository.CommitAsync();

                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Scheduled data updated successfully for all employees."
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
