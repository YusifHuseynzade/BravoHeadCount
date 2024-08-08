using AutoMapper;
using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Queries.Request;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Handlers.QueryHandlers;

public class GetByIdScheduledDataQueryHandler : IRequestHandler<GetByIdScheduledDataQueryRequest, GetByIdScheduledDataQueryResponse>
{
    private readonly IScheduledDataRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdScheduledDataQueryHandler(IScheduledDataRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdScheduledDataQueryResponse> Handle(GetByIdScheduledDataQueryRequest request, CancellationToken cancellationToken)
    {
        var scheduledData = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (scheduledData != null)
        {
            var response = _mapper.Map<GetByIdScheduledDataQueryResponse>(scheduledData);
            return response;
        }

        return new GetByIdScheduledDataQueryResponse();

    }
}