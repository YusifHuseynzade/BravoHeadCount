using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Profiles;

public class MoneyOrderMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public MoneyOrderMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<MoneyOrder, GetAllMoneyOrderQueryResponse>()
             .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
             .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
             .ReverseMap();

        CreateMap<MoneyOrder, GetByIdMoneyOrderQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            .ReverseMap();

        CreateMap<MoneyOrderHistory, GetMoneyOrderHistoryQueryResponse>().ReverseMap();
    }
}
