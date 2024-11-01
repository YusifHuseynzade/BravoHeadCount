using AutoMapper;
using Common.Constants;
using DCStockDetails.Queries.Request;
using DCStockDetails.Queries.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace DCStockDetails.Handlers.QueryHandlers
{
    public class GetAllDCStockQueryHandler : IRequestHandler<GetAllDCStockQueryRequest, List<GetAllDCStockListQueryResponse>>
    {
        private readonly IDCStockRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDCStockQueryHandler(IDCStockRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllDCStockListQueryResponse>> Handle(GetAllDCStockQueryRequest request, CancellationToken cancellationToken)
        {
            var DCStocks = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (DCStocks != null)
            {
                var response = _mapper.Map<List<GetAllDCStockQueryResponse>>(DCStocks);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = DCStocks.Count();

                PaginationListDto<GetAllDCStockQueryResponse> model =
                       new PaginationListDto<GetAllDCStockQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllDCStockListQueryResponse>
                {
                    new GetAllDCStockListQueryResponse
                    {
                        TotalDCStockCount = totalCount,
                        DCStocks = model.Items
                    }
                };
            }

            return new List<GetAllDCStockListQueryResponse>();
        }
    }
}
