using AutoMapper;
using BGSStockRequestDetails.Queries.Request;
using BGSStockRequestDetails.Queries.Response;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BGSStockRequestDetails.Handlers.QueryHandlers
{
    public class GetAllBGSStockRequestQueryHandler : IRequestHandler<GetAllBGSStockRequestQueryRequest, List<GetAllBGSStockRequestListQueryResponse>>
    {
        private readonly IBGSStockRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBGSStockRequestQueryHandler(IBGSStockRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllBGSStockRequestListQueryResponse>> Handle(GetAllBGSStockRequestQueryRequest request, CancellationToken cancellationToken)
        {
            var BGSStockRequests = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (BGSStockRequests != null)
            {
                var response = _mapper.Map<List<GetAllBGSStockRequestQueryResponse>>(BGSStockRequests);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = BGSStockRequests.Count();

                PaginationListDto<GetAllBGSStockRequestQueryResponse> model =
                       new PaginationListDto<GetAllBGSStockRequestQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllBGSStockRequestListQueryResponse>
                {
                    new GetAllBGSStockRequestListQueryResponse
                    {
                        TotalBGSStockRequestCount = totalCount,
                        BGSStockRequests = model.Items
                    }
                };
            }

            return new List<GetAllBGSStockRequestListQueryResponse>();
        }
    }
}
