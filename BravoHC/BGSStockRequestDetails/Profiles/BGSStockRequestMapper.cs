using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace BGSStockRequestDetails.Profiles;

public class BGSStockRequestMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public BGSStockRequestMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}
