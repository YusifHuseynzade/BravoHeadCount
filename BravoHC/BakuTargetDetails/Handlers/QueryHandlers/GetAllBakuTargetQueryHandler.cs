using AutoMapper;
using BakuTargetDetails.Queries.Request;
using BakuTargetDetails.Queries.Response;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BakuTargetDetails.Handlers.QueryHandlers
{
    public class GetAllBakuTargetQueryHandler : IRequestHandler<GetAllBakuTargetQueryRequest, List<GetBakuTargetListResponse>>
    {
        private readonly IBakuTargetRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBakuTargetQueryHandler(IBakuTargetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBakuTargetListResponse>> Handle(GetAllBakuTargetQueryRequest request, CancellationToken cancellationToken)
        {
            var bakuTargets = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllBakuTargetQueryResponse>>(bakuTargets);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = bakuTargets.Count();

            PaginationListDto<GetAllBakuTargetQueryResponse> model =
                   new PaginationListDto<GetAllBakuTargetQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetBakuTargetListResponse>
        {
           new GetBakuTargetListResponse
           {
              TotalBakuTargetCount = totalCount,
              BakuTargets = model.Items
           }
        };
        }
    }
}

