using AutoMapper;
using BakuDistrictDetails.Queries.Request;
using BakuDistrictDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace BakuDistrictDetails.Handlers.QueryHandlers;

public class GetByIdBakuDistrictQueryHandler : IRequestHandler<GetByIdBakuDistrictQueryRequest, GetByIdBakuDistrictQueryResponse>
{
    private readonly IResidentalAreaRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdBakuDistrictQueryHandler(IResidentalAreaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdBakuDistrictQueryResponse> Handle(GetByIdBakuDistrictQueryRequest request, CancellationToken cancellationToken)
    {
        var residentalAreas = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (residentalAreas != null)
        {
            var response = _mapper.Map<GetByIdBakuDistrictQueryResponse>(residentalAreas);
            return response;
        }

        return new GetByIdBakuDistrictQueryResponse();

    }
}
