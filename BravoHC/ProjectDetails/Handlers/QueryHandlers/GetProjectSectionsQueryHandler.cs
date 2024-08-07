using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectDetails.Handlers.QueryHandlers
{
    public class GetProjectSectionsQueryHandler : IRequestHandler<GetProjectSectionQueryRequest, List<GetProjectSectionListQueryResponse>>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectSectionsQueryHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetProjectSectionListQueryResponse>> Handle(GetProjectSectionQueryRequest request, CancellationToken cancellationToken)
        {
            var project = await _repository.FirstOrDefaultAsync(x => x.Id == request.ProjectId, "Sections");

            if (project == null)
            {
                return null;
            }

            var sections = project.Sections;
            var sectionResponse = _mapper.Map<List<GetProjectSectionQueryResponse>>(sections);

            if (request.ShowMore != null)
            {
                sectionResponse = sectionResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = sections.Count();

            PaginationListDto<GetProjectSectionQueryResponse> model =
                   new PaginationListDto<GetProjectSectionQueryResponse>(sectionResponse, request.Page, request.ShowMore?.Take ?? sectionResponse.Count, totalCount);

            return new List<GetProjectSectionListQueryResponse>
            {
                new GetProjectSectionListQueryResponse
                {
                    TotalProjectSectionCount = totalCount,
                    Sections = model.Items
                }
            };
        }
    }
}
