using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using SectionDetails.Queries.Request;
using SectionDetails.Queries.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SectionDetails.Handlers.QueryHandlers
{
    public class GetSectionSubSectionQueryHandler : IRequestHandler<GetSectionSubSectionQueryRequest, List<GetSectionSubSectionListResponse>>
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public GetSectionSubSectionQueryHandler(ISectionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetSectionSubSectionListResponse>> Handle(GetSectionSubSectionQueryRequest request, CancellationToken cancellationToken)
        {
            var section = await _repository.FirstOrDefaultAsync(x => x.Id == request.SectionId, "SubSections");

            if (section == null)
            {
                return null;
            }

            var subSections = section.SubSections;
            var subSectionResponse = _mapper.Map<List<GetSectionSubSectionQueryResponse>>(subSections);

            if (request.ShowMore != null)
            {
                subSectionResponse = subSectionResponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = subSections.Count();

            PaginationListDto<GetSectionSubSectionQueryResponse> model =
                   new PaginationListDto<GetSectionSubSectionQueryResponse>(subSectionResponse, request.Page, request.ShowMore?.Take ?? subSectionResponse.Count, totalCount);

            return new List<GetSectionSubSectionListResponse>
            {
                new GetSectionSubSectionListResponse
                {
                    TotalSectionSubSectionCount = totalCount,
                    SectionSubSections = model.Items
                }
            };
        }
    }
}
