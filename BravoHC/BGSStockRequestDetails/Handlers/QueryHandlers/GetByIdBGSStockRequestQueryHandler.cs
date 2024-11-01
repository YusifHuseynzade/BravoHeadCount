using AutoMapper;
using BGSStockRequestDetails.Queries.Request;
using BGSStockRequestDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace BGSStockRequestDetails.Handlers.QueryHandlers;

public class GetByIdBGSStockRequestQueryHandler : IRequestHandler<GetByIdBGSStockRequestQueryRequest, GetByIdBGSStockRequestQueryResponse>
{
    private readonly IBGSStockRequestRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdBGSStockRequestQueryHandler(IBGSStockRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdBGSStockRequestQueryResponse> Handle(GetByIdBGSStockRequestQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var BGSStockRequest = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (BGSStockRequest != null)
            {
                var response = _mapper.Map<GetByIdBGSStockRequestQueryResponse>(BGSStockRequest);

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
