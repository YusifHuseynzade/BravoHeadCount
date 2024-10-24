using AutoMapper;
using Domain.IRepositories;
using MediatR;
using SettingFinanceOperationDetails.Queries.Request;
using SettingFinanceOperationDetails.Queries.Response;

namespace SettingFinanceOperationDetails.Handlers.QueryHandlers;

public class GetByIdSettingFinanceOperationQueryHandler : IRequestHandler<GetByIdSettingFinanceOperationQueryRequest, GetByIdSettingFinanceOperationQueryResponse>
{
    private readonly ISettingFinanceOperationRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdSettingFinanceOperationQueryHandler(ISettingFinanceOperationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdSettingFinanceOperationQueryResponse> Handle(GetByIdSettingFinanceOperationQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var SettingFinanceOperation = await _repository.GetAsync(x => x.Id == request.Id);

            if (SettingFinanceOperation != null)
            {
                var response = _mapper.Map<GetByIdSettingFinanceOperationQueryResponse>(SettingFinanceOperation);

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
