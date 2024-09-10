using AutoMapper;
using Domain.IRepositories;
using HeadCountBackGroundColorDetails.Queries.Request;
using HeadCountBackGroundColorDetails.Queries.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Handlers.QueryHandlers;

public class GetByIdColorQueryHandler : IRequestHandler<GetByIdColorQueryRequest, GetByIdColorQueryResponse>
{
    private readonly IHeadCountBackgroundColorRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdColorQueryHandler(IHeadCountBackgroundColorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdColorQueryResponse> Handle(GetByIdColorQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var color = await _repository.GetAsync(x => x.Id == request.Id);

            if (color != null)
            {
                var response = _mapper.Map<GetByIdColorQueryResponse>(color);

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
