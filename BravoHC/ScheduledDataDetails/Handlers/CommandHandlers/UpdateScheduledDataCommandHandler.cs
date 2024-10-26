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

                   
                    // Şu anki yerel tarihi al
                    var today = DateTime.Now.Date;
                    // Pazartesi'yi haftanın ilk günü olarak kabul ederek hafta başlangıcını belirle
                    var currentWeekStart = today.AddDays(-(int)(today.DayOfWeek == DayOfWeek.Sunday ? 6 : today.DayOfWeek - DayOfWeek.Monday));
                    // Haftanın bitiş günü (Pazar)
                    var currentWeekEnd = currentWeekStart.AddDays(6);

                    // Haftanın başlangıcını ve bitişini belirle
                    var referenceDate = scheduledDataList.First().Date;
                    var weekStart = referenceDate.AddDays(-(int)referenceDate.DayOfWeek + 1).Date;
                    var weekEnd = weekStart.AddDays(6);

                    // Geçmiş haftaya güncelleme kontrolü
                    if (weekEnd < currentWeekStart)
                    {
                        failureMessages.Add($"Updates for the previous week ending {weekEnd:yyyy-MM-dd} are not allowed.");
                        continue;
                    }

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

                    bool isValidSchedule = true;
                    for (int i = 0; i < weeklyUpdate.WeeklyUpdates.Count - 1; i++)
                    {
                        var currentDay = weeklyUpdate.WeeklyUpdates[i];
                        var nextDay = weeklyUpdate.WeeklyUpdates[i + 1];

                        var currentPlan = await _planRepository.GetByIdAsync(currentDay.PlanId);
                        var nextPlan = await _planRepository.GetByIdAsync(nextDay.PlanId);

                        if (currentPlan != null && nextPlan != null)
                        {
                            // Eğer özel günlerden biri ise (Day Off, Məzuniyyət vs.), fark kontrolü atlanır
                            if (IsSpecialPlan(currentPlan.Value) || IsSpecialPlan(nextPlan.Value))
                            {
                                continue; // Bu günler için zaman farkı kontrol edilmez
                            }

                            // Saat aralıklarını ayrıştır
                            var currentEndTime = TimeSpan.Parse(currentPlan.Value.Split('-')[1]);
                            var nextStartTime = TimeSpan.Parse(nextPlan.Value.Split('-')[0]);

                            // Gece yarısını geçme durumunu kontrol et
                            double timeDifference;
                            if (currentEndTime > nextStartTime)
                            {
                                timeDifference = (TimeSpan.FromHours(24) - currentEndTime + nextStartTime).TotalHours;
                            }
                            else
                            {
                                timeDifference = (nextStartTime - currentEndTime).TotalHours;
                            }

                            // Fark 12 saatten azsa geçersiz program
                            if (timeDifference < 12)
                            {
                                isValidSchedule = false;
                                failureMessages.Add($"The time difference between the end of day {i + 1} and the start of day {i + 2} must be at least 12 hours.");
                                break;
                            }
                        }
                    }

                   

                    if (!isValidSchedule)
                    {
                        failureMessages.Add($"Invalid schedule for the week starting {weekStart:yyyy-MM-dd} due to insufficient time gap between shifts.");
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
        // Özel planları kontrol eden yardımcı metot
        private bool IsSpecialPlan(string planValue)
        {
            return planValue == "Day Off" || planValue == "Məzuniyyət" || planValue == "Xəstəlik vərəqi" || planValue == "Bayram";
        }
    }
}
