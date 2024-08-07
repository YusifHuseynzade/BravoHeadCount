using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using FunctionalAreaDetails.Queries.Request;
using FunctionalAreaDetails.Queries.Response;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Handlers.QueryHandlers
{
    public class GetFunctionalAreaProjectsQueryHandler : IRequestHandler<GetFunctionalAreaProjectsQueryRequest, List<GetFunctionalAreaProjectListResponse>>
    {
        private readonly IFunctionalAreaRepository _repository;
        private readonly IMapper _mapper;

        public GetFunctionalAreaProjectsQueryHandler(IFunctionalAreaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetFunctionalAreaProjectListResponse>> Handle(GetFunctionalAreaProjectsQueryRequest request, CancellationToken cancellationToken)
        {
            var functionalArea = await _repository.FirstOrDefaultAsync(x => x.Id == request.FunctionalAreaId, "Projects");

            if (functionalArea == null)
            {
                return null;
            }

            var projects = functionalArea.Projects;
            var projectResponse = _mapper.Map<List<GetFunctionalAreaProjectsQueryResponse>>(projects);

            if (request.ShowMore != null)
            {
                projectResponse = projectResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = projects.Count();

            PaginationListDto<GetFunctionalAreaProjectsQueryResponse> model =
                   new PaginationListDto<GetFunctionalAreaProjectsQueryResponse>(projectResponse, request.Page, request.ShowMore?.Take ?? projectResponse.Count, totalCount);

            return new List<GetFunctionalAreaProjectListResponse>
            {
                new GetFunctionalAreaProjectListResponse
                {
                    TotalFunctionalAreaProjectCount = totalCount,
                    FunctionalAreaProjects = model.Items
                }
            };
        }
    }
}
