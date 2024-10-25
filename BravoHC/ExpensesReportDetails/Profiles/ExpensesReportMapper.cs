using AutoMapper;
using Core.Helpers;
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

        CreateMap<ExpensesReport, GetAllExpensesReportQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
            .ForMember(dest => dest.AttachmentUrls, opt =>
                opt.MapFrom(src => src.Attachments.Select(a =>
                    $"{RequestExtensions.BaseUrl(_httpAccessor.HttpContext)}/{a.FileUrl}")))
            .ReverseMap();

        CreateMap<ExpensesReport, GetByIdExpensesReportQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
            .ForMember(dest => dest.AttachmentUrls, opt =>
                opt.MapFrom(src => src.Attachments.Select(a =>
                    $"{RequestExtensions.BaseUrl(_httpAccessor.HttpContext)}/{a.FileUrl}")))
            .ReverseMap();
    }
}
