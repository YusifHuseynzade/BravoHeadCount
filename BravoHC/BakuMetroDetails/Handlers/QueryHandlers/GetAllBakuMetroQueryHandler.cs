using AutoMapper;
using BakuMetroDetails.Queries.Request;
using BakuMetroDetails.Queries.Response;
using Common.Constants;
using Domain.IRepositories;
using MediatR;

namespace BakuMetroDetails.Handlers.QueryHandlers
{
    public class GetAllBakuMetroQueryHandler : IRequestHandler<GetAllBakuMetroQueryRequest, List<GetResidentalAreaListResponse>>
    {
        private readonly IBakuMetroRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBakuMetroQueryHandler(IBakuMetroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetResidentalAreaListResponse>> Handle(GetAllBakuMetroQueryRequest request, CancellationToken cancellationToken)
        {
            var bakuMetros = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllBakuMetroQueryResponse>>(bakuMetros);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = bakuMetros.Count();

            PaginationListDto<GetAllBakuMetroQueryResponse> model =
                   new PaginationListDto<GetAllBakuMetroQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetResidentalAreaListResponse>
        {
           new GetResidentalAreaListResponse
           {
              TotalBakuMetroCount = totalCount,
              BakuMetros = model.Items
           }
        };
        }
    }
}

