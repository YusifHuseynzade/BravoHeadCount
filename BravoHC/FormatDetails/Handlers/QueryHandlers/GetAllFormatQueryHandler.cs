using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using FormatDetails.Queries.Request;
using FormatDetails.Queries.Response;
using MediatR;

namespace FormatDetails.Handlers.QueryHandlers;

public class GetAllFormatQueryHandler : IRequestHandler<GetAllFormatQueryRequest, List<GetAllFormatQueryResponse>>
{
    private readonly IFormatRepository _repository;
    private readonly IMapper _mapper;

    public GetAllFormatQueryHandler(IFormatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetAllFormatQueryResponse>> Handle(GetAllFormatQueryRequest request, CancellationToken cancellationToken)
    {
        var formats = _repository.GetAll(x => true);

        var response = _mapper.Map<List<GetAllFormatQueryResponse>>(formats);
        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }
        PaginationListDto<GetAllFormatQueryResponse> model =
               new PaginationListDto<GetAllFormatQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, formats.Count());

        return model.Items;

    }

}
