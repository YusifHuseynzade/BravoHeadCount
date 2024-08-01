using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using FunctionalAreaDetails.Queries.Request;
using FunctionalAreaDetails.Queries.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.QueryHandlers
{
    public class GetAllFunctionalAreaQueryHandler : IRequestHandler<GetAllFunctionalAreaQueryRequest, List<GetFunctionalAreaListResponse>>
    {
        private readonly IFunctionalAreaRepository _repository;
        private readonly IMapper _mapper;

        public GetAllFunctionalAreaQueryHandler(IFunctionalAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetFunctionalAreaListResponse>> Handle(GetAllFunctionalAreaQueryRequest request, CancellationToken cancellationToken)
        {
            var functionalAreas = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllFunctionalAreaQueryResponse>>(functionalAreas);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = functionalAreas.Count();

            PaginationListDto<GetAllFunctionalAreaQueryResponse> model =
                   new PaginationListDto<GetAllFunctionalAreaQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetFunctionalAreaListResponse>
        {
           new GetFunctionalAreaListResponse
           {
              TotalFunctionalAreaCount = totalCount,
              FunctionalAreas = model.Items
           }
        };
        }
    }
}

