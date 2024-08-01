using AutoMapper;
using DepartmentDetails.Queries.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;


namespace DepartmentDetails.Profiles;

public class DepartmentMapper:Profile
{
	private readonly IHttpContextAccessor _httpAccessor;
	public DepartmentMapper(IHttpContextAccessor httpAccessor)
	{
		_httpAccessor = httpAccessor;

		CreateMap<Department, GetAllDepartmentQueryResponse>().ReverseMap();
		CreateMap<Department, GetByIdDepartmentQueryResponse>().ReverseMap();
		CreateMap<Project, GetDepartmentProjectQueryResponse>().ReverseMap();
	}
}
