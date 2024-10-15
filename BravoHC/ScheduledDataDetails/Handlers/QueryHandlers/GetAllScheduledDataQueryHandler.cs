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

        public GetAllScheduledDataQueryHandler(IScheduledDataRepository repository,
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

            // Oturum açan kullanıcının email bilgisi
            var userEmail = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("Kullanıcı emaili bulunamadı.");
            }

            // Kullanıcı bilgilerini veritabanından alıyoruz
            var loggedInUser = await _userRepository.GetLoggedInUserAsync(userEmail);

            // Kullanıcının projelerini filtrele
            var projects = await _projectRepository.GetAllAsync(x =>
                x.StoreManagerMail == userEmail);

            // Eğer kullanıcının projeleri yoksa boş bir liste döndür
            if (!projects.Any())
            {
                return new List<GetScheduledDataListResponse>();
            }

            // Proje ID'lerini filtrele
            var projectIds = projects.Select(p => p.Id).ToList();

            // Filtreleme işlemi
            var scheduledDatasQuery = _repository.GetAll(sd => projectIds.Contains(sd.ProjectId))
                .AsNoTracking() // İzlemeyi devre dışı bırakma
                .Include(sd => sd.Plan)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Position)
                .Include(sd => sd.Employee)
                    .ThenInclude(e => e.Section)
                .Include(sd => sd.Project)
                .OrderBy(sd => sd.Id)
                .AsQueryable();

            // Eğer tarih filtresi verilmemişse geçerli haftayı kullan
            var dateToFilter = request.WeekDate ?? DateTime.UtcNow;

            // Haftanın başını (Pazartesi) ve sonunu (Pazar) hesapla
            var startOfWeek = dateToFilter.AddDays(-(int)dateToFilter.DayOfWeek + (int)DayOfWeek.Sunday);
            var endOfWeek = startOfWeek.AddDays(7);

            // Haftanın başı ve sonu arasındaki verileri filtrele
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

            // Verileri listeye dönüştür
            var scheduledDatas = await scheduledDatasQuery.ToListAsync(cancellationToken);

            if (scheduledDatas.Any())
            {
                var response = _mapper.Map<List<GetAllScheduledDataQueryResponse>>(scheduledDatas);

                // Pagination işlemi ve show more
                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                // Toplam veri sayısını al
                var totalCount = scheduledDatas.Count;

                // Pagination model oluştur
                PaginationListDto<GetAllScheduledDataQueryResponse> model =
                       new PaginationListDto<GetAllScheduledDataQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                // Response hazırlama
                return new List<GetScheduledDataListResponse>
                {
                    new GetScheduledDataListResponse
                    {
                        TotalScheduledDataCount = totalCount,
                        ScheduledDatas = model.Items
                    }
                };
            }

            return new List<GetScheduledDataListResponse>();
        }
    }
}
