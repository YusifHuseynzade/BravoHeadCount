using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using ResidentalAreaDetails.Queries.Response;

namespace ResidentalAreaDetails.Profiles;

public class ResidentalAreaMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public ResidentalAreaMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<ResidentalArea, GetAllResidentalAreaQueryResponse>().ReverseMap();
        CreateMap<ResidentalArea, GetByIdResidentalAreaQueryResponse>().ReverseMap();
        CreateMap<Employee, GetResidentalAreaEmployeesQueryResponse>().ReverseMap();
    }
}
