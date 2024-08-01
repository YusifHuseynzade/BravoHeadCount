using AutoMapper;
using Domain.IRepositories;
using FunctionalAreaDetails.Queries.Request;
using FunctionalAreaDetails.Queries.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.QueryHandlers;

public class GetByIdFunctionalAreaQueryHandler : IRequestHandler<GetByIdFunctionalAreaQueryRequest, GetByIdFunctionalAreaQueryResponse>
{
    private readonly IFunctionalAreaRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdFunctionalAreaQueryHandler(IFunctionalAreaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdFunctionalAreaQueryResponse> Handle(GetByIdFunctionalAreaQueryRequest request, CancellationToken cancellationToken)
    {
        var functionalAreas = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (functionalAreas != null)
        {
            var response = _mapper.Map<GetByIdFunctionalAreaQueryResponse>(functionalAreas);
            return response;
        }

        return new GetByIdFunctionalAreaQueryResponse();

    }
}
