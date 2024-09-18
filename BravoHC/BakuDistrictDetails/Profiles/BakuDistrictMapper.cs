using AutoMapper;
using BakuDistrictDetails.Queries.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BakuDistrictDetails.Profiles;

public class BakuDistrictMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public BakuDistrictMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<BakuDistrict, GetAllBakuDistrictQueryResponse>().ReverseMap();
        CreateMap<BakuDistrict, GetByIdBakuDistrictQueryResponse>().ReverseMap();
        CreateMap<Employee, GetBakuDistrictEmployeesQueryResponse>().ReverseMap();
    }
}
