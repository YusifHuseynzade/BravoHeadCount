using AutoMapper;
using Domain.IRepositories;
using MediatR;
using StoreStockRequestDetails.Queries.Request;
using StoreStockRequestDetails.Queries.Response;

namespace StoreStockRequestDetails.Handlers.QueryHandlers;

public class GetByIdStoreStockRequestQueryHandler : IRequestHandler<GetByIdStoreStockRequestQueryRequest, GetByIdStoreStockRequestQueryResponse>
{
    private readonly IStoreStockRequestRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdStoreStockRequestQueryHandler(IStoreStockRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdStoreStockRequestQueryResponse> Handle(GetByIdStoreStockRequestQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var StoreStockRequest = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (StoreStockRequest != null)
            {
                var response = _mapper.Map<GetByIdStoreStockRequestQueryResponse>(StoreStockRequest);

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
