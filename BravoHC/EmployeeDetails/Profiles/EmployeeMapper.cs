using AutoMapper;
using Domain.Entities;
using EmployeeDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace EmployeeDetails.Profiles
{
    public class EmployeeMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public EmployeeMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;

            // Employee'den GetAllEmployeeQueryResponse'a ve GetByIdEmployeeQueryResponse'a map işlemleri
            CreateMap<Employee, GetAllEmployeeQueryResponse>()
                .ForMember(dest => dest.ResidentalArea, opt => opt.MapFrom(src => src.ResidentalArea))
                .ForMember(dest => dest.BakuDistrict, opt => opt.MapFrom(src => src.BakuDistrict))
                .ForMember(dest => dest.BakuMetro, opt => opt.MapFrom(src => src.BakuMetro))
                .ForMember(dest => dest.BakuTarget, opt => opt.MapFrom(src => src.BakuTarget))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.SubSection, opt => opt.MapFrom(src => src.SubSection))
                .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.StartedDate))
                .ForMember(dest => dest.ContractEndDate, opt => opt.MapFrom(src => src.ContractEndDate))
                .ReverseMap();

            CreateMap<Employee, GetByIdEmployeeQueryResponse>()
                .ForMember(dest => dest.ResidentalArea, opt => opt.MapFrom(src => src.ResidentalArea))
                .ForMember(dest => dest.BakuDistrict, opt => opt.MapFrom(src => src.BakuDistrict))
                .ForMember(dest => dest.BakuMetro, opt => opt.MapFrom(src => src.BakuMetro))
                .ForMember(dest => dest.BakuTarget, opt => opt.MapFrom(src => src.BakuTarget))
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.SubSection, opt => opt.MapFrom(src => src.SubSection))
                .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.StartedDate))
                .ForMember(dest => dest.ContractEndDate, opt => opt.MapFrom(src => src.ContractEndDate))
                .ReverseMap();

            CreateMap<ResidentalArea, ResidentalAreaResponse>().ReverseMap();
            CreateMap<BakuDistrict, BakuDistrictResponse>().ReverseMap();
            CreateMap<BakuMetro, BakuMetroResponse>().ReverseMap();
            CreateMap<BakuTarget, BakuTargetResponse>().ReverseMap();
            CreateMap<Project, ProjectResponse>().ReverseMap();
            CreateMap<Position, PositionResponse>().ReverseMap();
            CreateMap<Section, SectionResponse>().ReverseMap();
            CreateMap<SubSection, SubSectionResponse>().ReverseMap();
        }
    }
}
