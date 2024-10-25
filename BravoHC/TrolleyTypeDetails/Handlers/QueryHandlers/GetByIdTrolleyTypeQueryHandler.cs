using AutoMapper;
using Domain.IRepositories;
using MediatR;
using TrolleyTypeDetails.Queries.Request;
using TrolleyTypeDetails.Queries.Response;

namespace TrolleyTypeDetails.Handlers.QueryHandlers;

public class GetByIdTrolleyTypeQueryHandler : IRequestHandler<GetByIdTrolleyTypeQueryRequest, GetByIdTrolleyTypeQueryResponse>
{
    private readonly ITrolleyTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdTrolleyTypeQueryHandler(ITrolleyTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdTrolleyTypeQueryResponse> Handle(GetByIdTrolleyTypeQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var TrolleyType = await _repository.GetAsync(
                    x => x.Id == request.Id);

            if (TrolleyType != null)
            {
                var response = _mapper.Map<GetByIdTrolleyTypeQueryResponse>(TrolleyType);

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
