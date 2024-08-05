using AutoMapper;
using Domain.Entities;
using FormatDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace FormatDetails.Profiles;

public class FormatMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public FormatMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Format, GetAllFormatQueryResponse>().ReverseMap();
        CreateMap<Format, GetByIdFormatQueryResponse>().ReverseMap();
    }
}