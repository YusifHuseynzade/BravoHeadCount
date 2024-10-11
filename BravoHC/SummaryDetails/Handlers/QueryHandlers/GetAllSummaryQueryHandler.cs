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
            var summaries = _repository.GetAll(x => true).Include(s => s.Employee)
                .ThenInclude(e => e.Position)
                .Include(s => s.Employee)
                .ThenInclude(e => e.Section)
                .Include(s => s.Month);


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
