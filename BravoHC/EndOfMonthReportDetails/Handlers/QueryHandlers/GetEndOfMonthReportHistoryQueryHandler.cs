using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using EndOfMonthReportDetails.Queries.Request;
using EndOfMonthReportDetails.Queries.Response;
using MediatR;

namespace EndOfMonthReportDetails.Handlers.QueryHandlers
{
    public class GetEndOfMonthReportHistoryQueryHandler : IRequestHandler<GetEndOfMonthReportHistoryQueryRequest, List<GetListEndOfMonthReportHistoryQueryResponse>>
    {
        private readonly IEndOfMonthReportHistoryRepository _endOfMonthReportHistoryRepository;
        private readonly IMapper _mapper;

        public GetEndOfMonthReportHistoryQueryHandler(IEndOfMonthReportHistoryRepository endOfMonthReportHistoryRepository, IMapper mapper)
        {
            _endOfMonthReportHistoryRepository = endOfMonthReportHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListEndOfMonthReportHistoryQueryResponse>> Handle(GetEndOfMonthReportHistoryQueryRequest request, CancellationToken cancellationToken)
        {
           
            var endOfMonthReportHistoriesQuery = _endOfMonthReportHistoryRepository.GetAll(x => x.EndOfMonthReportId == request.EndOfMonthReportId);

            
            var endOfMonthReportHistories = endOfMonthReportHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetEndOfMonthReportHistoryQueryResponse>>(endOfMonthReportHistories);

            // Pagination işlemi (ShowMore)
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take).ToList();
            }

            var totalCount = endOfMonthReportHistories.Count();

            // PaginationListDto kullanarak veriyi dön
            PaginationListDto<GetEndOfMonthReportHistoryQueryResponse> model =
                   new PaginationListDto<GetEndOfMonthReportHistoryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetListEndOfMonthReportHistoryQueryResponse>
            {
                new GetListEndOfMonthReportHistoryQueryResponse
                {
                    TotalEndOfMonthReportHistoryCount = totalCount,
                    EndOfMonthReportHistories = model.Items
                }
            };
        }
    }
}
