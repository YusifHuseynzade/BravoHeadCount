using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Queries.Request;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Handlers.QueryHandlers
{
    public class GetAllPlanQueryHandler : IRequestHandler<GetAllPlanQueryRequest, List<GetAllPlanListQueryResponse>>
    {
        private readonly IPlanRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPlanQueryHandler(IPlanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPlanListQueryResponse>> Handle(GetAllPlanQueryRequest request, CancellationToken cancellationToken)
        {
            var plans = _repository.GetAll(x => true);

            if (plans != null)
            {
                var response = _mapper.Map<List<GetAllPlanQueryResponse>>(plans);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = plans.Count();

                PaginationListDto<GetAllPlanQueryResponse> model =
                       new PaginationListDto<GetAllPlanQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllPlanListQueryResponse>
                {
                    new GetAllPlanListQueryResponse
                    {
                        TotalPlanCount = totalCount,
                        Plans = model.Items
                    }
                };
            }

            return new List<GetAllPlanListQueryResponse>();
        }
    }
}
