using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace StoreStockRequestDetails.Profiles;

public class StoreStockRequestMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public StoreStockRequestMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}
