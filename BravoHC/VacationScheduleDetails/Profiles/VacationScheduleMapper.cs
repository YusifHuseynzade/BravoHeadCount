using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using VacationScheduleDetails.Queries.Response;

namespace VacationScheduleDetails.Profiles
{
    public class VacationScheduleMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public VacationScheduleMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;

            // SickLeave -> GetAllSickLeaveQueryResponse map'lemesi
            CreateMap<VacationSchedule, GetAllVacationScheduleQueryResponse>()
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                .ReverseMap();

            // SickLeave -> GetByIdSickLeaveQueryResponse map'lemesi
            CreateMap<VacationSchedule, GetByIdVacationScheduleQueryResponse>().ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
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
