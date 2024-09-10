using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using HeadCountBackGroundColorDetails.Queries.Request;
using HeadCountBackGroundColorDetails.Queries.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Handlers.QueryHandlers
{
    public class GetAllColorQueryHandler : IRequestHandler<GetAllColorQueryRequest, List<GetAllColorListQueryResponse>>
    {
        private readonly IHeadCountBackgroundColorRepository _repository;
        private readonly IMapper _mapper;

        public GetAllColorQueryHandler(IHeadCountBackgroundColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllColorListQueryResponse>> Handle(GetAllColorQueryRequest request, CancellationToken cancellationToken)
        {
            var colors = _repository.GetAll(x => true);

            if (colors != null)
            {
                var response = _mapper.Map<List<GetAllColorQueryResponse>>(colors);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = colors.Count();

                PaginationListDto<GetAllColorQueryResponse> model =
                       new PaginationListDto<GetAllColorQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllColorListQueryResponse>
                {
                    new GetAllColorListQueryResponse
                    {
                        TotalColorCount = totalCount,
                        Colors = model.Items
                    }
                };
            }

            return new List<GetAllColorListQueryResponse>();
        }
    }
}
