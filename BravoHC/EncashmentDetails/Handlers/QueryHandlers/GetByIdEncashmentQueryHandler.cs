using AutoMapper;
using Domain.IRepositories;
using EncashmentDetails.Queries.Request;
using EncashmentDetails.Queries.Response;
using MediatR;

namespace EncashmentDetails.Handlers.QueryHandlers;

public class GetByIdEncashmentQueryHandler : IRequestHandler<GetByIdEncashmentQueryRequest, GetByIdEncashmentQueryResponse>
{
    private readonly IEncashmentRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdEncashmentQueryHandler(IEncashmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdEncashmentQueryResponse> Handle(GetByIdEncashmentQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var encashment = await _repository.GetAsync(x => x.Id == request.Id);

            if (encashment != null)
            {
                var response = _mapper.Map<GetByIdEncashmentQueryResponse>(encashment);

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
