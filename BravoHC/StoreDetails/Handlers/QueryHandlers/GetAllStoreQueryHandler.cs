using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Queries.Request;
using StoreDetails.Queries.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreDetails.Handlers.QueryHandlers
{
    public class GetAllStoreQueryHandler : IRequestHandler<GetAllStoreQueryRequest, List<GetAllStoreListQueryResponse>>
    {
        private readonly IStoreRepository _repository;
        private readonly IMapper _mapper;

        public GetAllStoreQueryHandler(IStoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllStoreListQueryResponse>> Handle(GetAllStoreQueryRequest request, CancellationToken cancellationToken)
        {
            var stores = _repository.GetAll(x => true);
            var response = _mapper.Map<List<GetAllStoreQueryResponse>>(stores);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = stores.Count();

            PaginationListDto<GetAllStoreQueryResponse> model =
                   new PaginationListDto<GetAllStoreQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetAllStoreListQueryResponse>
            {
                new GetAllStoreListQueryResponse
                {
                    TotalStoreCount = totalCount,
                    Stores = model.Items
                }
            };
        }
    }
}
