using AutoMapper;
using Domain.IRepositories;
using MediatR;
using UniformConditionDetails.Queries.Request;
using UniformConditionDetails.Queries.Response;

namespace UniformConditionDetails.Handlers.QueryHandlers;

public class GetByIdUniformConditionQueryHandler : IRequestHandler<GetByIdUniformConditionQueryRequest, GetByIdUniformConditionQueryResponse>
{
    private readonly IUniformConditionRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdUniformConditionQueryHandler(IUniformConditionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdUniformConditionQueryResponse> Handle(GetByIdUniformConditionQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var UniformCondition = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (UniformCondition != null)
            {
                var response = _mapper.Map<GetByIdUniformConditionQueryResponse>(UniformCondition);

                return response;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xəta oldu: {ex.Message}");
            throw;
        }
    }
}
