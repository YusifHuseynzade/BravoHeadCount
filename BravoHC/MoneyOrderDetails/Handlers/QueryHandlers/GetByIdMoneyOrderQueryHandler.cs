using AutoMapper;
using Domain.IRepositories;
using MediatR;
using MoneyOrderDetails.Queries.Request;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Handlers.QueryHandlers;

public class GetByIdMoneyOrderQueryHandler : IRequestHandler<GetByIdMoneyOrderQueryRequest, GetByIdMoneyOrderQueryResponse>
{
    private readonly IMoneyOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdMoneyOrderQueryHandler(IMoneyOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdMoneyOrderQueryResponse> Handle(GetByIdMoneyOrderQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var MoneyOrder = await _repository.GetAsync(x => x.Id == request.Id);

            if (MoneyOrder != null)
            {
                var response = _mapper.Map<GetByIdMoneyOrderQueryResponse>(MoneyOrder);

                return response;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xəta oldu: {ex.Message}");
            throw;
        }
    }
}
