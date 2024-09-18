using AutoMapper;
using BakuMetroDetails.Queries.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BakuMetroDetails.Profiles;

public class BakuMetroMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public BakuMetroMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<BakuMetro, GetAllBakuMetroQueryResponse>().ReverseMap();
        CreateMap<BakuMetro, GetByIdBakuMetroQueryResponse>().ReverseMap();
        CreateMap<Employee, GetBakuMetroEmployeesQueryResponse>().ReverseMap();
    }
}
