using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace UniformDetails.Profiles;

public class UniformMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public UniformMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}
