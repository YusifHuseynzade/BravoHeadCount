using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Commands.Request;
using ScheduledDataDetails.Commands.Response;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ScheduledDataDetailsHandlers.CommandHandlers
{
    public class CreateScheduledDataCommandHandler : IRequestHandler<CreateScheduledDataCommandRequest, CreateScheduledDataCommandResponse>
    {
        private readonly IScheduledDataRepository _scheduledDataRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVacationScheduleRepository _vacationScheduleRepository;
        private readonly ISickLeaveRepository _sickLeaveRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateScheduledDataCommandHandler(
            IScheduledDataRepository scheduledDataRepository,
            IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository,
            IVacationScheduleRepository vacationScheduleRepository,
            ISickLeaveRepository sickLeaveRepository,
            IPlanRepository planRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _scheduledDataRepository = scheduledDataRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _vacationScheduleRepository = vacationScheduleRepository;
            _sickLeaveRepository = sickLeaveRepository;
            _planRepository = planRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateScheduledDataCommandResponse> Handle(CreateScheduledDataCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kullanıcının e-posta adresini al
                var userEmail = _httpContextAccessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    return new CreateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "User email not found."
                    };
                }

                // Kullanıcıya ait projeleri al
                var userProjects = await _projectRepository.GetAllAsync(p => p.StoreManagerMail == userEmail);
                if (!userProjects.Any())
                {
                    return new CreateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "No projects found for the user."
                    };
                }

                // Yalnızca ilgili projeler için çalışanları filtrele
                var projectIds = userProjects.Select(p => p.Id).ToList();

                // Veritabanındaki en son tarihli ScheduledData'yı al
                var lastScheduledData = await _scheduledDataRepository.GetLastScheduledDataAsync();
                DateTime nextMonday;
                DateTime nextSunday;

                if (lastScheduledData != null)
                {
                    nextMonday = lastScheduledData.Date.AddDays(1);
                    nextSunday = nextMonday.AddDays(6);
                }
                else
                {
                    var currentDate = DateTime.UtcNow;
                    nextMonday = currentDate.AddDays(8 - (int)currentDate.DayOfWeek);
                    nextSunday = nextMonday.AddDays(6);
                }

                var existingData = await _scheduledDataRepository.GetByDateRangeAsync(nextMonday, nextSunday);
                if (existingData.Any())
                {
                    return new CreateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Scheduled data already exists for the specified date range."
                    };
                }

                var vacationPlan = await _planRepository.GetByValueAsync("Məzuniyyət");
                var sickLeavePlan = await _planRepository.GetByValueAsync("Xəstəlik vərəqi");

                if (vacationPlan == null || sickLeavePlan == null)
                {
                    return new CreateScheduledDataCommandResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "'Məzuniyyət' or 'Xəstəlik vərəqi' plan not found."
                    };
                }

                foreach (var projectId in projectIds)
                {
                    var employees = await _employeeRepository.GetAllAsync(e => e.ProjectId == projectId);
                    foreach (var employee in employees)
                    {
                        var vacationSchedule = await _vacationScheduleRepository.GetByEmployeeIdAsync(employee.Id);
                        var sickLeave = await _sickLeaveRepository.GetByEmployeeIdAsync(employee.Id);

                        for (var date = nextMonday; date <= nextSunday; date = date.AddDays(1))
                        {
                            int? planId = null;

                            if (vacationSchedule != null && vacationSchedule.StartDate <= date && vacationSchedule.EndDate >= date)
                            {
                                planId = vacationPlan.Id;
                            }
                            else if (sickLeave != null && sickLeave.StartDate <= date && sickLeave.EndDate >= date)
                            {
                                planId = sickLeavePlan.Id;
                            }

                            var scheduledData = new ScheduledData
                            {
                                EmployeeId = employee.Id,
                                ProjectId = projectId,
                                Date = date.Date,
                                PlanId = planId
                            };

                            await _scheduledDataRepository.AddAsync(scheduledData);
                        }
                    }
                }

                await _scheduledDataRepository.CommitAsync();

                return new CreateScheduledDataCommandResponse { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new CreateScheduledDataCommandResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred while creating scheduled data: {ex.Message} - {ex.InnerException?.Message}"
                };
            }
        }
    }
}
