using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using FormatDetails.Queries.Request;
using FormatDetails.Queries.Response;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FormatDetails.Handlers.QueryHandlers
{
    public class GetAllFormatQueryHandler : IRequestHandler<GetAllFormatQueryRequest, List<GetAllFormatListQueryResponse>>
    {
        private readonly IFormatRepository _repository;
        private readonly IMapper _mapper;

        public GetAllFormatQueryHandler(IFormatRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllFormatListQueryResponse>> Handle(GetAllFormatQueryRequest request, CancellationToken cancellationToken)
        {
            var formats = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllFormatQueryResponse>>(formats);
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = formats.Count();

            PaginationListDto<GetAllFormatQueryResponse> model =
                   new PaginationListDto<GetAllFormatQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetAllFormatListQueryResponse>
            {
                new GetAllFormatListQueryResponse
                {
                    TotalFormatCount = totalCount,
                    Formats = model.Items
                }
            };
        }
    }
}
