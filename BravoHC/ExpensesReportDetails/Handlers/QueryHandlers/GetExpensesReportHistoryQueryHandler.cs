using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using ExpensesReportDetails.Queries.Request;
using ExpensesReportDetails.Queries.Response;
using MediatR;

namespace ExpensesReportDetails.Handlers.QueryHandlers
{
    public class GetExpensesReportHistoryQueryHandler : IRequestHandler<GetExpensesReportHistoryQueryRequest, List<GetListExpensesReportHistoryQueryResponse>>
    {
        private readonly IExpensesReportHistoryRepository _expensesReportHistoryRepository;
        private readonly IMapper _mapper;

        public GetExpensesReportHistoryQueryHandler(IExpensesReportHistoryRepository expensesReportHistoryRepository, IMapper mapper)
        {
            _expensesReportHistoryRepository = expensesReportHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListExpensesReportHistoryQueryResponse>> Handle(GetExpensesReportHistoryQueryRequest request, CancellationToken cancellationToken)
        {

            var expensesReportHistoriesQuery = _expensesReportHistoryRepository.GetAll(x => x.ExpensesReportId == request.ExpensesReportId);


            var expensesReportHistories = expensesReportHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetExpensesReportHistoryQueryResponse>>(expensesReportHistories);

            // Pagination işlemi (ShowMore)
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take).ToList();
            }

            var totalCount = expensesReportHistories.Count();

            // PaginationListDto kullanarak veriyi dön
            PaginationListDto<GetExpensesReportHistoryQueryResponse> model =
                   new PaginationListDto<GetExpensesReportHistoryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetListExpensesReportHistoryQueryResponse>
            {
                new GetListExpensesReportHistoryQueryResponse
                {
                    TotalExpensesReportHistoryCount = totalCount,
                    ExpensesReportHistories = model.Items
                }
            };
        }
    }
}
