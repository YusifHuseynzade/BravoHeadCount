using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using EmployeeDetails.Queries.Request;
using EmployeeDetails.Queries.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.QueryHandlers
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQueryRequest, List<GetEmployeeListResponse>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeeQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeListResponse>> Handle(GetAllEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            // Tüm ilişkisel entity'leri Include ederek sorguyu genişletiyoruz
            IQueryable<Employee> employeesQuery = _repository.GetAll(x => true)
                .Include(x => x.ResidentalArea)
                .Include(x => x.BakuDistrict)
                .Include(x => x.BakuMetro)
                .Include(x => x.BakuTarget)
                .Include(x => x.Project)
                .Include(x => x.Position)
                .Include(x => x.Section)
                .Include(x => x.SubSection);

            // Search filtresi varsa
            if (!string.IsNullOrEmpty(request.Search))
            {
                employeesQuery = employeesQuery.Where(x =>
                    x.Badge.Contains(request.Search) ||
                    x.FullName.Contains(request.Search));
            }

            // Toplam çalışan sayısını hesaplıyoruz (filtrelere göre)
            var totalCount = await employeesQuery.CountAsync(cancellationToken);

            // Sayfalama işlemi yapılacaksa (ShowMore parametresi varsa)
            if (request.ShowMore != null)
            {
                employeesQuery = employeesQuery
                    .Skip((request.Page - 1) * request.ShowMore.Take)
                    .Take(request.ShowMore.Take);
            }

            // Veritabanından sonuçları çekiyoruz
            var employees = await employeesQuery.ToListAsync(cancellationToken);

            // Elde edilen verileri DTO'ya mapliyoruz
            var response = _mapper.Map<List<GetAllEmployeeQueryResponse>>(employees);

            // Sonuç modelini oluşturuyoruz
            PaginationListDto<GetAllEmployeeQueryResponse> model =
                   new PaginationListDto<GetAllEmployeeQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetEmployeeListResponse>
            {
                new GetEmployeeListResponse
                {
                    TotalEmployeeCount = totalCount,
                    Employees = model.Items
                }
            };
        }
    }
}
