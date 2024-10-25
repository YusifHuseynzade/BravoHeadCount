using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.IRepositories;
using EncashmentDetails.Queries.Request;
using EncashmentDetails.Queries.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            // Encashment verisini ilişkili entity'ler ile birlikte getiriyoruz
            var encashments = _repository.GetAll(
                x => true,
                nameof(Encashment.Project),
                nameof(Encashment.Branch),
                nameof(Encashment.Attachments)
            );

            if (encashments != null)
            {
                var response = _mapper.Map<List<GetAllEncashmentQueryResponse>>(encashments);

                if (request.ShowMore != null)
                {
                    response = response
                        .Skip((request.Page - 1) * request.ShowMore.Take)
                        .Take(request.ShowMore.Take)
                        .ToList();
                }

                var totalCount = encashments.Count();

                var model = new PaginationListDto<GetAllEncashmentQueryResponse>(
                    response,
                    request.Page,
                    request.ShowMore?.Take ?? response.Count,
                    totalCount
                );

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
