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
                // Çalışan bilgilerini al
                var employee = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId);
                if (employee == null)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"Employee with ID {request.EmployeeId} not found."
                    };
                }

                // Planların ID'sini al
                var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                var dayOffPlan = await _planRepository.GetByValueAsync("Day Off");
                var sickLeavePlan = await _planRepository.GetByValueAsync("Xəstəlik vərəqi");

                if (vacationPlan == null || dayOffPlan == null || sickLeavePlan == null)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"Plan not found for employee ID {request.EmployeeId}."
                    };
                }

                // Güncellenebilir data
                var scheduledDataIds = request.WeeklyUpdates.Select(x => x.ScheduledDataId).ToList();
                var scheduledDataList = await _scheduledDataRepository.GetAllAsync(sd => scheduledDataIds.Contains(sd.Id) && sd.EmployeeId == request.EmployeeId);

                if (!scheduledDataList.Any())
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"No scheduled data found for employee ID {request.EmployeeId}."
                    };
                }

                // Tarih kontrolleri
                var referenceDate = scheduledDataList.First().Date;
                var currentWeekStart = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1);
                var referenceWeekStart = referenceDate.AddDays(-(int)referenceDate.DayOfWeek + 1);
                var referenceWeekEnd = referenceWeekStart.AddDays(6);

                if (referenceWeekEnd < currentWeekStart)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"Cannot update past weeks for employee ID {request.EmployeeId}."
                    };
                }

                var fullWeekData = scheduledDataList.Where(sd => sd.Date >= referenceWeekStart && sd.Date <= referenceWeekEnd).ToList();
                if (fullWeekData.Count != 7)
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"All 7 days of the week must be filled for employee ID {request.EmployeeId}."
                    };
                }

                var sickLeave = await _sickLeaveRepository.GetByEmployeeIdAsync(employee.Id);

                foreach (var updateDto in request.WeeklyUpdates)
                {
                    var scheduledData = scheduledDataList.FirstOrDefault(sd => sd.Id == updateDto.ScheduledDataId);
                    if (scheduledData != null)
                    {
                        if (scheduledData.PlanId == vacationPlan.Id || scheduledData.PlanId == sickLeavePlan.Id)
                        {
                            continue;
                        }

                        if (sickLeave != null && sickLeave.StartDate <= scheduledData.Date && sickLeave.EndDate >= scheduledData.Date)
                        {
                            scheduledData.PlanId = sickLeavePlan.Id;
                        }
                        else
                        {
                            scheduledData.PlanId = updateDto.PlanId;
                            scheduledData.FactId = updateDto.FactId;
                        }
                    }
                }

                if (!fullWeekData.Any(sd => sd.PlanId == dayOffPlan.Id))
                {
                    return new UpdateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"At least one 'Day Off' is required for employee ID {request.EmployeeId}."
                    };
                }

                foreach (var scheduledData in scheduledDataList)
                {
                    await _scheduledDataRepository.UpdateAsync(scheduledData);
                }

                await _scheduledDataRepository.CommitAsync();

                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = true,
                    Message = $"Update successful for employee ID {request.EmployeeId}."
                };
            }
            catch (Exception ex)
            {
                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error updating employee ID {request.EmployeeId}: {ex.Message}"
                };
            }
        }
    }
}
