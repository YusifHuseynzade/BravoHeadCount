using AutoMapper;
using Domain.Entities;
using ExpensesReportDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace ExpensesReportDetails.Profiles;

public class ExpensesReportMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public ExpensesReportMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<ExpensesReport, GetAllExpensesReportQueryResponse>().ReverseMap();
        CreateMap<ExpensesReport, GetByIdExpensesReportQueryResponse>().ReverseMap();
    }
}
