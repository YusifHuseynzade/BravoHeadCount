using AutoMapper;
using Domain.IRepositories;
using EndOfMonthReportDetails.Queries.Request;
using EndOfMonthReportDetails.Queries.Response;
using MediatR;

namespace EndOfMonthReportDetails.Handlers.QueryHandlers;

public class GetByIdEndOfMonthReportQueryHandler : IRequestHandler<GetByIdEndOfMonthReportQueryRequest, GetByIdEndOfMonthReportQueryResponse>
{
    private readonly IEndOfMonthReportRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdEndOfMonthReportQueryHandler(IEndOfMonthReportRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdEndOfMonthReportQueryResponse> Handle(GetByIdEndOfMonthReportQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var EndOfMonthReport = await _repository.GetAsync(x => x.Id == request.Id);

            if (EndOfMonthReport != null)
            {
                var response = _mapper.Map<GetByIdEndOfMonthReportQueryResponse>(EndOfMonthReport);

                return response;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xəta oldu: {ex.Message}");
            throw;
        }
    }
}
