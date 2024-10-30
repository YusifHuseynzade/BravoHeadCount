using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using GeneralSettingDetails.Queries.Request;
using GeneralSettingDetails.Queries.Response;
using MediatR;

namespace GeneralSettingDetails.Handlers.QueryHandlers
{
    public class GetAllGeneralSettingQueryHandler : IRequestHandler<GetAllGeneralSettingQueryRequest, List<GetAllGeneralSettingListQueryResponse>>
    {
        private readonly IGeneralSettingRepository _repository;
        private readonly IMapper _mapper;

        public GetAllGeneralSettingQueryHandler(IGeneralSettingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllGeneralSettingListQueryResponse>> Handle(GetAllGeneralSettingQueryRequest request, CancellationToken cancellationToken)
        {
            var GeneralSettings = _repository.GetAll(x => true);

            if (GeneralSettings != null)
            {
                var response = _mapper.Map<List<GetAllGeneralSettingQueryResponse>>(GeneralSettings);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = GeneralSettings.Count();

                PaginationListDto<GetAllGeneralSettingQueryResponse> model =
                       new PaginationListDto<GetAllGeneralSettingQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllGeneralSettingListQueryResponse>
                {
                    new GetAllGeneralSettingListQueryResponse
                    {
                        TotalGeneralSettingCount = totalCount,
                        GeneralSettings = model.Items
                    }
                };
            }

            return new List<GetAllGeneralSettingListQueryResponse>();
        }
    }
}
