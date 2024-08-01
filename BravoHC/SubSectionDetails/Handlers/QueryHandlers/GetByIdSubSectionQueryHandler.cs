using AutoMapper;
using Domain.IRepositories;
using MediatR;
using SubSectionDetails.Queries.Request;
using SubSectionDetails.Queries.Response;

namespace SubSectionDetails.Handlers.QueryHandlers;

public class GetByIdSubSectionQueryHandler : IRequestHandler<GetByIdSubSectionQueryRequest, GetByIdSubSectionQueryResponse>
{
    private readonly ISubSectionRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdSubSectionQueryHandler(ISubSectionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdSubSectionQueryResponse> Handle(GetByIdSubSectionQueryRequest request, CancellationToken cancellationToken)
    {
        var subSections = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (subSections != null)
        {
            var response = _mapper.Map<GetByIdSubSectionQueryResponse>(subSections);
            return response;
        }

        return new GetByIdSubSectionQueryResponse();

    }
}
