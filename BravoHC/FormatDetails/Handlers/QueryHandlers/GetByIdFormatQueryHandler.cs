using AutoMapper;
using Domain.IRepositories;
using FormatDetails.Queries.Request;
using FormatDetails.Queries.Response;
using MediatR;

namespace FormatDetails.Handlers.QueryHandlers;

public class GetByIdFormatQueryHandler : IRequestHandler<GetByIdFormatQueryRequest, GetByIdFormatQueryResponse>
{
    private readonly IFormatRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdFormatQueryHandler(IFormatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdFormatQueryResponse> Handle(GetByIdFormatQueryRequest request, CancellationToken cancellationToken)
    {
        var formats = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (formats != null)
        {
            var response = _mapper.Map<GetByIdFormatQueryResponse>(formats);
            return response;
        }

        return new GetByIdFormatQueryResponse();

    }
}