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
            {
                throw new UnauthorizedAccessException("Kullanıcı emaili bulunamadı.");
            }

            var loggedInUserProjects = await _projectRepository
                .GetAllAsync(p => p.StoreManagerMail == userEmail);

            if (!loggedInUserProjects.Any())
            {
                return new List<GetScheduledDataListResponse>();
            }

            var projectIds = loggedInUserProjects.Select(p => p.Id).ToList();

            // Scheduled data sorgulaması
            var scheduledDatasQuery = _repository.GetAll(sd => projectIds.Contains(sd.ProjectId))
                .AsNoTracking()
                .Include(sd => sd.Plan)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Position)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Section)
                .Include(sd => sd.Project)
                .AsQueryable();

            // Tarih filtresi
            var weekStartDate = request.WeekDate ?? DateTime.UtcNow;
            var startOfWeek = weekStartDate.AddDays(-(int)weekStartDate.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            scheduledDatasQuery = scheduledDatasQuery
                .Where(sd => sd.Date >= startOfWeek && sd.Date <= endOfWeek);

            int weekNumber = ISOWeek.GetWeekOfYear(weekStartDate);

            // Filtreleme işlemi (Section ve Arama)
            if (request.SectionId.HasValue)
            {
                scheduledDatasQuery = scheduledDatasQuery
                    .Where(sd => sd.Employee.Section.Id == request.SectionId.Value);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                var searchTerm = request.Search.ToLower();
                scheduledDatasQuery = scheduledDatasQuery.Where(sd =>
                    sd.Employee.FullName.ToLower().Contains(searchTerm) ||
                    sd.Employee.Badge.ToLower().Contains(searchTerm) ||
                    sd.Employee.Position.Name.ToLower().Contains(searchTerm));
            }

            // Çalışan başına gruplama
            var groupedData = await scheduledDatasQuery
                .GroupBy(sd => sd.EmployeeId)
                .Select(g => new GroupedScheduledDataDto
                {
                    Employee = g.FirstOrDefault().Employee,
                    ScheduledDataList = g.ToList()
                })
                .ToListAsync(cancellationToken);

            // Response oluşturma
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

            // Toplam Haftalık İstatistikler ve Sections
            var totalMorning = response.Sum(r => r.MorningShiftCount);
            var totalAfterNoon = response.Sum(r => r.AfterNoonShiftCount);
            var totalEvening = response.Sum(r => r.EveningShiftCount);

            var totalShifts = totalMorning + totalAfterNoon + totalEvening;

            string FormatPercentage(int value) => $"{value}%";

            // Toplam Haftalık İstatistikler ve Sections
            var result = new GetScheduledDataListResponse
            {
                TotalScheduledDataCount = groupedData.Count,
                ProjectName = loggedInUserProjects.FirstOrDefault()?.ProjectName,
                Sections = groupedData.Select(g => g.Employee.Section.Name).Distinct().ToList(),
                Week = weekNumber,
                WeeklyMorningShiftCount = response.Sum(r => r.MorningShiftCount),
                WeeklyAfterNoonShiftCount = response.Sum(r => r.AfterNoonShiftCount),
                WeeklyEveningShiftCount = response.Sum(r => r.EveningShiftCount),
                WeeklyDayOffCount = scheduledDatasQuery.Count(sd => sd.Plan.Value == "Day Off"),
                WeeklyHolidayCount = scheduledDatasQuery.Count(sd => sd.Plan.Value == "Bayram"),
                WeeklyVacationCount = scheduledDatasQuery.Count(sd => sd.Plan.Value == "Məzuniyyət"),
                WeeklyMorningShiftPercentage = totalShifts > 0 ? FormatPercentage((int)Math.Round((double)(totalMorning * 100) / totalShifts)) : "0%",
                WeeklyAfterNoonShiftPercentage = totalShifts > 0 ? FormatPercentage((int)Math.Round((double)(totalAfterNoon * 100) / totalShifts)) : "0%",
                WeeklyEveningShiftPercentage = totalShifts > 0 ? FormatPercentage((int)Math.Round((double)(totalEvening * 100) / totalShifts)) : "0%",
                ScheduledDatas = response
            };

            // Sayfalama işlemi
            if (request.ShowMore != null)
            {
                var skip = (request.Page - 1) * request.ShowMore.Take;
                result.ScheduledDatas = result.ScheduledDatas.Skip(skip).Take(request.ShowMore.Take).ToList();
            }

            return new List<GetScheduledDataListResponse> { result };
        }
    }
}
