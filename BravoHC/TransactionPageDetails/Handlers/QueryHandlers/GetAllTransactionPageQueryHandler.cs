using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using TransactionPageDetails.Queries.Request;
using TransactionPageDetails.Queries.Response;

namespace TransactionPageDetails.Handlers.QueryHandlers
{
    public class GetAllTransactionPageQueryHandler : IRequestHandler<GetAllTransactionPageQueryRequest, List<GetAllTransactionPageListQueryResponse>>
    {
        private readonly ITransactionPageRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTransactionPageQueryHandler(ITransactionPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllTransactionPageListQueryResponse>> Handle(GetAllTransactionPageQueryRequest request, CancellationToken cancellationToken)
        {
            var Transactions = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (Transactions != null)
            {
                var response = _mapper.Map<List<GetAllTransactionPageQueryResponse>>(Transactions);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = Transactions.Count();

                PaginationListDto<GetAllTransactionPageQueryResponse> model =
                       new PaginationListDto<GetAllTransactionPageQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllTransactionPageListQueryResponse>
                {
                    new GetAllTransactionPageListQueryResponse
                    {
                        TotalTransactionCount = totalCount,
                        Transactions = model.Items
                    }
                };
            }

            return new List<GetAllTransactionPageListQueryResponse>();
        }
    }
}
