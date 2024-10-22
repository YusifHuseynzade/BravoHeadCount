using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ScheduledDataDetails.Queries.Request;
using ScheduledDataDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledDataDetails.Handlers.QueryHandlers
{
    public class GetAllScheduledDataQueryHandler : IRequestHandler<GetAllScheduledDataQueryRequest, List<GetScheduledDataListResponse>>
    {
        private readonly IScheduledDataRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllScheduledDataQueryHandler(
            IScheduledDataRepository repository,
            IMapper mapper,
            IProjectRepository projectRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetScheduledDataListResponse>> Handle(GetAllScheduledDataQueryRequest request, CancellationToken cancellationToken)
        {
            request.NormalizeDates();

            var userEmail = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
                throw new UnauthorizedAccessException("Kullanıcı emaili bulunamadı.");

            var loggedInUserProjects = await _projectRepository
                .GetAllAsync(p => p.StoreManagerMail == userEmail);

            if (!loggedInUserProjects.Any())
                return new List<GetScheduledDataListResponse>();

            var projectIds = loggedInUserProjects.Select(p => p.Id).ToList();

            var scheduledDataList = await _repository.GetAll(sd => projectIds.Contains(sd.ProjectId))
                .AsNoTracking()
                .Include(sd => sd.Plan)
                .Include(sd => sd.Fact)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Position)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Section)
                .Include(sd => sd.Project)
                .ToListAsync(cancellationToken);

            // SectionId ve Search filtrelerini ekliyoruz
            if (request.SectionId.HasValue)
            {
                scheduledDataList = scheduledDataList
                    .Where(sd => sd.Employee.Section.Id == request.SectionId.Value)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                var searchLower = request.Search.ToLower();
                scheduledDataList = scheduledDataList
                    .Where(sd =>
                        (sd.Employee.FullName?.ToLower().Contains(searchLower) ?? false) ||
                        (sd.Employee.Badge?.ToLower().Contains(searchLower) ?? false) ||
                        (sd.Employee.Position?.Name?.ToLower().Contains(searchLower) ?? false))
                    .ToList();
            }

            // TargetDate'e göre 4 saat çıkma işlemi
            if (request.TargetDate.HasValue)
            {
                var targetDate = request.TargetDate.Value.AddHours(4).Date; // 4 saat çıkararak normalize et

                scheduledDataList = scheduledDataList
                    .Where(sd => sd.Date.Date == targetDate)
                    .ToList();

                if (!string.IsNullOrEmpty(request.StartHour) && !string.IsNullOrEmpty(request.EndHour))
                {
                    if (TimeSpan.TryParse(request.StartHour, out var startHour) &&
                        TimeSpan.TryParse(request.EndHour, out var endHour))
                    {
                        scheduledDataList = scheduledDataList
                            .Where(sd =>
                                sd.Plan != null &&
                                !string.IsNullOrEmpty(sd.Plan.Value) &&
                                ParsePlanTimeRange(sd.Plan.Value, out var planStart, out var planEnd) &&
                                TimeRangeOverlap(planStart, planEnd, startHour, endHour))
                            .ToList();
                    }
                    else
                    {
                        throw new ArgumentException("Geçerli bir saat formatı sağlanmadı.");
                    }
                }
            }
            else if (request.WeekDate.HasValue)
            {
                var weekStartDate = request.WeekDate.Value.Date.AddHours(4);
                var startOfWeek = weekStartDate.AddDays(-(int)weekStartDate.DayOfWeek); // Pazartesi başlangıç
                var endOfWeek = startOfWeek.AddDays(7);

                scheduledDataList = scheduledDataList
                    .Where(sd => sd.Date >= startOfWeek && sd.Date <= endOfWeek)
                    .ToList();
            }

            var groupedData = scheduledDataList
                .GroupBy(sd => sd.EmployeeId)
                .Select(g => new GroupedScheduledDataDto
                {
                    Employee = g.FirstOrDefault()?.Employee,
                    ScheduledDataList = g.OrderBy(sd => sd.Id).ToList()
                })
                .ToList();

            var response = new List<GetAllScheduledDataQueryResponse>();

            foreach (var group in groupedData)
            {
                var mappedResponse = _mapper.Map<GetAllScheduledDataQueryResponse>(group);

                mappedResponse.MorningShiftCount = group.ScheduledDataList
                    .Count(sd => sd.Plan != null && sd.Plan.Shift == "Səhər");

                mappedResponse.AfterNoonShiftCount = group.ScheduledDataList
                    .Count(sd => sd.Plan != null && sd.Plan.Shift == "Günorta");

                mappedResponse.EveningShiftCount = group.ScheduledDataList
                    .Count(sd => sd.Plan != null && sd.Plan.Shift == "Gecə");

                mappedResponse.DayOffCount = group.ScheduledDataList
                    .Count(sd => sd.Plan != null && sd.Plan.Shift == "Day Off");

                response.Add(mappedResponse);
            }

            var totalMorning = response.Sum(r => r.MorningShiftCount);
            var totalAfterNoon = response.Sum(r => r.AfterNoonShiftCount);
            var totalEvening = response.Sum(r => r.EveningShiftCount);
            var totalDayOff = response.Sum(r => r.DayOffCount);
            var totalHoliday = scheduledDataList.Count(sd => sd.Plan?.Shift == "Bayram");
            var totalVacation = scheduledDataList.Count(sd => sd.Plan?.Shift == "Məzuniyyət");
            var totalShifts = totalMorning + totalAfterNoon + totalEvening;

            string FormatPercentage(int value) =>
    totalShifts > 0 ? $"{(value * 100) / totalShifts}%" : "0%";

            var result = new GetScheduledDataListResponse
            {
                TotalScheduledDataCount = groupedData.Count,
                ProjectName = loggedInUserProjects.FirstOrDefault()?.ProjectName,
                Sections = groupedData.Select(g => g.Employee.Section.Name).Distinct().ToList(),
                Week = ISOWeek.GetWeekOfYear((request.WeekDate ?? DateTime.UtcNow).AddHours(4)),
                WeeklyMorningShiftCount = totalMorning,
                WeeklyAfterNoonShiftCount = totalAfterNoon,
                WeeklyEveningShiftCount = totalEvening,
                WeeklyMorningShiftPercentage = FormatPercentage(totalMorning),
                WeeklyAfterNoonShiftPercentage = FormatPercentage(totalAfterNoon),
                WeeklyEveningShiftPercentage = FormatPercentage(totalEvening),

                // İzin, bayram ve tatil sayıları
                WeeklyDayOffCount = totalDayOff,
                WeeklyHolidayCount = totalHoliday,
                WeeklyVacationCount = totalVacation,
                ScheduledDatas = response
            };

            if (request.ShowMore != null)
            {
                var skip = (request.Page - 1) * request.ShowMore.Take;
                result.ScheduledDatas = result.ScheduledDatas.Skip(skip).Take(request.ShowMore.Take).ToList();
            }

            return new List<GetScheduledDataListResponse> { result };
        }

        private bool ParsePlanTimeRange(string value, out TimeSpan planStart, out TimeSpan planEnd)
        {
            planStart = TimeSpan.Zero;
            planEnd = TimeSpan.Zero;

            if (string.IsNullOrEmpty(value))
                return false;

            var times = value.Split('-');
            if (times.Length != 2)
                return false;

            return TimeSpan.TryParse(times[0], out planStart) &&
                   TimeSpan.TryParse(times[1], out planEnd);
        }

        private bool TimeRangeOverlap(TimeSpan planStart, TimeSpan planEnd, TimeSpan startHour, TimeSpan endHour)
        {
            return planStart >= startHour && planEnd <= endHour;
        }

    }
}
