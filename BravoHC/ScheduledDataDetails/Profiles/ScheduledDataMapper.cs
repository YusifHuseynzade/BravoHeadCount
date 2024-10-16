using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Profiles;

public class ScheduledDataMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public ScheduledDataMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<ScheduledData, GetAllScheduledDataQueryResponse>()
                 .ForMember(dest => dest.PlanValue, opt => opt.MapFrom(src => src.Plan.Value))
                 .ForMember(dest => dest.PlanColor, opt => opt.MapFrom(src => src.Plan.Color)) // Plan rengini ekledik
                 .ForMember(dest => dest.PlanShift, opt => opt.MapFrom(src => src.Plan.Shift)) // Plan vardiya bilgisini ekledik
                 .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                 .ForMember(dest => dest.EmployeeBadge, opt => opt.MapFrom(src => src.Employee.Badge)) // Employee badge bilgisini ekledik
                 .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Employee.Position.Name)) // Employee pozisyon bilgisini ekledik
                 .ForMember(dest => dest.EmployeeSection, opt => opt.MapFrom(src => src.Employee.Section.Name))
                 .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName))
                 .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                 .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                 .ForMember(dest => dest.Fact, opt => opt.MapFrom(src => src.Fact))
                 .ForMember(dest => dest.HolidayBalance, opt => opt.MapFrom(src =>
                  src.Employee.EmployeeBalances.FirstOrDefault().HolidayBalance))
                 .ForMember(dest => dest.VacationBalance, opt => opt.MapFrom(src =>
                  src.Employee.EmployeeBalances.FirstOrDefault().VacationBalance));

        CreateMap<ScheduledData, GetByIdScheduledDataQueryResponse>().ForMember(dest => dest.PlanValue, opt => opt.MapFrom(src => src.Plan.Value))
                 .ForMember(dest => dest.PlanColor, opt => opt.MapFrom(src => src.Plan.Color)) // Plan rengini ekledik
                 .ForMember(dest => dest.PlanShift, opt => opt.MapFrom(src => src.Plan.Shift)) // Plan vardiya bilgisini ekledik
                 .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                 .ForMember(dest => dest.EmployeeBadge, opt => opt.MapFrom(src => src.Employee.Badge)) // Employee badge bilgisini ekledik
                 .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Employee.Position.Name)) // Employee pozisyon bilgisini ekledik
                 .ForMember(dest => dest.EmployeeSection, opt => opt.MapFrom(src => src.Employee.Section.Name))
                 .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName))
                 .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                 .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                 .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                 .ForMember(dest => dest.Fact, opt => opt.MapFrom(src => src.Fact))
                 .ForMember(dest => dest.HolidayBalance, opt => opt.MapFrom(src =>
                  src.Employee.EmployeeBalances.FirstOrDefault().HolidayBalance))
                 .ForMember(dest => dest.VacationBalance, opt => opt.MapFrom(src =>
                  src.Employee.EmployeeBalances.FirstOrDefault().VacationBalance));
    }
}