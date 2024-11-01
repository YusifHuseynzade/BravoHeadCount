using AutoMapper;
using Domain.IRepositories;
using MediatR;
using UniformDetails.Queries.Request;
using UniformDetails.Queries.Response;

namespace UniformDetails.Handlers.QueryHandlers;

public class GetByIdUniformQueryHandler : IRequestHandler<GetByIdUniformQueryRequest, GetByIdUniformQueryResponse>
{
    private readonly IUniformRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdUniformQueryHandler(IUniformRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdUniformQueryResponse> Handle(GetByIdUniformQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var Uniform = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (Uniform != null)
            {
                var response = _mapper.Map<GetByIdUniformQueryResponse>(Uniform);

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
