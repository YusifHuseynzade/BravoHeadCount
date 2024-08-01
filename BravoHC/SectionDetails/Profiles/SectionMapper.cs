using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using SectionDetails.Queries.Response;

namespace SectionDetails.Profiles;

public class SectionMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public SectionMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Section, GetAllSectionQueryResponse>().ReverseMap();
        CreateMap<Section, GetByIdSectionQueryResponse>().ReverseMap();
        CreateMap<SubSection, GetSectionSubSectionQueryResponse>().ReverseMap();
    }
}

