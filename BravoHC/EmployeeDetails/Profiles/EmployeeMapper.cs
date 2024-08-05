using AutoMapper;
using Domain.Entities;
using EmployeeDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace EmployeeDetails.Profiles;

public class EmployeeMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public EmployeeMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Employee, GetAllEmployeeQueryResponse>().ReverseMap();
        CreateMap<Employee, GetByIdEmployeeQueryResponse>().ReverseMap();
    }
}