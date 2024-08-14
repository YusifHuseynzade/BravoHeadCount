using AutoMapper;
using Domain.IRepositories;
using MediatR;
using ResidentalAreaDetails.Queries.Request;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Handlers.QueryHandlers;

public class GetByIdResidentalAreaQueryHandler : IRequestHandler<GetByIdResidentalAreaQueryRequest, GetByIdResidentalAreaQueryResponse>
{
    private readonly IResidentalAreaRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdResidentalAreaQueryHandler(IResidentalAreaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdResidentalAreaQueryResponse> Handle(GetByIdResidentalAreaQueryRequest request, CancellationToken cancellationToken)
    {
        var residentalAreas = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (residentalAreas != null)
        {
            var response = _mapper.Map<GetByIdResidentalAreaQueryResponse>(residentalAreas);
            return response;
        }

        return new GetByIdResidentalAreaQueryResponse();

    }
}
