using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using StoreDetails.Queries.Request;
using StoreDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDetails.Handlers.QueryHandlers
{
    public class GetStoreHistoryQueryHandler : IRequestHandler<GetStoreHistoryQueryRequest, List<GetListStoreHistoryQueryResponse>>
    {
        private readonly IStoreHistoryRepository _storeHistoryRepository;
        private readonly IMapper _mapper;

        public GetStoreHistoryQueryHandler(IStoreHistoryRepository storeHistoryRepository, IMapper mapper)
        {
            _storeHistoryRepository = storeHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListStoreHistoryQueryResponse>> Handle(GetStoreHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            // StoreId ile filtreleme ve StoreHistory sorgusu
            var storeHistoriesQuery = _storeHistoryRepository.GetAll(x => x.StoreId == request.StoreId);

            // StoreHistory verilerini getir ve sayfalamayı uygula
            var storeHistories = storeHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetStoreHistoryQueryResponse>>(storeHistories);

            // Pagination işlemi (ShowMore)
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take).ToList();
            }

            var totalCount = storeHistories.Count();

            // PaginationListDto kullanarak veriyi dön
            PaginationListDto<GetStoreHistoryQueryResponse> model =
                   new PaginationListDto<GetStoreHistoryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetListStoreHistoryQueryResponse>
            {
                new GetListStoreHistoryQueryResponse
                {
                    TotalStoreHistoryCount = totalCount,
                    StoreHistories = model.Items
                }
            };
        }
    }
}
