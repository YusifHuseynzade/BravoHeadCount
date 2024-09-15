using AutoMapper;
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
        private readonly IProjectSectionsRepository _projectSectionsRepository;
        private readonly IMapper _mapper;

        public GetProjectSectionsQueryHandler(IProjectSectionsRepository projectSectionsRepository, IMapper mapper)
        {
            _projectSectionsRepository = projectSectionsRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProjectSectionListQueryResponse>> Handle(GetProjectSectionQueryRequest request, CancellationToken cancellationToken)
        {
            // ProjectSections üzerinden ProjectId'ye göre section'ları getir
            var projectSections = await _projectSectionsRepository.GetAllAsync(ps => ps.ProjectId == request.ProjectId, "Section");

            if (projectSections == null || !projectSections.Any())
            {
                return null;
            }

            // ProjectSections'tan Section'ları map'le
            var sections = projectSections.Select(ps => ps.Section).ToList();
            var sectionResponse = _mapper.Map<List<GetProjectSectionQueryResponse>>(sections);

            // Pagination işlemi
            if (request.ShowMore != null)
            {
                sectionResponse = sectionResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = sections.Count();

            return new List<GetProjectSectionListQueryResponse>
            {
                new GetProjectSectionListQueryResponse
                {
                    TotalProjectSectionCount = totalCount,
                    Sections = sectionResponse
                }
            };
        }
    }
}
