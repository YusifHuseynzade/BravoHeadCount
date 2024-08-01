    using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using SubSectionDetails.Queries.Response;

namespace SubSectionDetails.Profiles;

public class SubSectionMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public SubSectionMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<SubSection, GetAllSubSectionQueryResponse>().ReverseMap();
        CreateMap<SubSection, GetByIdSubSectionQueryResponse>().ReverseMap();
    }
}

