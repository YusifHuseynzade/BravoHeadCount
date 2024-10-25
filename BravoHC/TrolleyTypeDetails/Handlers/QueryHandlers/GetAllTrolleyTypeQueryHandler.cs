using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using TrolleyTypeDetails.Queries.Request;
using TrolleyTypeDetails.Queries.Response;

namespace TrolleyTypeDetails.Handlers.QueryHandlers
{
    public class GetAllTrolleyTypeQueryHandler : IRequestHandler<GetAllTrolleyTypeQueryRequest, List<GetAllTrolleyTypeListQueryResponse>>
    {
        private readonly ITrolleyTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTrolleyTypeQueryHandler(ITrolleyTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllTrolleyTypeListQueryResponse>> Handle(GetAllTrolleyTypeQueryRequest request, CancellationToken cancellationToken)
        {
            var TrolleyTypes = _repository.GetAll(
                x => true);

            if (TrolleyTypes != null)
            {
                var response = _mapper.Map<List<GetAllTrolleyTypeQueryResponse>>(TrolleyTypes);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = TrolleyTypes.Count();

                PaginationListDto<GetAllTrolleyTypeQueryResponse> model =
                       new PaginationListDto<GetAllTrolleyTypeQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllTrolleyTypeListQueryResponse>
                {
                    new GetAllTrolleyTypeListQueryResponse
                    {
                        TotalTrolleyTypeCount = totalCount,
                        TrolleyTypes = model.Items
                    }
                };
            }

            return new List<GetAllTrolleyTypeListQueryResponse>();
        }
    }
}
