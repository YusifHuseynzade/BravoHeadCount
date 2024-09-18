using AutoMapper;
using BakuTargetDetails.Queries.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BakuTargetDetails.Profiles;

public class BakuTargetMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public BakuTargetMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<BakuTarget, GetAllBakuTargetQueryResponse>().ReverseMap();
        CreateMap<BakuTarget, GetByIdBakuTargetQueryResponse>().ReverseMap();
        CreateMap<Employee, GetBakuTargetEmployeesQueryResponse>().ReverseMap();
    }
}
