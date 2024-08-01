using AutoMapper;
using Domain.IRepositories;
using MediatR;
using SectionDetails.Queries.Request;
using SectionDetails.Queries.Response;

namespace SectionDetails.Handlers.QueryHandlers;

public class GetByIdSectionQueryHandler : IRequestHandler<GetByIdSectionQueryRequest, GetByIdSectionQueryResponse>
{
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdSectionQueryHandler(ISectionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdSectionQueryResponse> Handle(GetByIdSectionQueryRequest request, CancellationToken cancellationToken)
    {
        var sections = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (sections != null)
        {
            var response = _mapper.Map<GetByIdSectionQueryResponse>(sections);
            return response;
        }

        return new GetByIdSectionQueryResponse();

    }
}