using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace UniformConditionDetails.Profiles;

public class UniformConditionMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public UniformConditionMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}
