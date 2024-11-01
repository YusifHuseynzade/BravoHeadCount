using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using StoreStockRequestDetails.Queries.Request;
using StoreStockRequestDetails.Queries.Response;

namespace StoreStockRequestDetails.Handlers.QueryHandlers
{
    public class GetAllStoreStockRequestQueryHandler : IRequestHandler<GetAllStoreStockRequestQueryRequest, List<GetAllStoreStockRequestListQueryResponse>>
    {
        private readonly IStoreStockRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetAllStoreStockRequestQueryHandler(IStoreStockRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllStoreStockRequestListQueryResponse>> Handle(GetAllStoreStockRequestQueryRequest request, CancellationToken cancellationToken)
        {
            var StoreStockRequests = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (StoreStockRequests != null)
            {
                var response = _mapper.Map<List<GetAllStoreStockRequestQueryResponse>>(StoreStockRequests);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = StoreStockRequests.Count();

                PaginationListDto<GetAllStoreStockRequestQueryResponse> model =
                       new PaginationListDto<GetAllStoreStockRequestQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllStoreStockRequestListQueryResponse>
                {
                    new GetAllStoreStockRequestListQueryResponse
                    {
                        TotalStoreStockRequestCount = totalCount,
                        StoreStockRequests = model.Items
                    }
                };
            }

            return new List<GetAllStoreStockRequestListQueryResponse>();
        }
    }
}
