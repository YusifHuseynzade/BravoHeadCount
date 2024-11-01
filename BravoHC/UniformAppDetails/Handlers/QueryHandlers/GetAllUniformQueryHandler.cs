using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using UniformDetails.Queries.Request;
using UniformDetails.Queries.Response;

namespace UniformDetails.Handlers.QueryHandlers
{
    public class GetAllUniformQueryHandler : IRequestHandler<GetAllUniformQueryRequest, List<GetAllUniformListQueryResponse>>
    {
        private readonly IUniformRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUniformQueryHandler(IUniformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUniformListQueryResponse>> Handle(GetAllUniformQueryRequest request, CancellationToken cancellationToken)
        {
            var Uniforms = _repository.GetAll(
                x => true,
                nameof(Trolley.TrolleyType),
                nameof(Trolley.Project)
            );

            if (Uniforms != null)
            {
                var response = _mapper.Map<List<GetAllUniformQueryResponse>>(Uniforms);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = Uniforms.Count();

                PaginationListDto<GetAllUniformQueryResponse> model =
                       new PaginationListDto<GetAllUniformQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllUniformListQueryResponse>
                {
                    new GetAllUniformListQueryResponse
                    {
                        TotalUniformCount = totalCount,
                        Uniforms = model.Items
                    }
                };
            }

            return new List<GetAllUniformListQueryResponse>();
        }
    }
}
