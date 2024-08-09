using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using HeadCountDetails.Queries.Request;
using HeadCountDetails.Queries.Response;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeadCountDetails.Handlers.QueryHandlers
{
    public class GetAllHeadCountQueryHandler : IRequestHandler<GetAllHeadCountQueryRequest, List<GetHeadCountListResponse>>
    {
        private readonly IHeadCountRepository _repository;
        private readonly IMapper _mapper;

        public GetAllHeadCountQueryHandler(IHeadCountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHeadCountListResponse>> Handle(GetAllHeadCountQueryRequest request, CancellationToken cancellationToken)
        {
            var headCountsQuery = _repository.GetAll(x => true);

            if (request.ProjectId.HasValue)
            {
                headCountsQuery = headCountsQuery.Where(x => x.ProjectId == request.ProjectId.Value);
            }

            // Sıralama işlemi sadece HCNumber'a göre
            headCountsQuery = request.OrderBy?.ToLower() switch
            {
                "asc" => headCountsQuery.OrderBy(x => x.HCNumber),
                "desc" => headCountsQuery.OrderByDescending(x => x.HCNumber),
                _ => headCountsQuery.OrderBy(x => x.HCNumber), 
            };

            var headCounts = headCountsQuery.ToList();

            var response = _mapper.Map<List<GetAllHeadCountQueryResponse>>(headCounts);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = headCounts.Count();

            PaginationListDto<GetAllHeadCountQueryResponse> model =
                   new PaginationListDto<GetAllHeadCountQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetHeadCountListResponse>
    {
        new GetHeadCountListResponse
        {
            TotalHeadCount = totalCount,
            HeadCounts = model.Items
        }
    };
        }

    }
}
