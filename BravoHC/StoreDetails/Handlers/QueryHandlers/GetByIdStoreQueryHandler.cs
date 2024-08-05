using AutoMapper;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Queries.Request;
using StoreDetails.Queries.Response;

namespace StoreDetails.Handlers.QueryHandlers;

public class GetByIdStoreQueryHandler : IRequestHandler<GetByIdStoreQueryRequest, GetByIdStoreQueryResponse>
{
    private readonly IStoreRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdStoreQueryHandler(IStoreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdStoreQueryResponse> Handle(GetByIdStoreQueryRequest request, CancellationToken cancellationToken)
    {
        var store = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (store != null)
        {
            var response = _mapper.Map<GetByIdStoreQueryResponse>(store);
            return response;
        }

        return new GetByIdStoreQueryResponse();

    }
}