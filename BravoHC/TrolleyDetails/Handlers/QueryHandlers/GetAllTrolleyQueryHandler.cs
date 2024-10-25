using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using TrolleyDetails.Queries.Request;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Handlers.QueryHandlers
{
    public class GetAllTrolleyQueryHandler : IRequestHandler<GetAllTrolleyQueryRequest, List<GetAllTrolleyListQueryResponse>>
    {
        private readonly ITrolleyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTrolleyQueryHandler(ITrolleyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllTrolleyListQueryResponse>> Handle(GetAllTrolleyQueryRequest request, CancellationToken cancellationToken)
        {
            var Trolleys = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (Trolleys != null)
            {
                var response = _mapper.Map<List<GetAllTrolleyQueryResponse>>(Trolleys);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = Trolleys.Count();

                PaginationListDto<GetAllTrolleyQueryResponse> model =
                       new PaginationListDto<GetAllTrolleyQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllTrolleyListQueryResponse>
                {
                    new GetAllTrolleyListQueryResponse
                    {
                        TotalTrolleyCount = totalCount,
                        Trolleys = model.Items
                    }
                };
            }

            return new List<GetAllTrolleyListQueryResponse>();
        }
    }
}
