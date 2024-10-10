using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VacationScheduleDetails.Queries.Request;
using VacationScheduleDetails.Queries.Response;

namespace VacationScheduleDetails.Handlers.QueryHandlers
{
    public class GetAllVacationScheduleQueryHandler : IRequestHandler<GetAllVacationScheduleQueryRequest, List<GetAllVacationScheduleListQueryResponse>>
    {
        private readonly IVacationScheduleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVacationScheduleQueryHandler(IVacationScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllVacationScheduleListQueryResponse>> Handle(GetAllVacationScheduleQueryRequest request, CancellationToken cancellationToken)
        {
            var vacationSchedules = _repository.GetAll(x => true).Include(s => s.Employee)
                .ThenInclude(e => e.Position)
                .Include(s => s.Employee)
                .ThenInclude(e => e.Section);


            if (vacationSchedules != null)
            {
                var response = _mapper.Map<List<GetAllVacationScheduleQueryResponse>>(vacationSchedules);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = vacationSchedules.Count();

                PaginationListDto<GetAllVacationScheduleQueryResponse> model =
                       new PaginationListDto<GetAllVacationScheduleQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllVacationScheduleListQueryResponse>
                {
                    new GetAllVacationScheduleListQueryResponse
                    {
                        TotalVacationScheduleCount = totalCount,
                        VacationSchedules = model.Items
                    }
                };
            }

            return new List<GetAllVacationScheduleListQueryResponse>();
        }
    }
}
