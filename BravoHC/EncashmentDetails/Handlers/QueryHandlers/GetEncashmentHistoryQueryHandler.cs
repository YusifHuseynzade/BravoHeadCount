using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using EncashmentDetails.Queries.Request;
using EncashmentDetails.Queries.Response;
using MediatR;

namespace EncashmentDetails.Handlers.QueryHandlers
{
    public class GetEncashmentHistoryQueryHandler : IRequestHandler<GetEncashmentHistoryQueryRequest, List<GetListEncashmentHistoryQueryResponse>>
    {
        private readonly IEncashmentHistoryRepository _encashmentHistoryRepository;
        private readonly IMapper _mapper;

        public GetEncashmentHistoryQueryHandler(IEncashmentHistoryRepository encashmentHistoryRepository, IMapper mapper)
        {
            _encashmentHistoryRepository = encashmentHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListEncashmentHistoryQueryResponse>> Handle(GetEncashmentHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            // StoreId ile filtreleme ve StoreHistory sorgusu
            var encashmentHistoriesQuery = _encashmentHistoryRepository.GetAll(x => x.EncashmentId == request.EncashmentId);

            // StoreHistory verilerini getir ve sayfalamayı uygula
            var encashmentHistories = encashmentHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetEncashmentHistoryQueryResponse>>(encashmentHistories);

            // Pagination işlemi (ShowMore)
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take).ToList();
            }

            var totalCount = encashmentHistories.Count();

            // PaginationListDto kullanarak veriyi dön
            PaginationListDto<GetEncashmentHistoryQueryResponse> model =
                   new PaginationListDto<GetEncashmentHistoryQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetListEncashmentHistoryQueryResponse>
            {
                new GetListEncashmentHistoryQueryResponse
                {
                    TotalEncashmentHistoryCount = totalCount,
                    EncashmentHistories = model.Items
                }
            };
        }
    }
}
