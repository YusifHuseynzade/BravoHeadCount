using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using SettingFinanceOperationDetails.Queries.Response;

namespace SettingFinanceOperationDetails.Profiles;

public class SettingFinanceOperationMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public SettingFinanceOperationMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<SettingFinanceOperation, GetAllSettingFinanceOperationQueryResponse>().ReverseMap();
        CreateMap<SettingFinanceOperation, GetByIdSettingFinanceOperationQueryResponse>().ReverseMap();
    }
}
