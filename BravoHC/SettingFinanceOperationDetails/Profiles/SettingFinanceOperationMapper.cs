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

        CreateMap<SettingFinanceOperation, GetAllSettingFinanceOperationQueryResponse>()
               .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
               .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                .ReverseMap();
        CreateMap<SettingFinanceOperation, GetByIdSettingFinanceOperationQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
               .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                .ReverseMap();
    }
}
