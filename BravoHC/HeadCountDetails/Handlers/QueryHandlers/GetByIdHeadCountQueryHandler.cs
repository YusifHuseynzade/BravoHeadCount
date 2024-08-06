using AutoMapper;
using Domain.IRepositories;
using HeadCountDetails.Queries.Request;
using HeadCountDetails.Queries.Response;
using MediatR;

namespace HeadCountDetails.Handlers.QueryHandlers;

public class GetByIdHeadCountQueryHandler : IRequestHandler<GetByIdHeadCountQueryRequest, GetByIdHeadCountQueryResponse>
{
    private readonly IHeadCountRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdHeadCountQueryHandler(IHeadCountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdHeadCountQueryResponse> Handle(GetByIdHeadCountQueryRequest request, CancellationToken cancellationToken)
    {
        var headCount = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (headCount != null)
        {
            var response = _mapper.Map<GetByIdHeadCountQueryResponse>(headCount);
            return response;
        }

        return new GetByIdHeadCountQueryResponse();

    }
}