using AutoMapper;
using DCStockDetails.Queries.Request;
using DCStockDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace DCStockDetails.Handlers.QueryHandlers;

public class GetByIdDCStockQueryHandler : IRequestHandler<GetByIdDCStockQueryRequest, GetByIdDCStockQueryResponse>
{
    private readonly IDCStockRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdDCStockQueryHandler(IDCStockRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdDCStockQueryResponse> Handle(GetByIdDCStockQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var DCStock = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (DCStock != null)
            {
                var response = _mapper.Map<GetByIdDCStockQueryResponse>(DCStock);

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
