using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using ExpensesReportDetails.Queries.Request;
using ExpensesReportDetails.Queries.Response;
using MediatR;

namespace ExpensesReportDetails.Handlers.QueryHandlers
{
    public class GetAllExpensesReportQueryHandler : IRequestHandler<GetAllExpensesReportQueryRequest, List<GetAllExpensesReportListQueryResponse>>
    {
        private readonly IExpensesReportRepository _repository;
        private readonly IMapper _mapper;

        public GetAllExpensesReportQueryHandler(IExpensesReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllExpensesReportListQueryResponse>> Handle(GetAllExpensesReportQueryRequest request, CancellationToken cancellationToken)
        {
            var ExpensesReports = _repository.GetAll(
                x => true,
                nameof(ExpensesReport.Project),
                nameof(ExpensesReport.Attachments)
            );

            if (ExpensesReports != null)
            {
                var response = _mapper.Map<List<GetAllExpensesReportQueryResponse>>(ExpensesReports);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = ExpensesReports.Count();

                PaginationListDto<GetAllExpensesReportQueryResponse> model =
                       new PaginationListDto<GetAllExpensesReportQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllExpensesReportListQueryResponse>
                {
                    new GetAllExpensesReportListQueryResponse
                    {
                        TotalExpensesReportCount = totalCount,
                        ExpensesReports = model.Items
                    }
                };
            }

            return new List<GetAllExpensesReportListQueryResponse>();
        }
    }
}
