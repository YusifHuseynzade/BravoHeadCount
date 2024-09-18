using AutoMapper;
using BakuTargetDetails.Queries.Request;
using BakuTargetDetails.Queries.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BakuTargetDetails.Handlers.QueryHandlers;

public class GetByIdBakuTargetQueryHandler : IRequestHandler<GetByIdBakuTargetQueryRequest, GetByIdBakuTargetQueryResponse>
{
    private readonly IBakuTargetRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdBakuTargetQueryHandler(IBakuTargetRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdBakuTargetQueryResponse> Handle(GetByIdBakuTargetQueryRequest request, CancellationToken cancellationToken)
    {
        var bakuTargets = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (bakuTargets != null)
        {
            var response = _mapper.Map<GetByIdBakuTargetQueryResponse>(bakuTargets);
            return response;
        }

        return new GetByIdBakuTargetQueryResponse();

    }
}
