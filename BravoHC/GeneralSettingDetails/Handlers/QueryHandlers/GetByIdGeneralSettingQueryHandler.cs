using AutoMapper;
using Domain.IRepositories;
using GeneralSettingDetails.Queries.Request;
using GeneralSettingDetails.Queries.Response;
using MediatR;

namespace GeneralSettingDetails.Handlers.QueryHandlers;

public class GetByIdGeneralSettingQueryHandler : IRequestHandler<GetByIdGeneralSettingQueryRequest, GetByIdGeneralSettingQueryResponse>
{
    private readonly IGeneralSettingRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdGeneralSettingQueryHandler(IGeneralSettingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdGeneralSettingQueryResponse> Handle(GetByIdGeneralSettingQueryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var GeneralSetting = await _repository.GetAsync(
                  x => x.Id == request.Id);

            if (GeneralSetting != null)
            {
                var response = _mapper.Map<GetByIdGeneralSettingQueryResponse>(GeneralSetting);

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
