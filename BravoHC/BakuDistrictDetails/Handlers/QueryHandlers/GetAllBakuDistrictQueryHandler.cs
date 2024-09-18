using AutoMapper;
using BakuDistrictDetails.Queries.Request;
using BakuDistrictDetails.Queries.Response;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BakuDistrictDetails.Handlers.QueryHandlers
{
    public class GetAllBakuDistrictQueryHandler : IRequestHandler<GetAllBakuDistrictQueryRequest, List<GetBakuDistrictListResponse>>
    {
        private readonly IBakuDistrictRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBakuDistrictQueryHandler(IBakuDistrictRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBakuDistrictListResponse>> Handle(GetAllBakuDistrictQueryRequest request, CancellationToken cancellationToken)
        {
            var bakuDistricts = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllBakuDistrictQueryResponse>>(bakuDistricts);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = bakuDistricts.Count();

            PaginationListDto<GetAllBakuDistrictQueryResponse> model =
                   new PaginationListDto<GetAllBakuDistrictQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetBakuDistrictListResponse>
        {
           new GetBakuDistrictListResponse
           {
              TotalBakuDistrictCount = totalCount,
              BakuDistricts = model.Items
           }
        };
        }
    }
}

