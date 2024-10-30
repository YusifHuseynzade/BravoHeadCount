using AutoMapper;
using Domain.Entities;
using GeneralSettingDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace GeneralSettingDetails.Profiles;

public class GeneralSettingMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;

    public GeneralSettingMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<GeneralSetting, GetAllGeneralSettingQueryResponse>()
                .ForMember(dest => dest.EndOfMonthSendingTimes, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.SendingTimes))
                .ForMember(dest => dest.EndOfMonthSendingFrequency, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.SendingFrequency))
                .ForMember(dest => dest.EndOfMonthReceivers, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.Receivers))
                .ForMember(dest => dest.EndOfMonthReceiversCC, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.ReceiversCC))
                .ForMember(dest => dest.EndOfMonthAvailableCreatedDays, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.AvailableCreatedDays))
                .ForMember(dest => dest.ExpenseReportSendingTimes, opt => opt.MapFrom(src => src.ExpenseReportSettings.SendingTimes))
                .ForMember(dest => dest.ExpenseReportSendingFrequency, opt => opt.MapFrom(src => src.ExpenseReportSettings.SendingFrequency))
                .ForMember(dest => dest.ExpenseReportReceivers, opt => opt.MapFrom(src => src.ExpenseReportSettings.Receivers))
                .ForMember(dest => dest.ExpenseReportReceiversCC, opt => opt.MapFrom(src => src.ExpenseReportSettings.ReceiversCC))
                .ForMember(dest => dest.ExpenseReportAvailableCreatedDays, opt => opt.MapFrom(src => src.ExpenseReportSettings.AvailableCreatedDays));
        CreateMap<GeneralSetting, GetByIdGeneralSettingQueryResponse>()
            .ForMember(dest => dest.EndOfMonthSendingTimes, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.SendingTimes))
                .ForMember(dest => dest.EndOfMonthSendingFrequency, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.SendingFrequency))
                .ForMember(dest => dest.EndOfMonthReceivers, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.Receivers))
                .ForMember(dest => dest.EndOfMonthReceiversCC, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.ReceiversCC))
                .ForMember(dest => dest.EndOfMonthAvailableCreatedDays, opt => opt.MapFrom(src => src.EndOfMonthReportSettings.AvailableCreatedDays))
                .ForMember(dest => dest.ExpenseReportSendingTimes, opt => opt.MapFrom(src => src.ExpenseReportSettings.SendingTimes))
                .ForMember(dest => dest.ExpenseReportSendingFrequency, opt => opt.MapFrom(src => src.ExpenseReportSettings.SendingFrequency))
                .ForMember(dest => dest.ExpenseReportReceivers, opt => opt.MapFrom(src => src.ExpenseReportSettings.Receivers))
                .ForMember(dest => dest.ExpenseReportReceiversCC, opt => opt.MapFrom(src => src.ExpenseReportSettings.ReceiversCC))
                .ForMember(dest => dest.ExpenseReportAvailableCreatedDays, opt => opt.MapFrom(src => src.ExpenseReportSettings.AvailableCreatedDays));

    }
}
