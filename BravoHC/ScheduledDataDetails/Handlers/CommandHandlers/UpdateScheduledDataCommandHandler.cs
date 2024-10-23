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
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ISickLeaveRepository _sickLeaveRepository;

        public UpdateScheduledDataCommandHandler(
            IScheduledDataRepository scheduledDataRepository,
            IPlanRepository planRepository,
            ISickLeaveRepository sickLeaveRepository)
        {
            _scheduledDataRepository = scheduledDataRepository;
            _planRepository = planRepository;
            _sickLeaveRepository = sickLeaveRepository;
        }

        public async Task<UpdateScheduledDataCommandResponse> Handle(UpdateScheduledDataCommandRequest request, CancellationToken cancellationToken)
        {
            var successCount = 0;
            var failureMessages = new List<string>();

            try
            {
                foreach (var weeklyUpdate in request.ScheduleUpdates)
                {
                    var scheduledDataIds = weeklyUpdate.WeeklyUpdates.Select(x => x.ScheduledDataId).ToList();
                    var scheduledDataList = await _scheduledDataRepository.GetAllAsync(sd => scheduledDataIds.Contains(sd.Id));

                    if (!scheduledDataList.Any())
                    {
                        failureMessages.Add("No valid scheduled data found for the provided IDs.");
                        continue;
                    }

                    // Haftanın başlangıcını ve bitişini belirle
                    var referenceDate = scheduledDataList.First().Date;
                    var weekStart = referenceDate.AddDays(-(int)referenceDate.DayOfWeek + 1).Date;
                    var weekEnd = weekStart.AddDays(6);

                    if (scheduledDataList.Count(sd => sd.Date >= weekStart && sd.Date <= weekEnd) != 7)
                    {
                        failureMessages.Add($"Incomplete data for the week starting {weekStart:yyyy-MM-dd}.");
                        continue;
                    }

                    var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                    var dayOffPlan = await _planRepository.GetByValueAsync("Day Off");
                    var sickLeavePlan = await _planRepository.GetByValueAsync("Xəstəlik vərəqi");

                    if (vacationPlan == null || dayOffPlan == null || sickLeavePlan == null)
                    {
                        failureMessages.Add("'Məzuniyyət', 'Day Off', or 'Xəstəlik vərəqi' plan not found.");
                        continue;
                    }

                    if (!weeklyUpdate.WeeklyUpdates.Any(update => update.PlanId == dayOffPlan.Id))
                    {
                        failureMessages.Add($"Missing 'Day Off' plan for the week starting {weekStart:yyyy-MM-dd}.");
                        continue;
                    }

                    foreach (var updateDto in weeklyUpdate.WeeklyUpdates)
                    {
                        var scheduledData = scheduledDataList.FirstOrDefault(sd => sd.Id == updateDto.ScheduledDataId);
                        if (scheduledData != null)
                        {
                            if (scheduledData.PlanId == vacationPlan.Id || scheduledData.PlanId == sickLeavePlan.Id)
                                continue;

                            var sickLeave = await _sickLeaveRepository.GetByEmployeeIdAsync(scheduledData.EmployeeId);
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

                    foreach (var scheduledData in scheduledDataList)
                    {
                        await _scheduledDataRepository.UpdateAsync(scheduledData);
                    }

                    successCount++;
                }

                await _scheduledDataRepository.CommitAsync();

                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = failureMessages.Count == 0,
                    Message = failureMessages.Count == 0
                        ? "All weekly updates completed successfully."
                        : $"Updates completed with {failureMessages.Count} failures.",
                    SuccessCount = successCount,
                    FailureCount = failureMessages.Count,
                    FailureMessages = failureMessages
                };
            }
            catch (Exception ex)
            {
                failureMessages.Add($"Error processing week starting: {ex.Message}");
                return new UpdateScheduledDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"An error occurred while processing: {ex.Message}",
                    SuccessCount = successCount,
                    FailureCount = failureMessages.Count,
                    FailureMessages = failureMessages
                };
            }
        }
    }
}
