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

        // HeadCount -> GetAllHeadCountQueryResponse
        CreateMap<HeadCount, GetAllHeadCountQueryResponse>()
            .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ReverseMap();

        // Employee -> EmployeeResponse
        CreateMap<Employee, EmployeeResponse>()
            .ForMember(dest => dest.ResidentalArea, opt => opt.MapFrom(src => src.ResidentalArea))
            .ReverseMap();

        // ResidentalArea -> ResidentalAreaResponse
        CreateMap<ResidentalArea, ResidentalAreaResponse>()
            .ReverseMap();
        CreateMap<HeadCount, GetByIdHeadCountQueryResponse>().ReverseMap();
    }
}