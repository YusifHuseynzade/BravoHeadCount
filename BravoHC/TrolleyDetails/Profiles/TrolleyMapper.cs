using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using TrolleyDetails.Queries.Response;

namespace TrolleyDetails.Profiles;

public class TrolleyMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public TrolleyMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Trolley, GetAllTrolleyQueryResponse>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
                .ForMember(dest => dest.TrolleyTypeName, opt => opt.MapFrom(src => src.TrolleyType.Name))
                .ReverseMap();

        CreateMap<Trolley, GetByIdTrolleyQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
            .ForMember(dest => dest.TrolleyTypeName, opt => opt.MapFrom(src => src.TrolleyType.Name))
            .ReverseMap();

        CreateMap<MoneyOrderHistory, GetTrolleyHistoryQueryResponse>().ReverseMap();
    }
}
