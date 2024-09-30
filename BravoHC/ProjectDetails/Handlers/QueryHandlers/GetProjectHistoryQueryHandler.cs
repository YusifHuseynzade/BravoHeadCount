using AutoMapper;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Handlers.QueryHandlers
{
    public class GetProjectHistoryQueryHandler : IRequestHandler<GetProjectHistoryQueryRequest, List<GetListProjectHistoryQueryResponse>>
    {
        private readonly IProjectHistoryRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectHistoryQueryHandler(IProjectHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetListProjectHistoryQueryResponse>> Handle(GetProjectHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            var projectHistoriesQuery = _repository.GetAll(x => x.ProjectId == request.ProjectId);

            var projectHistories = projectHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetProjectHistoryQueryResponse>>(projectHistories);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = projectHistories.Count;

            return new List<GetListProjectHistoryQueryResponse>
            {
                new GetListProjectHistoryQueryResponse
                {
                    TotalProjectHistoryCount = totalCount,
                    ProjectHistories = response
                }
            };
        }
    }
}
