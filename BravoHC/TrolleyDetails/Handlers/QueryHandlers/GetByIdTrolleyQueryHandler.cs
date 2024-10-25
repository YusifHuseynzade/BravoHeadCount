using AutoMapper;
using Domain.IRepositories;
using MediatR;
using TrolleyDetails.Queries.Request;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Handlers.QueryHandlers;

public class GetByIdTrolleyQueryHandler : IRequestHandler<GetByIdTrolleyQueryRequest, GetByIdTrolleyQueryResponse>
{
    private readonly ITrolleyRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdTrolleyQueryHandler(ITrolleyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdTrolleyQueryResponse> Handle(GetByIdTrolleyQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var Trolley = await _repository.GetAsync(
                  x => x.Id == request.Id,
                  includes: new[] { "Project", "TrolleyType" }
              );

            if (Trolley != null)
            {
                var response = _mapper.Map<GetByIdTrolleyQueryResponse>(Trolley);

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
