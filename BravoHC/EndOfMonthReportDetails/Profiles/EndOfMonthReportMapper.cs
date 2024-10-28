using AutoMapper;
using Domain.Entities;
using EndOfMonthReportDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace EndOfMonthReportDetails.Profiles;

public class EndOfMonthReportMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;

    public EndOfMonthReportMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<EndOfMonthReport, GetAllEndOfMonthReportQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode)) // Project bilgisi
            .ReverseMap();

        CreateMap<EndOfMonthReport, GetByIdEndOfMonthReportQueryResponse>()
             .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode)) // Project bilgisi
            .ReverseMap()
            .ReverseMap();

        CreateMap<EndOfMonthReportHistory, GetEndOfMonthReportHistoryQueryResponse>().ReverseMap();
    }
}
