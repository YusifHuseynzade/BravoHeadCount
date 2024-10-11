using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using SummaryDetails.Queries.Response;

namespace SummaryDetails.Profiles
{
    public class SummaryMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public SummaryMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;

            // SickLeave -> GetAllSickLeaveQueryResponse map'lemesi
            CreateMap<Summary, GetAllSummaryQueryResponse>()
                .ForMember(dest => dest.MonthName, opt => opt.MapFrom(src => src.Month.Name))
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                .ReverseMap();

            // SickLeave -> GetByIdSickLeaveQueryResponse map'lemesi
            CreateMap<Summary, GetByIdSummaryQueryResponse>()
                .ForMember(dest => dest.MonthName, opt => opt.MapFrom(src => src.Month.Name))
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                .ReverseMap();

            // Employee -> EmployeeResponse map'lemesi
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Badge, opt => opt.MapFrom(src => src.Badge))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ReverseMap();

            // Position -> PositionResponse map'lemesi
            CreateMap<Position, PositionResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            // Section -> SectionResponse map'lemesi
            CreateMap<Section, SectionResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
