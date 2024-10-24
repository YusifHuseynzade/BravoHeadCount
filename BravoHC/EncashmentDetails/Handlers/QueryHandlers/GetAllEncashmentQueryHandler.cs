using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using EncashmentDetails.Queries.Request;
using EncashmentDetails.Queries.Response;
using MediatR;

namespace EncashmentDetails.Handlers.QueryHandlers
{
    public class GetAllEncashmentQueryHandler : IRequestHandler<GetAllEncashmentQueryRequest, List<GetAllEncashmentListQueryResponse>>
    {
        private readonly IEncashmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEncashmentQueryHandler(IEncashmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllEncashmentListQueryResponse>> Handle(GetAllEncashmentQueryRequest request, CancellationToken cancellationToken)
        {
            var Encashments = _repository.GetAll(x => true);

            if (Encashments != null)
            {
                var response = _mapper.Map<List<GetAllEncashmentQueryResponse>>(Encashments);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = Encashments.Count();

                PaginationListDto<GetAllEncashmentQueryResponse> model =
                       new PaginationListDto<GetAllEncashmentQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllEncashmentListQueryResponse>
                {
                    new GetAllEncashmentListQueryResponse
                    {
                        TotalEncashmentCount = totalCount,
                        Encashments = model.Items
                    }
                };
            }

            return new List<GetAllEncashmentListQueryResponse>();
        }
    }
}
