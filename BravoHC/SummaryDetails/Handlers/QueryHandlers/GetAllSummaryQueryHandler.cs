using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SummaryDetails.Queries.Request;
using SummaryDetails.Queries.Response;

namespace SummaryDetails.Handlers.QueryHandlers
{
    public class GetAllSummaryQueryHandler : IRequestHandler<GetAllSummaryQueryRequest, List<GetAllSummaryListQueryResponse>>
    {
        private readonly ISummaryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSummaryQueryHandler(ISummaryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllSummaryListQueryResponse>> Handle(GetAllSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            // Get all summaries and include related entities
            var summariesQuery = _repository.GetAll(x => true)
                .Include(s => s.Employee)
                    .ThenInclude(e => e.Position)
                .Include(s => s.Employee)
                    .ThenInclude(e => e.Section)
                .Include(s => s.Month).AsQueryable();

            // Apply year filter if provided
            if (request.Year.HasValue)
            {
                summariesQuery = summariesQuery.Where(s => s.Year == request.Year.Value);
            }

            if (request.Month.HasValue)
            {
                summariesQuery = summariesQuery.Where(s => s.Month != null && s.Month.Number == request.Month.Value);
            }

            summariesQuery = summariesQuery.OrderBy(s => s.Id).ThenBy(s => s.Month.Number);

            var summaries = await summariesQuery.ToListAsync(cancellationToken);


            if (summaries != null)
            {
                var response = _mapper.Map<List<GetAllSummaryQueryResponse>>(summaries);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = summaries.Count();

                PaginationListDto<GetAllSummaryQueryResponse> model =
                       new PaginationListDto<GetAllSummaryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllSummaryListQueryResponse>
                {
                    new GetAllSummaryListQueryResponse
                    {
                        TotalVacationScheduleCount = totalCount,
                        VacationSchedules = model.Items
                    }
                };
            }

            return new List<GetAllSummaryListQueryResponse>();
        }
    }
}
