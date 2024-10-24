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

        CreateMap<EndOfMonthReport, GetAllEndOfMonthReportQueryResponse>().ReverseMap();
        CreateMap<EndOfMonthReport, GetByIdEndOfMonthReportQueryResponse>().ReverseMap();
    }
}
