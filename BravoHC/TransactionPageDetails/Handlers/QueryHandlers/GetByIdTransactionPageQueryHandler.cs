using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using TransactionPageDetails.Queries.Request;
using TransactionPageDetails.Queries.Response;

namespace TransactionPageDetails.Handlers.QueryHandlers;

public class GetByIdTransactionPageQueryHandler : IRequestHandler<GetByIdTransactionPageQueryRequest, GetByIdTransactionPageQueryResponse>
{
    private readonly ITransactionPageRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdTransactionPageQueryHandler(ITransactionPageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdTransactionPageQueryResponse> Handle(GetByIdTransactionPageQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var Transaction = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (Transaction != null)
            {
                var response = _mapper.Map<GetByIdTransactionPageQueryResponse>(Transaction);

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
