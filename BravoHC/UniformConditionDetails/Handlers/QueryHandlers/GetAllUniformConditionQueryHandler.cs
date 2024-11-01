using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using UniformConditionDetails.Queries.Request;
using UniformConditionDetails.Queries.Response;

namespace TrolleyDetails.Handlers.QueryHandlers
{
    public class GetAllUniformConditionQueryHandler : IRequestHandler<GetAllUniformConditionQueryRequest, List<GetAllUniformConditionListQueryResponse>>
    {
        private readonly IUniformConditionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUniformConditionQueryHandler(IUniformConditionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUniformConditionListQueryResponse>> Handle(GetAllUniformConditionQueryRequest request, CancellationToken cancellationToken)
        {
            var UniformConditions = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (UniformConditions != null)
            {
                var response = _mapper.Map<List<GetAllUniformConditionQueryResponse>>(UniformConditions);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = UniformConditions.Count();

                PaginationListDto<GetAllUniformConditionQueryResponse> model =
                       new PaginationListDto<GetAllUniformConditionQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllUniformConditionListQueryResponse>
                {
                    new GetAllUniformConditionListQueryResponse
                    {
                        TotalUniformConditionCount = totalCount,
                        UniformConditions = model.Items
                    }
                };
            }

            return new List<GetAllUniformConditionListQueryResponse>();
        }
    }
}
