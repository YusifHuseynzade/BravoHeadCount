using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using SettingFinanceOperationDetails.Queries.Request;
using SettingFinanceOperationDetails.Queries.Response;

namespace SettingFinanceOperationDetails.Handlers.QueryHandlers
{
    public class GetAllSettingFinanceOperationQueryHandler : IRequestHandler<GetAllSettingFinanceOperationQueryRequest, List<GetAllSettingFinanceOperationListQueryResponse>>
    {
        private readonly ISettingFinanceOperationRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSettingFinanceOperationQueryHandler(ISettingFinanceOperationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllSettingFinanceOperationListQueryResponse>> Handle(GetAllSettingFinanceOperationQueryRequest request, CancellationToken cancellationToken)
        {
            var SettingFinanceOperations = _repository.GetAll(x => true);

            if (SettingFinanceOperations != null)
            {
                var response = _mapper.Map<List<GetAllSettingFinanceOperationQueryResponse>>(SettingFinanceOperations);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = SettingFinanceOperations.Count();

                PaginationListDto<GetAllSettingFinanceOperationQueryResponse> model =
                       new PaginationListDto<GetAllSettingFinanceOperationQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllSettingFinanceOperationListQueryResponse>
                {
                    new GetAllSettingFinanceOperationListQueryResponse
                    {
                        TotalSettingFinanceOperationCount = totalCount,
                        SettingFinanceOperations = model.Items
                    }
                };
            }

            return new List<GetAllSettingFinanceOperationListQueryResponse>();
        }
    }
}
