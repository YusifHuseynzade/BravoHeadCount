using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using EndOfMonthReportDetails.Queries.Request;
using EndOfMonthReportDetails.Queries.Response;
using MediatR;

namespace EndOfMonthReportDetails.Handlers.QueryHandlers
{
    public class GetAllEndOfMonthReportQueryHandler : IRequestHandler<GetAllEndOfMonthReportQueryRequest, List<GetAllEndOfMonthReportListQueryResponse>>
    {
        private readonly IEndOfMonthReportRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEndOfMonthReportQueryHandler(IEndOfMonthReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllEndOfMonthReportListQueryResponse>> Handle(GetAllEndOfMonthReportQueryRequest request, CancellationToken cancellationToken)
        {
            var EndOfMonthReports = _repository.GetAll(
            x => true,
            nameof(EndOfMonthReport.Project) // Project bilgisini include et
        );

            if (EndOfMonthReports != null)
            {
                var response = _mapper.Map<List<GetAllEndOfMonthReportQueryResponse>>(EndOfMonthReports);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = EndOfMonthReports.Count();

                PaginationListDto<GetAllEndOfMonthReportQueryResponse> model =
                       new PaginationListDto<GetAllEndOfMonthReportQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllEndOfMonthReportListQueryResponse>
                {
                    new GetAllEndOfMonthReportListQueryResponse
                    {
                        TotalEndOfMonthReportCount = totalCount,
                        EndOfMonthReports = model.Items
                    }
                };
            }

            return new List<GetAllEndOfMonthReportListQueryResponse>();
        }
    }
}
