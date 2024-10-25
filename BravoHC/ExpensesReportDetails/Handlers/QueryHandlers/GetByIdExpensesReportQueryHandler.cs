using AutoMapper;
using Domain.IRepositories;
using ExpensesReportDetails.Queries.Request;
using ExpensesReportDetails.Queries.Response;
using MediatR;

namespace ExpensesReportDetails.Handlers.QueryHandlers;

public class GetByIdExpensesReportQueryHandler : IRequestHandler<GetByIdExpensesReportQueryRequest, GetByIdExpensesReportQueryResponse>
{
    private readonly IExpensesReportRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdExpensesReportQueryHandler(IExpensesReportRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdExpensesReportQueryResponse> Handle(GetByIdExpensesReportQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var ExpensesReport = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "Attachments" }
              );

            if (ExpensesReport != null)
            {
                var response = _mapper.Map<GetByIdExpensesReportQueryResponse>(ExpensesReport);

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
