using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace DCStockDetails.Profiles;

public class DCStockMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public DCStockMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}
