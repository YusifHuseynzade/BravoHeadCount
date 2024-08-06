using AutoMapper;
using Domain.Entities;
using HeadCountDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace HeadCountDetails.Profiles;

public class HeadCountMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public HeadCountMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<HeadCount, GetAllHeadCountQueryResponse>().ReverseMap();
        CreateMap<HeadCount, GetByIdHeadCountQueryResponse>().ReverseMap();
    }
}