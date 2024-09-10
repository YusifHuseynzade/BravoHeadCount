using AutoMapper;
using Domain.Entities;
using HeadCountBackGroundColorDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace HeadCountBackGroundColorDetails.Profiles;

public class ColorMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public ColorMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<HeadCountBackgroundColor, GetAllColorQueryResponse>().ReverseMap();
        CreateMap<Position, GetByIdColorQueryResponse>().ReverseMap();
    }
}
