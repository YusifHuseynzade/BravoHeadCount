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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectRepository _projectRepository;
        private readonly IAppUserRepository _userRepository;

        public GetAllScheduledDataQueryHandler(
            IScheduledDataRepository repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IProjectRepository projectRepository,
            IAppUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<List<GetScheduledDataListResponse>> Handle(GetAllScheduledDataQueryRequest request, CancellationToken cancellationToken)
        {
            request.NormalizeDates();

            var userEmail = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("Kullanıcı emaili bulunamadı.");
            }

            var loggedInUser = await _userRepository.GetLoggedInUserAsync(userEmail);
            var projects = await _projectRepository.GetAllAsync(x => x.StoreManagerMail == userEmail);

            if (!projects.Any())
            {
                return new List<GetScheduledDataListResponse>();
            }

            var projectIds = projects.Select(p => p.Id).ToList();

            // Filtreleme işlemi
            var scheduledDatasQuery = _repository.GetAll(sd => projectIds.Contains(sd.ProjectId))
                .AsNoTracking()
                .Include(sd => sd.Plan)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Position)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Section)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.EmployeeBalances)
                .Include(sd => sd.Project)
                .AsQueryable();

            // Tarih filtresi
            var dateToFilter = request.WeekDate ?? DateTime.UtcNow;
            var startOfWeek = dateToFilter.AddDays(-(int)dateToFilter.DayOfWeek + (int)DayOfWeek.Sunday);
            var endOfWeek = startOfWeek.AddDays(7);

            scheduledDatasQuery = scheduledDatasQuery.Where(sd => sd.Date >= startOfWeek && sd.Date <= endOfWeek);

            // Filtreler: Section, Position, Badge ve FullName
            if (!string.IsNullOrEmpty(request.SectionName))
            {
                scheduledDatasQuery = scheduledDatasQuery.Where(sd => sd.Employee.Section.Name.Contains(request.SectionName));
            }

            if (!string.IsNullOrEmpty(request.PositionName))
            {
                scheduledDatasQuery = scheduledDatasQuery.Where(sd => sd.Employee.Position.Name.Contains(request.PositionName));
            }

            if (!string.IsNullOrEmpty(request.Badge))
            {
                scheduledDatasQuery = scheduledDatasQuery.Where(sd => sd.Employee.Badge.Contains(request.Badge));
            }

            if (!string.IsNullOrEmpty(request.FullName))
            {
                scheduledDatasQuery = scheduledDatasQuery.Where(sd => sd.Employee.FullName.Contains(request.FullName));
            }

            // Toplam sayıyı hesapla
            var totalCount = await scheduledDatasQuery
                .Select(sd => sd.EmployeeId)
                .Distinct()
                .CountAsync(cancellationToken); // Çalışan sayısını al

            // Çalışan başına gruplama işlemi
            var groupedData = await scheduledDatasQuery
                .GroupBy(sd => sd.EmployeeId)
                .Select(g => new GroupedScheduledDataDto
                {
                    Employee = g.FirstOrDefault().Employee,
                    ScheduledDataList = g.ToList()
                })
                .ToListAsync(cancellationToken); // Tüm verileri al

            // Şift sayılarını hesapla
            var response = new List<GetAllScheduledDataQueryResponse>();

            foreach (var group in groupedData)
            {
                var mappedResponse = _mapper.Map<GetAllScheduledDataQueryResponse>(group);

                // Null kontrolü ile şift sayısını hesaplıyoruz
                mappedResponse.MorningShiftCount = group.ScheduledDataList.Count(sd => sd.Plan != null && sd.Plan.Shift == "Səhər");
                mappedResponse.AfterNoonShiftCount = group.ScheduledDataList.Count(sd => sd.Plan != null && sd.Plan.Shift == "Günorta");
                mappedResponse.EveningShiftCount = group.ScheduledDataList.Count(sd => sd.Plan != null && sd.Plan.Shift == "Gecə");

                response.Add(mappedResponse);
            }

            // Page ve ShowMore parametrelerine göre işlem yapma
            if (request.ShowMore != null)
            {
                var skip = (request.Page - 1) * request.ShowMore.Take;
                response = response.Skip(skip).Take(request.ShowMore.Take).ToList();
            }

            // Boş liste döndürme kontrolü
            if (!response.Any())
            {
                return new List<GetScheduledDataListResponse>(); // Boş liste dönüyor
            }

            // Response hazırlama
            return new List<GetScheduledDataListResponse>
    {
        new GetScheduledDataListResponse
        {
            TotalScheduledDataCount = totalCount, // Toplam çalışan sayısını döndür
            ScheduledDatas = response
        }
    };
        }

    }
}
