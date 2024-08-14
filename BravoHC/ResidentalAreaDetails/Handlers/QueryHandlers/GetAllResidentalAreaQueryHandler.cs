using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ResidentalAreaDetails.Queries.Request;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Handlers.QueryHandlers
{
    public class GetAllResidentalAreaQueryHandler : IRequestHandler<GetAllResidentalAreaQueryRequest, List<GetResidentalAreaListResponse>>
    {
        private readonly IResidentalAreaRepository _repository;
        private readonly IMapper _mapper;

        public GetAllResidentalAreaQueryHandler(IResidentalAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetResidentalAreaListResponse>> Handle(GetAllResidentalAreaQueryRequest request, CancellationToken cancellationToken)
        {
            var residentalAreas = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllResidentalAreaQueryResponse>>(residentalAreas);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = residentalAreas.Count();

            PaginationListDto<GetAllResidentalAreaQueryResponse> model =
                   new PaginationListDto<GetAllResidentalAreaQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetResidentalAreaListResponse>
        {
           new GetResidentalAreaListResponse
           {
              TotalResidentalAreaCount = totalCount,
              ResidentalAreas = model.Items
           }
        };
        }
    }
}

