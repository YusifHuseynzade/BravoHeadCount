using AutoMapper;
using BakuMetroDetails.Queries.Request;
using BakuMetroDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuMetroDetails.Handlers.QueryHandlers;

public class GetByIdBakuMetroQueryHandler : IRequestHandler<GetByIdBakuMetroQueryRequest, GetByIdBakuMetroQueryResponse>
{
    private readonly IBakuMetroRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdBakuMetroQueryHandler(IBakuMetroRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdBakuMetroQueryResponse> Handle(GetByIdBakuMetroQueryRequest request, CancellationToken cancellationToken)
    {
        var bakuMetros = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (bakuMetros != null)
        {
            var response = _mapper.Map<GetByIdBakuMetroQueryResponse>(bakuMetros);
            return response;
        }

        return new GetByIdBakuMetroQueryResponse();

    }
}
